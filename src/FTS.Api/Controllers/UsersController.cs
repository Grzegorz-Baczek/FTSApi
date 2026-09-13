using System.Security.Claims;
using FTS.Application.DTO;
using FTS.Application.Handlers.Users.Commands;
using FTS.Application.Handlers.Users.Models;
using FTS.Application.Handlers.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FTS.Api.Controllers;

[ApiController]
[Route("api")]

public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPost("user/sign-up")]
    [SwaggerOperation("Create the user account")]
    public async Task<ActionResult> Post(SignUp.Command command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("user/sign-in")]
    [SwaggerOperation("Sign in the user and return the JSON Web Token")]
    public async Task<ActionResult<JwtDto>> Post(SignIn.Command command)
    {
        var jwt = await mediator.Send(command);
        return Ok(jwt);
    }

    [Authorize]
    [HttpGet("user/me")]
    public async Task<ActionResult<UserDto>> Get()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var user = await mediator.Send(new GetUser.Query(Id: userId));
        return Ok(user);
    }
}

