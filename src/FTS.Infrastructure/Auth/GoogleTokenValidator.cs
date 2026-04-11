using FTS.Application.Security;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;

namespace FTS.Infrastructure.Auth;

internal sealed class GoogleTokenValidator : IGoogleTokenValidator
{
    private readonly string _clientId;

    public GoogleTokenValidator(IOptions<AuthOptions> options)
    {
        _clientId = options.Value.GoogleClientId
                    ?? throw new InvalidOperationException("GoogleClientId is not configured in auth options.");
    }

    public async Task<GoogleUserPayload> ValidateAsync(string idToken)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new[] { _clientId }
        };

        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            return new GoogleUserPayload(payload.Email, payload.Name);
        }
        catch (InvalidJwtException ex)
        {
            throw new UnauthorizedAccessException("Invalid Google token.", ex);
        }
    }
}
