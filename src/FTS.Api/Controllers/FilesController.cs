using FTS.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FTS.Api.Controllers;

[ApiController]
[Route("api/files")]
public class FilesController(IFileStorage fileStorage) : ControllerBase
{
    private const string ContainerName = "uploads";

    [HttpPost]
    [SwaggerOperation(Summary = "Upload a file")]
    [RequestSizeLimit(10 * 1024 * 1024)] // 10 MB
    public async Task<ActionResult<FileUploadResponse>> Upload(IFormFile file, CancellationToken ct)
    {
        if (file.Length == 0)
            return BadRequest("File is empty.");

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        await using var stream = file.OpenReadStream();
        var url = await fileStorage.UploadAsync(ContainerName, fileName, stream, file.ContentType, ct);

        return Ok(new FileUploadResponse(fileName, url, file.ContentType, file.Length));
    }

    [HttpGet("{fileName}")]
    [SwaggerOperation(Summary = "Download a file")]
    public async Task<ActionResult> Download(string fileName, CancellationToken ct)
    {
        var stream = await fileStorage.DownloadAsync(ContainerName, fileName, ct);

        if (stream is null)
            return NotFound();

        return File(stream, "application/octet-stream", fileName);
    }

    [HttpDelete("{fileName}")]
    [SwaggerOperation(Summary = "Delete a file")]
    public async Task<ActionResult> Delete(string fileName, CancellationToken ct)
    {
        await fileStorage.DeleteAsync(ContainerName, fileName, ct);
        return NoContent();
    }
}

public record FileUploadResponse(string FileName, string Url, string ContentType, long Size);
