using FluentValidation;
using FTS.Application.Abstractions;
using FTS.Application.Handlers.Ingredients.Models;
using FTS.Core.Entities;
using FTS.Core.Exceptions;
using MediatR;

namespace FTS.Application.Handlers.Recipes.Commands;

public static class CreateRecipe
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
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
                    .NotEmpty()
                    .MaximumLength(20)
                    .WithMessage("jednostka nie może być pusta i musi mieć maksymalnie 20 znaków.");
            });
        }
    }

    public class Command : IRequest
    {
        public string Title { get; set; } = null!;
        public string Steps { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public ICollection<CreateRecipeIngredientDto> RecipeIngredients { get; set; } = new List<CreateRecipeIngredientDto>();
    }

    internal sealed class Handler(
        IRecipeRepository recipeRepository,
        IUserRepository userRepository,
        IIngredientRepository ingredientRepository) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var userId = userRepository.GetUserId();
            if (userId is null)
            {
                throw new UnauthorizedException("User is not authenticated.");
            }

            var recipe = Recipe.Create(
                command.Title,
                command.Steps,
                false,
                command.ImageUrl,
                userId.Value);

            foreach (var ingredientDto in command.RecipeIngredients)
            {
                var ingredient = await ingredientRepository.GetAsync(ingredientDto.IngredientId, cancellationToken);
                if (ingredient is null)
                {
                    throw new NotFoundException<Ingredient>(ingredientDto.IngredientId);
                }

                var recipeIngredient = RecipeIngredient.Create(
                    ingredientDto.Amount,
                    ingredientDto.Unit,
                    recipe.Id,
                    ingredientDto.IngredientId);

                recipe.RecipeIngredients.Add(recipeIngredient);
            }

            await recipeRepository.AddAsync(recipe, cancellationToken);
        }
    }
}
