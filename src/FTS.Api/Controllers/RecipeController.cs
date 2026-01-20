using FTS.Application.DTO;
using FTS.Application.Handlers.Recipes.Commands.CreateRecipe;
using FTS.Application.Handlers.Recipes.Queries.GetRecipes;
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

    [HttpGet("recipes")]
    public async Task<IEnumerable<RecipeDto>> GetRecipes([FromQuery] GetRecipesQuery query,
      CancellationToken ct)
    {
        var recipes = await mediator.Send(query, ct);
         return recipes;
    }
}