namespace FTS.Core.Entities;

public class RecipeIngredient
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Unit { get; set; } = null!;

    //relacje
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;

    public Guid IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;
}
