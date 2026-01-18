using FTS.Core.Exceptions;

namespace FTS.Application.Exceptions;

internal class UsernameAlreadyInUseException : CustomException
{
    public string Name { get; }

    public UsernameAlreadyInUseException(string name) : base($"name: '{name}' is already in use.")
    {
        Name = name;
    }
}