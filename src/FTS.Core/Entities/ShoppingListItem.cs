namespace FTS.Core.Entities;

public class ShoppingListItem
{
    public Guid Id { get; set; }
    public Guid ShoppingListId { get; set; }
    public ShoppingList? ShoppingList { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal? Quantity { get; set; }
    public string? Unit { get; set; }
    public string? Category { get; set; }
    public bool IsChecked { get; set; }
}
