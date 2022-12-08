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
public class CalcDateTimeApi : ICalcDateTimeApi
{
    private readonly IDateTimeHandler _dateTimeHandler;

    public CalcDateTimeApi(IDateTimeHandler dateTimeHandler) => _dateTimeHandler = dateTimeHandler;

    public DateTimeResponse GetDateTime(DateTimeRequest request)
    {
        Guard.Against.Null(request);
        Log.Information("CalcDateTimeApi: GetDateTime using julian day {jd}.", request.JulDay);
        return _dateTimeHandler.CalcDateTime(request);
    }
    
    
    public CheckDateTimeResponse CheckDateTime(CheckDateTimeRequest request)
    {
        Guard.Against.Null(request);
        Log.Information("CalcDateTimeApi: CheckDateTime.");
        return _dateTimeHandler.CheckDateTime(request);
    }

}