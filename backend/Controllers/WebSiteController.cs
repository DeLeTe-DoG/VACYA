using backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/websites")]
public class WebsiteController : ControllerBase
{
    private readonly List<WebSiteDTO> _websites;

    public WebsiteController(List<WebSiteDTO> websites)
    {
        _websites = websites;
    }

    [HttpGet]
    public ActionResult<List<WebSiteDTO>> GetAll() => _websites;

    [HttpPost]
    public ActionResult<WebSiteDTO> Add([FromBody] WebSiteDTO site)
    {
        site.Id = _websites.Count + 1;
        _websites.Add(site);
        return site;
    }
}
