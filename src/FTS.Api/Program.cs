using FTS.Application;
using FTS.Infrastructure;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddRequestTimeouts(options =>
{
    options.DefaultPolicy = new Microsoft.AspNetCore.Http.Timeouts.RequestTimeoutPolicy
    {
        Timeout = TimeSpan.FromMinutes(5)
    };
});

builder.Host.UseSerilog((context, LoggerConfiguration) =>
{
    LoggerConfiguration.WriteTo
    .Console();
});

var app = builder.Build();
app.MapDefaultEndpoints();
app.UseRequestTimeouts();
app.UseInfrastructure();
app.MapGet("api", (IOptions<AppOptions> options) => Results.Ok(options.Value.Name));
app.Run();
