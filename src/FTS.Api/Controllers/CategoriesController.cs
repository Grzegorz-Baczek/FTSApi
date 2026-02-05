using FTS.Application.Commands.Categories.Handlers;
using FTS.Application.DTO;
using FTS.Application.Handlers.Categories.Queries.GetCategories;
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
    public async Task<ActionResult> CreateCategory(CreateCategoryCommand command,
        CancellationToken token)
    {
        await mediator.Send(command, token);
        return NoContent();
    }

    [HttpGet("categories")]
    public async Task<IReadOnlyCollection<CategoryDto>> GetCategories([FromQuery] GetCategoriesQuery query,
    CancellationToken ct)
    {
        var categories = await mediator.Send(query, ct);
        return categories;
    }
}