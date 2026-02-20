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

    public RecipeIngredient(Guid id, decimal amount, string unit, Guid recipeId, Guid ingredientId)
    {
        Id = id;
        Amount = amount;
        Unit = unit;
        RecipeId = recipeId;
        IngredientId = ingredientId;

    }

    public static RecipeIngredient Create(decimal amount, string unit, Guid recipeId, Guid ingredientId)
    {
        var recipeIngredient = new RecipeIngredient(Guid.NewGuid(), amount, unit, recipeId, ingredientId);
        return recipeIngredient;
    }
}
