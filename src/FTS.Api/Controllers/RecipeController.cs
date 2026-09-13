using FTS.Application.Handlers.Recipes.Commands;
using FTS.Application.Handlers.Recipes.Models;
using FTS.Application.Handlers.Recipes.Queries;
using FTS.Core.Security;
using FTS.Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FTS.Api.Controllers;

[ApiController]
[Route("api")]
public class RecipeController(IMediator mediator) : ControllerBase
{
    [Authorize(Roles = Roles.User)]
    [HttpPost("recipe")]
    public async Task<ActionResult> CreateRecipe(CreateRecipe.Command command,
        CancellationToken token)
    {
        await mediator.Send(command, token);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpGet("recipes")]
    public async Task<IReadOnlyCollection<RecipeDto>> GetRecipes([FromQuery] GetRecipes.Query query,
      CancellationToken ct)
    {
        var recipesDto = await mediator.Send(query, ct);
         return recipesDto;
    }

    [AllowAnonymous]
    [HttpGet("recipe/{id:guid}")]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<RecipeDto> GetRecipe(Guid id, CancellationToken ct)
    {
        var recipeDto = await mediator.Send(new GetRecipe.Query(id), ct);
        return recipeDto;
    }

    [Authorize(Roles = Roles.User)]
    [HttpDelete("recipe/{id:guid}")]
    public async Task<ActionResult> DeleteRecipe(Guid id, CancellationToken ct)
    {
        await mediator.Send(new DeleteRecipe.Command(id), ct);
        return NoContent();
    }
}