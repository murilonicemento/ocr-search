namespace OCRSearch.Infrastructure.Models;

public class FileModel
{
    public string Id { get; set; }

    public string Name { get; set; }
    
    public string Extension  { get; set; }

    public string Url { get; set; }

    public string ExtractedText { get; set; }

    public DateTime UploadedAt { get; set; }
}