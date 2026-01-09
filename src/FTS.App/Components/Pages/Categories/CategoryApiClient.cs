using FTS.App.Components.Pages.Categories.Models;
using FTS.App.Components.Pages.Products.Models;

namespace FTS.App.Components.Pages.Categories;

public class CategoryApiClient(HttpClient HttpClient)
{
    public async Task<IEnumerable<CategoryViewModel>?> GetCategoriesAsync()
    {
        var result = await HttpClient.GetAsync("api/categories");
        if (result.IsSuccessStatusCode)
        {
            return await result.Content.ReadFromJsonAsync<IEnumerable<CategoryViewModel>?>();
        }

        throw new Exception($"Something went wrong. Result code from server: {result.StatusCode}");
    }   
    
    public async Task<CategoryViewModel>? GetCategoryAsync(Guid categoryId)
    {
        var result = await HttpClient.GetAsync($"api/category/{categoryId}");
        if (result.IsSuccessStatusCode)
        {
            return await result.Content.ReadFromJsonAsync<CategoryViewModel?>();
        }

        throw new Exception($"Something went wrong. Result code from server: {result.StatusCode}");
    }

    public async Task CreateCategoryAsync(CreateCategoryModel createCategory)
    {
        await HttpClient.PostAsJsonAsync("api/category", createCategory);
    }
}