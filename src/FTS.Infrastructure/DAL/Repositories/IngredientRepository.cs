using FTS.Application.Abstractions;
using FTS.Application.Exceptions;
using FTS.Core.Entities;
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
            throw new NotFoundIngredientException(id);
        }
        return ingredient;
    }
}
