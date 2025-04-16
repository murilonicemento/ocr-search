using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Options;
using OCRSearch.Application.DTOs;
using OCRSearch.Application.Interfaces;
using OCRSearch.Infrastructure.Configurations;
using OCRSearch.Infrastructure.Models;

namespace OCRSearch.Infrastructure.Providers;

public class ElasticSearchProvider : ISearchProvider
{
    private readonly ElasticsearchClient _client;

    public ElasticSearchProvider(IOptions<ElasticSearchConfiguration> options)
    {
        var configuration = options.Value;
        var settings =
            new ElasticsearchClientSettings(new Uri(configuration.Url)).DefaultIndex(configuration.DefaultIndex);
        _client = new ElasticsearchClient(settings);
    }

    public async Task<bool> IndexDocumentAsync(FileDto fileDto, string indexName)
    {
        var indexExist = (await _client.Indices.ExistsAsync(indexName)).Exists;

        if (!indexExist)
        {
            await _client.Indices.CreateAsync(indexName);
        }

        var file = new FileModel
        {
            Id = fileDto.Id,
            Name = fileDto.Name,
            ExtractedText = fileDto.ExtractedText,
            Url = fileDto.Url,
            UploadedAt = fileDto.UploadedAt
        };

        var response = await _client.IndexAsync(file,
            i => i.Index(indexName).OpType(OpType.Index));

        return response.IsValidResponse;
    }

    public Task SearchDocumentAsync()
    {
        throw new NotImplementedException();
    }
}