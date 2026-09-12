using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface IRecipeRepository : IRepository<Recipe>
{
    Task<Recipe?> GetOwnedAsync(Guid id, Guid authorId, CancellationToken ct);
}
