using FTS.Application.Abstractions;
using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class RecipeRepository(FTSDbContext dbContext) :
    BaseRepository<Recipe>(dbContext), IRecipeRepository
{
    public Task<Recipe?> GetOwnedAsync(Guid id, Guid authorId, CancellationToken ct)
        => DbContext.Recipes.FirstOrDefaultAsync(r => r.Id == id && r.AuthorId == authorId, ct);
}
