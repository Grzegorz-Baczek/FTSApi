using FTS.Core.Exceptions;

namespace FTS.Core.Entities;

public class Ingredient
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    //kod kreskowy
    public string? Barcode { get; set; }

    public decimal Calories { get; set; }
    public decimal Carbohydrates { get; set; }
    public decimal Proteins { get; set; }
    public decimal Fat { get; set; }

    // W tym kwasy tłuszczowe nasycone
    public decimal? SaturatedFat { get; set; }
    public decimal? Sugars { get; set; }

    // Błonnik
    public decimal? Fiber { get; set; }
    public decimal? Salt { get; set; }

    //relacje
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    private Ingredient(Guid id,
        string name, decimal calories, decimal carbohydrates, decimal proteins, decimal fat,
        string? barcode, decimal? saturatedFat, decimal? sugars, decimal? fiber, decimal? salt)
    {
        Id = id;
        Name = name;
        Calories = calories;
        Carbohydrates = carbohydrates;
        Proteins = proteins;
        Fat = fat;
        Barcode = barcode;
        SaturatedFat = saturatedFat;
        Sugars = sugars;
        Fiber = fiber;
        Salt = salt;
    }

    public static Ingredient Create(string name, decimal calories, decimal carbohydrates, decimal proteins,
        decimal fat, string? barcode, decimal? saturatedFat, decimal? sugars, decimal? fiber, decimal? salt)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidIngredientNameException(name);
        }

        return new Ingredient(Guid.NewGuid(), name, calories,
            carbohydrates, proteins, fat, barcode, saturatedFat, sugars, fiber, salt);
    }
}
