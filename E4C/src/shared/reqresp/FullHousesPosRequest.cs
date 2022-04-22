// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.domain.shared.specifications;
using E4C.Shared.References;

namespace E4C.Shared.ReqResp;

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