using FTS.Application.Abstractions;
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

        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync(CancellationToken ct)
        {
            var ingredients = await dbContext.Ingredients.ToListAsync(ct);
            return ingredients;
        }
    }
}
