using FTS.Application.Abstractions;
using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class CategoryRepository(FTSDbContext dbContext) : ICategoryRepository
{
    public async Task AddCategoryAsync(Category category, CancellationToken ct)
    {
        await dbContext.Categories.AddAsync(category, ct);
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken ct)
    {
        var categories = await dbContext.Categories.ToListAsync(ct);
        return categories;
    }

    public async Task<Category> GetCategoryAsync(Guid id, CancellationToken ct)
    {
        var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, ct);
        return category;
    }
}