using Azure.AI.DocumentIntelligence;
using Azure.Identity;
using FTS.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FTS.Infrastructure.OCR;

internal static class Extensions
{
    public static IServiceCollection AddOcr(this IServiceCollection services, IConfiguration configuration)
    {
        // Aspire wstrzykuje endpoint jako ConnectionStrings:document-intelligence
        var endpoint = configuration.GetConnectionString("document-intelligence");

        if (string.IsNullOrEmpty(endpoint))
        {
            // Fallback — appsettings
            endpoint = configuration["DocumentIntelligence:Endpoint"];
        }

        if (!string.IsNullOrEmpty(endpoint))
        {
            services.AddSingleton(_ => new DocumentIntelligenceClient(
                new Uri(endpoint),
                new DefaultAzureCredential()));

            services.AddScoped<IOcrService, DocumentIntelligenceOcrService>();
        }

        return services;
    }
}
