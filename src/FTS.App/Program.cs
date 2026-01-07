using FTS.App.Components;
using FTS.App.Components.Pages.Categories;
using FTS.App.Components.Pages.Products;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMudServices()
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<CategoryApiClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5298");
});

builder.Services.AddHttpClient<ProductApiClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5298");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
