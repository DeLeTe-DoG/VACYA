using System.Net.Http;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
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

        foreach (var user in users)
        {
            foreach (var site in user.Sites.ToList())
            {
                var data = new WebSiteDataDTO
                {
                    LastChecked = DateTime.UtcNow
                };

                try
                {
                    var response = await client.GetAsync(site.URL, stoppingToken);
                    int status = (int)response.StatusCode;
                    data.StatusCode = status;

                        // Простой способ обработки статусов
                        if (status >= 200 && status < 400)
                        {
                            site.IsAvailable = true;
                            continue;
                        }
                        else
                        {
                            site.IsAvailable = false;
                            data.ErrorMessage = response.ReasonPhrase;
                        }
                }
                catch (Exception ex)
                {
                    site.IsAvailable = false;
                    data.StatusCode = null;
                    data.ErrorMessage = ex.Message;
                }

                data.Id = $"{data.StatusCode}/{Guid.NewGuid()}";

                // Добавляем проверку в отдельный список и только потом присоединяем
                var newWebSiteData = site.WebSiteData.ToList();
                newWebSiteData.Add(data);
                site.WebSiteData = newWebSiteData;

                // Считаем количество ошибок (404 и 500)
                site.TotalErrors = site.WebSiteData.Count(d => d.StatusCode == 404 || d.StatusCode == 500);

                await Task.Delay(500, stoppingToken); // пауза между сайтами
            }
        }

        await Task.Delay(5000, stoppingToken); // пауза между циклами проверки
    }
}


}
