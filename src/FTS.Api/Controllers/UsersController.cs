using FTS.Application.DTO;
using FTS.Application.Handlers.Users.Commands.SignIn;
using FTS.Application.Handlers.Users.Commands.SignUp;
using FTS.Application.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FTS.Api.Controllers;

[ApiController]
[Route("api")]

public class UsersController(IMediator mediator,
    ITokenStorage tokenStorage) : ControllerBase
{
    [HttpPost("user/sign-up")]
    [SwaggerOperation("Create the user account")]
    public async Task<ActionResult> Post(SignUpCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("user/sign-in")]
    [SwaggerOperation("Sign in the user and return the JSON Web Token")]
    public async Task<ActionResult<JwtDto>> Post(SignInCommand command)
    {
        await mediator.Send(command);
        var jwt = tokenStorage.Get();
        return jwt;
    }
}

