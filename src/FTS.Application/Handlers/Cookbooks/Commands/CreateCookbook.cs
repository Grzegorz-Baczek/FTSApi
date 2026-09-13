using FluentValidation;
using FTS.Application.Abstractions;
using FTS.Core.Entities;
using FTS.Core.Exceptions;
using MediatR;

namespace FTS.Application.Handlers.Cookbooks.Commands;

public static class CreateCookbook
{
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(cb => cb.Name)
                .NotEmpty().WithMessage("Nazwa książki kucharskiej jest wymagana.")
                .Length(3, 40).WithMessage("Nazwa musi mieć od 3 do 40 znaków.");
        }
    }

    public record Command(string Name) : IRequest;

    internal sealed class Handler(ICookbookRepository cookbookRepository, ICurrentUser currentUser) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var userId = currentUser.Id;
            if (userId is null)
            {
                throw new UnauthorizedException("User is not authenticated.");
            }

            var cookbook = Cookbook.Create(command.Name, userId.Value);
            await cookbookRepository.AddAsync(cookbook, cancellationToken);
        }
    }
}

