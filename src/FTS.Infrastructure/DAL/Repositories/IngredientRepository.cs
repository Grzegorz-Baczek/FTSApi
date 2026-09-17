using FTS.Application.Abstractions;
using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class IngredientRepository(FTSDbContext dbContext) :
    BaseRepository<Ingredient>(dbContext), IIngredientRepository
{
    public Task<Ingredient?> GetAsync(Guid id, CancellationToken ct)
        => DbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == id, ct);

    public async Task<IReadOnlyDictionary<Guid, Ingredient>> GetManyAsync(IEnumerable<Guid> ids, CancellationToken ct)
        => await DbContext.Ingredients
            .Where(i => ids.Contains(i.Id))
            .ToDictionaryAsync(i => i.Id, ct);
}
