using FTS.Core.Exceptions;

namespace FTS.Application.Exceptions;

internal sealed class IdentityValidationException : CustomException
{
    public IdentityValidationException(string message) : base(message) { }
}
