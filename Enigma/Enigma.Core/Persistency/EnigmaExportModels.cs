// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2026.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Enigma.Core.Persistency;

/// <summary>
/// Top-level container for the Enigma JSON export format. The field names, id and date notation match
/// EnigmaWin and the Apple version of Enigma, so the exported files can be imported there.
/// Calculated positions are not exported, they are derived at calculation time.
/// </summary>
public sealed class EnigmaExportFile
{
    public const int CurrentFormatVersion = 1;

    public int FormatVersion { get; set; }
    public DateTimeOffset ExportedAt { get; set; }
    public List<EnigmaChartDto> Charts { get; set; } = [];
    public List<EnigmaEventDto> Events { get; set; } = [];

    public static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new UpperCaseGuidConverter(), new Iso8601SecondsConverter() }
    };
}

/// <summary>Date and time for a chart. JulianDate is for UT.</summary>
public sealed class EnigmaDateTimeDto
{
    public Guid Id { get; set; }
    public double JulianDate { get; set; }
    public string TimeZoneIdentifier { get; set; } = "UTC";
    public bool TimeIsUnknown { get; set; }
    public bool IsPreferred { get; set; }
    public string? Label { get; set; }
    public string? OriginalInput { get; set; }
}

public sealed class EnigmaChartDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public string? Notes { get; set; }
    public string? Source { get; set; }
    public string RoddenRating { get; set; } = "None";
    public string? PlaceName { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public List<EnigmaDateTimeDto> DateTimes { get; set; } = [];
}

/// <summary>Event, JulianDate is for UT.</summary>
public sealed class EnigmaEventDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string? EventDescription { get; set; }
    public double JulianDate { get; set; }
    public string TimeZoneIdentifier { get; set; } = "UTC";
    public string? OriginalInput { get; set; }
    public string? PlaceName { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    /// <summary>Ids of the charts this event is linked to.</summary>
    public List<Guid> ChartIds { get; set; } = [];
}

/// <summary>Writes ids in upper case, as the Apple version does; reading accepts any case.</summary>
internal sealed class UpperCaseGuidConverter : JsonConverter<Guid>
{
    public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Guid.Parse(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString().ToUpperInvariant());
}

/// <summary>
/// Writes timestamps as ISO 8601 in UTC without fractional seconds ("2026-09-30T10:15:00Z"), the only form
/// the Apple version can read; reading accepts any ISO 8601 form.
/// </summary>
internal sealed class Iso8601SecondsConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        DateTimeOffset.Parse(reader.GetString()!, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.UtcDateTime.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture));
}
