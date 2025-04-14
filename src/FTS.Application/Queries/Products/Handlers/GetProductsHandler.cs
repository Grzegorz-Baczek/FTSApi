using FTS.Application.Abstractions;
using FTS.Core.Entities;
using MediatR;

namespace FTS.Application.Queries.Products.Handlers;

public record GetProductsQuery : IRequest<IEnumerable<Product>>;

internal sealed class GetProductsHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    public async Task<IEnumerable<Product>> Handle(GetProductsQuery query,
        CancellationToken ct)
    {
        var products = await productRepository.GetProductsAsync(ct);
        return products;
    }
}