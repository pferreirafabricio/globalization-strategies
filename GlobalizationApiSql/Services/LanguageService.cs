using GlobalizationApiSql.Constants;
using GlobalizationApiSql.Database;
using GlobalizationApiSql.Domain;
using Microsoft.EntityFrameworkCore;

namespace GlobalizationApiSql.Services;

public class LanguageService(TechnicalMessagesDbContext dbContext)
{
    public async Task<IEnumerable<Language>> GetLanguagesAsync()
        => await dbContext.Languages!.ToListAsync();

    public async Task<Language?> GetLanguageAsync(int id)
        => await dbContext.Languages!.FindAsync(id);

    public async Task<Language?> GetLanguageAsync(string code)
        => await dbContext.Languages!.FirstOrDefaultAsync(l => l.Code == code);

    public Language? GetLanguage(string code)
        => dbContext.Languages!.FirstOrDefault(l => l.Code == code);

    public async Task<Language?> GetLanguageFallbackAsync(string code)
    {
        var fallbackCode = MakeLanguageCodeFallback(code);

        var language = await GetLanguageAsync(fallbackCode);

        if (language is not null)
            return language;

        return await GetLanguageFallbackAsync(fallbackCode);
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
