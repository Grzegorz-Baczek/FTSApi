namespace FTS.Core.Exceptions;

internal class InvalidNameException : CustomException
{
    public string Name { get; }

    public InvalidNameException(string name) : base($"Invalid name: {name}")
    {
        Name = name;
    }
}