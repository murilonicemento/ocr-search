using OCRSearch.Application.DTOs;

namespace OCRSearch.Application.Interfaces;

public interface IFileService
{
    public void Upload(UploadFileDto uploadFileDto);
}