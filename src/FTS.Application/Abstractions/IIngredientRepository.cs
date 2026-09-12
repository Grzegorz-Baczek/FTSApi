using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface IIngredientRepository : IRepository<Ingredient>
{
    Task<Ingredient?> GetAsync(Guid id, CancellationToken ct);
}
