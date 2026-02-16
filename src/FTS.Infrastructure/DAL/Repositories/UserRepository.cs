using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FTS.Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class UserRepository(
    FTSDbContext dbContext,
    IHttpContextAccessor contextAccessor) : IUserRepository
{
    public Guid? GetUserId()
    {
        var user = contextAccessor.HttpContext?.User;
        var userClaim = user?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                        ?? user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return Guid.TryParse(userClaim, out var userId) ? userId : null;
    }
}

