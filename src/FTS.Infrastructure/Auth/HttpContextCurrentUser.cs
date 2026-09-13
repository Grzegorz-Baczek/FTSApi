using System.Security.Claims;
using FTS.Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace FTS.Infrastructure.Auth;

internal sealed class HttpContextCurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public Guid? Id
    {
        get
        {
            var user = httpContextAccessor.HttpContext?.User;
            var value = user?.FindFirstValue(ClaimTypes.NameIdentifier) ?? user?.FindFirstValue("sub");

            return Guid.TryParse(value, out var id) ? id : null;
        }
    }

    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true;
}
