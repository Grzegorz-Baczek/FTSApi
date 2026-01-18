using FTS.Application.Security;
using FTS.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace FTS.Infrastructure.Security;

internal sealed class PasswordManager(IPasswordHasher<User> passwordHasher) : IPasswordManager
{
    public string Secure(string password)
    {
        return passwordHasher.HashPassword(
            default, password);
    }

    public bool Validate(string password, string securedPassword)
    {
        return passwordHasher.VerifyHashedPassword(
            default, securedPassword, password) == PasswordVerificationResult.Success;
    }
}
