using FTS.Application.Handlers.Cookbooks.Commands.CreateCookbook;
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
}
