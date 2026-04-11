namespace FTS.Application.Security;

public interface IGoogleTokenValidator
{
    Task<GoogleUserPayload> ValidateAsync(string idToken);
}

public record GoogleUserPayload(string Email, string? Name);
