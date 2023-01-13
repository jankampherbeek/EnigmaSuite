// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Persistency;


public class PersistableDate
{
    public int Year { get; }
    public int Month { get; }
    public int Day { get; }
    public string Calendar { get; }

    public PersistableDate(int year, int month, int day, string calendar)
    {
        Year = year;
        Month = month;
        Day = day;
        Calendar = calendar;
    }
}


public class PersistableTime
{
    public int Hour { get; }
    public int Minute { get; }
    public int Second { get; }
    public double ZoneOffset { get; }
    public double Dst { get; }

    public PersistableTime(int hour, int minute, int second, double zoneOffset, double dst)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
        ZoneOffset = zoneOffset;
        Dst = dst;
    }
}
