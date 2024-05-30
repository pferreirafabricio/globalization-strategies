using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Localization;

namespace GlobalizationApiJson.Services;

public sealed class ErrorMessagesServices(IStringLocalizer<ErrorMessagesServices> localizer)
{
    [return: NotNullIfNotNull(nameof(localizer))]
    public string? GetError404()
    {
        LocalizedString localizedString = localizer["Error404"];

        return localizedString;
    }

    [return: NotNullIfNotNull(nameof(localizer))]
    public string? GetError500()
    {
        LocalizedString localizedString = localizer["Error500", localizer["DefaultError500Reason"]];

        return localizedString;
    }

    [return: NotNullIfNotNull(nameof(localizer))]
    public string? GetError500(string? reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            return GetError500();

        LocalizedString localizedString = localizer["Error500", reason];

        return localizedString;
    }
}
