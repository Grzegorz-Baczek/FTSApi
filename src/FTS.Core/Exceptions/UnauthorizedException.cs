namespace FTS.Core.Exceptions;

public sealed class UnauthorizedException(string message) : CustomException(message);
