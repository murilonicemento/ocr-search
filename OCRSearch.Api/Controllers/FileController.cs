using Microsoft.AspNetCore.Mvc;
using OCRSearch.Application.DTOs;
using OCRSearch.Application.Interfaces;

namespace OCRSearch.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost("/upload-file")]
    public ActionResult UploadFile(IFormFile file)
    {
        if (file.Length == 0) return BadRequest("Invalid file.");
        
        var fileName = Path.GetFileName(file.FileName);
        var fileInfo = new FileInfo(fileName);

        if (fileInfo.Extension != ".pdf" && fileInfo.Extension != ".png" && fileInfo.Extension != ".jpeg" &&
            fileInfo.Extension != ".jpg")
        {
            return BadRequest("File must be pdf, png or jpg/jpeg.");
        }

        var uploadFileDto = new UploadFileDto
        {
            Name = fileInfo.Name,
            ContentType = file.ContentType,
            Content = file.OpenReadStream()
        };

        try
        {
            _fileService.Upload(uploadFileDto);

            return Ok("File uploaded with success.");
        }
        catch (Exception exception)
        {
            return Problem(exception.Message, statusCode: 500);
        }
    }
}