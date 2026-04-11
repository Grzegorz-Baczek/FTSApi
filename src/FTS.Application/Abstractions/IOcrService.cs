using FTS.Application.DTO;

namespace FTS.Application.Abstractions;

public interface IOcrService
{
    Task<string> ExtractTextAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ExtractedProductDto>> ExtractProductsAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default);
}
