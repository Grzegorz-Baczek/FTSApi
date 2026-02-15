using FTS.App.Components.Pages.Login.Models;

namespace FTS.App.Components.TokenService;

public interface ITokenService
{
    Task<JwtDto> GetToken();
    Task RemoveToken();
    Task SetToken(JwtDto accessToken);
}
