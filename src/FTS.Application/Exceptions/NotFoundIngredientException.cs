using FTS.Core.Exceptions;

namespace FTS.Application.Exceptions;

public class NotFoundIngredientException : CustomException
{
    public Guid Id { get; }

    public NotFoundIngredientException(Guid id) : base($"IngredientId: {id} not found")
    {
        Id = id;
    }
}
