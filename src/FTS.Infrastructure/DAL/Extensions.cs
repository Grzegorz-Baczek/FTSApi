using FTS.Application.Abstractions;
using FTS.Core.Entities;
using FTS.Infrastructure.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FTS.Infrastructure.DAL;

internal static class Extensions
{
    private const string OptionsSectionName = "MSql";

    public static IServiceCollection AddMSql(this IServiceCollection services, IConfiguration configuration)
    {
        // Aspire wstrzykuje connection string jako ConnectionStrings:ftsdb
        // Fallback na MSql:ConnectionString (user secrets / appsettings)
        var connectionString = configuration.GetConnectionString("ftsdb");

        if (string.IsNullOrEmpty(connectionString))
        {
            services.Configure<MSqlOptions>(configuration.GetRequiredSection(OptionsSectionName));
            var mSqlOptions = configuration.GetOptions<MSqlOptions>(OptionsSectionName);
            connectionString = mSqlOptions.ConnectionString;
        }

        services.AddDbContext<FTSDbContext>(x => x.UseSqlServer(connectionString));
        services.AddHostedService<DatabaseInitializer>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IIngredientRepository, IngredientRepository>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IShoppingListRepository, ShoppingListRepository>();

        services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<FTSDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}