namespace FTS.Core.Exceptions;

public sealed class MissingPieceWeightException(string ingredientName)
    : DomainException($"Ingredient '{ingredientName}' requires grams per piece to convert pieces to grams.");