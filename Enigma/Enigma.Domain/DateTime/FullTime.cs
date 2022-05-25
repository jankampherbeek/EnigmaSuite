// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.DateTime;

/// <summary>
/// Record for a full definition of a time.
/// </summary>
public record FullTime
{
    public readonly int[] HourMinuteSecond;
    public readonly double Ut;
    public readonly int CorrectionForDay;
    public readonly string TimeFullText;

    /// <summary>
    /// Constructor for FullTime.
    /// </summary>
    /// <param name="hourMinuteSecond">Texts for hour, minute and second in that sequence.</param>
    /// <param name="ut">Value of Universal Time, using 24 hour notation.</param>
    /// <param name="correctionForDay">Correction for day, due to time overflow. Poswsible values -1, 0, +1.</param>
    /// <param name="timeFullText">Text for the time, includes texts between [] that needs to be replaced with texts from Rosetta.</param>
    public FullTime(int[] hourMinuteSecond, double ut, int correctionForDay, string timeFullText)
    {
        HourMinuteSecond = hourMinuteSecond;
        Ut = ut;
        CorrectionForDay = correctionForDay;
        TimeFullText = timeFullText;
    }

}