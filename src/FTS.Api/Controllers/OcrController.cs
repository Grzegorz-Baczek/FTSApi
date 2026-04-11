using FTS.Application.Abstractions;
using FTS.Application.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FTS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OcrController : ControllerBase
{
    private readonly IOcrService _ocrService;

    public OcrController(IOcrService ocrService)
    {
        _ocrService = ocrService;
    }

    [HttpPost("extract-text")]
    [SwaggerOperation(Summary = "Extract text from a file using OCR")]
    [ProducesResponseType(typeof(OcrResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [RequestTimeout(milliseconds: 300_000)]
    public async Task<ActionResult<OcrResultDto>> ExtractText(
        IFormFile file, CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0)
        {
            return BadRequest("File is required.");
        }

        await using var stream = file.OpenReadStream();
        var text = await _ocrService.ExtractTextAsync(stream, file.FileName, cancellationToken);

        var pageCount = text.Split('\f', StringSplitOptions.RemoveEmptyEntries).Length;
        if (pageCount == 0) pageCount = 1;

        return Ok(new OcrResultDto(text, pageCount));
    }
}
