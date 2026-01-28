using FTS.Application.Abstractions;
using FTS.Core.Entities;

namespace FTS.Infrastructure.DAL.Repositories
{
    internal sealed class IngredientRepository(FTSDbContext dbContext) : IIngredientRepository
    {
        public async Task AddIngredientAsync(Ingredient ingredient, CancellationToken cancellationToken)
        {
            await dbContext.Ingredients.AddAsync(ingredient, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
