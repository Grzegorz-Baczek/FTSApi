using FTS.Application.Handlers.CookbookRecipes.Models;
using FTS.Application.Handlers.CookbookRecipes.Queries;
using FTS.Core.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FTS.Api.Controllers;

[ApiController]
[Route("api")]
[Authorize(Roles = Roles.User)]
public class CookbookRecipeController(IMediator mediator) : ControllerBase
{
    [HttpGet("cookbook/{cookbookId:guid}/recipes")]
    public async Task<IReadOnlyCollection<CookbookRecipeDto>> GetCookbookRecipes(Guid cookbookId,
        CancellationToken ct)
    {
        var cookbookRecipes = await mediator.Send(new GetCookbookRecipes.Query(cookbookId), ct);
        return cookbookRecipes;
    }
}
