using FluentValidation;
using FTS.Application.Abstractions;
using FTS.Core.Entities;
using MediatR;

namespace FTS.Application.Handlers.Ingredients.Commands;

public static class CreateIngredient
{
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
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

    public class Command : IRequest
    {
        public string Name { get; set; } = null!;
        public string? Barcode { get; set; }
        public decimal Calories { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Proteins { get; set; }
        public decimal Fat { get; set; }
        public decimal? SaturatedFat { get; set; }
        public decimal? Sugars { get; set; }
        public decimal? Fiber { get; set; }
        public decimal? Salt { get; set; }
    }

    internal sealed class Handler(
        IIngredientRepository ingredientRepository) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var ingredient = Ingredient.Create(command.Name, command.Calories, command.Carbohydrates, command.Proteins,
                command.Fat, command.Barcode, command.SaturatedFat, command.Sugars, command.Fiber, command.Salt);

            await ingredientRepository.AddAsync(ingredient, cancellationToken);
        }
    }

}