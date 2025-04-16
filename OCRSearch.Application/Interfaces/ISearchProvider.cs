namespace OCRSearch.Application.Interfaces;

public interface ISearchProvider
{
    public Task IndexDocumentAsync();
    public Task SearchDocumentAsync();
}