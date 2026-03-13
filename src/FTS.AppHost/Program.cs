var builder = DistributedApplication.CreateBuilder(args);

// SQL Server — lokalnie kontener Docker, na Azure → Azure SQL Database
var sql = builder.AddAzureSqlServer("sql")
    .RunAsContainer()
    .AddDatabase("ftsdb");

// Blob Storage — lokalnie Azurite emulator, na Azure → Azure Storage Account
var storage = builder.AddAzureStorage("storage")
    .RunAsEmulator();
var blobs = storage.AddBlobs("blobs");

// TODO: Azure AI Document Intelligence (OCR) — do przywrócenia później
// var docIntelligence = builder.AddConnectionString("document-intelligence");

// Azure AI Foundry — GPT-4.1 (multimodalny, vision, szybki, wyższy rate limit)
var foundry = builder.AddAzureAIFoundry("ai-foundry");
var vision = foundry.AddDeployment("vision", "gpt-4.1", "2025-04-14", "OpenAI");

var api = builder.AddProject<Projects.FTS_Api>("fts-api")
    .WithReference(sql)
    .WithReference(blobs)
    .WithReference(vision)
    .WaitFor(sql)
    .WaitFor(storage)
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.FTS_App>("fts-app")
    .WithReference(api)
    .WithExternalHttpEndpoints()
    .WaitFor(api);

builder.Build().Run();
