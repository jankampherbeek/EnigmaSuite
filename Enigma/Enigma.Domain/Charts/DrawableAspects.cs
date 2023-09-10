// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Points;
using Enigma.Domain.References;

namespace Enigma.Domain.Charts;

/// <summary>Aspect, between two celestial points, to be shown in a chart wheel.</summary>
/// <param name="Point1">The first celestial point.</param>
/// <param name="Point2">The second celestial point.</param>
/// <param name="Exactness">The Exactness (unused fraction of the max. orb) as a percentage.</param>
/// <param name="AspectType">The aspect type (conjunction, opposition etc.).</param>
public record DrawableCelPointAspect(ChartPoints Point1, ChartPoints Point2, double Exactness, AspectTypes AspectType);



/// <summary>Aspect between a mundane point and a celestial point, that can be shown in a wheel.</summary>
/// <param name="MundanePoint"></param>
/// <param name="CelPoint">The celestial point point.</param>
/// <param name="Exactness">The Exactness (unused fraction of the max. orb) as a percentage.</param>
/// <param name="AspectType">The aspect type (conjunction, opposition etc.).</param>
public record DrawableMundaneAspect(ChartPoints MundanePoint, ChartPoints CelPoint, double Exactness, AspectTypes AspectType);



/// <summary>X-Y-coordinates for the start, or end, of a drawable aspect-line.</summary>
/// <remarks>The coordinates are for a celestial point.</remarks>
/// <param name="CelPoint">The celestial point at one side of the aspect line.</param>
/// <param name="XCoordinate">Value for X.</param>
/// <param name="YCoordinate">Value for Y.</param>
public record DrawableAspectCoordinatesCp(ChartPoints CelPoint, double XCoordinate, double YCoordinate);



/// <summary>X-Y-coordinates for the start, or end, of a drawable aspect-line.</summary>
/// <remarks>The coordinates are for a mundane point.</remarks>
/// <param name="MundanePoint">The mundane point at one side of the aspect line.</param>
/// <param name="XCoordinate">Value for X.</param>
/// <param name="YCoordinate">Value for Y.</param>
public record DrawableAspectCoordinatesMu(string MundanePoint, double XCoordinate, double YCoordinate);





