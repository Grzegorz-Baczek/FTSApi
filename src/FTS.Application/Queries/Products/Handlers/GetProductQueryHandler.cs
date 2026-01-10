using FTS.Application.Abstractions;
using FTS.Application.Exceptions;
using FTS.Core.Entities;
using MediatR;

namespace FTS.Application.Queries.Products.Handlers;

public record GetProductQuery(Guid Id) : IRequest<Product>;

internal sealed class GetProductQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductQuery, Product>
{
    public async Task<Product> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductAsync(query.Id, cancellationToken);
        if (product == null)
        {
            throw new NotFoundProductException(query.Id);
        }

        return product;
    }
}
