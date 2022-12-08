// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.RequestResponse;
using Serilog;

namespace Enigma.Api.Calc;


/// <inheritdoc/>
public class JulianDayApi : IJulianDayApi
{
    private readonly IJulDayHandler _julDayHandler;

    public JulianDayApi(IJulDayHandler julDayHandler) => _julDayHandler = julDayHandler;

    /// <inheritdoc/>
    public JulianDayResponse GetJulianDay(JulianDayRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.DateTime);
        Log.Information("JulianDayApi GetJulianDay for year {y}/{m}/{d} {ut}", request.DateTime.Year, request.DateTime.Month, request.DateTime.Day, request.DateTime.Ut);
        return _julDayHandler.CalcJulDay(request);
    }

}