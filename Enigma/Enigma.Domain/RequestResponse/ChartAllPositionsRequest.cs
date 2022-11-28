// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;

namespace Enigma.Domain.RequestResponse;

/// <summary>
/// Data for a request to calculate a full chart.
/// </summary>
public record ChartAllPositionsRequest
{
    public readonly CelPointsRequest celPointsRequest;
    public readonly HouseSystems HouseSystem;

    /// <summary>
    /// Constructor for the record FullChartRequest.
    /// </summary>
    /// <param name="celPointRequest">All data except the housesystem.</param>
    /// <param name="houseSystem">The preferred house system.</param>
    public ChartAllPositionsRequest(CelPointsRequest celPointRequest, HouseSystems houseSystem)
    {
        celPointsRequest = celPointRequest;
        HouseSystem = houseSystem;
    }
}