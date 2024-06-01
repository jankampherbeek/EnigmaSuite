// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.References;

public enum TimeZones
{
    Ut = 0, Cet = 1, Eet = 2, Eat = 3, Irst = 4, Amt = 5, Aft = 6, Pkt = 7, Ist = 8, Iot = 9, 
    Mmt = 10, Ict = 11, Wst = 12, Jst = 13, Acst = 14, Aest = 15, Lhst = 16, Nct = 17, Nzst = 18, Sst = 19, 
    Hast = 20, Mart = 21, Akst = 22, Pst = 23, Mst = 24, Cst = 25, Est = 26, Ast = 27, Nst = 28, Brt = 29, 
    Gst = 30, Azot = 31, Lmt = 32
}


/// <summary>Details for a Time Zone.</summary>
/// <param name="TimeZone">The TimeZone.</param>
/// <param name="OffsetFromUt">The difference with Universal Time.</param>
/// <param name="RbKey">Key to descriptive text in resource bundle.</param>
public record TimeZoneDetails(TimeZones TimeZone, double OffsetFromUt, string RbKey);


/// <summary>Extension class for enum TimeZones.</summary>
public static class TimeZonesExtensions
{
    /// <summary>Retrieve details for TimeZone.</summary>
    /// <param name="timeZone">The time zone, is automatically filled.</param>
    /// <returns>Details for the time zone.</returns>
    public static TimeZoneDetails GetDetails(this TimeZones timeZone)
    {
        return timeZone switch
        {
            TimeZones.Ut => new TimeZoneDetails(timeZone, 0.0, "ref.timezone.ut"),
            TimeZones.Cet => new TimeZoneDetails(timeZone, 1.0, "ref.timezone.cet"),
            TimeZones.Eet => new TimeZoneDetails(timeZone, 2.0, "ref.timezone.eet"),
            TimeZones.Eat => new TimeZoneDetails(timeZone, 3.0, "ref.timezone.eat"),
            TimeZones.Irst => new TimeZoneDetails(timeZone, 3.5, "ref.timezone.irst"),
            TimeZones.Amt => new TimeZoneDetails(timeZone, 4.0, "ref.timezone.amt"),
            TimeZones.Aft => new TimeZoneDetails(timeZone, 4.5, "ref.timezone.aft"),
            TimeZones.Pkt => new TimeZoneDetails(timeZone, 5.0, "ref.timezone.pkt"),
            TimeZones.Ist => new TimeZoneDetails(timeZone, 5.5, "ref.timezone.ist"),
            TimeZones.Iot => new TimeZoneDetails(timeZone, 6.0, "ref.timezone.iot"),
            TimeZones.Mmt => new TimeZoneDetails(timeZone, 6.5, "ref.timezone.mmt"),
            TimeZones.Ict => new TimeZoneDetails(timeZone, 7.0, "ref.timezone.ict"),
            TimeZones.Wst => new TimeZoneDetails(timeZone, 8.0, "ref.timezone.wst"),
            TimeZones.Jst => new TimeZoneDetails(timeZone, 9.0, "ref.timezone.jst"),
            TimeZones.Acst => new TimeZoneDetails(timeZone, 9.5, "ref.timezone.acst"),
            TimeZones.Aest => new TimeZoneDetails(timeZone, 10.0, "ref.timezone.aest"),
            TimeZones.Lhst => new TimeZoneDetails(timeZone, 10.5, "ref.timezone.aest"),
            TimeZones.Nct => new TimeZoneDetails(timeZone, 11.0, "ref.timezone.nct"),
            TimeZones.Nzst => new TimeZoneDetails(timeZone, 12.0, "ref.timezone.nzst"),
            TimeZones.Sst => new TimeZoneDetails(timeZone, -11.0, "ref.timezone.sst"),
            TimeZones.Hast => new TimeZoneDetails(timeZone, -10.0, "ref.timezone.hast"),
            TimeZones.Mart => new TimeZoneDetails(timeZone, -9.5, "ref.timezone.mart"),
            TimeZones.Akst => new TimeZoneDetails(timeZone, -9.0, "ref.timezone.akst"),
            TimeZones.Pst => new TimeZoneDetails(timeZone, -8.0, "ref.timezone.pst"),
            TimeZones.Mst => new TimeZoneDetails(timeZone, -7.0, "ref.timezone.mst"),
            TimeZones.Cst => new TimeZoneDetails(timeZone, -6.0, "ref.timezone.cst"),
            TimeZones.Est => new TimeZoneDetails(timeZone, -5.0, "ref.timezone.est"),
            TimeZones.Ast => new TimeZoneDetails(timeZone, -4.0, "ref.timezone.ast"),
            TimeZones.Nst => new TimeZoneDetails(timeZone, -3.5, "ref.timezone.nst"),
            TimeZones.Brt => new TimeZoneDetails(timeZone, -3.0, "ref.timezone.brt"),
            TimeZones.Gst => new TimeZoneDetails(timeZone, -2.0, "ref.timezone.gst"),
            TimeZones.Azot => new TimeZoneDetails(timeZone, -1.0, "ref.timezone.azot"),
            TimeZones.Lmt => new TimeZoneDetails(timeZone, 0.0, "ref.timezone.lmt"),
            _ => throw new ArgumentException("TimeZones : " + timeZone)
        };
    }
    
    
    /// <summary>Retrieve details for items in the enum TimeZones.</summary>
    /// <returns>All details.</returns>
    public static List<TimeZoneDetails> AllDetails()
    {
        return (from TimeZones currentTz in Enum.GetValues(typeof(TimeZones)) select GetDetails(currentTz)).ToList();
    }


    /// <summary>Find time zone for an index</summary>
    /// <param name="index">Index to look for</param>
    /// <returns>The time zone for the index</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static TimeZones TimeZoneForIndex(int index)
    {
        foreach (TimeZones currentTz in Enum.GetValues(typeof(TimeZones)))
        {
            if ((int)currentTz == index) return currentTz;
        }
        Log.Error("TimeZones.TimeZoneForIndex(): Could not find TimeZone for index : {Index}", index);
        throw new ArgumentException("Wrong time zone");
    }

}
