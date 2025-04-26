using Microsoft.AspNetCore.Mvc;
using OCRSearch.Application.DTOs;
using OCRSearch.Application.Exceptions;
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

    [HttpPost("Upload-File")]
    public async Task<ActionResult<string>> UploadFile(IFormFile file)
    {
        if (file.Length == 0) return BadRequest("Invalid file.");

        var fileName = Path.GetFileName(file.FileName);
        var fileInfo = new FileInfo(fileName);

        if (!ValidateFileExtension(fileInfo.Extension)) return BadRequest("File must be pdf, png or jpg/jpeg.");
        if (file.Length > 1048576) return BadRequest("File can't be more larger than 100MB.");

        var uploadFileDto = new UploadFileDto
        {
            Name = fileInfo.Name,
            Extension = fileInfo.Extension,
            Content = file.OpenReadStream()
        };

        try
        {
            await _fileService.UploadAsync(uploadFileDto);

            return Ok("File uploaded with success.");
        }
        catch (InvalidOperationException exception)
        {
            return Problem(exception.Message, statusCode: 500);
        }
        catch (FileUploadException exception)
        {
            return Problem(exception.Message, statusCode: 500);
        }
    }

    [HttpGet("Search-File")]
    public async Task<ActionResult<List<FileDto>>> SearchFile([FromQuery] string content, int? size)
    {
        if (string.IsNullOrEmpty(content)) return BadRequest("Content can't be blank.");
        if (size == 0) return BadRequest("Number of documents must be greater than 0.");

        try
        {
            var files = await _fileService.SearchAsync(content, size);

            return Ok(files);
        }
        catch (Exception exception)
        {
            return Problem(exception.Message, statusCode: 500);
        }
    }

    private static bool ValidateFileExtension(string extension)
    {
        return extension is ".pdf" or ".png" or ".jpeg" or ".jpg";
    }
}