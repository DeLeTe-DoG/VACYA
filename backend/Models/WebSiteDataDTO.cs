using System;

namespace backend.Models;

public class WebSiteDataDTO
{
    public bool IsAvailable { get; set; }
    public int? StatusCode { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime LastChecked { get; set; }
}