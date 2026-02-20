namespace FTS.Application.Handlers.Recipes.Commands.CreateRecipe;

public record CreateRecipeIngredientDto(Guid IngredientId, decimal Amount, string Unit);