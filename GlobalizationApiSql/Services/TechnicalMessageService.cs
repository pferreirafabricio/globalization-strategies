using GlobalizationApiSql.Constants;
using GlobalizationApiSql.Database;
using GlobalizationApiSql.Domain;
using Microsoft.EntityFrameworkCore;

namespace GlobalizationApiSql.Services;

public class TechnicalMessageService(TechnicalMessagesDbContext dbContext, LanguageService languageService)
{
    public IEnumerable<TechnicalMessage> GetTechnicalMessages()
        => [.. dbContext.TechnicalMessages!];

    public async Task<IEnumerable<TechnicalMessage>> GetTechnicalMessagesAsync(CancellationToken cancellationToken = default)
        => await dbContext.TechnicalMessages!.ToListAsync(cancellationToken);

    public async Task<TechnicalMessage?> GetTechnicalMessageAsync(int id, CancellationToken cancellationToken = default)
        => await dbContext.TechnicalMessages!.FindAsync([id, cancellationToken], cancellationToken: cancellationToken);

    public async Task<TechnicalMessage?> GetTechnicalMessageAsync(string code, CancellationToken cancellationToken = default)
        => await dbContext.TechnicalMessages!.FirstOrDefaultAsync(tm => tm.Code == code, cancellationToken);

    public async Task<IEnumerable<TechnicalMessage>> GetTechnicalMessagesByLanguageAsync(int languageId, CancellationToken cancellationToken = default)
        => await dbContext.TechnicalMessages!.Where(tm => tm.LanguageId == languageId).ToListAsync(cancellationToken);

    public async Task<bool> TechnicalMessageExistsInLanguageAsync(int languageId, string code, CancellationToken cancellationToken = default)
        => await dbContext.TechnicalMessages!.AnyAsync(tm => tm.LanguageId == languageId && tm.Code == code, cancellationToken);

    public async Task<TechnicalMessage?> GetTechnicalMessageFallbackAsync(string code, string languageCode, CancellationToken cancellationToken = default)
    {
        var language = await languageService.GetLanguageAsync(languageCode, cancellationToken);

        if (language is null)
            return null;

        var message = await dbContext.TechnicalMessages
            !.FirstOrDefaultAsync(tm => tm.LanguageId == language.Id && tm.Code == code, cancellationToken);

        if (message is not null)
            return message;

        if (message is null && languageCode == "en")
            return null;

        var fallbackLanguageCode = (await languageService.GetLanguageFallbackAsync(languageCode, cancellationToken))?.Code;

        if (fallbackLanguageCode is null)
            return null;

        return await GetTechnicalMessageFallbackAsync(code, fallbackLanguageCode, cancellationToken);
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
