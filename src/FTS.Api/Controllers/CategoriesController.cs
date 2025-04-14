using MediatR;
using Microsoft.AspNetCore.Mvc;
using FTS.Application.Commands.Categories.Handlers;

namespace FTS.Api.Controllers;

[Route("Category")]
[ApiController]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateCategory(CreateCategoryCommand command,
        CancellationToken token)
    {
        await mediator.Send(command, token);
        return NoContent();
    }
}