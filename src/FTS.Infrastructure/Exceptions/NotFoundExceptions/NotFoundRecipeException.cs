using FTS.Core.Exceptions;

namespace FTS.Infrastructure.Exceptions.NotFoundExceptions;

internal class NotFoundRecipeException : CustomException
{
    public Guid Id { get; }
    public string? Name { get; }

    public NotFoundRecipeException(Guid id) : base($"RecipeId: {id} not found")
    {
        Id = id;
    }
}
