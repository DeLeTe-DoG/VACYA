namespace backend.Models;

public class WebSiteDTO
{
    public int Id { get; set; }
    public string URL { get; set; } = string.Empty;
    public string Name { get; set; }
    public bool IsAvailable { get; set; }
    public int TotalErrors { get; set; } = 0;
    public List<WebSiteDataDTO> WebSiteData { get; set; } = [];
}