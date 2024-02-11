// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Responses;
using Serilog;

namespace Enigma.Api;

/// <summary>API for the calculation of the Julian Day Number.</summary>
public interface IJulianDayApi
{
    /// <summary>Api call to calculate a Julian Day Number based on UT.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null or if SimpleDateTime in the request is null.</remarks> 
    /// <returns>Response with validation and a value for a Julian Day number.</returns>
    public JulianDayResponse GetJulianDay(SimpleDateTime request);
}

/// <inheritdoc/>
public sealed class JulianDayApi : IJulianDayApi
{
    private readonly IJulDayHandler _julDayHandler;

    public JulianDayApi(IJulDayHandler julDayHandler) => _julDayHandler = julDayHandler;

    /// <inheritdoc/>
    public JulianDayResponse GetJulianDay(SimpleDateTime dateTime)
    {
        Guard.Against.Null(dateTime);
        Log.Information("JulianDayApi.GetJulianDay() for {Y}/{M}/{D} {Ut}",
            dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Ut);
        return _julDayHandler.CalcJulDay(dateTime);
    }

}