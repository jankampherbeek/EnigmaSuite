// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;

namespace Enigma.Core.Calc.ReqResp;

/// <summary>
/// Data for a request to calculate a full chart.
/// </summary>
public record ChartAllPositionsRequest
{
    public readonly SolSysPointsRequest SolSysPointRequest;
    public readonly HouseSystems HouseSystem;

    /// <summary>
    /// Constructor for the record FullChartRequest.
    /// </summary>
    /// <param name="solSysPointRequest">All data except the housesystem.</param>
    /// <param name="houseSystem">The preferred house system.</param>
    public ChartAllPositionsRequest(SolSysPointsRequest solSysPointRequest, HouseSystems houseSystem)
    {
        SolSysPointRequest = solSysPointRequest;
        HouseSystem = houseSystem;
    }
}