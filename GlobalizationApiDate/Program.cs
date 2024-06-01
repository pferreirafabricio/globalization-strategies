using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

if (args is [var cultureName])
{
    CultureInfo.CurrentCulture =
        CultureInfo.CurrentUICulture =
            CultureInfo.GetCultureInfo(cultureName);
}

Console.WriteLine("Current Culture: " + CultureInfo.CurrentCulture.Name);
Console.WriteLine("Current time zone: " + TimeZoneInfo.Local.DisplayName);
Console.WriteLine("Current offset: " + TimeZoneInfo.Local.BaseUtcOffset);

var localTime = DateTime.Now;
var utcTime = DateTime.UtcNow;

DrawSeparatorText("DateTime");

Console.WriteLine("Local Time: " + localTime + " (Kind: " + localTime.Kind + ")");
Console.WriteLine("UTC Time: " + utcTime + " (Kind: " + utcTime.Kind + ")");

DrawSeparatorText("DateTimeOffset");

var localTimeOffset = DateTimeOffset.Now;
var utcTimeOffset = DateTimeOffset.UtcNow;

Console.WriteLine("Local Time with Offset: " + localTimeOffset);
Console.WriteLine("UTC Time with Offset: " + utcTimeOffset);
Console.WriteLine("Offset: " + localTimeOffset.Offset);

DrawSeparatorText("Ambiguity Handling");

var dateTime = new DateTime(2024, 6, 1, 10, 0, 0, DateTimeKind.Local);
var dateTimeOffset = new DateTimeOffset(2024, 6, 1, 10, 0, 0, TimeZoneInfo.Local.BaseUtcOffset);

Console.WriteLine("DateTime: " + dateTime);
Console.WriteLine("DateTime (UTC): " + dateTime.ToUniversalTime());
Console.WriteLine("DateTimeOffset: " + dateTimeOffset);
Console.WriteLine("DateTimeOffset (UTC): " + dateTimeOffset.UtcDateTime);

DrawSeparatorText("JSON Conversion");

var json = "{ \"date\": \"2024-06-01T10:00:00\" }";

var jsonDateTime = JsonSerializer.Deserialize<JsonWithDateTime>(json)!;

Console.WriteLine("JSON DateTime: " + jsonDateTime.Date);
Console.WriteLine("JSON DateTime (Kind): " + jsonDateTime.Date.Kind);
Console.WriteLine("JSON DateTime (UTC): " + jsonDateTime.Date.ToUniversalTime());
Console.WriteLine("JSON DateTime (Local): " + jsonDateTime.Date.ToLocalTime());

static void DrawSeparatorText(string text)
    => Console.WriteLine($"\n{new string('-', 10)} {text} {new string('-', 10)}\n");

record JsonWithDateTime(
    [property: JsonPropertyName("date")]
    DateTime Date
);