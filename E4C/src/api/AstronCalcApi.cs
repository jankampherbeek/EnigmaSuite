// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using E4C.api.handlers;
using E4C.core.astron.obliquity;
using E4C.domain.shared.reqresp;


namespace E4C.api;

/// <summary>
/// API for astronomical calculations.
/// </summary>
public interface IAstronCalcApi
{
    /// <summary>
    /// Api enabled access to retrieve all house positions. 
    /// </summary>
    /// <param name="request"/>
    /// <returns>Validated response with all positions (cusps, MC, Asc, Vertex, Eastpoint) and all relevant coordinates (ecliptical, equatorial and nhorizontal). 
    /// The field Success is set to false if an error occurs. Any errors are explained in the field ErrorText.</returns>
    public FullMundanePosResponse getAllHousePositions(FullMundanePosRequest request);

    /// <summary>
    /// Api enabled access to retrieve positions for one or more solar system points.
    /// </summary>
    /// <param name="request"/>
    /// <returns>Validated response with all positions for the solar system ints in the request and all relevant coordinates (ecliptical, equatorial and nhorizontal). 
    /// The field Success is set to false if an error occurs. Any errors are explained in the field ErrorText</returns>
    public SolSysPointsResponse getSolSysPoints(SolSysPointsRequest request);

    /// <summary>
    /// Api access to retrieve all positions for a full chart.
    /// </summary>
    /// <param name="request"/>
    /// <returns>The calculated chart with all positions.</returns>
    public FullChartResponse getFullChart(FullChartRequest request);
}


/// <inheritdoc>
public class AstronCalcApi : IAstronCalcApi
{

    private readonly IMundanePosHandler _mundanePosHandler;
    private readonly ISolSysPointsHandler _solSysPointsHandler;
    private readonly IFullChartHandler _fullChartHandler;

    /// <param name="mundanePosHandler">Handler for the calculation of houses.</param>
    public AstronCalcApi(IMundanePosHandler mundanePosHandler, 
                         ISolSysPointsHandler solSysPointsHandler, 
                         IFullChartHandler fullChartHandler)
    {
        _mundanePosHandler = mundanePosHandler;
        _solSysPointsHandler = solSysPointsHandler;
        _fullChartHandler = fullChartHandler;
    }

    /// <inheritdoc>
    public FullMundanePosResponse getAllHousePositions(FullMundanePosRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.ChartLocation);
        return _mundanePosHandler.CalculateAllMundanePositions(request);
    }

    public FullChartResponse getFullChart(FullChartRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.SolSysPointRequest);
        Guard.Against.Null(request.SolSysPointRequest.ChartLocation);
        return _fullChartHandler.CalcFullChart(request);
    }

    public SolSysPointsResponse getSolSysPoints(SolSysPointsRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.NullOrEmpty(request.SolarSystemPoints);
        Guard.Against.Null(request.ChartLocation);
        return _solSysPointsHandler.CalcSolSysPoints(request);
    }
}