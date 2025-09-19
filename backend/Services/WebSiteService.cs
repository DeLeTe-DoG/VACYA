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

    public (bool Success, string Message, WebSiteDTO?) Add(string url, string userName, string name, string expectedContent)
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
            ExpectedContent = expectedContent,
            TotalErrors = 0,
            WebSiteData = new()
        };

        user.Sites.Add(site);
        return (true, "Сайт успешно добавлен", site);
    }
}
