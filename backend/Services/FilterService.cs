using System;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using backend.Models;

namespace backend.Services;

public class FilterService
{
  private List<WebSiteDTO> _sites;
  private WebSiteDataDTO _site;
  public List<WebSiteDTO> DateFilter(UserDTO user, DateTime dateFrom, DateTime dateTo)
  {
      var filteredSites = user.Sites
          .Select(site => new WebSiteDTO
          {
              Id = site.Id,
              URL = site.URL,
              WebSiteData = site.WebSiteData
                  .Where(d =>
                      d.LastChecked.Date >= dateFrom &&
                      d.LastChecked.Date <= dateTo
                  )
                  .ToList()
          })
          .Where(site => site.WebSiteData.Any())
          .ToList();
  
      return filteredSites;
  }

}