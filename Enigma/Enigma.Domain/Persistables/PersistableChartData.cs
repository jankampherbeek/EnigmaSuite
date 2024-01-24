// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Domain.Persistables;

/// <summary>Data for a chart that can be saved into, or read from the database.</summary>
/// <param name="Identification">The identification part of the data.</param>
/// <param name="DateTimeLocs">1 to n Date/Time/Location definitions for the chart.</param>
public record PersistableChartData(PersistableChartIdentification Identification, 
    List<PersistableChartDateTimeLocation> DateTimeLocs);

/// <summary>Persistable identification part of a chart.</summary>
public class PersistableChartIdentification
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long ChartCategoryId { get; set; }
};

/// <summary>Persistable date, time and location for a chart.</summary>
public class PersistableChartDateTimeLocation
{
    public long Id { get; set; }
    public long ChartId { get; set; }
    public string Source { get; set; }
    public string DateText { get; set; }
    public string TimeText { get; set; }
    public string LocationName { get; set; }
    public long RatingId { get; set; }
    public double GeoLong { get; set; }
    public double GeoLat { get; set; }
    public double JdForEt { get; set; }
}
 