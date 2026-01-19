using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface IRecipeRepository
{
    Task AddRecipeAsync(Recipe recipe, CancellationToken cancellationToken);
}
