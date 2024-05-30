using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace GlobalizationApiJson.Localizer;

public class JsonStringLocalizer(IDistributedCache cache) : IStringLocalizer
{
    private readonly IDistributedCache _cache = cache;

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
    {
        var filePath = $"Resources/{CultureInfo.CurrentCulture.Name}.json";
        using var str = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var sReader = new StreamReader(str);
        using var jsonDoc = JsonDocument.Parse(sReader.ReadToEnd());

        foreach (var element in jsonDoc.RootElement.EnumerateObject())
        {
            yield return new LocalizedString(element.Name, element.Value.GetString() ?? "", false);
        }
    }

    private string? GetString(string key)
    {
        string? relativeFilePath = $"Resources/{CultureInfo.CurrentCulture.Name}.json";
        var fullFilePath = Path.GetFullPath(relativeFilePath);
        if (File.Exists(fullFilePath))
        {
            var cacheKey = $"locale_{CultureInfo.CurrentCulture.Name}_{key}";
            var cacheValue = _cache.GetString(cacheKey);
            if (!string.IsNullOrEmpty(cacheValue))
            {
                return cacheValue;
            }

            var result = GetValueFromJSON(key, Path.GetFullPath(relativeFilePath));

            if (!string.IsNullOrEmpty(result))
            {
                _cache.SetString(cacheKey, result);
            }
            return result;
        }
        return default;
    }

    private string? GetValueFromJSON(string propertyName, string filePath)
    {
        if (propertyName == null || filePath == null)
        {
            return default;
        }
        using var str = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var sReader = new StreamReader(str);
        using var jsonDoc = JsonDocument.Parse(sReader.ReadToEnd());
        if (jsonDoc.RootElement.TryGetProperty(propertyName, out JsonElement value))
        {
            return value.GetString();
        }
        return default;
    }
}
