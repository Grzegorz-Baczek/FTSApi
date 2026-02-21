using System.ComponentModel.DataAnnotations;

namespace FTS.App.Components.Pages.Recipes.Models;

public class RecipeViewModel
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Steps is required")]
    public string Steps { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public bool IsPublic { get; set; }
    //relacje
    public string Author { get; set; }
    public ICollection<RecipeIngredientViewModel> RecipeIngredients { get; set; } = new List<RecipeIngredientViewModel>();
}
