using GlobalizationApiJson.Localizer;
using GlobalizationApiJson.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLocalization();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
builder.Services.AddTransient<ErrorMessagesServices>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRequestLocalization(options =>
{
    var supportedCultures = new[] { "en", "pt", "pt-BR", "pt-PT" };

    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);

    options.ApplyCurrentCultureToResponseHeaders = true;
    options.FallBackToParentUICultures = true;
});

app.MapGet("/support/error-messages/404", (ErrorMessagesServices errorMessagesService) => new
{
    Message = errorMessagesService.GetError404()
});

app.MapGet("/support/error-messages/500", (ErrorMessagesServices errorMessagesService, [FromQuery] string? reason) => new
{
    Message = errorMessagesService.GetError500(reason)
});

app.Run();
