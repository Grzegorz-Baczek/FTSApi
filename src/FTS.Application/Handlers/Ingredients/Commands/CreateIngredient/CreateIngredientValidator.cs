using FluentValidation;

namespace FTS.Application.Handlers.Ingredients.Commands.CreateIngredient;

public class CreateIngredientValidator : AbstractValidator<CreateIngredientCommand>
{
    public CreateIngredientValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty()
            .WithMessage("Nazwa składnika jest wymagana.")
            .Length(3, 100).WithMessage("Nazwa musi mieć od 3 do 100 znaków.");

        RuleFor(i => i.Barcode)
            .Length(8, 13)
            .WithMessage("Kod kreskowy musi mieć od 8 do 13 znaków.");

        RuleFor(i => i.Calories)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Kalorie nie mogą być ujemne.");

        RuleFor(i => i.Proteins)
            .InclusiveBetween(0, 100)
            .WithMessage("Białko musi mieścić się w przedziale 0-100.");

        RuleFor(i => i.Carbohydrates)
            .InclusiveBetween(0, 100)
            .WithMessage("Węglowodany muszą mieścić się w przedziale 0-100.");

        RuleFor(i => i.Fat)
            .InclusiveBetween(0, 100)
            .WithMessage("Tłuszcz musi mieścić się w przedziale 0-100.");

        RuleFor(i => i.Sugars)
            .LessThanOrEqualTo(i => i.Carbohydrates)
            .When(i => i.Sugars.HasValue)
            .WithMessage("Cukry nie mogą przekraczać całkowitej ilości węglowodanów.");

        RuleFor(i => i.SaturatedFat)
            .LessThanOrEqualTo(i => i.Fat)
            .When(i => i.SaturatedFat.HasValue)
            .WithMessage("Kwasy nasycone nie mogą przekraczać całkowitej ilości tłuszczu.");

        RuleFor(i => i.Salt)
            .GreaterThanOrEqualTo(0)
            .When(i => i.Salt.HasValue);

        RuleFor(i => i.Fiber)
            .GreaterThanOrEqualTo(0)
            .When(i => i.Fiber.HasValue);

        RuleFor(i => i)
            .Must(i => (i.Proteins + i.Carbohydrates + i.Fat) <= 100)
            .WithMessage("Suma makroskładników (białko, węgle, tłuszcz) nie może przekroczyć 100g.");
    }
}
