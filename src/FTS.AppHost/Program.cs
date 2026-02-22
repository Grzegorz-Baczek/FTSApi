var builder = DistributedApplication.CreateBuilder(args);

// SQL Server — lokalnie kontener Docker, na Azure → Azure SQL Database
var sql = builder.AddAzureSqlServer("sql")
    .RunAsContainer()
    .AddDatabase("ftsdb");

// Blob Storage — lokalnie Azurite emulator, na Azure → Azure Storage Account
var storage = builder.AddAzureStorage("storage")
    .RunAsEmulator();
var blobs = storage.AddBlobs("blobs");

var api = builder.AddProject<Projects.FTS_Api>("fts-api")
    .WithReference(sql)
    .WithReference(blobs)
    .WaitFor(sql)
    .WaitFor(storage)
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.FTS_App>("fts-app")
    .WithReference(api)
    .WithExternalHttpEndpoints()
    .WaitFor(api);

builder.Build().Run();
