using FTS.Core.Exceptions;

namespace FTS.Core.Entities;

public class Ingredient
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

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
