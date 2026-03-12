using Azure;
using Azure.AI.DocumentIntelligence;
using FTS.Application.Abstractions;

namespace FTS.Infrastructure.OCR;

internal sealed class DocumentIntelligenceOcrService : IOcrService
{
    private readonly DocumentIntelligenceClient _client;

    public DocumentIntelligenceOcrService(DocumentIntelligenceClient client)
    {
        _client = client;
    }

    public async Task<string> ExtractTextAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default)
    {
        var binaryData = await BinaryData.FromStreamAsync(fileStream, cancellationToken);

        var content = new AnalyzeDocumentContent
        {
            Base64Source = binaryData
        };

        var operation = await _client.AnalyzeDocumentAsync(
            WaitUntil.Completed,
            "prebuilt-read",
            content,
            cancellationToken: cancellationToken);

        var result = operation.Value;

        return result.Content ?? string.Empty;
    }
}
