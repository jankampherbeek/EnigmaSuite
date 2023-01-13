// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Points;

namespace Enigma.Domain.Calc.ChartItems;

/// <summary>Complete calculation results for a full chart.</summary>
/// <param name="CelPointPositions">Positions for chart points.</param>
/// <param name="MundanePositions">Positions for mundane points.</param>
public record ChartAllPositionsResponse(List<FullChartPointPos> CelPointPositions, FullHousesPositions? MundanePositions);


