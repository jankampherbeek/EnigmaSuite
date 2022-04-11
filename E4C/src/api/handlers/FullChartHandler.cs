// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.domain.shared.reqresp;

namespace E4C.api.handlers;

public interface IFullChartHandler
{
    public FullChartResponse CalcFullChart(FullChartRequest request);
}


public class FullChartHandler : IFullChartHandler
{

    private readonly ISolSysPointsHandler _solSysPointsHandler;
    private readonly IMundanePosHandler _mundanePosHandler;


    public FullChartHandler(ISolSysPointsHandler solSysPointsHandler, IMundanePosHandler mundanePosHandler)
    {
        _solSysPointsHandler = solSysPointsHandler;
        _mundanePosHandler = mundanePosHandler;
    }

    public FullChartResponse CalcFullChart(FullChartRequest request)
    {
        SolSysPointsResponse solSysPointsResponse = _solSysPointsHandler.CalcSolSysPoints(request.SolSysPointRequest);
        FullMundanePosRequest mundaneRequest = new FullMundanePosRequest(request.SolSysPointRequest.JulianDayUt, request.SolSysPointRequest.ChartLocation, request.HouseSystem);
        FullMundanePosResponse mundanePosResponse = _mundanePosHandler.CalculateAllMundanePositions(mundaneRequest);
        string errorText = solSysPointsResponse.ErrorText + mundanePosResponse.ErrorText;
        bool success = solSysPointsResponse.Success && mundanePosResponse.Success;
        return new FullChartResponse(solSysPointsResponse.SolarSystemPointPositions, mundanePosResponse.FullMundanePositions, success, errorText);
    }
}

