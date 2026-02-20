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

        RuleFor(x => x.RecipeIngredients)
            .NotEmpty()
            .WithMessage("recipe must have at least one ingredient.");

        RuleForEach(x => x.RecipeIngredients).ChildRules(ingredient =>
        {
            ingredient.RuleFor(x => x.IngredientId)
                .NotEmpty()
                .WithMessage("ingredient id must not be empty.");

            ingredient.RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("amount must be greater than 0.");

            ingredient.RuleFor(x => x.Unit)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("unit must not be empty and max 20 characters.");
        });
    }
}
