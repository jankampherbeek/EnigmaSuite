// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Houses;
using Enigma.Core.Calc.Interfaces;
using Enigma.Core.Calc.ReqResp;
using Enigma.Core.Calc.SolSysPoints;

namespace Enigma.Core.Calc.ChartAllPositions;

public class ChartAllPositionsHandler : IChartAllPositionsHandler
{

    private readonly ISolSysPointsHandler _solSysPointsHandler;
    private readonly IHousesHandler _housesHandler;


    public ChartAllPositionsHandler(ISolSysPointsHandler solSysPointsHandler, IHousesHandler housesHandler)
    {
        _solSysPointsHandler = solSysPointsHandler;
        _housesHandler = housesHandler;
    }

    public ChartAllPositionsResponse CalcFullChart(ChartAllPositionsRequest request)
    {
        SolSysPointsResponse solSysPointsResponse = _solSysPointsHandler.CalcSolSysPoints(request.SolSysPointRequest);
        FullHousesPosRequest housesRequest = new (request.SolSysPointRequest.JulianDayUt, request.SolSysPointRequest.ChartLocation, request.HouseSystem);
        FullHousesPosResponse housesResponse = _housesHandler.CalcHouses(housesRequest);
        string errorText = solSysPointsResponse.ErrorText + housesResponse.ErrorText;
        bool success = solSysPointsResponse.Success && housesResponse.Success;
        return new ChartAllPositionsResponse(solSysPointsResponse.SolarSystemPointPositions, housesResponse.FullHousesPositions, success, errorText);
    }
}

