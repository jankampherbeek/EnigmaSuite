// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Astron.Houses;
using E4C.Core.Astron.SolSysPoints;
using E4C.Shared.ReqResp;

namespace E4C.Core.Astron.ChartAllPositions;

public interface IChartAllPositionsHandler
{
    public ChartAllPositionsResponse CalcFullChart(ChartAllPositionsRequest request);
}


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
        FullHousesPosRequest housesRequest = new FullHousesPosRequest(request.SolSysPointRequest.JulianDayUt, request.SolSysPointRequest.ChartLocation, request.HouseSystem);
        FullHousesPosResponse housesResponse = _housesHandler.CalcHouses(housesRequest);
        string errorText = solSysPointsResponse.ErrorText + housesResponse.ErrorText;
        bool success = solSysPointsResponse.Success && housesResponse.Success;
        return new ChartAllPositionsResponse(solSysPointsResponse.SolarSystemPointPositions, housesResponse.FullHousesPositions, success, errorText);
    }
}

