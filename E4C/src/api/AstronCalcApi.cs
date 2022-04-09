// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using E4C.api.handlers;
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
}


/// <inheritdoc>
public class AstronCalcApi : IAstronCalcApi
{

    private readonly IMundanePosHandler _mundanePosHandler;
    private readonly ISolSysPointsHandler _solSysPointsHandler;

    /// <summary>
    /// Constructor defines all handlers.
    /// </summary>
    /// <param name="mundanePosHandler">Handler for the calculation of houses.</param>
    public AstronCalcApi(IMundanePosHandler mundanePosHandler, ISolSysPointsHandler solSysPointsHandler)
    {
        _mundanePosHandler = mundanePosHandler;
        _solSysPointsHandler = solSysPointsHandler;
    }

    /// <inheritdoc>
    public FullMundanePosResponse getAllHousePositions(FullMundanePosRequest request)
    {
        Guard.Against.Null(request, nameof(request));
        Guard.Against.Null(request.ChartLocation);
        return _mundanePosHandler.CalculateAllMundanePositions(request);
    }

    public SolSysPointsResponse getSolSysPoints(SolSysPointsRequest request)
    {
        Guard.Against.Null(request, nameof(request));
        Guard.Against.NullOrEmpty(request.SolarSystemPoints);
        Guard.Against.Null(request.ChartLocation);
        return _solSysPointsHandler.CalcSolSysPoints(request);
    }
}