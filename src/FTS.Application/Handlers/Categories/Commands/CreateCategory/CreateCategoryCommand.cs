using FTS.Application.Abstractions;
using FTS.Core.Entities;
using MediatR;

namespace FTS.Application.Handlers.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name) : IRequest;

internal sealed class CreateCategoryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<CreateCategoryCommand>
{
    public async Task Handle(CreateCategoryCommand command, CancellationToken ct)
    {
        var category = Category.Create(command.Name);
        await categoryRepository.AddAsync(category, ct);
    }
}
