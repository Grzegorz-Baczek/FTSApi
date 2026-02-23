namespace FTS.App.Components.Pages.Recipes.Models;

public class RecipeIngredientViewModel
{
    public Guid IngredientId { get; set; }
    public string IngredientName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Unit { get; set; } = string.Empty;
}
