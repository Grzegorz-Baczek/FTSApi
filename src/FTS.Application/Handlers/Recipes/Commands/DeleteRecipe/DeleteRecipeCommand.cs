using FTS.Application.Abstractions;
using FTS.Core.Entities;
using FTS.Core.Exceptions;
using MediatR;

namespace FTS.Application.Handlers.Recipes.Commands.DeleteRecipe;

public record DeleteRecipeCommand(Guid Id) : IRequest;

internal sealed class DeleteRecipeCommandHandler(
    IRecipeRepository recipeRepository,
    IUserRepository userRepository) : IRequestHandler<DeleteRecipeCommand>
{
    public async Task Handle(DeleteRecipeCommand command, CancellationToken cancellationToken)
    {
        var userId = userRepository.GetUserId();
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
