using FTS.Application.Abstractions;
using FTS.Core.Entities;
using MediatR;

namespace FTS.Application.Commands.Products.Handlers;

public record CreateProductCommand(string Name, Guid CategoryId) : IRequest;

internal sealed class CreateProductHandler(IProductRepository productRepository)
    : IRequestHandler<CreateProductCommand>
{
    public async Task Handle(CreateProductCommand command, CancellationToken ct)
    {
        var product = Product.Create(command.Name, command.CategoryId);
        await productRepository.AddProductAsync(product, ct);
    }
}