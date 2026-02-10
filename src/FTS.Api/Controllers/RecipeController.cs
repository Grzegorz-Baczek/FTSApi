using FTS.Application.DTO;
using FTS.Application.Handlers.Recipes.Commands.CreateRecipe;
using FTS.Application.Handlers.Recipes.Commands.DeleteRecipe;
using FTS.Application.Handlers.Recipes.Queries.GetRecipeById;
using FTS.Application.Handlers.Recipes.Queries.GetRecipes;
using FTS.Core.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IReadOnlyCollection<RecipeDto>> GetRecipes([FromQuery] GetRecipesQuery query,
      CancellationToken ct)
    {
        var recipesDto = await mediator.Send(query, ct);
         return recipesDto;
    }

    [HttpGet("recipe/{id:guid}")]
    public async Task<RecipeDto> GetRecipe(Guid id, CancellationToken ct)
    {
        var recipeDto = await mediator.Send(new GetRecipeQuery(id), ct);
        return recipeDto;
    }

    [Authorize(Roles = Roles.User)]
    [HttpDelete("recipe/{id:guid}")]
    public async Task<ActionResult> DeleteRecipe(Guid id, CancellationToken ct)
    {
        await mediator.Send(new DeleteRecipeCommand(id), ct);
        return NoContent();
    }
}