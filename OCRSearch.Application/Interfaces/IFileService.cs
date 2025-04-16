using OCRSearch.Application.DTOs;

namespace OCRSearch.Application.Interfaces;

public interface IFileService
{
    public Task Upload(UploadFileDto uploadFileDto);
}