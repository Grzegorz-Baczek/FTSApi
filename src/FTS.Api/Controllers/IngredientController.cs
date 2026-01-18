using FTS.Application.Handlers.Ingredients.Commands.CreateIngredient;
using FTS.Application.Handlers.Ingredients.Queries.GetIngredients;
using FTS.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FTS.Api.Controllers;

[ApiController]
[Route("api")]
public class IngredientController(IMediator mediator) : ControllerBase
{
    [HttpPost("ingredient")]
    public async Task<ActionResult> CreateIngredient(CreateIngredientCommand command,
        CancellationToken token)
    {
        await mediator.Send(command, token);
        return NoContent();
    }

    [HttpGet("ingredients")]
    public async Task<IEnumerable<Ingredient>> GetIngredients([FromQuery] GetIngredientsQuery query,
        CancellationToken ct)
    {
        var ingredients = await mediator.Send(query, ct);
        return ingredients;
    }
}
