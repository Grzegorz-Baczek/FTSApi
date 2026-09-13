using FTS.Application.Handlers.Cookbooks.Commands;
using FTS.Application.Handlers.Cookbooks.Models;
using FTS.Application.Handlers.Cookbooks.Queries;
using FTS.Core.Security;
using FTS.Infrastructure.Exceptions;
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
    public async Task<ActionResult> CreateCookBook(CreateCookbook.Command command,
        CancellationToken token)
    {
        await mediator.Send(command, token);
        return NoContent();
    }

    [HttpGet("cookbooks")]
    public async Task<IReadOnlyCollection<CookbookDto>> GetCookbooks([FromQuery] GetCookbooks.Query query,
        CancellationToken ct)
    {
        var cookbooks = await mediator.Send(query, ct);
        return cookbooks;
    }

    [HttpGet("cookbook/{id:guid}")]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<CookbookDto> GetCookbook(Guid id, CancellationToken ct)
    {
        var cookbook = await mediator.Send(new GetCookbook.Query(id), ct);
        return cookbook;
    }
}
