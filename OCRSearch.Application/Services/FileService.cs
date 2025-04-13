using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using OCRSearch.Application.DTOs;
using OCRSearch.Application.Exceptions;
using OCRSearch.Application.Interfaces;

namespace OCRSearch.Application.Services;

public class FileService : IFileService
{
    private readonly IConfiguration _configuration;

    public FileService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Upload(UploadFileDto uploadFileDto)
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
    }
}