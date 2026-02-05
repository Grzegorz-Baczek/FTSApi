using FTS.Application.DTO;
using FTS.Application.Handlers.Ingredients.Commands.CreateIngredient;
using FTS.Application.Handlers.Ingredients.Queries.GetIngredients;
using FTS.Core.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FTS.Api.Controllers;

[ApiController]
[Route("api")]
public class IngredientController(IMediator mediator) : ControllerBase
{
    [Authorize(Roles = Roles.User)]
    [HttpPost("ingredient")]
    public async Task<ActionResult> CreateIngredient(CreateIngredientCommand command,
        CancellationToken token)
    {
        await mediator.Send(command, token);
        return NoContent();
    }

    [Authorize(Roles = Roles.User)]
    [HttpGet("ingredients")]
    public async Task<IReadOnlyCollection<IngredientDto>> GetIngredients([FromQuery] GetIngredientsQuery query,
        CancellationToken ct)
    {
        var ingredients = await mediator.Send(query, ct);
        return ingredients;
    }
}
