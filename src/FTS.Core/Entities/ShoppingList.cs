namespace FTS.Core.Entities;

public class ShoppingList
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ICollection<ShoppingListItem> Items { get; set; } = new List<ShoppingListItem>();

    public ShoppingList() { }

    public static ShoppingList Create(string name, Guid userId, DateTime createdAt)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Shopping list name cannot be empty.", nameof(name));

        return new ShoppingList
        {
            Id = Guid.NewGuid(),
            Name = name,
            UserId = userId,
            CreatedAt = createdAt
        };
    }

    public ShoppingListItem AddItem(string productName, decimal? quantity, string? unit, string? category)
    {
        var item = new ShoppingListItem
        {
            Id = Guid.NewGuid(),
            ShoppingListId = Id,
            ProductName = productName,
            Quantity = quantity,
            Unit = unit,
            Category = category,
            IsChecked = false
        };

        Items.Add(item);
        UpdatedAt = DateTime.UtcNow;
        return item;
    }

    public void RemoveItem(Guid itemId)
    {
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item is not null)
        {
            Items.Remove(item);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void ToggleItem(Guid itemId)
    {
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item is not null)
        {
            item.IsChecked = !item.IsChecked;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
