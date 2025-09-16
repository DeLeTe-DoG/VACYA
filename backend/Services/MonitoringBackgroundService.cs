using System.Net.Http;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using backend.Models;
using backend.Services;

public class MonitoringBackgroundService : BackgroundService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly UserService _userService;
    public MonitoringBackgroundService(IHttpClientFactory httpClientFactory, UserService userService)
    {
        _httpClientFactory = httpClientFactory;
        _userService = userService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var client = _httpClientFactory.CreateClient();

        while (!stoppingToken.IsCancellationRequested)
        {
            var users = _userService.GetAll();
            foreach (var user  in users)
            {
                foreach (var site in user.Sites)
                {
                    var data = new WebSiteDataDTO { };
                    data.LastChecked = DateTime.UtcNow;

                    try
                    {
                        var response = await client.GetAsync(site.URL, stoppingToken);
                        data.StatusCode = (int)response.StatusCode;
                        data.IsAvailable = response.IsSuccessStatusCode;
                        data.ErrorMessage = data.IsAvailable ? null : response.ReasonPhrase;
                    }
                    catch (Exception ex)
                    {
                        data.IsAvailable = false;
                        data.StatusCode = null;
                        data.ErrorMessage = ex.Message;
                    }
                    site.WebSiteData.Add(data);

                    await Task.Delay(500, stoppingToken); // небольшая пауза между сайтами
                }   
            }
            await Task.Delay(5000, stoppingToken); // цикл каждые 5 секунд
        }
    }
}
