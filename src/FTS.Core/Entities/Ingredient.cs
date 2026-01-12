namespace FTS.Core.Entities;

public class Ingredient
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    //relacje
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}
