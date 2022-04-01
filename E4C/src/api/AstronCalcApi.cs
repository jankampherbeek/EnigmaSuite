// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

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
    /// <returns>Validated response with all positions (cusps, MC, Asc, Vertex, Eastpoint) and all relevant coordinates (longitude, right ascension, declination, azimuth, altitude). 
    /// The field Success is set to false if an error occurs. Any errors are explained in the field ErrorText.</returns>
    public FullMundanePosResponse getAllHousePositions(FullMundanePosRequest request);
}


/// <inheritdoc>
public class AstronCalcApi : IAstronCalcApi
{

    private MundanePosHandler _mundanePosHandler;

    /// <summary>
    /// Constructor defines all handlers.
    /// </summary>
    /// <param name="mundanePosHandler">Handler for the calculation of houses.</param>
    public AstronCalcApi(MundanePosHandler mundanePosHandler)
    {
        _mundanePosHandler = mundanePosHandler;
    }

    /// <inheritdoc>
    public FullMundanePosResponse getAllHousePositions(FullMundanePosRequest request)
    {
        return _mundanePosHandler.CalculateAllMundanePositions(request);
    }
}