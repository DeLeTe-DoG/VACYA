using System.Net.Http;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using backend.Models;

public class MonitoringBackgroundService : BackgroundService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly List<WebSiteDTO> _websites;

    public MonitoringBackgroundService(IHttpClientFactory httpClientFactory, List<WebSiteDTO> websites)
    {
        _httpClientFactory = httpClientFactory;
        _websites = websites;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var client = _httpClientFactory.CreateClient();

        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (var site in _websites)
            {
                site.LastChecked = DateTime.UtcNow;

                try
                {
                    var response = await client.GetAsync(site.URL, stoppingToken);
                    site.StatusCode = (int)response.StatusCode;
                    site.IsAvailable = response.IsSuccessStatusCode;
                    site.ErrorMessage = site.IsAvailable ? null : response.ReasonPhrase;
                }
                catch (Exception ex)
                {
                    site.IsAvailable = false;
                    site.StatusCode = null;
                    site.ErrorMessage = ex.Message;
                }

                await Task.Delay(500, stoppingToken); // небольшая пауза между сайтами
            }

            await Task.Delay(5000, stoppingToken); // цикл каждые 5 секунд
        }
    }
}
