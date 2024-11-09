namespace NexusProject.API.Controllers;
using global::NexusProject.API.Helpers;
using global::NexusProject.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private readonly IFileStorage _fileStorage;

    public FileController(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }

    [HttpPost("upload")]
    public async Task<ActionResult<string>> UploadFile([FromForm] FileUploadDTO fileDTO)
    {
        try
        {
            if (fileDTO.File == null || fileDTO.File.Length == 0)
                return BadRequest("No file was uploaded.");

            using var memoryStream = new MemoryStream();
            await fileDTO.File.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            string extension = Path.GetExtension(fileDTO.File.FileName);
            string fileUrl = await _fileStorage.SaveFileAsync(fileBytes, extension, "activities");

            return Ok(new { url = fileUrl });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}

public class FileUploadDTO
{
    [Required]
    public IFormFile File { get; set; }
}