// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Points;

namespace Enigma.Domain.Calc.ChartItems;

/// <summary>
/// Full results of calculation for houses, including Cusps, asc. Mc, Vertex, Eastpoint. Supports ecliptic, equatorial and horizontal coordinates.
/// </summary>
/// <param name="Angles">Positions for mundane angles. </param>
/// <param name="Cusps">Positions for cusps.</param>
public record FullHousesPositions(Dictionary<ChartPoints, FullPointPos> Angles, Dictionary<ChartPoints, FullPointPos> Cusps);


