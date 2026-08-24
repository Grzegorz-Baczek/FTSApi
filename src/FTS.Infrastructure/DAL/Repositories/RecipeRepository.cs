using FTS.Application.Abstractions;
using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class RecipeRepository(FTSDbContext dbContext) :
    BaseRepository<Recipe>(dbContext), IRecipeRepository
{
    public async Task<Recipe?> GetAsync(Guid id, CancellationToken ct)
    {
        var recipe = await dbContext.Recipes.FirstOrDefaultAsync(r => r.Id == id, ct);
        return recipe;
    }
}
