using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using OCRSearch.Application.DTOs;
using OCRSearch.Application.Exceptions;
using OCRSearch.Application.Interfaces;
using Tesseract;

namespace OCRSearch.Application.Services;

public class FileService : IFileService
{
    private const string IndexName = "files";

    private readonly IConfiguration _configuration;
    private readonly ISearchProvider _searchProvider;

    public FileService(IConfiguration configuration, ISearchProvider searchProvider)
    {
        _configuration = configuration;
        _searchProvider = searchProvider;
    }

    public async Task UploadAsync(UploadFileDto uploadFileDto)
    {
        var url = _configuration.GetSection("CloudinaryConfiguration")["Url"];
        var cloudinary = new Cloudinary(url)
        {
            Api =
            {
                Secure = true
            }
        };
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(uploadFileDto.Name, uploadFileDto.Content),
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = true
        };

        var result = cloudinary.Upload(uploadParams);

        if (result.Error is not null)
        {
            throw new FileUploadException($"Error uploading file: {result.Error.Message}");
        }

        var imageUrl = result.SecureUrl.ToString();

        if (imageUrl is null)
        {
            throw new InvalidOperationException("Can't load file to do OCR.");
        }

        var content = await ExtractContentAsync(imageUrl);

        var fileDto = new FileDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = uploadFileDto.Name,
            Extension = uploadFileDto.Extension,
            Url = imageUrl,
            ExtractedText = content,
            UploadedAt = DateTime.UtcNow
        };

        var searchResult = await _searchProvider.IndexDocumentAsync(fileDto, IndexName);

        if (!searchResult)
        {
            throw new InvalidOperationException("Can't index document.");
        }
    }

    public async Task<List<FileDto>> SearchAsync(string content, int? size)
    {
        return await _searchProvider.SearchDocumentAsync(content, size, IndexName);
    }

    private static async Task<string> ExtractContentAsync(string imageUrl)
    {
        using var httpClient = new HttpClient();
        var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

        using var engine = new TesseractEngine(@"../OCRSearch.Application/tessdata", "eng", EngineMode.Default);

        var file = Pix.LoadFromMemory(imageBytes.ToArray());

        if (file is null)
        {
            throw new InvalidOperationException("Error to load file.");
        }

        var page = engine.Process(file);
        var content = page.GetText();

        if (content.Contains("error", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(content);
        }

        return content;
    }
}