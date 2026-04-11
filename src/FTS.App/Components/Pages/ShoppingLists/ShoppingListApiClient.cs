using System.Net.Http.Headers;
using FTS.App.Components.Pages.ShoppingLists.Models;
using FTS.App.Components.TokenService;

namespace FTS.App.Components.Pages.ShoppingLists;

public class ShoppingListApiClient(HttpClient httpClient, ITokenService tokenService)
{
    private async Task SetAuthHeaderAsync()
    {
        var jwt = await tokenService.GetToken();
        if (jwt is not null)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", jwt.AccessToken);
        }
    }

    public async Task<List<ShoppingListViewModel>?> GetAllAsync()
    {
        await SetAuthHeaderAsync();
        var response = await httpClient.GetAsync("api/shopping-lists");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ShoppingListViewModel>>();
    }

    public async Task<ShoppingListViewModel?> GetAsync(Guid id)
    {
        await SetAuthHeaderAsync();
        var response = await httpClient.GetAsync($"api/shopping-lists/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ShoppingListViewModel>();
    }

    public async Task<ShoppingListViewModel?> CreateAsync(CreateShoppingListModel model)
    {
        await SetAuthHeaderAsync();
        var response = await httpClient.PostAsJsonAsync("api/shopping-lists", model);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ShoppingListViewModel>();
    }

    public async Task DeleteAsync(Guid id)
    {
        await SetAuthHeaderAsync();
        var response = await httpClient.DeleteAsync($"api/shopping-lists/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<ShoppingListItemViewModel?> AddItemAsync(Guid listId, AddShoppingListItemModel model)
    {
        await SetAuthHeaderAsync();
        var response = await httpClient.PostAsJsonAsync($"api/shopping-lists/{listId}/items", model);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ShoppingListItemViewModel>();
    }

    public async Task RemoveItemAsync(Guid listId, Guid itemId)
    {
        await SetAuthHeaderAsync();
        var response = await httpClient.DeleteAsync($"api/shopping-lists/{listId}/items/{itemId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task ToggleItemAsync(Guid listId, Guid itemId)
    {
        await SetAuthHeaderAsync();
        var response = await httpClient.PatchAsync($"api/shopping-lists/{listId}/items/{itemId}/toggle", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<ShoppingListItemViewModel>?> AddItemsFromImageAsync(Guid listId, Stream fileStream, string fileName)
    {
        await SetAuthHeaderAsync();
        using var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(fileStream);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(GetMimeType(fileName));
        content.Add(streamContent, "file", fileName);

        var response = await httpClient.PostAsync($"api/shopping-lists/{listId}/items/from-image", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ShoppingListItemViewModel>>();
    }

    private static string GetMimeType(string fileName)
    {
        var ext = Path.GetExtension(fileName)?.ToLowerInvariant();
        return ext switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".webp" => "image/webp",
            _ => "application/octet-stream"
        };
    }
}
