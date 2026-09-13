using FTS.Application.Abstractions;
using FTS.Core.Entities;
using MediatR;

namespace FTS.Application.Handlers.Categories.Commands;

public static class CreateCategory
{
    public record Command(string Name) : IRequest;

    internal sealed class Handler(ICategoryRepository categoryRepository) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken ct)
        {
            var category = Category.Create(command.Name);
            await categoryRepository.AddAsync(category, ct);
        }
    }
}