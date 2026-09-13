using FTS.Application.Abstractions;
using FTS.Core.Entities;
using FTS.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FTS.Infrastructure.Security;

internal static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddScoped<IIdentityService, IdentityService>()
            .AddScoped<ICurrentUser, HttpContextCurrentUser>();

        return services;
    }
}
