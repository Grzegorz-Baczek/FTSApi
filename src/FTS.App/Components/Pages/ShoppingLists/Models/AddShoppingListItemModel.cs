namespace FTS.App.Components.Pages.ShoppingLists.Models;

public class AddShoppingListItemModel
{
    public string ProductName { get; set; } = string.Empty;
    public decimal? Quantity { get; set; }
    public string? Unit { get; set; }
    public string? Category { get; set; }
}
