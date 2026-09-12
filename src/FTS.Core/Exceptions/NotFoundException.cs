namespace FTS.Core.Exceptions;

public class NotFoundException(string resource, object key)
    : CustomException($"{resource} with id '{key}' was not found.")
{
    public string Resource { get; } = resource;
}
