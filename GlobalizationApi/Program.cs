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

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
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
    var supportedCultures = new[] { "pt", "pt-BR", "pt-PT", "pt-BR-SP" };

    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);

    options.ApplyCurrentCultureToResponseHeaders = true;
});

app.MapGet("/greeting/hi", () =>
{
    var messageService = app.Services.GetRequiredService<GreetingService>();

    return new
    {
        Message = messageService.GetHiMessage()
    };
});

app.Run();
