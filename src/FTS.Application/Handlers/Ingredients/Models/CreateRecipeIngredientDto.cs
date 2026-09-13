namespace FTS.Application.Handlers.Ingredients.Models;

public record CreateRecipeIngredientDto(Guid IngredientId, decimal Amount, string Unit);