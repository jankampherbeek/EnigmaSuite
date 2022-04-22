// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using E4C.Core.Astron.Houses;
using E4C.Shared.ReqResp;

namespace E4C.Core.Api.Astron;

/// <summary>API for calculation of house cusps and other mundane points.</summary>
public interface IHousesApi
{
    /// <summary>Api call to calculate house cusps and other mundane points.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Instance of FullHousesPosResponse with all coordinates for cusps, MC, Ascendant, EastPoint and Vertex.</returns>
    public FullHousesPosResponse getHouses(FullHousesPosRequest request);
}


/// <inheritdoc/>
public class HousesApi : IHousesApi
{
    private readonly IHousesHandler _housesHandler;

    /// <param name="housesHandler">Handler for the calculation of the houses.</param>
    public HousesApi(IHousesHandler housesHandler) => _housesHandler = housesHandler;

    public FullHousesPosResponse getHouses(FullHousesPosRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.ChartLocation);
        return _housesHandler.CalcHouses(request);
    }

}