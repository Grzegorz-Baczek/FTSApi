using FTS.Application.DTO;
using FTS.Application.Handlers.CookbookRecipes.Queries.GetCookbookRecipes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FTS.Api.Controllers;

[ApiController]
[Route("api")]
public class CookbookRecipeController(IMediator mediator) : ControllerBase
{
    [HttpGet("cookbook/{cookbookId:guid}/recipes")]
    public async Task<IReadOnlyCollection<CookbookRecipeDto>> GetCookbookRecipes(Guid cookbookId,
        CancellationToken ct)
    {
        var cookbookRecipes = await mediator.Send(new GetCookbookRecipesQuery(cookbookId), ct);
        return cookbookRecipes;
    }
}
