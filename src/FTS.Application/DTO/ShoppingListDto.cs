namespace FTS.Application.DTO;

public record ShoppingListDto(
    Guid Id,
    string Name,
    Guid UserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IReadOnlyList<ShoppingListItemDto> Items);

public record ShoppingListItemDto(
    Guid Id,
    string ProductName,
    decimal? Quantity,
    string? Unit,
    string? Category,
    bool IsChecked);

public record CreateShoppingListDto(string Name);

public record AddShoppingListItemDto(
    string ProductName,
    decimal? Quantity,
    string? Unit,
    string? Category);
