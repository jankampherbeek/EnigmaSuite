// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Domain.Dtos;


/// <summary>Representation for a date and time, including Calendar.</summary>
/// <param name="Year">Astronomical year.</param>
/// <param name="Month">Month 1..12.</param>
/// <param name="Day">Day</param>
/// <param name="Ut">(Universal Time) add the time in 0..23 hours and a decimal fraction for the total of minutes and seconds.</param>
/// <param name="Calendar">Calendar.</param>
public record SimpleDateTime(int Year, int Month, int Day, double Ut, Calendars Calendar);

/// <summary>Representation for a date withouot time.</summary>
/// <param name="Year">Astronomical year.</param>
/// <param name="Month">Month 1..12.</param>
/// <param name="Day">Day</param>
/// <param name="Calendar">Calendar.</param>
public record SimpleDate(int Year, int Month, int Day, Calendars Calendar);