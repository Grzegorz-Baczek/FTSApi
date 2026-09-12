using FTS.Application.Abstractions;
using FTS.Core.Entities;
using FTS.Core.Exceptions;
using MediatR;

namespace FTS.Application.Commands.Products.Handlers.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest;

internal sealed class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetAsync(command.Id, cancellationToken);
        if (product == null)
        {
            throw new NotFoundException<Product>(command.Id);
        }

        await productRepository.DeleteAsync(product, cancellationToken);
    }
}
