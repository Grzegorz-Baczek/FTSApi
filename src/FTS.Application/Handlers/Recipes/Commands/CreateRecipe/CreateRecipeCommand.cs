using FluentValidation;
using FTS.Application.Abstractions;
using FTS.Core.Entities;
using MediatR;

namespace FTS.Application.Handlers.Recipes.Commands.CreateRecipe;

public class CreateRecipeCommand : IRequest
{
    public string Title { get; set; } = null!;
    public string Steps { get; set; } = null!;
    public bool IsPublic { get; set; }
    public string? ImageUrl { get; set; }
    public Guid AuthorId { get; set; }
}

internal sealed class CreateRecipeCommandHandler(
    IRecipeRepository recipeRepository, 
    IValidator<CreateRecipeCommand> validator) : IRequestHandler<CreateRecipeCommand>
{
    public async Task Handle(CreateRecipeCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var recipe = Recipe.Create(command.Title, command.Steps, command.IsPublic, command.ImageUrl, command.AuthorId);
        await recipeRepository.AddRecipeAsync(recipe, cancellationToken);
    }
}
