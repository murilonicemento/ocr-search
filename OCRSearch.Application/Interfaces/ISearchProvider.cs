using OCRSearch.Application.DTOs;

namespace OCRSearch.Application.Interfaces;

public interface ISearchProvider
{
    public Task<bool> IndexDocumentAsync(FileDto fileDto, string indexName);
    public Task SearchDocumentAsync();
}