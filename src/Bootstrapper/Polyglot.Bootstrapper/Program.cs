using Azure.Identity;
using Polyglot.Bootstrapper.DI;
using Projects.Api.Controllers;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.RegisterModules();

var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");
var jsonFile = $"appsettings.{environment}.json";

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false,reloadOnChange: true)
    .AddJsonFile(jsonFile, optional: true);

var keyVaultUrl = builder.Configuration["KeyVault"];

var credentials = new DefaultAzureCredential ();
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), credentials);

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", corsBuilder =>
    {
        corsBuilder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
    options.AddPolicy("ProdCors", corsBuilder =>
    {
        corsBuilder.WithOrigins("")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseCors("DevCors");
    app.MapScalarApiReference();
}
else
{
    app.UseCors("ProdCors");
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseModules();
app.MapControllers();

app.Run();

public partial class Program
{
}