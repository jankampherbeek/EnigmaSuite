// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Domain.References;
using Enigma.Domain.Responses;
using Enigma.Facades.Se;
using Serilog;

namespace Enigma.Core.Handlers;

/// <summary>Handler for the calculation of a Julian Day Number.</summary>
public interface IJulDayHandler
{
    /// <summary>Calculates a Julian Day Number.</summary>
    /// <param name="dateTime">Date and time.</param>
    /// <returns>Response with JD related results and an indication if the calculation was successful.</returns>
    public JulianDayResponse CalcJulDay(SimpleDateTime dateTime);

    /// <summary>Defines the date for a given Julian Day number.</summary>
    /// <param name="jdNr">Julian Day number.</param>
    /// <param name="cal">Calendar.</param>
    /// <returns>The resulting date and time.</returns>
    public SimpleDateTime CalcDateTime(double jdNr, Calendars cal);
}

/// <inheritdoc/>
public sealed class JulDayHandler : IJulDayHandler
{
    private readonly IJulDayCalc _julDayCalc;
    private readonly IJulDayFacade _julDayFacade;

    /// <inheritdoc/>
    public JulDayHandler(IJulDayCalc julDayCalc, IJulDayFacade julDayFacade)
    {
        _julDayCalc = julDayCalc;
        _julDayFacade = julDayFacade;
    }
    

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

    /// <inheritdoc/>
    public SimpleDateTime CalcDateTime(double jdNr, Calendars cal)
    {
        return _julDayFacade.DateTimeFromJd(jdNr, cal);
    }
}