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
            .WithMessage("tytuł musi mieć od 3 do 30 znaków.");

        RuleFor(x => x.Steps)
            .NotEmpty()
            .WithMessage("opis nie mogą być puste");

        RuleFor(x => x.RecipeIngredients)
            .NotEmpty()
            .WithMessage("przepis musi zawierać co najmniej jeden składnik.");

        RuleForEach(x => x.RecipeIngredients).ChildRules(ingredient =>
        {
            ingredient.RuleFor(x => x.IngredientId)
                .NotEmpty()
                .WithMessage("id składnika nie może być pusty");

            ingredient.RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("kwota musi być większa niż 0.");

            ingredient.RuleFor(x => x.Unit)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("jednostka nie może być pusta i musi mieć maksymalnie 20 znaków.");
        });
    }
}
