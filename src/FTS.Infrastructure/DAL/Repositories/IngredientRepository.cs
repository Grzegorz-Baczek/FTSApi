using FTS.Application.Abstractions;
using FTS.Core.Entities;
using FTS.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class IngredientRepository(FTSDbContext dbContext) :
    BaseRepository<Ingredient>(dbContext), IIngredientRepository
{
    public async Task<Ingredient> GetAsync(Guid id, CancellationToken ct)
    {
        var ingredient = await dbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == id, ct);
        if (ingredient is null)
        {
            throw new NotFoundException<Ingredient>(id);
        }
        return ingredient;
    }
}
