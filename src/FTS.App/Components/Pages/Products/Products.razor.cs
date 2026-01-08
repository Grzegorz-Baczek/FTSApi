using FTS.App.Components.Pages.Products.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor.Extensions;

namespace FTS.App.Components.Pages.Products;

public partial class Products : ComponentBase
{
    [Inject]
    public ProductApiClient ProductApiClient { get; set; }
    [Inject] 
    public NavigationManager NavigationManager { get; set; }

    public IEnumerable<ProductViewModel>? products;

    protected override async Task OnInitializedAsync()
    {
        products = await ProductApiClient.GetProductsAsync();
    }

    private void NavigateCreateProduct()
    {
        NavigationManager.NavigateTo("/createproduct");
    }
}