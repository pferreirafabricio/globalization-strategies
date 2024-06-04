using System.Globalization;
using GlobalizationApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

// [assembly: RootNamespace("GlobalizationApi.Services")]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLocalization((options) =>
{
    options.ResourcesPath = "Resources";
});
builder.Services.AddTransient<GreetingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRequestLocalization(options =>
{
    var supportedCultures = new[] { "en", "pt", "pt-BR", "pt-PT" };

    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);

    options.ApplyCurrentCultureToResponseHeaders = true;
    options.FallBackToParentUICultures = true;
});

app.MapGet("/greeting/hi", (GreetingService messageService) => new
{
    Message = messageService.GetHiMessage()
});

app.MapGet("/temperature", (GreetingService messageService, [FromQuery] float temperature) => new
{
    Message = messageService.GetTemperatureFormattedMessage(temperature)
});

app.MapGet("/greeting", (GreetingService messageService, [FromQuery] string name) => new
{
    Message = messageService.GetGreetingMessage(name)
});

app.Run();
