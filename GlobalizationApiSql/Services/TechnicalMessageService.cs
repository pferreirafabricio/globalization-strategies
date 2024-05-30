using GlobalizationApiSql.Database;
using GlobalizationApiSql.Domain;
using Microsoft.EntityFrameworkCore;

namespace GlobalizationApiSql.Services;

public class TechnicalMessageService(TechnicalMessagesDbContext dbContext, LanguageService languageService)
{
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
        var language = await languageService.GetLanguageFallbackAsync(languageCode);

        if (language is null)
            return null;

        var message = await dbContext.TechnicalMessages
            !.FirstOrDefaultAsync(tm => tm.LanguageId == language.Id && tm.Code == code);

        if (message is not null)
            return message;

        return await GetTechnicalMessageFallbackAsync(code, language.Code);
    }
}
