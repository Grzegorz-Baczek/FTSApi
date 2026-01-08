using FTS.App.Components.Pages.Products.Models;

namespace FTS.App.Components.Pages.Products;

public class ProductApiClient(HttpClient HttpClient)
{
    public async Task<IEnumerable<ProductViewModel>?> GetProductsAsync()
    {
        var result = await HttpClient.GetAsync("api/products");
        if (result.IsSuccessStatusCode)
        {
            return await result.Content.ReadFromJsonAsync<IEnumerable<ProductViewModel>?>();
        }

        throw new Exception($"Something went wrong. Result code from server: {result.StatusCode}");
    }

    public async Task CreateProductAsync(CreateProductModel createProduct)
    {
        await HttpClient.PostAsJsonAsync("api/product", createProduct);
    }
}