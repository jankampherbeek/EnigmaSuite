// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Calc.DateTime;

/// <summary>
/// Record for a full definition of a time.
/// </summary>
/// <param name="HourMinuteSecond">Texts for hour, minute and second in that sequence.</param>
/// <param name="Ut">Value of Universal Time, using 24 hour notation.</param>
/// <param name="CorrectionForDay">Correction for day, due to time overflow. Poswsible values -1, 0, +1.</param>
/// <param name="TimeFullText">Text for the time, includes texts between [] that needs to be replaced with texts from Rosetta.</param>
public record FullTime(int[] HourMinuteSecond, double Ut, int CorrectionForDay, string TimeFullText);
