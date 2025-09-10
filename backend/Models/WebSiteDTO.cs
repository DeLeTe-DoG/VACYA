namespace backend.Models;

public class WebSiteDTO
{
    public int Id { get; set; }
    public string URL { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public int? StatusCode { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime LastChecked { get; set; }
}