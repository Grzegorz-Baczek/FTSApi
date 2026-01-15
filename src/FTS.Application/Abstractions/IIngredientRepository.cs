using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface IIngredientRepository 
{
    Task AddIngredientAsync(Ingredient ingredient, CancellationToken ct);
    Task<IEnumerable<Ingredient>> GetIngredientsAsync(CancellationToken ct);
}
