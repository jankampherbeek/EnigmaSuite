// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Calc.DateTime.JulDay;
using Enigma.Core.Calc.ReqResp;

namespace Enigma.Core.Calc.Api.DateTime;

/// <summary>API for the calculation of the Julian Day Number.</summary>
public interface IJulianDayApi
{

    /// <summary>Api call to calculate a Julian Day Number based on UT.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null or if SimpleDateTime in the request is null.</remarks> 
    /// <returns>Response with validation and a value for a Julian Day number.</returns>
    public JulianDayResponse getJulianDay(JulianDayRequest request);
}


/// <inheritdoc/>
public class JulianDayApi : IJulianDayApi
{
    private readonly IJulDayHandler _julDayHandler;

    public JulianDayApi(IJulDayHandler julDayHandler) => _julDayHandler = julDayHandler;


    public JulianDayResponse getJulianDay(JulianDayRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.DateTime);
        return _julDayHandler.CalcJulDay(request);
    }
}