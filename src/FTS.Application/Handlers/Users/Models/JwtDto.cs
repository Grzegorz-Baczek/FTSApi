namespace FTS.Application.Handlers.Users.Models;

public class JwtDto
{
    public string AccessToken { get; set; }

    public JwtDto(string accessToken)
    {
        AccessToken = accessToken;
    }
}
