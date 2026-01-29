using FTS.Core.Exceptions;

namespace FTS.Application.Exceptions;

internal class NotFoundRecipeException : CustomException
{
    public Guid Id { get; }

    public NotFoundRecipeException(Guid id) : base($"RecipeId: {id} not found")
    {
        Id = id;
    }
}

