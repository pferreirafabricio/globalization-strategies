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
    public string? GetGreetingMessage(string name)
    {
        LocalizedString localizedString = localizer[GetGreetingFromTime(), name];

        return localizedString;
    }

    [return: NotNullIfNotNull(nameof(localizer))]
    public string? GetTemperatureFormattedMessage(float temperature)
    {
        LocalizedString localizedString = localizer["TemperatureFormat", temperature];

        return localizedString;
    }

    private static string GetGreetingFromTime()
        => DateTime.Now.Hour switch
        {
            < 12 => "GoodMorning",
            < 18 => "GoodAfternoon",
            _ => "GoodEvening"
        };
}
