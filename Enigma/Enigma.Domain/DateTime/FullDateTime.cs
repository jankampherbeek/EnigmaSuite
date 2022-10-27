// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.DateTime;

/// <summary>
/// Date/time related data.
/// </summary>
public class FullDateTime
{
    public readonly string DateText;
    public readonly string TimeText;
    public readonly double JulianDayForEt;

    /// <summary>
    /// Constructor for FullDateTime, using predefined values.
    /// </summary>
    /// <param name="dateText">Textual presentation for the date.</param>
    /// <param name="timeText">Textual presentation for the time.</param>
    /// <param name="julianDayForEt">Julian Day for ephemeris time.</param>
    public FullDateTime(string dateText, string timeText, double julianDayForEt)
    {
        DateText = dateText;
        TimeText = timeText;
        JulianDayForEt = julianDayForEt;
    }
}