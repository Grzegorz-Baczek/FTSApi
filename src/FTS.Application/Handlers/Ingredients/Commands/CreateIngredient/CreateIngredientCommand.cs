using FTS.Application.Abstractions;
using FTS.Core.Entities;
using MediatR;

namespace FTS.Application.Handlers.Ingredients.Commands.CreateIngredient;

public class CreateIngredientCommand : IRequest
{
    public string Name { get; set; } = null!;
}

internal sealed class CreateIngredientCommandHandler(IIngredientRepository ingredientRepository) : IRequestHandler<CreateIngredientCommand>
{
    public async Task Handle(CreateIngredientCommand command, CancellationToken cancellationToken)
    {
        var ingredient = Ingredient.Create(command.Name);
        await ingredientRepository.AddIngredientAsync(ingredient, cancellationToken);
    }
}
