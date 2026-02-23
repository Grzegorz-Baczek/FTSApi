namespace FTS.App.Components.Pages.Recipes.Models;

public class CreateRecipeModel
{
    public string Title { get; set; }
    public string Steps { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<CreateRecipeIngredientModel> RecipeIngredients { get; set; } = new List<CreateRecipeIngredientModel>();

    public CreateRecipeModel(string title, string steps, string? imageUrl, ICollection<CreateRecipeIngredientModel> recipeIngredients)
    {
        Title = title;
        Steps = steps;
        ImageUrl = imageUrl;
        RecipeIngredients = recipeIngredients;
    }
}

public class CreateRecipeIngredientModel
{
    public Guid IngredientId { get; set; }
    public decimal Amount { get; set; }
    public string Unit { get; set; } = string.Empty;
}
