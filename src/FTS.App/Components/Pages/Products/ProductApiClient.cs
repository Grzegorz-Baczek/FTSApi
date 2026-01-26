using FTS.App.Components.Pages.Products.Models;

namespace FTS.App.Components.Pages.Products;

public class ProductApiClient(HttpClient HttpClient)
{
    public async Task CreateProductAsync(CreateProductModel createProduct)
    {
        await HttpClient.PostAsJsonAsync("api/product", createProduct);
    }

    public async Task<ProductViewModel>? GetProductAsync(Guid productId)
    {
        var result = await HttpClient.GetAsync($"api/product/{productId}");
        if (result.IsSuccessStatusCode)
        {
            return await result.Content.ReadFromJsonAsync<ProductViewModel?>();
        }

        throw new Exception($"Something went wrong. Result code from server: {result.StatusCode}");
    }
}