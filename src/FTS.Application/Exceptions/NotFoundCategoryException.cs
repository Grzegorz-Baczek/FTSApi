using FTS.Core.Exceptions;

namespace FTS.Application.Exceptions;

public class NotFoundCategoryException : CustomException
{
    public Guid Id { get; }

    public NotFoundCategoryException(Guid id) : base($"CategoryId: {id} not found") 
        => Id = id;
}
