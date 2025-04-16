using OCRSearch.Application.Interfaces;

namespace OCRSearch.Infrastructure.Providers;

public class ElasticSearchProvider : ISearchProvider
{
    public Task IndexDocumentAsync()
    {
        throw new NotImplementedException();
    }

    public Task SearchDocumentAsync()
    {
        throw new NotImplementedException();
    }
}