using FTS.Application.DTO;
using FTS.Application.Handlers.Cookbooks.Commands.CreateCookbook;
using FTS.Application.Handlers.Cookbooks.Queries.GetCookbookById;
using FTS.Application.Handlers.Cookbooks.Queries.GetCookbooks;
using FTS.Core.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FTS.Api.Controllers;

[ApiController]
[Route("api")]
public class CookbookController(IMediator mediator) : ControllerBase
{
    [Authorize(Roles = Roles.User)]
    [HttpPost("cookbook")]
    public async Task<ActionResult> CreateCookBook(CreateCookbookCommand command,
        CancellationToken token)
    {
        await mediator.Send(command, token);
        return NoContent();
    }

    [Authorize(Roles = Roles.User)]
    [HttpGet("cookbooks")]
    public async Task<IReadOnlyCollection<CookbookDto>> GetCookbooks([FromQuery] GetCookbooksQuery query,
        CancellationToken ct)
    {
        var cookbooks = await mediator.Send(query, ct);
        return cookbooks;
    }

    [Authorize(Roles = Roles.User)]
    [HttpGet("cookbook/{id:guid}")]
    public async Task<CookbookDto> GetCookbook(Guid id, CancellationToken ct)
    {
        var cookbook = await mediator.Send(new GetCookbookByIdQuery(id), ct);
        return cookbook;
    }
}
