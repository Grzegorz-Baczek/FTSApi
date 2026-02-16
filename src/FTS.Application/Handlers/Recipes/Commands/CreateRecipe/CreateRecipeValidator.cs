using FluentValidation;

namespace FTS.Application.Handlers.Recipes.Commands.CreateRecipe;

public class CreateRecipeValidator : AbstractValidator<CreateRecipeCommand>
{
    public CreateRecipeValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(30)
            .WithMessage("title must be between 3 and 30 characters.");

        RuleFor(x => x.Steps)
            .NotEmpty()
            .WithMessage("steps must not be empty");
    }
}
