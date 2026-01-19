using FTS.Application.Abstractions;
using FTS.Infrastructure.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FTS.Infrastructure.DAL;

internal static class Extensions
{
    private const string OptionsSectionName = "MSql";

    public static IServiceCollection AddMSql(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MSqlOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var mSqlOptions = configuration.GetOptions<MSqlOptions>(OptionsSectionName);
        services.AddDbContext<FTSDbContext>(x => x.UseSqlServer(mSqlOptions.ConnectionString));
        services.AddHostedService<DatabaseInitializer>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IIngredientRepository, IngredientRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();

        return services;
    }
}