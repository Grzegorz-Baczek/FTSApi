using MediatR;
using Microsoft.AspNetCore.Mvc;
using FTS.Application.Commands.Categories.Handlers;
using FTS.Core.Entities;
using FTS.Application.Queries.Categories.Handlers;

namespace FTS.Api.Controllers;

[Route("api")]
[ApiController]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    [HttpPost("category")]
    public async Task<ActionResult> CreateCategory(CreateCategoryCommand command,
        CancellationToken token)
    {
        await mediator.Send(command, token);
        return NoContent();
    }

    [HttpGet("categories")]
    public async Task<IEnumerable<Category>> GetCategories([FromQuery] GetCategoriesQuery query,

        CancellationToken ct)
    {
        var categories = await mediator.Send(query, ct);
        return categories;
    }

    [HttpGet("category/{id}")]
    public async Task<Category> GetCategory([FromQuery] Guid id, CancellationToken ct)
    {
        var category = await mediator.Send(new GetCategoryQuery(id), ct);
        return category;
    }
}