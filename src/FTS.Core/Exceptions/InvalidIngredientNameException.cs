namespace FTS.Core.Exceptions;

internal class InvalidIngredientNameException : CustomException
{
    public string Name { get; }

    public InvalidIngredientNameException(string name) : base($"Invalid ingredient name: {name}")
    {
        Name = name;
    }
}
