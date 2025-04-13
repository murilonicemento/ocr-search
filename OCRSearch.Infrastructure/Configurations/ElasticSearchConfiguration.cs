namespace OCRSearch.Infrastructure.Configurations;

public class ElasticSearchConfiguration
{
    public string Url { get; set; }
    public string DefaultIndex { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}