// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Domain.Dtos;

/// <summary>DTO for Out of Bounds Event, including calendar date.</summary>
/// <remarks>The date is based on the local calendar.</remarks>
/// <param name="Point">The celestial point.</param>
/// <param name="EventType">The type of event.</param>
/// <param name="Year">The year.</param>
/// <param name="Month">The month.</param>
/// <param name="Day">The day.</param>
public record OobCalEvent(ChartPoints Point, OobEventTypes EventType, int Year, int Month, int Day);

/// <summary>DTO for Out of Bounds Event, using secondary Julian Day.</summary>
/// <param name="Point">The celestial point.</param>
/// <param name="EventType">The type of event.</param>
/// <param name="SecJd">Julian day measured in secondary days.</param>
public record OobSecJdEvent(ChartPoints Point, OobEventTypes EventType, double SecJd);
