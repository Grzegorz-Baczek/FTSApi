using FTS.Application.Exceptions;
using FTS.Application.Security;
using FTS.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FTS.Application.Handlers.Users.Commands.SignIn;

public record SignInCommand(string Email, string Password) : IRequest;

internal sealed class SignInCommandHandler(
    UserManager<User> userManager,
    IAuthenticator authenticator,
    ITokenStorage tokenStorage) : IRequestHandler<SignInCommand>
{
    public async Task Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(command.Email);
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(user, command.Password);
        if (!isPasswordValid)
        {
            throw new InvalidCredentialsException();
        }

        var roles = await userManager.GetRolesAsync(user);

        var jwt = authenticator.CreateToken(user.Id, roles);
        tokenStorage.Set(jwt);
    }
}

