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

    private Ingredient(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Ingredient Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidIngredientNameException(name);
        }

        return new Ingredient(Guid.NewGuid(), name);
    }
}
