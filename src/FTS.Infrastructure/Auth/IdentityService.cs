using FTS.Application.Abstractions;
using FTS.Application.Enum;
using FTS.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace FTS.Infrastructure.Auth;

internal sealed class IdentityService(UserManager<User> userManager) : IIdentityService
{
    public async Task<IdentityOperationResult> RegisterAsync(User user, string password, string role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var createResult = await userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
        {
            return ToResult(createResult);
        }

        var roleResult = await userManager.AddToRoleAsync(user, role);
        if (roleResult.Succeeded)
        {
            return IdentityOperationResult.Success();
        }

        var deleteResult = await userManager.DeleteAsync(user);
        if (!deleteResult.Succeeded)
        {
            return new IdentityOperationResult(false, IdentityErrorCode.Failed);
        }

        return ToResult(roleResult);
    }

    public Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return userManager.FindByEmailAsync(email);
    }

    public Task<bool> CheckPasswordAsync(User user, string password)
        => userManager.CheckPasswordAsync(user, password);

    public async Task<IReadOnlyCollection<string>> GetRolesAsync(User user)
        => (await userManager.GetRolesAsync(user)).ToArray();

    private static IdentityOperationResult ToResult(IdentityResult result)
    {
        var errors = result.Errors.ToArray();

        if (errors.Any(error =>
            error.Code.StartsWith("Password", StringComparison.Ordinal)))
        {
            return new IdentityOperationResult(false, IdentityErrorCode.PasswordPolicy);
        }

        if (errors.Any(error => error.Code is "DuplicateEmail" or "DuplicateUserName"))
        {
            return new IdentityOperationResult(false, IdentityErrorCode.DuplicateUser);
        }

        return new IdentityOperationResult(false, IdentityErrorCode.Failed);
    }
}
