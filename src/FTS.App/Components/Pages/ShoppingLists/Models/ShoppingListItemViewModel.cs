namespace FTS.App.Components.Pages.ShoppingLists.Models;

public class ShoppingListItemViewModel
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal? Quantity { get; set; }
    public string? Unit { get; set; }
    public string? Category { get; set; }
    public bool IsChecked { get; set; }
}
