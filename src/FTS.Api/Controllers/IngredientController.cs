using FTS.Application.Handlers.Ingredients.Commands;
using FTS.Application.Handlers.Ingredients.Models;
using FTS.Application.Handlers.Ingredients.Queries;
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
    public async Task<ActionResult> CreateIngredient(CreateIngredient.Command command,
        CancellationToken ct)
    {
        await mediator.Send(command, ct);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpGet("ingredients")]
    public async Task<IReadOnlyCollection<IngredientDto>> GetIngredients([FromQuery] GetIngredients.Query query,
        CancellationToken ct)
    {
        var ingredients = await mediator.Send(query, ct);
        return ingredients;
    }
}
