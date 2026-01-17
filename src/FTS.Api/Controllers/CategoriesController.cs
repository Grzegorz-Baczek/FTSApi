using MediatR;
using Microsoft.AspNetCore.Mvc;
using FTS.Application.Commands.Categories.Handlers;

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
}