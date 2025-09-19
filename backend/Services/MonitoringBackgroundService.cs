using System.Net.Http;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
using backend.Models;
using backend.Services;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class MonitoringBackgroundService : BackgroundService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly UserService _userService;

    public MonitoringBackgroundService(IHttpClientFactory httpClientFactory, UserService userService)
    {
        _httpClientFactory = httpClientFactory;
        _userService = userService;
    }

    private bool checkDNS(string host)
    {
        try
        {
            var entry = Dns.GetHostEntry(host);
            return entry.AddressList.Length > 0;
        }
        catch
        {
            return false;
        }
    }

    private bool CheckSslCertificate(string host)
    {
        try
        {
            using var client = new TcpClient();
            client.Connect(host, 443);

            using var sslStream = new SslStream(client.GetStream(), false,
                new RemoteCertificateValidationCallback((sender, cert, chain, errors) => true));
            sslStream.AuthenticateAsClient(host);

            var cert = new X509Certificate2(sslStream.RemoteCertificate);
            return DateTime.UtcNow >= cert.NotBefore && DateTime.UtcNow <= cert.NotAfter;
        }
        catch
        {
            return false;
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var client = _httpClientFactory.CreateClient();

        while (!stoppingToken.IsCancellationRequested)
        {
            var users = _userService.GetAll();

            foreach (var user in users)
            {
                foreach (var site in user.Sites.ToList())
                {
                    var data = new WebSiteDataDTO
                    {
                        LastChecked = DateTime.UtcNow
                    };

                    //  1. Проверка URL перед всем остальным
                    if (!Uri.TryCreate(site.URL, UriKind.Absolute, out var uri))
                    {
                        data.StatusCode = 0;
                        data.ErrorMessage = "Некорректный URL";
                        data.Id = $"INVALID_URL/";
                        site.IsAvailable = false;
                        site.TotalErrors++;

                        site.WebSiteData = site.WebSiteData.Append(data).ToList();
                        continue;
                    }

                    try
                    {
                        //  2. Проверка DNS
                        bool dnsOK = checkDNS(uri.Host);
                        if (!dnsOK)
                        {
                            data.StatusCode = 0;
                            data.ErrorMessage = "DNS не найден";
                            data.Id = $"DNS_ERROR";
                            site.DNS = "DNS не найден";
                            site.IsAvailable = false;
                            site.TotalErrors++;

                            site.WebSiteData = site.WebSiteData.Append(data).ToList();
                            continue;
                        }
                        site.DNS = "OK";

                        //  3. Проверка SSL (если HTTPS)
                        bool sslOk = uri.Scheme == Uri.UriSchemeHttps ? CheckSslCertificate(uri.Host) : true;
                        if (!sslOk)
                        {
                            data.StatusCode = 0;
                            data.ErrorMessage = "SSL сертификат недействителен";
                            data.Id = $"SSL_ERROR/{Guid.NewGuid()}";
                            site.SSL = "Сертификат недействителен";
                            site.IsAvailable = false;
                            site.TotalErrors++;

                            site.WebSiteData = site.WebSiteData.Append(data).ToList();
                            continue;
                        }
                        site.SSL = "OK";

                        //  4. HTTP-запрос
                        var response = await client.GetAsync(uri, stoppingToken);
                        int status = (int)response.StatusCode;

                        data.StatusCode = status;
                        data.Id = $"{status}/{Guid.NewGuid()}";

                        if (status >= 200 && status < 400)
                        {
                            site.IsAvailable = true;
                            continue;
                        }
                        else
                        {
                            site.IsAvailable = false;
                            data.ErrorMessage = response.ReasonPhrase;
                            site.TotalErrors++;
                        }
                    }
                    catch (Exception ex)
                    {
                        data.StatusCode = 0;
                        data.ErrorMessage = ex.Message;
                        data.Id = $"EXCEPTION/{Guid.NewGuid()}";
                        site.IsAvailable = false;
                        site.TotalErrors++;
                    }

                    site.WebSiteData = site.WebSiteData.Append(data).ToList();
                    await Task.Delay(500, stoppingToken);
                }
            }

            await Task.Delay(5000, stoppingToken); 
        }
    }
}