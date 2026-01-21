using FTS.App.Components.Pages.Recipes.Models;

namespace FTS.App.Components.Pages.Recipes;

public class RecipeApiClient(HttpClient HttpClient)
{
    public async Task<IEnumerable<RecipeViewModel>?> GetRecipesAsync()
    {
        var result = await HttpClient.GetAsync("api/recipes");
        if (result.IsSuccessStatusCode)
        {
            return await result.Content.ReadFromJsonAsync<IEnumerable<RecipeViewModel>?>();
        }

        throw new Exception($"Something went wrong. Result code from server: {result.StatusCode}");
    }
}