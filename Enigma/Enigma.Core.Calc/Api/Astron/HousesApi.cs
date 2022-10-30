// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Calc.Houses;
using Enigma.Core.Calc.Interfaces;
using Enigma.Core.Calc.ReqResp;

namespace Enigma.Core.Calc.Api.Astron;


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