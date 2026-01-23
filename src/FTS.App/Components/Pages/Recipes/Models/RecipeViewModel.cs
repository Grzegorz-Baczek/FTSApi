namespace FTS.App.Components.Pages.Recipes.Models;

public class RecipeViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Steps { get; set; }
    public bool IsPublic { get; set; }
    public string? ImageUrl { get; set; }
    //relacje
    public string Author { get; set; }
}
