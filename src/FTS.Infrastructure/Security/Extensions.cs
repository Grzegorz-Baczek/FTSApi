using FTS.Application.Security;
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
            .AddSingleton<ITokenStorage, HttpContextTokenStorage>()
            .AddSingleton<IAuthenticator, Authenticator>();

        return services;
    }
}
