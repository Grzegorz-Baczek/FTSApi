namespace FTS.Application.Abstractions;

public interface IOcrService
{
    Task<string> ExtractTextAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default);
}
