using FTS.Application.DTO;
using FTS.Application.Security;
using FTS.Core.Entities;
using FTS.Core.Enum;
using FTS.Core.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FTS.Application.Handlers.Users.Commands.GoogleSignIn;

public record GoogleSignInCommand(string IdToken) : IRequest;

internal sealed class GoogleSignInCommandHandler(
    UserManager<User> userManager,
    IGoogleTokenValidator googleTokenValidator,
    IAuthenticator authenticator,
    ITokenStorage tokenStorage) : IRequestHandler<GoogleSignInCommand>
{
    public async Task Handle(GoogleSignInCommand command, CancellationToken cancellationToken)
    {
        var payload = await googleTokenValidator.ValidateAsync(command.IdToken);

        var user = await userManager.FindByEmailAsync(payload.Email);

        if (user is null)
        {
            user = User.Create(
                name: payload.Name ?? payload.Email,
                userName: payload.Email,
                email: payload.Email,
                passwordHash: string.Empty,
                rankPoints: 0,
                level: UserLevel.Beginner,
                createdAt: DateTime.UtcNow);

            var result = await userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            await userManager.AddToRoleAsync(user, Roles.User);
        }

        var roles = await userManager.GetRolesAsync(user);
        var jwt = authenticator.CreateToken(user.Id, roles);
        tokenStorage.Set(jwt);
    }
}
