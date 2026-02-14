namespace FTS.App.Components.Pages.Ingredients.Models;

public class CreateIngredientModel
{
    public string Name { get; set; } = null!;
    public string? Barcode { get; set; }
    public decimal Calories { get; set; }
    public decimal Carbohydrates { get; set; }
    public decimal Proteins { get; set; }
    public decimal Fat { get; set; }
    public decimal? SaturatedFat { get; set; }
    public decimal? Sugars { get; set; }
    public decimal? Fiber { get; set; }
    public decimal? Salt { get; set; }
}
