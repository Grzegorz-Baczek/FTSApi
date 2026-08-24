using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface IRecipeRepository : IRepository<Recipe>
{
    Task<Recipe?> GetAsync(Guid id, CancellationToken ct);
}
