using FTS.Core.Exceptions;
using FTS.Application.Security;
using FTS.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FTS.Application.Handlers.Users.Commands;

public static class SignIn
{
    public record Command(string Email, string Password) : IRequest;

    internal sealed class Handler(
        UserManager<User> userManager,
        IAuthenticator authenticator,
        ITokenStorage tokenStorage) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(command.Email);
            if (user is null)
            {
                throw new UnauthorizedException("Invalid credentials.");
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, command.Password);
            if (!isPasswordValid)
            {
                throw new UnauthorizedException("Invalid credentials.");
            }

            var roles = await userManager.GetRolesAsync(user);

            var jwt = authenticator.CreateToken(user.Id, roles);
            tokenStorage.Set(jwt);
        }
    }
}


