using FluentValidation;
using FTS.Core.Entities;
using FTS.Core.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using FTS.Core.Security;
using FTS.Core.Exceptions;

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

    internal sealed class Handler(UserManager<User> userManager) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var user = User.Create(command.Name, command.Email, command.Email, command.Password, 0, UserLevel.Beginner, DateTime.UtcNow);

            IdentityResult identityResult = await userManager.CreateAsync(user, command.Password);
            if (!identityResult.Succeeded)
            {
                if (identityResult.Errors.Any(e => e.Code == "DuplicateEmail" || e.Code == "DuplicateUserName"))
                {
                    throw new ConflictException("Email or username is already in use.");
                }
                else
                {
                    var errorDescription = identityResult.Errors.First().Description;
                    throw new DomainException(errorDescription);
                }
            }

            IdentityResult addToRoleResult = await userManager.AddToRoleAsync(user, Roles.User);
            if (!addToRoleResult.Succeeded)
            {
                var error = addToRoleResult.Errors.FirstOrDefault()?.Description;
                throw new DomainException($"Failed to assign role '{Roles.User}'. Details: {error}");
            }
        }
    }
}