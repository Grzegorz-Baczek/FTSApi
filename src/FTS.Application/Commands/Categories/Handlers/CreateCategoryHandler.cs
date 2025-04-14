using MediatR;
using FTS.Core.Entities;
using FTS.Application.Abstractions;

namespace FTS.Application.Commands.Categories.Handlers;

public record CreateCategoryCommand(string Name) : IRequest;

internal sealed class CreateCategoryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<CreateCategoryCommand>
{
    public async Task Handle(CreateCategoryCommand command, CancellationToken ct)
    {
        var category = Category.Create(command.Name);
        await categoryRepository.AddCategoryAsync(category, ct);
    }
}