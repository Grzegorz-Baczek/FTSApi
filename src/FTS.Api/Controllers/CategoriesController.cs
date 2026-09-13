using FTS.Application.Handlers.Categories.Commands;
using FTS.Application.Handlers.Categories.Models;
using FTS.Application.Handlers.Categories.Queries;
using FTS.Core.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FTS.Api.Controllers;

[ApiController]
[Route("api")]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    [Authorize(Roles = Roles.Admin)]
    [HttpPost("category")]
    public async Task<ActionResult> CreateCategory(CreateCategory.Command command,
        CancellationToken ct)
    {
        await mediator.Send(command, ct);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpGet("categories")]
    public async Task<IReadOnlyCollection<CategoryDto>> GetCategories([FromQuery] GetCategories.Query query,
        CancellationToken ct)
    {
        var categories = await mediator.Send(query, ct);
        return categories;
    }
}