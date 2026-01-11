using FTS.Application.Abstractions;
using FTS.Application.Exceptions;
using MediatR;

namespace FTS.Application.Commands.Products.Handlers.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest;

internal sealed class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductAsync(command.Id, cancellationToken);
        if (product == null)
        {
            throw new NotFoundProductException(command.Id);
        }

        await productRepository.DeleteProductAsync(product, cancellationToken);
    }
}
