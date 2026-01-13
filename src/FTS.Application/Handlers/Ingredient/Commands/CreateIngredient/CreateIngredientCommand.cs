using FTS.Application.Abstractions;
using MediatR;

namespace FTS.Application.Handlers.Ingredient.Commands.CreateIngredient;

public class CreateIngredientCommand : IRequest
{
    public string Name { get; set; } = null!;
}

internal sealed class CreateIngredientCommandHandler(IIngredientRepository ingredientRepository) : IRequestHandler<CreateIngredientCommand>
{
    public async Task Handle(CreateIngredientCommand command, CancellationToken cancellationToken)
    {
        var ingredient = Core.Entities.Ingredient.Create(command.Name);
        await ingredientRepository.AddIngredientAsync(ingredient, cancellationToken);
    }
}
