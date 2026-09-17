using FTS.Core.Enum;

namespace FTS.Application.Handlers.Recipes.Models;

public class RecipeIngredientDto
{
    public string IngredientName { get; set; }
    public decimal AmountInGrams { get; set; }
    public Unit Unit { get; set; }

    public RecipeIngredientDto(string ingredientName, decimal amountInGrams, Unit unit)
    {
        IngredientName = ingredientName;
        AmountInGrams = amountInGrams;
        Unit = unit;
    }
}
