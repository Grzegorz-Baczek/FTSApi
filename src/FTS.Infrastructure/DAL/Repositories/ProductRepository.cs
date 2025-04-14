using FTS.Application.Abstractions;
using FTS.Core.Entities;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class ProductRepository(FTSDbContext dbContext) : IProductRepository
{
    public async Task AddProductAsync(Product product, CancellationToken ct)
    {
        await dbContext.Products.AddAsync(product, ct);
        await dbContext.SaveChangesAsync(ct);
    }
}