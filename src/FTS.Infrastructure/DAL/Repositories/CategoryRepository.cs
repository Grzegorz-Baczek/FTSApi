using FTS.Application.Abstractions;
using FTS.Core.Entities;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class CategoryRepository(FTSDbContext dbContext) : ICategoryRepository
{
    public async Task AddCategoryAsync(Category category, CancellationToken ct)
    {
        await dbContext.Categories.AddAsync(category, ct);
        await dbContext.SaveChangesAsync(ct);
    }
}