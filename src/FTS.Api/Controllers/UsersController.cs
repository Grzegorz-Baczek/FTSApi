using FTS.Application.Handlers.Users.Commands.SignUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FTS.Api.Controllers;

[ApiController]
[Route("api")]

public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPost("user/sign-up")]
    [SwaggerOperation("Create the user account")]
    public async Task<ActionResult> Post(SignUpCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}
