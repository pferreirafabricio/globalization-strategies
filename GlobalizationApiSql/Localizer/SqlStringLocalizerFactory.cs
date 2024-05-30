using GlobalizationApiSql.Database;
using GlobalizationApiSql.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace GlobalizationApiSql.Localizer;

/// <summary>
/// https://stackoverflow.com/a/76317452/12542704
/// </summary>
/// <typeparam name="TechnicalMessagesDbContext"></typeparam>
public class SqlStringLocalizerFactory(IDbContextFactory<TechnicalMessagesDbContext> contextFactory) : IStringLocalizerFactory
{
    public IStringLocalizer Create(Type resourceSource) =>
        new SqlStringLocalizer(new(CreateDbContext(), new(CreateDbContext())));

    public IStringLocalizer Create(string baseName, string location) =>
        new SqlStringLocalizer(new(CreateDbContext(), new(CreateDbContext())));

    private TechnicalMessagesDbContext CreateDbContext() =>
        contextFactory.CreateDbContext();
}
