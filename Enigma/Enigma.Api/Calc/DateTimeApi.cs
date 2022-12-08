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
public class DateTimeApi : IDateTimeApi
{
    private readonly IDateTimeHandler _checkDateTimeHandler;

    public DateTimeApi(IDateTimeHandler checkDateTimeHandler) => _checkDateTimeHandler = checkDateTimeHandler;

    /// <inheritdoc/>
    public CheckDateTimeResponse CheckDateTime(CheckDateTimeRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.DateTime);
        Log.Information("DateTimeapi CheckDateTime");
        return _checkDateTimeHandler.CheckDateTime(request);
    }

}