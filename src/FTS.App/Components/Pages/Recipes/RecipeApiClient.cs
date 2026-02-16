using System.Reflection;
using FTS.App.Components.Pages.Recipes.Models;
using FTS.App.Components.TokenService;

namespace FTS.App.Components.Pages.Recipes;

public class RecipeApiClient(HttpClient HttpClient, ITokenService token)
{
    public async Task<IReadOnlyCollection<RecipeViewModel>?> GetRecipesAsync()
    {
        var result = await HttpClient.GetAsync("api/recipes");
        if (result.IsSuccessStatusCode)
        {
            return await result.Content.ReadFromJsonAsync<IReadOnlyCollection<RecipeViewModel>?>();
        }

        throw new Exception($"Something went wrong. Result code from server: {result.StatusCode}");
    }

    public async Task<RecipeViewModel?> GetRecipeAsync(Guid id)
    {
        var result = await HttpClient.GetAsync($"api/recipe/{id}");
        if (result.IsSuccessStatusCode)
        {
            return await result.Content.ReadFromJsonAsync<RecipeViewModel?>();
        }

        throw new Exception($"Something went wrong. Result code from server: {result.StatusCode}");
    }

    public async Task CreateRecipeAsync(CreateRecipeModel createRecipe)
    {
        var jwt = await token.GetToken();
        if (jwt != null)
        { 
            HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt.AccessToken);
        }

        var response = await HttpClient.PostAsJsonAsync("api/recipe", createRecipe);
        response.EnsureSuccessStatusCode();
    }
}