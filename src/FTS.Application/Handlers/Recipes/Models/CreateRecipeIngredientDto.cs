using FTS.Core.Enum;

namespace FTS.Application.Handlers.Recipes.Models;

public record CreateRecipeIngredientDto(Guid IngredientId, decimal Amount, Unit Unit);