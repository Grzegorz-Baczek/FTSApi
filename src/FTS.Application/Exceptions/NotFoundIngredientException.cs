using FTS.Core.Exceptions;

namespace FTS.Application.Exceptions;

internal class NotFoundIngredientException : CustomException
{
    public Guid Id { get; }
    public string? Name { get; }

    public NotFoundIngredientException(Guid id) : base($"IngredientId: {id} not found")
    {
        Id = id;
    }
}
