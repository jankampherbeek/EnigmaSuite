// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Calc.DateTime;

/// <summary>
/// Record for a full definition of a date.
/// </summary>
/// <remarks>Assumes an astronomical year count.</remarks>
/// <param name="YearMonthDay">Texts for year, month and day, in that sequence.</param>
/// <param name="Calendar">Instance of enu Calendars.</param>
/// <param name="DateFullText">Text for the date, includes texts between [] that needs to be replaced with texts from Rosetta.</param>
public record FullDate(int[] YearMonthDay, Calendars Calendar, string DateFullText);

