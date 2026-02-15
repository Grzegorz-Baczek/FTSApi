using FTS.Core.Exceptions;

namespace FTS.Infrastructure.Exceptions.NotFoundExceptions;

internal class NotFoundUserException : CustomException
{
    public Guid Id { get; }
    public string? Name { get; }

    public NotFoundUserException(Guid id) : base($"UserId: {id} not found")
    {
        Id = id;
    }
}