// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.RequestResponse;
using Serilog;

namespace Enigma.Api.Calc;


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