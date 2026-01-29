using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface IRecipeRepository
{
    Task AddRecipeAsync(Recipe recipe, CancellationToken cancellationToken);
    Task DeleteRecipeAsync(Recipe recipe, CancellationToken ct);
    Task<Recipe?> GetRecipeAsync(Guid id, CancellationToken ct);
}
