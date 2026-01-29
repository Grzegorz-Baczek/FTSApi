using FTS.Application.Abstractions;
using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class RecipeRepository(FTSDbContext dbContext) : IRecipeRepository
{
    public async Task AddRecipeAsync(Recipe recipe, CancellationToken cancellationToken)
    {
        await dbContext.Recipes.AddAsync(recipe, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRecipeAsync(Recipe recipe, CancellationToken ct)
    {
        dbContext.Recipes.Remove(recipe);
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task<Recipe?> GetRecipeAsync(Guid id, CancellationToken ct)
    {
        var recipe = await dbContext.Recipes.FirstOrDefaultAsync(r => r.Id == id, ct);
        return recipe;
    }
}
