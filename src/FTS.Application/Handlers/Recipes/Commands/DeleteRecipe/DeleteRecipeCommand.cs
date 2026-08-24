using FTS.Application.Abstractions;
using FTS.Application.Exceptions;
using FTS.Core.Entities;
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
            throw new NotFoundRecipeException(command.Id);
        }

        await recipeRepository.DeleteAsync(recipe, cancellationToken);
    }
}
