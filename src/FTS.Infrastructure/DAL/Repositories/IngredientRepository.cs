using FTS.Application.Abstractions;
using FTS.Application.Exceptions;
using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Repositories
{
    internal sealed class IngredientRepository(FTSDbContext dbContext) : IIngredientRepository
    {
        public async Task AddIngredientAsync(Ingredient ingredient, CancellationToken cancellationToken)
        {
            await dbContext.Ingredients.AddAsync(ingredient, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Ingredient> GetIngredient(Guid id, CancellationToken ct)
        {
            var ingredient = await dbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == id, ct);
            if (ingredient is null)
            {
                throw new NotFoundIngredientException(id);
            }
            return ingredient;
        }
    }
}
