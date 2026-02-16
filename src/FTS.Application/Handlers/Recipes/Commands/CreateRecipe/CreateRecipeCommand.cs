using FluentValidation;
using FTS.Application.Abstractions;
using FTS.Core.Entities;
using MediatR;

namespace FTS.Application.Handlers.Recipes.Commands.CreateRecipe;

public class CreateRecipeCommand : IRequest
{
    public string Title { get; set; } = null!;
    public string Steps { get; set; } = null!;
    public string? ImageUrl { get; set; }
}

internal sealed class CreateRecipeCommandHandler(
    IRecipeRepository recipeRepository,
    IValidator<CreateRecipeCommand> validator,
    IUserRepository userRepository) : IRequestHandler<CreateRecipeCommand>
{
    public async Task Handle(CreateRecipeCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var getCurrentUser = userRepository.GetUserId();
        var recipe = Recipe.Create(command.Title, command.Steps, false, command.ImageUrl, getCurrentUser.Value);
        await recipeRepository.AddRecipeAsync(recipe, cancellationToken);
    }
}
