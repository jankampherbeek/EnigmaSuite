// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Charts;

namespace Enigma.Domain.Persistency;

/// <summary>Representation of chartdata to be saved in Json format</summary>
/// <remarks>The format is flat to simplify the corresponding Json structure.</remarks>
public sealed class PersistableChartData
{
    /// <summary>Unique id for the chart.</summary>
    public int Id { get; set; }
    /// <summary>Name or other identification for the chart.</summary>
    /// <remarks>Should be unique within then database.</remarks>
    public string Name { get; set; }
    /// <summary>Optional description of the chart.</summary>
    public string Description { get; set; }
    /// <summary>Optional descrip[tion of the source of the chart data.</summary>
    public string Source { get; set; }
    /// <summary>Optional cartegory of the chart.</summary>
    /// <remarks>If not used defaults to ChartCategories.Unknown.</remarks>
    public ChartCategories ChartCategory { get; set; }
    /// <summary>Optional rating of the quality of the source according to Louise Rodden.</summary>
    /// <remarks>If not used defaults to RoddenRatings.Unknown.</remarks>
    public RoddenRatings Rating { get; set; }
    /// <summary>Julian day number for ephemeris time.</summary>
    public double JulianDayEt { get; set; }
    /// <summary>Data as text.</summary>
    /// <remarks>Only for presentational purposes.</remarks>
    public string DateText { get; set; }
    /// <summary>Time as text.</summary>
    /// <remarks>Only for presentational purposes.</remarks>
    public string TimeText { get; set; }
    /// <summary>Locationname.</summary>
    /// <remarks>Only for presentational purposes.</remarks>
    public string LocationName { get; set; }
    /// <summary>Geographic longitude (east = +, west = -.</summary>
    public double GeoLong { get; set; }
    /// <summary>Geographic latitude (north = +, south = -).</summary>
    public double GeoLat { get; set; }



    public PersistableChartData(int id, string name, string description, string source, ChartCategories chartCategory, RoddenRatings rating, double julianDayEt, string dateText, string timeText, string locationName, double geoLong, double geoLat)
    {
        Id = id;
        Name = name;
        Description = description;
        Source = source;
        ChartCategory = chartCategory;
        Rating = rating;
        JulianDayEt = julianDayEt;
        DateText = dateText;
        TimeText = timeText;
        LocationName = locationName;
        GeoLong = geoLong;
        GeoLat = geoLat;
    }


}