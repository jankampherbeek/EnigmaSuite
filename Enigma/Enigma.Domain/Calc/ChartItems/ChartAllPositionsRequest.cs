// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Calc.ChartItems;

/// <summary>Data for a request to calculate a full chart.</summary>
/// <param name="CelPointRequest">All data except the housesystem.</param>
/// <param name="HouseSystem">The preferred house system.</param>
public record ChartAllPositionsRequest(CelPointsRequest CelPointRequest, HouseSystems HouseSystem);
