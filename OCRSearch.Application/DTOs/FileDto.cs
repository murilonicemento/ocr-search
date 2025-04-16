namespace OCRSearch.Application.DTOs;

public class FileDto
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

    public string ExtractedText { get; set; }

    public DateTime UploadedAt { get; set; }
}