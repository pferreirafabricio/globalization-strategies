using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Localization;

namespace GlobalizationApi.Services;

public sealed class GreetingService(IStringLocalizer<GreetingService> localizer)
{
    [return: NotNullIfNotNull(nameof(localizer))]
    public string? GetHiMessage()
    {
        LocalizedString localizedString = localizer["HiMessage"];

        return localizedString;
    }

    [return: NotNullIfNotNull(nameof(localizer))]
    public string? GetGreetingMessage()
    {
        LocalizedString localizedString = localizer["GreetingMessage"];

        return localizedString;
    }

    [return: NotNullIfNotNull(nameof(localizer))]
    public string? GetTemperatureFormattedMessage(DateTime dateTime, double dinnerPrice)
    {
        LocalizedString localizedString = localizer["TemperatureFormat", dateTime, dinnerPrice];

        return localizedString;
    }
}
