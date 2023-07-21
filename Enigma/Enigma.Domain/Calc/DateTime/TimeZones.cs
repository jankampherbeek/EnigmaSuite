// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Calc.DateTime;

public enum TimeZones
{
    Ut = 0, Cet = 1, Eet = 2, Eat = 3, Irst = 4, Amt = 5, Aft = 6, Pkt = 7, Ist = 8, Iot = 9, 
    Mmt = 10, Ict = 11, Wst = 12, Jst = 13, Acst = 14, Aest = 15, Lhst = 16, Nct = 17, Nzst = 18, Sst = 19, 
    Hast = 20, Mart = 21, Akst = 22, Pst = 23, Mst = 24, Cst = 25, Est = 26, Ast = 27, Nst = 28, Brt = 29, 
    Gst = 30, Azot = 31, Lmt = 32
}


/// <summary>Details for a Time Zone</summary>
/// <param name="TimeZone">The TimeZone</param>
/// <param name="OffsetFromUt">The difference with Universal Time</param>
/// <param name="Text">Descriptive text</param>
public record TimeZoneDetails(TimeZones TimeZone, double OffsetFromUt, string Text);


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
            TimeZones.Ut => new TimeZoneDetails(timeZone, 0.0, "+00:00: UT/Universal Time, GMT/Greenwich Mean Time"),
            TimeZones.Cet => new TimeZoneDetails(timeZone, 1.0, "+01:00: CET/Central European Time"),
            TimeZones.Eet => new TimeZoneDetails(timeZone, 2.0, "+02:00: EET/Eastern European Time"),
            TimeZones.Eat => new TimeZoneDetails(timeZone, 3.0, "+03:00: EAT/East African time, TRT/Turkey Time"),
            TimeZones.Irst => new TimeZoneDetails(timeZone, 3.5, "+03:30: IRST/Iran Standard Time"),
            TimeZones.Amt => new TimeZoneDetails(timeZone, 4.0, "+04:00: AMT/Armenia Time, MUT/Mauritius Time"),
            TimeZones.Aft => new TimeZoneDetails(timeZone, 4.5, "+04:30: AFT/Afghanistan Time"),
            TimeZones.Pkt => new TimeZoneDetails(timeZone, 5.0, "+05:00: PKT/Pakistan Standard Time, ORAT/Oral Time"),
            TimeZones.Ist => new TimeZoneDetails(timeZone, 5.5, "+05:30: IST/Indian Standard Time"),
            TimeZones.Iot => new TimeZoneDetails(timeZone, 6.0, "+06:00: IOT/Indian Chagos Time, OMST/Omsk Time"),
            TimeZones.Mmt => new TimeZoneDetails(timeZone, 6.5, "+06:30: MMT/Myanmar Standard Time"),
            TimeZones.Ict => new TimeZoneDetails(timeZone, 7.0, "+07:00: ICT/Indochina Time"),
            TimeZones.Wst => new TimeZoneDetails(timeZone, 8.0, "+08:00: WST/Western Standard Time, CST/China Standard Time"),
            TimeZones.Jst => new TimeZoneDetails(timeZone, 9.0, "+09:00: JST/Japan Standard Time"),
            TimeZones.Acst => new TimeZoneDetails(timeZone, 9.5, "+09:30: ACST/Australian Central Standard Time"),
            TimeZones.Aest => new TimeZoneDetails(timeZone, 10.0, "+10:00: AEST/Australian Eastern Standard Time"),
            TimeZones.Lhst => new TimeZoneDetails(timeZone, 10.5, "+10:30: LHST/Lord Howe Standard Time"),
            TimeZones.Nct => new TimeZoneDetails(timeZone, 11.0, "+11:00: NCT/New Caledonia Time"),
            TimeZones.Nzst => new TimeZoneDetails(timeZone, 12.0, "+12:00: NZST/New Zealand Standard Time"),
            TimeZones.Sst => new TimeZoneDetails(timeZone, -11.0, "-11:00: SST/Samoa Standard Time"),
            TimeZones.Hast => new TimeZoneDetails(timeZone, -10.0, "-10:00: HAST/Hawaii-Aleutian Standard Time"),
            TimeZones.Mart => new TimeZoneDetails(timeZone, -9.5, "-09:30: MART/Marquesas Islands Time"),
            TimeZones.Akst => new TimeZoneDetails(timeZone, -9.0, "-09:00: AKST/Alaska Standard Time"),
            TimeZones.Pst => new TimeZoneDetails(timeZone, -8.0, "-08:00: PST/Pacific Standard Time"),
            TimeZones.Mst => new TimeZoneDetails(timeZone, -7.0, "-07:00: MST/Mountain Standard Time"),
            TimeZones.Cst => new TimeZoneDetails(timeZone, -6.0, "-06:00: CST/Central Standard Time"),
            TimeZones.Est => new TimeZoneDetails(timeZone, -5.0, "-05:00: EST/Eastern Standard Time"),
            TimeZones.Ast => new TimeZoneDetails(timeZone, -4.0, "-04:00: AST Atlantic Standard Time"),
            TimeZones.Nst => new TimeZoneDetails(timeZone, -3.5, "-03:30: NST/Newfoundland Standard Time"),
            TimeZones.Brt => new TimeZoneDetails(timeZone, -3.0, "-03:00: BRT/Brasilia Time, ART/Argentina Time"),
            TimeZones.Gst => new TimeZoneDetails(timeZone, -2.0, "-02:00: GST/South Georgia Time"),
            TimeZones.Azot => new TimeZoneDetails(timeZone, -1.0, "-01:00: AZOT/Azores Standard Time"),
            TimeZones.Lmt => new TimeZoneDetails(timeZone, 0.0, "LMT: Local Mean Time"),
            _ => throw new ArgumentException("TimeZones : " + timeZone.ToString())
        };
    }

    /// <summary>Retrieve details for items in the enum TimeZones.</summary>
    /// <returns>All details.</returns>
    public static List<TimeZoneDetails> AllDetails(this TimeZones _)
    {
        return (from TimeZones currentTz in Enum.GetValues(typeof(TimeZones)) select GetDetails(currentTz)).ToList();
    }


    /// <summary>Find time zone for an index</summary>
    /// <param name="_">Instance of enum TimeZones</param>
    /// <param name="index">Index to look for</param>
    /// <returns>The time zone for the index</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static TimeZones TimeZoneForIndex(this TimeZones _, int index)
    {
        foreach (TimeZones currentTz in Enum.GetValues(typeof(TimeZones)))
        {
            if ((int)currentTz == index) return currentTz;
        }
        Log.Error("TimeZones.TimeZoneForIndex(): Could not find TimeZone for index : {Index}", index);
        throw new ArgumentException("Wrong time zone");
    }

}
