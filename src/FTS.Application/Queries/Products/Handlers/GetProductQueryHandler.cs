using FTS.Application.Abstractions;
using FTS.Core.Entities;
using FTS.Core.Exceptions;
using MediatR;

namespace FTS.Application.Queries.Products.Handlers;

public record GetProductQuery(Guid Id) : IRequest<Product>;

internal sealed class GetProductQueryHandler(IProductRepository productRepository) 
    : IRequestHandler<GetProductQuery, Product>
{
    public async Task<Product> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetAsync(query.Id, cancellationToken);
        if (product == null)
        {
            throw new NotFoundException<Product>(query.Id);
        }

        return product;
    }
}
