using FTS.Core.Entities;
using FTS.Core.Exceptions;

namespace FTS.UnitTests.Domain;

public class RecipeCreateTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Non_positive_servings_throw_domain_exception(int servings)
    {
        // Arrange
        var action = () => Recipe.Create(title: "Test recipe", steps: "Mix ingredients.",
            isPublic: true, imageUrl: null, authorId: Guid.NewGuid(), servings: servings);

        // Assert: the domain rejects a non-positive serving count.
        Assert.Throws<DomainException>(action);
    }
}
