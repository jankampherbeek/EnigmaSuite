// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Astron.Houses;
using E4C.Core.Astron.SolSysPoints;
using E4C.Shared.ReqResp;

namespace E4C.Core.Astron.FullChart;

public interface IFullChartHandler
{
    public FullChartResponse CalcFullChart(FullChartRequest request);
}


public class FullChartHandler : IFullChartHandler
{

    private readonly ISolSysPointsHandler _solSysPointsHandler;
    private readonly IHousesHandler _housesHandler;


    public FullChartHandler(ISolSysPointsHandler solSysPointsHandler, IHousesHandler housesHandler)
    {
        _solSysPointsHandler = solSysPointsHandler;
        _housesHandler = housesHandler;
    }

    public FullChartResponse CalcFullChart(FullChartRequest request)
    {
        SolSysPointsResponse solSysPointsResponse = _solSysPointsHandler.CalcSolSysPoints(request.SolSysPointRequest);
        FullHousesPosRequest housesRequest = new FullHousesPosRequest(request.SolSysPointRequest.JulianDayUt, request.SolSysPointRequest.ChartLocation, request.HouseSystem);
        FullHousesPosResponse housesResponse = _housesHandler.CalcHouses(housesRequest);
        string errorText = solSysPointsResponse.ErrorText + housesResponse.ErrorText;
        bool success = solSysPointsResponse.Success && housesResponse.Success;
        return new FullChartResponse(solSysPointsResponse.SolarSystemPointPositions, housesResponse.FullHousesPositions, success, errorText);
    }
}

