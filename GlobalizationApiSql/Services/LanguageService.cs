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

    public async Task<Language?> GetLanguageFallbackAsync(string code)
    {
        var language = await GetLanguageAsync(code);

        if (language is not null)
            return language;

        var fallbackCode = GetLanguageCodeFallback(code);

        return await GetLanguageFallbackAsync(fallbackCode);
    }

    private static string GetLanguageCodeFallback(string code)
    {
        var tokens = code.Split('-');

        if (tokens.Length > 2)
            return tokens[0] + "-" + tokens[1];

        if (tokens.Length > 1)
            return tokens[0];

        // If the language is only one token (ex: pt, es, etc.), return English as the fallback
        return "en";
    }
}
