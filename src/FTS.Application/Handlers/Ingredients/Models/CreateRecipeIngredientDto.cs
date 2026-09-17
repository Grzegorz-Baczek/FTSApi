using FTS.Core.Enum;

namespace FTS.Application.Handlers.Ingredients.Models;

public record CreateRecipeIngredientDto(Guid IngredientId, decimal Amount, Unit Unit);