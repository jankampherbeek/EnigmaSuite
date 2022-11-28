// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Calc.Celestialpoints;

public class ChartAllPositionsHandler : IChartAllPositionsHandler
{

    private readonly ICelPointsHandler _celPointsHandler;
    private readonly IHousesHandler _housesHandler;


    public ChartAllPositionsHandler(ICelPointsHandler celPointsHandler, IHousesHandler housesHandler)
    {
        _celPointsHandler = celPointsHandler;
        _housesHandler = housesHandler;
    }

    public ChartAllPositionsResponse CalcFullChart(ChartAllPositionsRequest request)
    {
        CelPointsResponse celPointsResponse = _celPointsHandler.CalcCelPoints(request.celPointsRequest);
        FullHousesPosRequest housesRequest = new(request.celPointsRequest.JulianDayUt, request.celPointsRequest.ChartLocation, request.HouseSystem);
        FullHousesPosResponse housesResponse = _housesHandler.CalcHouses(housesRequest);
        string errorText = celPointsResponse.ErrorText + housesResponse.ErrorText;
        bool success = celPointsResponse.Success && housesResponse.Success;
        return new ChartAllPositionsResponse(celPointsResponse.CelPointPositions, housesResponse.FullHousesPositions, success, errorText);
    }

}

