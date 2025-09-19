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
           .Select(site =>
           {
             var filteredData = site.WebSiteData
                 .Where(d =>
                     (d.StatusCode == 404 || d.StatusCode == 500) &&
                     d.LastChecked.Date >= dateFrom &&
                     d.LastChecked.Date <= dateTo
                 )
                 .ToList();

             return new WebSiteDTO
             {
               Id = site.Id,
               Name = site.Name,
               URL = site.URL,
               WebSiteData = filteredData,
               TotalErrors = filteredData.Count(d => d.StatusCode == 404 || d.StatusCode == 500)
             };
           }).Where(site => site.WebSiteData.Any())
            .Where(site => site.TotalErrors > 0)
            .ToList();  
      return filteredSites;
  }
}