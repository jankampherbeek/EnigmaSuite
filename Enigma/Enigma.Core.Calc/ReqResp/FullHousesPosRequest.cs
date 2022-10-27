// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;
using Enigma.Domain.Locational;

namespace Enigma.Core.Calc.ReqResp;

/// <summary>
/// Request to calculate a complete set of fully defined mundane positions.
/// </summary>
public record FullHousesPosRequest
{
    public readonly double JdUt;
    public readonly Location ChartLocation;
    public readonly HouseSystems HouseSystem;

    /// <summary>
    /// Constructor for request.
    /// </summary>
    /// <param name="jdUt">Julian Day for UT.</param>
    /// <param name="chartLocation">The location with the correct coordinates.</param>
    /// <param name="houseSystem">The houseSystem to use.</param>
    public FullHousesPosRequest(double jdUt, Location chartLocation, HouseSystems houseSystem)
    {
        JdUt = jdUt;
        ChartLocation = chartLocation;
        HouseSystem = houseSystem;
    }

}