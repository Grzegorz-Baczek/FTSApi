namespace FTS.App.Components.Pages.Recipes.Models;

public class CreateRecipeModel
{
    public string Title { get; set; }
    public string Steps { get; set; }
    public string? ImageUrl { get; set; }

    public CreateRecipeModel(string title, string steps, string? imageUrl)
    {
        Title = title;
        Steps = steps;
        ImageUrl = imageUrl;
    }
}
