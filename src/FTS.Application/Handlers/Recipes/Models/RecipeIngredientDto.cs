using FTS.Core.Enum;

namespace FTS.Application.Handlers.Recipes.Models;

public class RecipeIngredientDto
{
    public Guid IngredientId { get; set; }
    public string IngredientName { get; set; } = null!;
    public decimal Amount { get; set; }
    public decimal AmountInGrams { get; set; }
    public Unit Unit { get; set; }
}
