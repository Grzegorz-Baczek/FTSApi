using FTS.App.Components.Authentication;
using FTS.App.Components.Pages.Login.Models;
using FTS.App.Components.TokenService;

namespace FTS.App.Components.Pages.Login;

public class UserApiClient(HttpClient HttpClient,
    ITokenService tokenService, CustomAuthenticationStateProvider myAuthenticationStateProvider)
{
    public async Task<JwtDto?> LoginAsync(LoginModel loginModel)
    {
        var response = await HttpClient.PostAsJsonAsync("api/user/sign-in", loginModel);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<JwtDto>();
            await tokenService.SetToken(result);
            myAuthenticationStateProvider.StateChanged();
            return result;
        }
        else
        {
            return null;
        }
    }

    public async Task LogoutAsync()
    {
        await tokenService.RemoveToken();
        myAuthenticationStateProvider.StateChanged();
    }

    public async Task<UserViewModel?> GetMeAsync()
    {
        var jwt = await tokenService.GetToken();
        if (jwt != null)
        {
            HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt.AccessToken);
        }

        return await HttpClient.GetFromJsonAsync<UserViewModel>("api/user/me");
    }
}
