using FTS.Application.Abstractions;
using FTS.Core.Entities;
using FTS.Core.Exceptions;
using MediatR;

namespace FTS.Application.Handlers.Recipes.Commands.DeleteRecipe;

public record DeleteRecipeCommand(Guid Id) : IRequest;

public class DeleteRecipeCommandHandler(IRecipeRepository recipeRepository) : IRequestHandler<DeleteRecipeCommand>
{
    public async Task Handle(DeleteRecipeCommand command, CancellationToken cancellationToken)
    {
        var recipe = await recipeRepository.GetAsync(command.Id, cancellationToken);
        if(recipe == null)
        {
            throw new NotFoundException<Recipe>(command.Id);
        }

        await recipeRepository.DeleteAsync(recipe, cancellationToken);
    }
}
