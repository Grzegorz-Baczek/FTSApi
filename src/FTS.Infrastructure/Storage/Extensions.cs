using Azure.Storage.Blobs;
using FTS.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FTS.Infrastructure.Storage;

internal static class Extensions
{
    public static IServiceCollection AddBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("blobs");

        services.AddSingleton(_ => new BlobServiceClient(connectionString));
        services.AddScoped<IFileStorage, BlobFileStorage>();

        return services;
    }
}
