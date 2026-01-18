namespace FTS.Application.DTO;

public class JwtDto
{
    public string AccessToken { get; set; }

    public JwtDto(string accessToken)
    {
        AccessToken = accessToken;
    }
}
