using FTS.Application.Handlers.Ingredient.Commands.CreateIngredient;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FTS.Api.Controllers;

public class IngredientController(IMediator mediator) : ControllerBase
{
    [HttpPost("ingredient")]
    public async Task<ActionResult> CreateIngredient(CreateIngredientCommand command,
        CancellationToken token)
    {
        await mediator.Send(command, token);
        return NoContent();
    }
}
