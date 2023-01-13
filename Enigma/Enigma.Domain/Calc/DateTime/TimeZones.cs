// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Calc.DateTime;

public enum TimeZones
{
    UT = 0, CET = 1, EET = 2, EAT = 3, IRST = 4, AMT = 5, AFT = 6, PKT = 7, IST = 8, IOT = 9, MMT = 10, ICT = 11, WST = 12, JST = 13, ACST = 14, AEST = 15, LHST = 16,
    NCT = 17, NZST = 18, SST = 19, HAST = 20, MART = 21, AKST = 22, PST = 23, MST = 24, CST = 25, EST = 26, AST = 27, NST = 28, BRT = 29, GST = 30, AZOT = 31, LMT = 32
}


/// <summary>Details for a Time Zone.</summary>
/// <param name="TimeZone">The TimeZone.</param>
/// <param name="OffsetFromUt">The difference with Universal Time.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
public record TimeZoneDetails(TimeZones TimeZone, double OffsetFromUt, string TextId);


/// <summary>Extension class for enum TimeZones.</summary>
public static class TimeZonesExtensions
{
    /// <summary>Retrieve details for TimeZone.</summary>
    /// <param name="timezone">The time zone, is automatically filled.</param>
    /// <returns>Details for the time zone.</returns>
    public static TimeZoneDetails GetDetails(this TimeZones timeZone)
    {
        return timeZone switch
        {
            TimeZones.UT => new TimeZoneDetails(timeZone, 0.0, "ref.enum.timezone.ut"),
            TimeZones.CET => new TimeZoneDetails(timeZone, 1.0, "ref.enum.timezone.cet"),
            TimeZones.EET => new TimeZoneDetails(timeZone, 2.0, "ref.enum.timezone.eet"),
            TimeZones.EAT => new TimeZoneDetails(timeZone, 3.0, "ref.enum.timezone.eat"),
            TimeZones.IRST => new TimeZoneDetails(timeZone, 3.5, "ref.enum.timezone.irst"),
            TimeZones.AMT => new TimeZoneDetails(timeZone, 4.0, "ref.enum.timezone.amt"),
            TimeZones.AFT => new TimeZoneDetails(timeZone, 4.5, "ref.enum.timezone.aft"),
            TimeZones.PKT => new TimeZoneDetails(timeZone, 5.0, "ref.enum.timezone.pkt"),
            TimeZones.IST => new TimeZoneDetails(timeZone, 5.5, "ref.enum.timezone.ist"),
            TimeZones.IOT => new TimeZoneDetails(timeZone, 6.0, "ref.enum.timezone.iot"),
            TimeZones.MMT => new TimeZoneDetails(timeZone, 6.5, "ref.enum.timezone.mmt"),
            TimeZones.ICT => new TimeZoneDetails(timeZone, 7.0, "ref.enum.timezone.ict"),
            TimeZones.WST => new TimeZoneDetails(timeZone, 8.0, "ref.enum.timezone.wst"),
            TimeZones.JST => new TimeZoneDetails(timeZone, 9.0, "ref.enum.timezone.jst"),
            TimeZones.ACST => new TimeZoneDetails(timeZone, 9.5, "ref.enum.timezone.acst"),
            TimeZones.AEST => new TimeZoneDetails(timeZone, 10.0, "ref.enum.timezone.aest"),
            TimeZones.LHST => new TimeZoneDetails(timeZone, 10.5, "ref.enum.timezone.lhst"),
            TimeZones.NCT => new TimeZoneDetails(timeZone, 11.0, "ref.enum.timezone.nct"),
            TimeZones.NZST => new TimeZoneDetails(timeZone, 12.0, "ref.enum.timezone.nzst"),
            TimeZones.SST => new TimeZoneDetails(timeZone, -11.0, "ref.enum.timezone.sst"),
            TimeZones.HAST => new TimeZoneDetails(timeZone, -10.0, "ref.enum.timezone.hast"),
            TimeZones.MART => new TimeZoneDetails(timeZone, -9.5, "ref.enum.timezone.mart"),
            TimeZones.AKST => new TimeZoneDetails(timeZone, -9.0, "ref.enum.timezone.akst"),
            TimeZones.PST => new TimeZoneDetails(timeZone, -8.0, "ref.enum.timezone.pst"),
            TimeZones.MST => new TimeZoneDetails(timeZone, -7.0, "ref.enum.timezone.mst"),
            TimeZones.CST => new TimeZoneDetails(timeZone, -6.0, "ref.enum.timezone.cst"),
            TimeZones.EST => new TimeZoneDetails(timeZone, -5.0, "ref.enum.timezone.est"),
            TimeZones.AST => new TimeZoneDetails(timeZone, -4.0, "ref.enum.timezone.ast"),
            TimeZones.NST => new TimeZoneDetails(timeZone, -3.5, "ref.enum.timezone.nst"),
            TimeZones.BRT => new TimeZoneDetails(timeZone, -3.0, "ref.enum.timezone.brt"),
            TimeZones.GST => new TimeZoneDetails(timeZone, -2.0, "ref.enum.timezone.gst"),
            TimeZones.AZOT => new TimeZoneDetails(timeZone, -1.0, "ref.enum.timezone.azot"),
            TimeZones.LMT => new TimeZoneDetails(timeZone, 0.0, "ref.enum.timezone.lmt"),
            _ => throw new ArgumentException("TimeZones : " + timeZone.ToString())
        };
    }


    /// <summary>Retrieve details for items in the enum TimeZones.</summary>
    /// <returns>All details.</returns>
    public static List<TimeZoneDetails> AllDetails(this TimeZones _)
    {
        var allDetails = new List<TimeZoneDetails>();
        foreach (TimeZones currentTz in Enum.GetValues(typeof(TimeZones)))
        {
            allDetails.Add(GetDetails(currentTz));
        }
        return allDetails;
    }


    /// <summary>Find time zone for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The time zone for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static TimeZones TimeZoneForIndex(this TimeZones _, int index)
    {
        foreach (TimeZones currentTz in Enum.GetValues(typeof(TimeZones)))
        {
            if ((int)currentTz == index) return currentTz;
        }
        string errorText = "TimeZones.TimeZoneForIndex(): Could not find TimeZone for index : " + index;
        Log.Error(errorText);
        throw new ArgumentException(errorText);
    }

}
