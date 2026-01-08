using FTS.App.Components.Pages.Categories;
using FTS.App.Components.Pages.Categories.Models;
using FTS.App.Components.Pages.Products.Models;
using Microsoft.AspNetCore.Components;

namespace FTS.App.Components.Pages.Products;

public partial class CreateProduct
{
    [Inject]
    public CategoryApiClient CategoryApiClient { get; set; } = default!;
    [Inject]
    public ProductApiClient ProductApiClient { get; set; } = default!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    public Guid? SelectedCategoryId { get; set; }
    public ProductViewModel ProductDto = new ProductViewModel();
    public IEnumerable<CategoryViewModel> CategoryDtos = new List<CategoryViewModel>();

    protected override async Task OnInitializedAsync()
    {
        await LoadGetCategories();
    }

    private async Task LoadGetCategories()
    {
        var result = await CategoryApiClient.GetCategoriesAsync();
        if (result != null)
        {
            CategoryDtos = result.ToList();
        }
        else
        {
            CategoryDtos = new List<CategoryViewModel>();
        }
    }

    private async Task CreateProductAsync()
    {
        var createProductDto = new CreateProductModel(ProductDto.Name, SelectedCategoryId);
        await ProductApiClient.CreateProductAsync(createProductDto);
        NavigateToProducts();
    }

    private void NavigateToProducts()
    {
        NavigationManager.NavigateTo("/products");
    }
}
