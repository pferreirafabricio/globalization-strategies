using GlobalizationApiSql.Database;
using GlobalizationApiSql.Localizer;
using GlobalizationApiSql.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<TechnicalMessagesDbContext>(
    x => x.UseSqlite(builder.Configuration.GetConnectionString("DatabaseConnection"))
);
// builder.Services.AddSqlite<TechnicalMessagesDbContext>(builder.Configuration.GetConnectionString("DatabaseConnection"));
builder.Services.AddScoped<LanguageService>();
builder.Services.AddScoped<TechnicalMessageService>();
builder.Services.AddLocalization();
builder.Services.AddSingleton<IStringLocalizerFactory, SqlStringLocalizerFactory>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRequestLocalization(options =>
{
    var supportedCultures = new[] { "en", "pt", "pt-BR", "pt-PT", "pt-BR-SP" };

    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);

    options.ApplyCurrentCultureToResponseHeaders = true;
    options.FallBackToParentUICultures = true;
});

app.MapControllers();

app.Run();
