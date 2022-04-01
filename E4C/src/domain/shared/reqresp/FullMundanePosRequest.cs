// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.domain.shared.references;
using E4C.domain.shared.specifications;

namespace E4C.domain.shared.reqresp;

/// <summary>
/// Request to calculate a complete set of fully defined mundane positions.
/// </summary>
public record FullMundanePosRequest
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
    public FullMundanePosRequest(double jdUt, Location chartLocation, HouseSystems houseSystem)
    {
        JdUt = jdUt;
        ChartLocation = chartLocation;
        HouseSystem = houseSystem;
    }

}