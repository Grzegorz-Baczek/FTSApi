using FluentValidation;
using FTS.Application.Abstractions;
using FTS.Core.Entities;
using FTS.Core.Exceptions;
using MediatR;
using FTS.Application.Handlers.Recipes.Models;

namespace FTS.Application.Handlers.Recipes.Commands;

public static class CreateRecipe
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Servings)
                .GreaterThan(0)
                .WithMessage("Liczba porcji musi być większa niż 0.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30)
                .WithMessage("tytuł musi mieć od 3 do 30 znaków.");

            RuleFor(x => x.Steps)
                .NotEmpty()
                .WithMessage("opis nie mogą być puste");

            RuleFor(x => x.RecipeIngredients)
                .NotEmpty()
                .WithMessage("przepis musi zawierać co najmniej jeden składnik.");

            RuleForEach(x => x.RecipeIngredients).ChildRules(ingredient =>
            {
                ingredient.RuleFor(x => x.IngredientId)
                    .NotEmpty()
                    .WithMessage("id składnika nie może być pusty");

                ingredient.RuleFor(x => x.Amount)
                    .GreaterThan(0)
                    .WithMessage("kwota musi być większa niż 0.");

                ingredient.RuleFor(x => x.Unit)
                    .IsInEnum()
                    .WithMessage("jednostka jest nieprawidłowa.");
            });
        }
    }

    public class Command : IRequest
    {
        public string Title { get; set; } = null!;
        public string Steps { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int Servings { get; set; }
        public ICollection<CreateRecipeIngredientDto> RecipeIngredients { get; set; } = new List<CreateRecipeIngredientDto>();
    }

    internal sealed class Handler(
        IRecipeRepository recipeRepository,
        ICurrentUser currentUser,
        IIngredientRepository ingredientRepository) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var userId = currentUser.Id;
            if (userId is null)
            {
                throw new UnauthorizedException("User is not authenticated.");
            }

            var recipe = Recipe.Create(
                command.Title,
                command.Steps,
                false,
                command.ImageUrl,
                userId.Value,
                command.Servings);

            var ingredientIds = command.RecipeIngredients.Select(i => i.IngredientId).Distinct();
            var ingredients = await ingredientRepository.GetManyAsync(ingredientIds, cancellationToken);

            foreach (var ingredientDto in command.RecipeIngredients)
            {
                if (!ingredients.TryGetValue(ingredientDto.IngredientId, out var ingredient))
                {
                    throw new NotFoundException<Ingredient>(ingredientDto.IngredientId);
                }

                var recipeIngredient = RecipeIngredient.Create(
                    ingredientDto.Amount,
                    ingredientDto.Unit,
                    recipe.Id,
                    ingredient);

                recipe.RecipeIngredients.Add(recipeIngredient);
            }

            recipe.RecalculateNutrition(ingredients);

            await recipeRepository.AddAsync(recipe, cancellationToken);
        }
    }
}
