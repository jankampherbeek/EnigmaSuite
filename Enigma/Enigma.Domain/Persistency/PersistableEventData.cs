﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Persistency;

/// <summary>Representation of event data to be saved in Json format</summary>
/// <remarks>The format is flat to simplify the corresponding Json structure.</remarks>
public sealed class PersistableEventData
{
    /// <summary>Unique id for the event.</summary>
    public int Id { get; set; }
    /// <summary>Description of the event.</summary>
    public string Description { get; set; }
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



    public PersistableEventData(int id, string description, double julianDayEt, string dateText, string timeText, string locationName, double geoLong, double geoLat)
    {
        Id = id;
        Description = description;
        JulianDayEt = julianDayEt;
        DateText = dateText;
        TimeText = timeText;
        LocationName = locationName;
        GeoLong = geoLong;
        GeoLat = geoLat;
    }


}