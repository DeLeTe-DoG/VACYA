using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using backend.Models;
using backend.Services;
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
    

    public WebSiteDTO? Add(string url, string userName, string name)
    {
        var user = _userService.GetByName(userName);
        if (user == null) return null;
        var site = new WebSiteDTO
        {
            Id = user.Sites.Count + 1,
            Name = name,
            URL = url,
            WebSiteData = new()
        };

        user.Sites.Add(site);
        return site;
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
    }

    public async Task CheckAllAsync()
    {
        foreach (var site in _websites)
        {
            await CheckWebsiteAsync(site.URL);
        };
    }
}
