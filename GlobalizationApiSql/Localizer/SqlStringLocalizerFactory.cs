using GlobalizationApiSql.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace GlobalizationApiSql.Localizer;

/// <summary>
/// https://stackoverflow.com/a/76317452/12542704
/// </summary>
/// <typeparam name="TechnicalMessagesDbContext"></typeparam>
public class SqlStringLocalizerFactory(IDbContextFactory<TechnicalMessagesDbContext> contextFactory) : IStringLocalizerFactory
{
    private TechnicalMessagesDbContext DbContext => contextFactory.CreateDbContext();

    public IStringLocalizer Create(Type resourceSource) =>
        new SqlStringLocalizer(new(DbContext, new(DbContext)));

    public IStringLocalizer Create(string baseName, string location) =>
        new SqlStringLocalizer(new(DbContext, new(DbContext)));

}
