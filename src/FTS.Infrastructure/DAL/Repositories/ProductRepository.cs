using FTS.Application.Abstractions;
using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class ProductRepository(FTSDbContext dbContext) : IProductRepository
{
    public async Task AddProductAsync(Product product, CancellationToken ct)
    {
        await dbContext.Products.AddAsync(product, ct);
        await dbContext.SaveChangesAsync(ct);
    }
    public async Task DeleteProductAsync(Product product, CancellationToken ct)
    {
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(ct);
    }
    public async Task<Product?> GetProductAsync(Guid id, CancellationToken ct)
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, ct);
        return product;
    }
}