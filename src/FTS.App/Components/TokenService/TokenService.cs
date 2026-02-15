using FTS.App.Components.Pages.Login.Models;
using Blazored.LocalStorage;

namespace FTS.App.Components.TokenService;

public class TokenService(ILocalStorageService localStorageService) : ITokenService
{
    private const string Token = "accessToken";

    public async Task<JwtDto> GetToken()
    {
        return await localStorageService.GetItemAsync<JwtDto>(Token);
    }

    public async Task RemoveToken()
    {
        await localStorageService.RemoveItemAsync(Token);
    }

    public async Task SetToken(JwtDto accessToken)
    {
        await localStorageService.SetItemAsync(Token, accessToken);
    }
}
