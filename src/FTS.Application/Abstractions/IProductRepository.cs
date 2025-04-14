using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface IProductRepository
{
    Task AddProductAsync(Product product, CancellationToken ct);
}