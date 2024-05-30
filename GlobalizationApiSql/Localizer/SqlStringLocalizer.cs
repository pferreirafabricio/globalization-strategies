using System.Globalization;
using GlobalizationApiSql.Services;
using Microsoft.Extensions.Localization;

namespace GlobalizationApiSql.Localizer;

public class SqlStringLocalizer(TechnicalMessageService technicalMessageService) : IStringLocalizer
{
    public LocalizedString this[string name]
    {
        get
        {
            var value = GetString(name);
            return new LocalizedString(name, value ?? name, value == null);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var actualValue = this[name];
            return !actualValue.ResourceNotFound
                ? new LocalizedString(name, string.Format(actualValue.Value, arguments), false)
                : actualValue;
        }
    }
    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        => technicalMessageService.GetTechnicalMessages()
            .Select(x => new LocalizedString(x.Code, x.Message));

    private string? GetString(string key)
        => technicalMessageService.GetTechnicalMessageFallback(key, CultureInfo.CurrentCulture.Name)?.Message;
}
