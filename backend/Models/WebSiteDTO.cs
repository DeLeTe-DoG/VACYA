namespace backend.Models;

public class WebSiteDTO
{
    public int Id { get; set; }
    public string URL { get; set; } = string.Empty;
    public string Name { get; set; }
    public List<WebSiteDataDTO> WebSiteData { get; set; } = [];
}