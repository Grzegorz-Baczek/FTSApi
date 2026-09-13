using FluentValidation;
using FTS.Application.Abstractions;
using FTS.Core.Entities;
using FTS.Core.Enum;
using MediatR;
using FTS.Core.Security;
using FTS.Core.Exceptions;
using FTS.Application.Enum;

namespace FTS.Application.Handlers.Users.Commands;

public static class SignUp
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8);
        }
    }

    public class Command : IRequest
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    internal sealed class Handler(IIdentityService identityService) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var user = User.Create(command.Name, command.Email, command.Email, command.Password, 0, UserLevel.Beginner, DateTime.UtcNow);

            var identityResult = await identityService.RegisterAsync(user, command.Password, Roles.User, cancellationToken);
            if (!identityResult.Succeeded)
            {
                if (identityResult.ErrorCode == IdentityErrorCode.DuplicateUser)
                {
                    throw new ConflictException("Email or username is already in use.");
                }

                throw new DomainException("Unable to create the user.");
            }
        }
    }
}