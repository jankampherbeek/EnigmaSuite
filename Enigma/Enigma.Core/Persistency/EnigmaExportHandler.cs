// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2026.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Enigma.Core.Calc;
using Enigma.Domain.Persistables;
using Serilog;

namespace Enigma.Core.Persistency;

/// <summary>Result of an export to the Enigma JSON format.</summary>
/// <param name="Success">True if the file was written.</param>
/// <param name="ChartsExported">Number of exported charts.</param>
/// <param name="EventsExported">Number of exported events.</param>
public record EnigmaExportResult(bool Success, int ChartsExported, int EventsExported);

/// <summary>Exports all charts and events to the Enigma JSON format, as used by EnigmaWin and the Apple version.</summary>
public interface IEnigmaExportHandler
{
    /// <summary>Export all charts and their events to a JSON file.</summary>
    /// <param name="jsonFileLocation">Full path of the file to write.</param>
    /// <returns>Success indication and the number of exported charts and events.</returns>
    public EnigmaExportResult ExportToJson(string jsonFileLocation);
}

/// <inheritdoc/>
/// <remarks>
/// The JSON format uses Guids and a Julian Day for UT, Enigma uses long ids and a Julian Day for ET.
/// Guids are derived from the ids, so repeated exports of the same chart or event produce the same Guid and
/// importing applications can recognize them as already present. The time zone of the input is only available as
/// text, so the original date and time texts are written to OriginalInput and the time zone identifier is UTC.
/// Events are only exported if they are linked to at least one chart.
/// </remarks>
public sealed class EnigmaExportHandler(
    IChartDataDao chartDataDao,
    IEventDataDao eventDataDao,
    IReferencesDao referencesDao,
    IJulDayCalc julDayCalc) : IEnigmaExportHandler
{
    private static readonly HashSet<string> KnownRatings = ["AA", "A", "B", "C", "DD", "X", "XX"];

    /// <inheritdoc/>
    public EnigmaExportResult ExportToJson(string jsonFileLocation)
    {
        try
        {
            var file = CreateExportFile();
            File.WriteAllBytes(jsonFileLocation, JsonSerializer.SerializeToUtf8Bytes(file, EnigmaExportFile.JsonOptions));
            Log.Information("EnigmaExportHandler: exported {Charts} charts and {Events} events to {File}",
                file.Charts.Count, file.Events.Count, jsonFileLocation);
            return new EnigmaExportResult(true, file.Charts.Count, file.Events.Count);
        }
        catch (Exception e)
        {
            Log.Error("EnigmaExportHandler.ExportToJson: export to {File} failed. Msg: {Msg}", jsonFileLocation, e.Message);
            return new EnigmaExportResult(false, 0, 0);
        }
    }

    private EnigmaExportFile CreateExportFile()
    {
        var categories = referencesDao.ReadAllChartCategories();
        var ratings = referencesDao.ReadAllRatings();
        var charts = new List<EnigmaChartDto>();
        var events = new Dictionary<long, EnigmaEventDto>();

        foreach (var chartIdent in chartDataDao.ReadAllChartIdentifications() ?? [])
        {
            var chartData = chartDataDao.ReadChartData(chartIdent.Id);
            if (chartData is null || chartData.DateTimeLocs.Count == 0) continue;
            charts.Add(ToChartDto(chartData, categories, ratings));
            var chartGuid = ToGuid("chart", chartIdent.Id);
            foreach (var eventData in eventDataDao.SearchEventData(chartIdent.Id))
            {
                if (!events.TryGetValue(eventData.Id, out var eventDto))
                {
                    eventDto = ToEventDto(eventData);
                    events.Add(eventData.Id, eventDto);
                }
                eventDto.ChartIds.Add(chartGuid);
            }
        }

        return new EnigmaExportFile
        {
            FormatVersion = EnigmaExportFile.CurrentFormatVersion,
            ExportedAt = DateTimeOffset.UtcNow,
            Charts = charts,
            Events = events.Values.ToList()
        };
    }

    private EnigmaChartDto ToChartDto(PersistableChartData chartData, Dictionary<long, string> categories,
        Dictionary<long, string> ratings)
    {
        var ident = chartData.Identification;
        // Location, source and rating are defined per date/time in Enigma; the first one is used for the chart.
        var firstDtLoc = chartData.DateTimeLocs[0];
        var rating = ToRoddenRating(ratings.GetValueOrDefault(firstDtLoc.RatingId, ""));
        return new EnigmaChartDto
        {
            Id = ToGuid("chart", ident.Id),
            Name = ident.Name,
            Category = categories.GetValueOrDefault(ident.ChartCategoryId, ""),
            Notes = NullIfEmpty(ident.Description),
            Source = NullIfEmpty(firstDtLoc.Source),
            RoddenRating = rating,
            PlaceName = NullIfEmpty(firstDtLoc.LocationName),
            Latitude = firstDtLoc.GeoLat,
            Longitude = firstDtLoc.GeoLong,
            DateTimes = chartData.DateTimeLocs.Select((dtLoc, i) => new EnigmaDateTimeDto
            {
                Id = ToGuid("datetime", dtLoc.Id),
                JulianDate = JdEtToUt(dtLoc.JdForEt),
                TimeIsUnknown = rating == "X",
                IsPreferred = i == 0,
                OriginalInput = $"{dtLoc.DateText} {dtLoc.TimeText}".Trim()
            }).ToList()
        };
    }

    private EnigmaEventDto ToEventDto(PersistableEventData eventData) => new()
    {
        Id = ToGuid("event", eventData.Id),
        Title = eventData.Description,
        JulianDate = JdEtToUt(eventData.JdForEt),
        OriginalInput = $"{eventData.DateText} {eventData.TimeText}".Trim(),
        PlaceName = NullIfEmpty(eventData.LocationName),
        Latitude = eventData.GeoLat,
        Longitude = eventData.GeoLong
    };

    /// <summary>Converts JD for ET to JD for UT. Delta T is defined for UT, so the result is refined once.</summary>
    private double JdEtToUt(double jdEt)
    {
        var jdUt = jdEt - julDayCalc.CalcDeltaT(jdEt);
        return jdEt - julDayCalc.CalcDeltaT(jdUt);
    }

    /// <summary>Maps a rating name like "AA - Accurate" to the abbreviation; "Unknown" and unrecognized names become None.</summary>
    private static string ToRoddenRating(string ratingName)
    {
        var abbreviation = ratingName.Split(" - ")[0].Trim();
        return KnownRatings.Contains(abbreviation) ? abbreviation : "None";
    }

    /// <summary>Creates a deterministic Guid for an id of a specific kind of record.</summary>
    private static Guid ToGuid(string kind, long id) =>
        new(MD5.HashData(Encoding.UTF8.GetBytes($"Enigma/{kind}/{id}")));

    private static string? NullIfEmpty(string? text) => string.IsNullOrWhiteSpace(text) ? null : text;
}
