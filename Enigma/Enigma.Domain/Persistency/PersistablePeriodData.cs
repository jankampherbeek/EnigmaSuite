// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using LiteDB;

namespace Enigma.Domain.Persistency;

/// <summary>Representation of periopd data to be saved in Json format.</summary>
/// <remarks>The format is flat to simplify the corresponding Json structure.</remarks>
public sealed class PersistablePeriodData
{
    /// <summary>Unique id for the period, primary key, generated by LiteDB.</summary>
    public int Id { get; }
    /// <summary>Description of the event.</summary>
    public string Description { get; }
    
    /// <summary>Julian day number for ephemeris time startdate.</summary>
    public double StartJulianDayEt { get; }
    
    /// <summary>Julian day number for ephemeris time enddate.</summary>
    public double EndJulianDayEt { get; }
    
    /// <summary>Start date as text.</summary>
    /// <remarks>Only for presentational purposes.</remarks>
    public string StartDateText { get; }
    
    /// <summary>End date as text.</summary>
    /// <remarks>Only for presentational purposes.</remarks>
    public string EndDateText { get; }
    
    [BsonCtor]
    public PersistablePeriodData(string description, double startJd, double endJd, string startDateText, 
        string endDateText, int id = 0)
    {
        StartJulianDayEt = startJd;
        EndJulianDayEt = endJd;
        Description = description;
        StartDateText = startDateText;
        EndDateText = endDateText;
        Id = id;
    }

    
}