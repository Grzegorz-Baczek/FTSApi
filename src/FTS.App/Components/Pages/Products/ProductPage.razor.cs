using FTS.App.Components.Pages.Products.Models;
using Microsoft.AspNetCore.Components;

namespace FTS.App.Components.Pages.Products;

public partial class ProductPage : ComponentBase
{
    [Inject]
    public ProductApiClient ProductApiClient { get; set; } = default!;

    [Parameter]
    public Guid Id { get; set; }

    public ProductViewModel? product;

    protected override async Task OnInitializedAsync()
    {
        product = await ProductApiClient.GetProductAsync(Id);
    }
}
