using FTS.Application.Abstractions;
using FTS.Core.Entities;
using FTS.Core.Exceptions;
using MediatR;

namespace FTS.Application.Handlers.Recipes.Commands;

public static class DeleteRecipe
{
    public record Command(Guid Id) : IRequest;

    internal sealed class Handler(
        IRecipeRepository recipeRepository,
        ICurrentUser currentUser) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var userId = currentUser.Id;
            if (userId is null)
            {
                throw new UnauthorizedException("User is not authenticated.");
            }

            var recipe = await recipeRepository.GetOwnedAsync(command.Id, userId.Value, cancellationToken);
            if (recipe is null)
            {
                throw new NotFoundException<Recipe>(command.Id);
            }

            await recipeRepository.DeleteAsync(recipe, cancellationToken);
        }
    }

}
