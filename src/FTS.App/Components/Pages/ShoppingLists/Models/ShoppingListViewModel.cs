namespace FTS.App.Components.Pages.ShoppingLists.Models;

public class ShoppingListViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<ShoppingListItemViewModel> Items { get; set; } = [];
}
