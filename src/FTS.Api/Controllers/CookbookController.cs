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
[Authorize(Roles = Roles.User)]
public class CookbookController(IMediator mediator) : ControllerBase
{
    [HttpPost("cookbook")]
    public async Task<ActionResult> CreateCookBook(CreateCookbookCommand command,
        CancellationToken token)
    {
        await mediator.Send(command, token);
        return NoContent();
    }

    [HttpGet("cookbooks")]
    public async Task<IReadOnlyCollection<CookbookDto>> GetCookbooks([FromQuery] GetCookbooksQuery query,
        CancellationToken ct)
    {
        var cookbooks = await mediator.Send(query, ct);
        return cookbooks;
    }

    [HttpGet("cookbook/{id:guid}")]
    public async Task<CookbookDto> GetCookbook(Guid id, CancellationToken ct)
    {
        var cookbook = await mediator.Send(new GetCookbookByIdQuery(id), ct);
        return cookbook;
    }
}
