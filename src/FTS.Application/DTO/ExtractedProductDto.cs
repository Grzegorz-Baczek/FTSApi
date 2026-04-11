namespace FTS.Application.DTO;

public record ExtractedProductDto(
    string Name,
    decimal? Quantity,
    string? Unit,
    string? Category);
