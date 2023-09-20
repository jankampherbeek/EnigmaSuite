// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Responses;
using Serilog;

namespace Enigma.Core.Handlers;

/// <inheritdoc/>
public sealed class JulDayHandler : IJulDayHandler
{
    private readonly IJulDayCalc _julDayCalc;

    public JulDayHandler(IJulDayCalc julDayCalc) => _julDayCalc = julDayCalc;

    /// <inheritdoc/>
    public JulianDayResponse CalcJulDay(SimpleDateTime dateTime)
    {
        double julDayUt;
        double julDayEt;
        double deltaT;
        try
        {
            julDayUt = _julDayCalc.CalcJulDayUt(dateTime);
            deltaT = _julDayCalc.CalcDeltaT(julDayUt);
            julDayEt = julDayUt + deltaT;
        }
        catch (SwissEphException see)
        {
            Log.Error("JulDayHandler.CalcJulDay(): encountered error, exception msg: {Msg}", see.Message);
            throw new EnigmaException("error when calculating Julian Day Number");
        }
        return new JulianDayResponse(julDayUt, julDayEt, deltaT);
    }

}