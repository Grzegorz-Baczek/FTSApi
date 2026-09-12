using FluentValidation;
using FTS.Core.Entities;
using FTS.Core.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using FTS.Core.Security;
using FTS.Core.Exceptions;

namespace FTS.Application.Handlers.Users.Commands.SignUp;

public class SignUpCommand : IRequest
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class SignUpCommandHandler(
    UserManager<User> userManager,
    IValidator<SignUpCommand> signUpValidator) : IRequestHandler<SignUpCommand>
{
    public async Task Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await signUpValidator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

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
