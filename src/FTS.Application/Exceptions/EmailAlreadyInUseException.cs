using FTS.Core.Exceptions;

namespace FTS.Application.Exceptions;

internal sealed class EmailAlreadyInUseException : CustomException
{
    public string Email { get; }

    public EmailAlreadyInUseException(string email) : base($"email: '{email}' is already in use.")
    {
        Email = email;
    }
}
