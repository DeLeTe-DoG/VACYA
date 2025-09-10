namespace backend.Models;

public class WebSiteResponseDTO
{
    public int Id { get; set; }
    public string URL { get; set; } = string.Empty;

    public bool IsAvailable { get; set; }
    public int? StatusCode { get; set; }
    public long? ResponseTimeMs { get; set; }
    public long? ContentLength { get; set; }
    public string? ErrorMessage { get; set; }

    public DateTime LastChecked { get; set; }
} 