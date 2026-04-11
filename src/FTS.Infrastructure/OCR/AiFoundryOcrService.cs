using System.Text.Json;
using FTS.Application.Abstractions;
using FTS.Application.DTO;
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

    public async Task<IReadOnlyList<ExtractedProductDto>> ExtractProductsAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default)
    {
        using var ms = new MemoryStream();
        await fileStream.CopyToAsync(ms, cancellationToken);
        var fileBytes = ms.ToArray();
        var mimeType = GetMimeType(fileName);

        var messages = new List<ChatMessage>
        {
            new SystemChatMessage(
                """
                Jesteś asystentem do rozpoznawania produktów spożywczych ze zdjęć paragonów, list zakupów i notatek.
                Twoim zadaniem jest zidentyfikowanie wszystkich produktów widocznych na obrazku i zwrócenie ich jako tablica JSON.
                Każdy produkt powinien mieć pola:
                - "name" (string, wymagane) — nazwa produktu
                - "quantity" (number lub null) — ilość
                - "unit" (string lub null) — jednostka (np. "kg", "szt", "l", "g", "opak")
                - "category" (string lub null) — kategoria (np. "Nabiał", "Warzywa", "Owoce", "Mięso", "Pieczywo", "Napoje", "Chemia", "Inne")

                Zwróć TYLKO tablicę JSON, bez dodatkowego tekstu, bez markdown, bez komentarzy.
                Przykład: [{"name":"Mleko","quantity":2,"unit":"l","category":"Nabiał"}]
                """),
            new UserChatMessage(
            [
                ChatMessageContentPart.CreateImagePart(BinaryData.FromBytes(fileBytes), mimeType),
                ChatMessageContentPart.CreateTextPart("Rozpoznaj produkty z tego obrazka i zwróć jako JSON:")
            ])
        };

        var options = new ChatCompletionOptions
        {
            Temperature = 0f
        };

        var response = await _chatClient.CompleteChatAsync(messages, options, cancellationToken);

        var json = response.Value.Content[0].Text ?? "[]";

        // Strip potential markdown code fences
        json = json.Trim();
        if (json.StartsWith("```"))
        {
            var firstNewline = json.IndexOf('\n');
            if (firstNewline >= 0)
                json = json[(firstNewline + 1)..];
            if (json.EndsWith("```"))
                json = json[..^3];
            json = json.Trim();
        }

        var products = JsonSerializer.Deserialize<List<ExtractedProductJson>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? [];

        return products
            .Select(p => new ExtractedProductDto(p.Name ?? "Nieznany produkt", p.Quantity, p.Unit, p.Category))
            .ToList();
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

    private sealed class ExtractedProductJson
    {
        public string? Name { get; set; }
        public decimal? Quantity { get; set; }
        public string? Unit { get; set; }
        public string? Category { get; set; }
    }
}
