using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Options;
using OCRSearch.Application.DTOs;
using OCRSearch.Application.Interfaces;
using OCRSearch.Infrastructure.Configurations;
using OCRSearch.Domain.Entities;
using File = OCRSearch.Domain.Entities.File;

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

        var file = new File
        {
            Id = fileDto.Id,
            Name = fileDto.Name,
            Extension = fileDto.Extension,
            ExtractedText = fileDto.ExtractedText,
            Url = fileDto.Url,
            UploadedAt = fileDto.UploadedAt
        };

        var response = await _client.IndexAsync(file,
            i => i.Index(indexName).OpType(OpType.Index));

        return response.IsValidResponse;
    }

    public async Task<List<FileDto>> SearchDocumentAsync(string content, int? size, string indexName)
    {
        var files = await _client.SearchAsync<FileDto>(s => s
            .Index(indexName)
            .From(0)
            .Size(size)
            .Query(q => q
                .MultiMatch(m => m
                    .Fields("name")
                    .Fields("extractedText")
                    .Query(content)
                )
            )
        );

        return files.Documents.ToList();
    }
}