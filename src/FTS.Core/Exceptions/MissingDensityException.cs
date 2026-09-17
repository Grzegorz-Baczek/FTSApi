namespace FTS.Core.Exceptions;

public sealed class MissingDensityException(string ingredientName)
    : DomainException($"Ingredient '{ingredientName}' requires density to convert volume to grams.");