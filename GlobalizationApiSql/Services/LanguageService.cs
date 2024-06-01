using GlobalizationApiSql.Constants;
using GlobalizationApiSql.Database;
using GlobalizationApiSql.Domain;
using Microsoft.EntityFrameworkCore;

namespace GlobalizationApiSql.Services;

public class LanguageService(TechnicalMessagesDbContext dbContext)
{
    public async Task<IEnumerable<Language>> GetLanguagesAsync(CancellationToken cancellationToken = default)
        => await dbContext.Languages!.ToListAsync(cancellationToken);

    public async Task<Language?> GetLanguageAsync(int id, CancellationToken cancellationToken = default)
        => await dbContext.Languages!.FindAsync([id, cancellationToken], cancellationToken: cancellationToken);

    public async Task<Language?> GetLanguageAsync(string code, CancellationToken cancellationToken = default)
        => await dbContext.Languages!.FirstOrDefaultAsync(l => l.Code == code, cancellationToken);

    public Language? GetLanguage(string code)
        => dbContext.Languages!.FirstOrDefault(l => l.Code == code);

    public async Task<Language?> GetLanguageFallbackAsync(string code, CancellationToken cancellationToken = default)
    {
        var fallbackCode = MakeLanguageCodeFallback(code);

        var language = await GetLanguageAsync(fallbackCode, cancellationToken);

        if (language is not null)
            return language;

        return await GetLanguageFallbackAsync(fallbackCode, cancellationToken);
    }

    public Language? GetLanguageFallback(string code)
    {
        var fallbackCode = MakeLanguageCodeFallback(code);

        var language = GetLanguage(fallbackCode);

        if (language is not null)
            return language;

        return GetLanguageFallback(fallbackCode);
    }

    private static string MakeLanguageCodeFallback(string code)
    {
        var tokens = code.Split('-');

        if (tokens.Length > 2)
            return tokens[0] + "-" + tokens[1];

        if (tokens.Length > 1)
            return tokens[0];

        // If the language is only one token (ex: pt, es, etc.), return English as the fallback
        return GlobalizationApiSqlConstants.DefaultCulture;
    }
}
