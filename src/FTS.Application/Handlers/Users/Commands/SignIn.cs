using FTS.Core.Exceptions;
using FTS.Application.Abstractions;
using FTS.Application.Security;
using MediatR;
using FTS.Application.Handlers.Users.Models;

namespace FTS.Application.Handlers.Users.Commands;

public static class SignIn
{
    public record Command(string Email, string Password) : IRequest<JwtDto>;

    internal sealed class Handler(
        IIdentityService identityService,
        IAuthenticator authenticator) : IRequestHandler<Command, JwtDto>
    {
        public async Task<JwtDto> Handle(Command command, CancellationToken cancellationToken)
        {
            var user = await identityService.FindByEmailAsync(command.Email, cancellationToken);
            if (user is null)
            {
                throw new UnauthorizedException("Invalid credentials.");
            }

            var isPasswordValid = await identityService.CheckPasswordAsync(user, command.Password);
            if (!isPasswordValid)
            {
                throw new UnauthorizedException("Invalid credentials.");
            }

            var roles = await identityService.GetRolesAsync(user);

            return authenticator.CreateToken(user.Id, roles);
        }
    }
}


