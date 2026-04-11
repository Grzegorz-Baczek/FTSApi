using FTS.Core.Entities;
using FTS.Core.Enum;
using FTS.Core.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FTS.Infrastructure.DAL;

internal class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider) 
        => _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FTSDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        if(!await roleManager.RoleExistsAsync(Roles.Admin))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Admin));
        }

        if(!await roleManager.RoleExistsAsync(Roles.User))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.User));
        }

        // Seed default user
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        const string seedEmail = "Marek@gmail.com";
        var existingUser = await userManager.FindByEmailAsync(seedEmail);
        if (existingUser is null)
        {
            var user = User.Create(
                name: "Marek",
                userName: seedEmail,
                email: seedEmail,
                passwordHash: string.Empty,
                rankPoints: 0,
                level: UserLevel.Beginner,
                createdAt: DateTime.UtcNow);

            await userManager.CreateAsync(user, "12345678Gg!");
            await userManager.AddToRoleAsync(user, Roles.User);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}