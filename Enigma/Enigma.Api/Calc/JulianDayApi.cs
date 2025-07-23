// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Responses;
using Enigma.Facades.Se;
using Serilog;

namespace Enigma.Api.Calc;

/// <summary>API for the calculation of the Julian Day Number.</summary>
public interface IJulianDayApi
{
    /// <summary>Api call to calculate a Julian Day Number based on UT.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null or if SimpleDateTime in the request is null.</remarks> 
    /// <returns>Response with validation and a value for a Julian Day number.</returns>
    public JulianDayResponse GetJulianDay(SimpleDateTime request);
    
    /// <summary>Calculated date and time for a given julian day</summary>
    /// <param name="julDay">The Julian Day</param>
    /// <param name="calendar">The calendar</param>
    /// <returns>Date and time that correspond to the julian day</returns>
    public SimpleDateTime DateTimeFromJd(double julDay, Calendars calendar);
    
}

/// <inheritdoc/>
public sealed class JulianDayApi(IJulDayHandler julDayHandler, IRevJulFacade revJulFacade) : IJulianDayApi
{
    /// <inheritdoc/>
    public JulianDayResponse GetJulianDay(SimpleDateTime dateTime)
    {
        Guard.Against.Null(dateTime);
        Log.Information("JulianDayApi.GetJulianDay() for {Y}/{M}/{D} {Ut}",
            dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Ut);
        return julDayHandler.CalcJulDay(dateTime);
    }




    /// <inheritdoc/>
    public SimpleDateTime DateTimeFromJd(double julDay, Calendars calendar)
    {
        return revJulFacade.DateTimeFromJd(julDay, calendar);
    }
}