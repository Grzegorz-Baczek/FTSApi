using FluentValidation;

namespace FTS.Application.Handlers.Cookbooks.Commands.CreateCookbook;

public class CreateCookbookValidator : AbstractValidator<CreateCookbookCommand>
{
    public CreateCookbookValidator()
    {
        RuleFor(cb => cb.Name)
        .NotEmpty()
        .WithMessage("Nazwa książki kucharskiej jest wymagana.")
        .Length(3, 40).WithMessage("Nazwa musi mieć od 3 do 40 znaków.");
    }
}
