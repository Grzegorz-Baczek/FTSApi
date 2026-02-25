using FTS.App.Components.Pages.Coobooks.Models;
using FTS.App.Components.TokenService;

namespace FTS.App.Components.Pages.Coobooks.ApiClient;

public class CookbookApiClient(HttpClient HttpClient, ITokenService token)
{
    public async Task CreateCookbookAsync(CreateCookbookModel createCookbook)
    {
        var jwt = await token.GetToken();
        if (jwt != null)
        {
            HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt.AccessToken);
        }

        var response = await HttpClient.PostAsJsonAsync("api/cookbook", createCookbook);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ApiError>();
            throw new Exception(error?.Reason ?? "Wystąpił nieznany błąd.");
        }
    }

    public async Task<IReadOnlyCollection<CookbookViewModel>?> GetCookbookAsync()
    {
        var jwt = await token.GetToken();
        if (jwt != null)
        {
            HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt.AccessToken);
        }

        var response = await HttpClient.GetAsync("api/cookbooks");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<CookbookViewModel>?>();
        }
        else
        {
            var error = await response.Content.ReadFromJsonAsync<ApiError>();
            throw new Exception(error?.Reason ?? "Wystąpił nieznany błąd.");
        }
    }

    private record ApiError(string Code, string Reason);
}