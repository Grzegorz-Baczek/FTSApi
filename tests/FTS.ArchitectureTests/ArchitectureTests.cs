using FTS.Application;
using FTS.Core.Entities;
using FTS.Core.Exceptions;
using MediatR;
using NetArchTest.Rules;

namespace FTS.ArchitectureTests;

public class ArchitectureTests
{
    [Fact]
    public void Core_does_not_depend_on_entity_framework_core()
    {
        // Arrange
        var types = Types.InAssembly(typeof(Ingredient).Assembly);

        // Act
        var result = types
            .ShouldNot()
            .HaveDependencyOn("Microsoft.EntityFrameworkCore")
            .GetResult();

        // Assert: fail with the violating types when the dependency rule is broken.
        Assert.True(result.IsSuccessful,
            $"FTS.Core must not depend on Microsoft.EntityFrameworkCore. Violating types: {Format(result.FailingTypes)}");
    }

    [Fact]
    public void Application_does_not_depend_on_infrastructure()
    {
        // Arrange
        var types = Types.InAssembly(typeof(Extensions).Assembly);

        // Act
        var result = types
            .ShouldNot()
            .HaveDependencyOn("FTS.Infrastructure")
            .GetResult();

        // Assert: fail with the violating types when the dependency rule is broken.
        Assert.True(result.IsSuccessful,
            $"FTS.Application must not depend on FTS.Infrastructure. Violating types: {Format(result.FailingTypes)}");
    }

    [Fact]
    public void Request_handlers_are_internal_and_sealed()
    {
        // Arrange
        var applicationTypes = typeof(Extensions).Assembly.GetTypes();

        // Act
        var handlerTypes = applicationTypes
            .Where(type => type.GetInterfaces().Any(@interface =>
                @interface == typeof(IRequestHandler<>) ||
                (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IRequestHandler<>)) ||
                (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))))
            .ToArray();

        var violatingTypes = handlerTypes
            .Where(type => type.IsPublic || !type.IsSealed)
            .Select(type => type.FullName ?? type.Name)
            .ToArray();

        // Assert: every request handler follows the internal sealed convention.
        Assert.True(violatingTypes.Length == 0,
            $"Every IRequestHandler must be internal sealed. Violating types: {string.Join(", ", violatingTypes)}");
    }

    [Fact]
    public void Domain_exceptions_derive_from_custom_exception()
    {
        // Arrange
        var coreTypes = typeof(CustomException).Assembly.GetTypes();

        // Act
        var exceptionTypes = coreTypes
            .Where(type =>
                type.Namespace == typeof(CustomException).Namespace &&
                type.Name.EndsWith("Exception", StringComparison.Ordinal) &&
                type != typeof(CustomException))
            .ToArray();

        var violatingTypes = exceptionTypes
            .Where(type => !typeof(CustomException).IsAssignableFrom(type))
            .Select(type => type.FullName ?? type.Name)
            .ToArray();

        // Assert: every domain exception follows the CustomException inheritance rule.
        Assert.True(violatingTypes.Length == 0,
            $"Every *Exception in FTS.Core.Exceptions must derive from CustomException. Violating types: {string.Join(", ", violatingTypes)}");
    }

    private static string Format(IEnumerable<Type>? types)
    {
        return types is null ? "none" : string.Join(", ", types.Select(type => type.FullName ?? type.Name));
    }
}
