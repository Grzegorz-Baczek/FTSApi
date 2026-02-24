using FluentValidation;
using FTS.Application.Abstractions;
using FTS.Core.Entities;
using MediatR;

namespace FTS.Application.Handlers.Cookbooks.Commands.CreateCookbook;

public record CreateCookbookCommand(string Name) : IRequest;

internal sealed class CreateCookbookCommandHandler(
    ICookbookRepository cookbookRepository,
    IUserRepository userRepository,
    IValidator<CreateCookbookCommand> validator) : IRequestHandler<CreateCookbookCommand>
{
    public async Task Handle(CreateCookbookCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var getCurrentUser = userRepository.GetUserId();
        var cookbook = Cookbook.Create(command.Name, getCurrentUser!.Value);
        await cookbookRepository.AddCookbookAsync(cookbook, cancellationToken);
    }
}
