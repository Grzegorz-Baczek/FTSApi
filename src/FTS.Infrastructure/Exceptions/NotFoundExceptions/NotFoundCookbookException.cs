using FTS.Core.Exceptions;

namespace FTS.Infrastructure.Exceptions.NotFoundExceptions;

internal class NotFoundCookbookException : CustomException
{
    public Guid Id { get; }

    public NotFoundCookbookException(Guid id) : base($"CookbookId: {id} not found")
    {
        Id = id;
    }
}
