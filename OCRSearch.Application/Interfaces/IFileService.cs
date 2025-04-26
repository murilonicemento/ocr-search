using OCRSearch.Application.DTOs;

namespace OCRSearch.Application.Interfaces;

public interface IFileService
{
    public Task UploadAsync(UploadFileDto uploadFileDto);
    public Task<List<FileDto>> SearchAsync(string content, int? size);
}