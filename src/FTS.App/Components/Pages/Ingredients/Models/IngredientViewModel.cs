using System.ComponentModel.DataAnnotations;

namespace FTS.App.Components.Pages.Ingredients.Models;

public class IngredientViewModel
{
    [Required(ErrorMessage = "Nazwa jest wymagana")]
    public string Name { get; set; } = null!;
    public string? Barcode { get; set; }

    [Required(ErrorMessage = "Kalorie są wymagane")]
    public decimal Calories { get; set; }

    [Required(ErrorMessage = "Węglowodany są wymagane")]
    public decimal Carbohydrates { get; set; }

    [Required(ErrorMessage = "Białko jest wymagane")]
    public decimal Proteins { get; set; }

    [Required(ErrorMessage = "Tłuszcze są wymagane")]
    public decimal Fat { get; set; }
    public decimal? SaturatedFat { get; set; }
    public decimal? Sugars { get; set; }
    public decimal? Fiber { get; set; }
    public decimal? Salt { get; set; }
}
