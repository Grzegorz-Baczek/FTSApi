using Azure.AI.OpenAI;
using Azure.Identity;
using FTS.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FTS.Infrastructure.OCR;

internal static class Extensions
{
    public static IServiceCollection AddOcr(this IServiceCollection services, IConfiguration configuration)
    {
        // Aspire wstrzykuje endpoint AI Foundry jako ConnectionStrings:vision
        var connectionString = configuration.GetConnectionString("vision");

        if (!string.IsNullOrEmpty(connectionString))
        {
            var endpoint = ParseEndpoint(connectionString);

            services.AddSingleton(_ =>
            {
                var client = new AzureOpenAIClient(endpoint, new DefaultAzureCredential());
                return client.GetChatClient("vision");
            });

            services.AddScoped<IOcrService, AiFoundryOcrService>();
        }

        return services;
    }

    private static Uri ParseEndpoint(string connectionString)
    {
        // Format "Endpoint=https://...;..." — parsujemy
        if (connectionString.Contains('=') && !connectionString.StartsWith("http", StringComparison.OrdinalIgnoreCase))
        {
            var parts = connectionString.Split(';', StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                var kv = part.Split('=', 2);
                if (kv.Length == 2 && kv[0].Trim().Equals("Endpoint", StringComparison.OrdinalIgnoreCase))
                {
                    return new Uri(kv[1].Trim());
                }
            }
        }

        // Czysty URL
        return new Uri(connectionString);
    }
}
