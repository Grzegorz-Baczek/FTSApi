using FluentValidation;

namespace FTS.Application.Handlers.ShoppingLists.Commands.AddShoppingListItem;

public class AddShoppingListItemValidator : AbstractValidator<AddShoppingListItemCommand>
{
    public AddShoppingListItemValidator()
    {
        RuleFor(x => x.ShoppingListId)
            .NotEmpty()
            .WithMessage("Shopping list id must not be empty.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User id must not be empty.");

        RuleFor(x => x.ProductName)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(200)
            .WithMessage("Product name must be between 1 and 200 characters.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .When(x => x.Quantity.HasValue)
            .WithMessage("Quantity must be greater than 0.");
    }
}
