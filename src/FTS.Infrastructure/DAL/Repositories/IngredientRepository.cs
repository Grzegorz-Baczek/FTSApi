using FTS.Application.Abstractions;
using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class IngredientRepository(FTSDbContext dbContext) :
    BaseRepository<Ingredient>(dbContext), IIngredientRepository
{
    public Task<Ingredient?> GetAsync(Guid id, CancellationToken ct)
        => DbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == id, ct);
}
