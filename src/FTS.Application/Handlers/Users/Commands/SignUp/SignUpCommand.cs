using FluentValidation;
using FTS.Application.Abstractions;
using FTS.Application.Exceptions;
using FTS.Application.Security;
using FTS.Core.Entities;
using FTS.Core.Enum;
using MediatR;

namespace FTS.Application.Handlers.Users.Commands.SignUp;

public class SignUpCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int RankPoints { get; set; }
    public UserLevel Level { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class SignUpCommandHandler(
    IUserRepository repository,
    IPasswordManager passwordManager,
    IValidator<SignUpCommand> signUpValidator,
    IUserRepository userRepository) : IRequestHandler<SignUpCommand>
{
    public async Task Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await signUpValidator.ValidateAsync(command, cancellationToken);
        if(!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        if (await userRepository.GetByEmailAsync(command.Email) is not null)
        {
            throw new EmailAlreadyInUseException(command.Email);
        }

        if (await userRepository.GetByUsernameAsync(command.Name) is not null)
        {
            throw new UsernameAlreadyInUseException(command.Name);
        }

        var securedPassword = passwordManager.Secure(command.Password);
        var user = User.Create(command.Name, command.Email, securedPassword, command.RankPoints, command.Level, command.CreatedAt);
        await repository.AddAsync(user);
    }
}
