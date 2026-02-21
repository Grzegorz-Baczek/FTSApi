namespace FTS.Application.DTO;

public class RecipeIngredientDto
{
    public string IngredientName { get; set; }
    public decimal Amount { get; set; }
    public string Unit { get; set; }

    public RecipeIngredientDto(string ingredientName, decimal amount, string unit)
    {
        IngredientName = ingredientName;
        Amount = amount;
        Unit = unit;
    }
}
