using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface IIngredientRepository
{
    Task AddIngredientAsync(Ingredient ingredient, CancellationToken ct);
    Task<Ingredient> GetIngredient(Guid id, CancellationToken ct);
}
