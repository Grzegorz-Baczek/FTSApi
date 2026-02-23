namespace FTS.App.Components.Pages.Ingredients.Models;

public class IngredientListItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public override string ToString() => Name;
}
