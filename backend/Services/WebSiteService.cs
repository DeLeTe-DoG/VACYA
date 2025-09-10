using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using backend.Models;

public class WebsiteService
{
    private readonly List<WebSiteDTO> _websites;
    private readonly IHttpClientFactory _httpClientFactory;

    public WebsiteService(List<WebSiteDTO> websites, IHttpClientFactory httpClientFactory)
    {
        _websites = websites;
        _httpClientFactory = httpClientFactory;
    }

    public List<WebSiteDTO> GetAll() => _websites;

    public WebSiteDTO Add(string url)
    {
        var site = new WebSiteDTO
        {
            Id = _websites.Count + 1,
            URL = url
        };
        _websites.Add(site);
        return site;
    }

    public async Task CheckWebsiteAsync(WebSiteDTO site)
    {
        var client = _httpClientFactory.CreateClient();
        site.LastChecked = DateTime.UtcNow;

        try
        {
            var response = await client.GetAsync(site.URL);
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
    }

    public async Task CheckAllAsync()
    {
        foreach (var site in _websites)
        {
            await CheckWebsiteAsync(site);
        }
    }
}
