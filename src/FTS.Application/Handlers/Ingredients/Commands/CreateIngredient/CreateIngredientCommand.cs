using FluentValidation;
using FTS.Application.Abstractions;
using FTS.Core.Entities;
using MediatR;

namespace FTS.Application.Handlers.Ingredients.Commands.CreateIngredient;

public class CreateIngredientCommand : IRequest
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

internal sealed class CreateIngredientCommandHandler(
    IIngredientRepository ingredientRepository,
    IValidator<CreateIngredientCommand> validator) : IRequestHandler<CreateIngredientCommand>
{
    public async Task Handle(CreateIngredientCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var ingredient = Ingredient.Create(command.Name, command.Calories, command.Carbohydrates, command.Proteins,
            command.Fat, command.Barcode, command.SaturatedFat, command.Sugars, command.Fiber, command.Salt);

        await ingredientRepository.AddIngredientAsync(ingredient, cancellationToken);
    }
}
