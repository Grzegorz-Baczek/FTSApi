using FTS.Core.Exceptions;

namespace FTS.Application.Exceptions;

internal class NotFoundProductException : CustomException
{
    public Guid Id { get; }

    public NotFoundProductException(Guid id) : base($"ProductId: {id} not found")
    {
        Id = id;
    }
}
