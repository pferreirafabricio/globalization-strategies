using GlobalizationApiSql.Database;
using GlobalizationApiSql.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlite<TechnicalMessagesDbContext>(builder.Configuration.GetConnectionString("DatabaseConnection"));
builder.Services.AddScoped<LanguageService>();
builder.Services.AddScoped<TechnicalMessageService>();
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

app.MapControllers();

app.Run();
