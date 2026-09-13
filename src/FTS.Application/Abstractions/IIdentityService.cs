using FTS.Application.Enum;
using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface IIdentityService
{
    Task<IdentityOperationResult> RegisterAsync(User user, string password, string role, CancellationToken cancellationToken);
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<IReadOnlyCollection<string>> GetRolesAsync(User user);
}

public sealed record IdentityOperationResult(bool Succeeded, IdentityErrorCode ErrorCode = IdentityErrorCode.None)
{
    public static IdentityOperationResult Success() => new(true);
}
