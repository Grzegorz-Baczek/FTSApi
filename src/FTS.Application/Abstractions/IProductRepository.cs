using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface IProductRepository
{
    Task AddProductAsync(Product product, CancellationToken ct);
    Task<IEnumerable<Product>> GetProductsAsync(CancellationToken ct);
    Task<Product?> GetProductAsync(Guid id, CancellationToken ct);
}