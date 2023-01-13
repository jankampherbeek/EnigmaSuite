// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Points;

namespace Enigma.Domain.Calc.ChartItems;

/// <summary>
/// Full results of calculation for houses, including Cusps, asc. Mc, Vertex, Eastpoint. Supports ecliptic, equatorial and horizontal coordinates.
/// </summary>
/// <param name="Cusps">List with full positions for Cusps, in the sequence 1 ..n. </param>
/// <param name="Mc"/>
/// <param name="Ascendant"/>
/// <param name="Vertex"/>
/// <param name="EastPoint"/>
public record FullHousesPositions(List<FullChartPointPos> Cusps, FullChartPointPos Mc, FullChartPointPos Ascendant, FullChartPointPos Vertex, FullChartPointPos EastPoint);


