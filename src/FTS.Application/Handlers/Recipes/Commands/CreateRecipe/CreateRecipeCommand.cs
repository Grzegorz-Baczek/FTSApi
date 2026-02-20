using FluentValidation;
using FTS.Application.Abstractions;
using FTS.Core.Entities;
using MediatR;

namespace FTS.Application.Handlers.Recipes.Commands.CreateRecipe;

public class CreateRecipeCommand : IRequest
{
    public string Title { get; set; } = null!;
    public string Steps { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public ICollection<CreateRecipeIngredientDto> RecipeIngredients { get; set; } = new List<CreateRecipeIngredientDto>();
}

internal sealed class CreateRecipeCommandHandler(
    IRecipeRepository recipeRepository,
    IValidator<CreateRecipeCommand> validator,
    IUserRepository userRepository,
    IIngredientRepository ingredientRepository) : IRequestHandler<CreateRecipeCommand>
{
    public async Task Handle(CreateRecipeCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var getCurrentUser = userRepository.GetUserId();
        var recipe = Recipe.Create(
            command.Title,
            command.Steps,
            false,
            command.ImageUrl,
            getCurrentUser!.Value);

        foreach (var ingredientDto in command.RecipeIngredients)
        {
            await ingredientRepository.GetIngredient(ingredientDto.IngredientId, cancellationToken);

            var recipeIngredient = RecipeIngredient.Create(
                ingredientDto.Amount,
                ingredientDto.Unit,
                recipe.Id,
                ingredientDto.IngredientId);

            recipe.RecipeIngredients.Add(recipeIngredient);
        }

        await recipeRepository.AddRecipeAsync(recipe, cancellationToken);
    }
}
