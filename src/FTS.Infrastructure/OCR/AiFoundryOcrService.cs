using FTS.Application.Abstractions;
using OpenAI.Chat;

namespace FTS.Infrastructure.OCR;

internal sealed class AiFoundryOcrService : IOcrService
{
    private readonly ChatClient _chatClient;

    public AiFoundryOcrService(ChatClient chatClient)
    {
        _chatClient = chatClient;
    }

    public async Task<string> ExtractTextAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default)
    {
        using var ms = new MemoryStream();
        await fileStream.CopyToAsync(ms, cancellationToken);
        var fileBytes = ms.ToArray();
        var mimeType = GetMimeType(fileName);

        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(
                "Jesteś asystentem OCR. Twoim zadaniem jest odczytanie CAŁEGO tekstu widocznego na obrazku. " +
                "Zwróć tylko odczytany tekst, bez komentarzy, opisów ani formatowania markdown."),
            new UserChatMessage(
            [
                ChatMessageContentPart.CreateImagePart(BinaryData.FromBytes(fileBytes), mimeType),
                ChatMessageContentPart.CreateTextPart("Odczytaj cały tekst z tego obrazka:")
            ])
        };

        var options = new ChatCompletionOptions
        {
            Temperature = 0f
        };

        var response = await _chatClient.CompleteChatAsync(messages, options, cancellationToken);

        return response.Value.Content[0].Text ?? string.Empty;
    }

    private static string GetMimeType(string fileName)
    {
        var extension = Path.GetExtension(fileName)?.ToLowerInvariant();
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            ".bmp" => "image/bmp",
            _ => "application/octet-stream"
        };
    }
}
