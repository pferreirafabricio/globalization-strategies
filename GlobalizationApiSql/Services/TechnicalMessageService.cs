using GlobalizationApiSql.Constants;
using GlobalizationApiSql.Database;
using GlobalizationApiSql.Domain;
using Microsoft.EntityFrameworkCore;

namespace GlobalizationApiSql.Services;

public class TechnicalMessageService(TechnicalMessagesDbContext dbContext, LanguageService languageService)
{
    public IEnumerable<TechnicalMessage> GetTechnicalMessages()
        => [.. dbContext.TechnicalMessages!];

    public async Task<IEnumerable<TechnicalMessage>> GetTechnicalMessagesAsync()
        => await dbContext.TechnicalMessages!.ToListAsync();

    public async Task<TechnicalMessage?> GetTechnicalMessageAsync(int id)
        => await dbContext.TechnicalMessages!.FindAsync(id);

    public async Task<TechnicalMessage?> GetTechnicalMessageAsync(string code)
        => await dbContext.TechnicalMessages!.FirstOrDefaultAsync(tm => tm.Code == code);

    public async Task<IEnumerable<TechnicalMessage>> GetTechnicalMessagesByLanguageAsync(int languageId)
        => await dbContext.TechnicalMessages!.Where(tm => tm.LanguageId == languageId).ToListAsync();

    public async Task<bool> TechnicalMessageExistsInLanguageAsync(int languageId, string code)
        => await dbContext.TechnicalMessages!.AnyAsync(tm => tm.LanguageId == languageId && tm.Code == code);

    public async Task<TechnicalMessage?> GetTechnicalMessageFallbackAsync(string code, string languageCode)
    {
        var language = await languageService.GetLanguageAsync(languageCode);

        if (language is null)
            return null;

        var message = await dbContext.TechnicalMessages
            !.FirstOrDefaultAsync(tm => tm.LanguageId == language.Id && tm.Code == code);

        if (message is not null)
            return message;

        if (message is null && languageCode == "en")
            return null;

        var fallbackLanguageCode = (await languageService.GetLanguageFallbackAsync(languageCode))?.Code;

        if (fallbackLanguageCode is null)
            return null;

        return await GetTechnicalMessageFallbackAsync(code, fallbackLanguageCode);
    }

    public TechnicalMessage? GetTechnicalMessageFallback(string code, string languageCode)
    {
        var language = languageService.GetLanguage(languageCode);

        if (language is null)
            return null;

        var message = dbContext.TechnicalMessages
            !.FirstOrDefault(tm => tm.LanguageId == language.Id && tm.Code == code);

        if (message is not null)
            return message;

        if (message is null && languageCode == GlobalizationApiSqlConstants.DefaultCulture)
            return null;

        var fallbackLanguageCode = languageService.GetLanguageFallback(languageCode)?.Code;

        if (fallbackLanguageCode is null)
            return null;

        return GetTechnicalMessageFallback(code, fallbackLanguageCode);
    }
}
