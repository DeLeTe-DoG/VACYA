using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Routing;

public class WebsiteService
{
    private readonly UserService _userService;
    private readonly List<WebSiteDTO> _websites;
    private readonly IHttpClientFactory _httpClientFactory;

    public WebsiteService(UserService userService, IHttpClientFactory httpClientFactory)
    {
        _userService = userService;
        _websites = new List<WebSiteDTO>();
        _httpClientFactory = httpClientFactory;
    }

    public List<WebSiteDTO> GetAll()
    {
        return _websites;
    }

    public (bool Success, string Message, WebSiteDTO?) Add(string url, string userName, string name)
    {
        var user = _userService.GetByName(userName);

        if (user.Sites.Any(s => s.URL == url))
            return (false, $"Сайт с URL '{url}' уже существует", null);

        if (user.Sites.Any(s => s.Name == name))
            return (false, $"Сайт с именем '{name}' уже существует", null);
        if (user == null) 
            return (false, "Пользователь не найден", null);

            
        var site = new WebSiteDTO
        {
            Id = user.Sites.Count + 1,
            Name = name,
            URL = url,
            TotalErrors = 0,
            WebSiteData = new()
        };

        user.Sites.Add(site);
        return (true, "Сайт успешно добавлен", site);
    }
    public async Task CheckWebsiteAsync(string URL)
    {

        var site = _websites.FirstOrDefault(s => s.URL == URL);
        if (site == null) return;

        var client = _httpClientFactory.CreateClient();
        var data = new WebSiteDataDTO
        {
            LastChecked = DateTime.UtcNow
        };
        try
        {
            var response = await client.GetAsync(URL);
            data.StatusCode = (int)response.StatusCode;
            site.IsAvailable = response.IsSuccessStatusCode;
            data.ErrorMessage = site.IsAvailable ? null : response.ReasonPhrase;
        }
        catch (Exception ex)
        {
            site.IsAvailable = false;
            data.StatusCode = null;
            data.ErrorMessage = ex.Message;
        }
        data.Id = data.StatusCode.ToString() + Guid.NewGuid().ToString();
        site.WebSiteData.Add(data);
    }

    public async Task CheckAllAsync()
    {
        foreach (var site in _websites)
        {
            await CheckWebsiteAsync(site.URL);
        };
    }
}
