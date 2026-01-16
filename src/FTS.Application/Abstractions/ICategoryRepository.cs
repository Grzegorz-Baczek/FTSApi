using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface ICategoryRepository
{
    Task AddCategoryAsync(Category category, CancellationToken ct);
    Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken ct);
}