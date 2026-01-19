using FTS.Application.Handlers.Recipes.Commands.CreateRecipe;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FTS.Api.Controllers;

[ApiController]
[Route("api")]
public class RecipeController(IMediator mediator) : ControllerBase
{
    [HttpPost("recipe")]
    public async Task<ActionResult> CreateRecipe(CreateRecipeCommand command,
        CancellationToken token)
    {
        await mediator.Send(command, token);
        return NoContent();
    }
}