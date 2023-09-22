// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Serilog;

namespace Enigma.Api;


/// <inheritdoc/>
public sealed class DateTimeApi : IDateTimeApi
{
    private readonly IDateTimeHandler _dateTimeHandler;

    public DateTimeApi(IDateTimeHandler dateTimeHandler) => _dateTimeHandler = dateTimeHandler;

    /// <inheritdoc/>
    public DateTimeResponse GetDateTime(DateTimeRequest request)
    {
        Guard.Against.Null(request);
        Log.Information("DateTimeApi.GetDateTime() using julian day {Jd}", request.JulDay);
        return _dateTimeHandler.CalcDateTime(request);
    }

    /// <inheritdoc/>
    public bool CheckDateTime(SimpleDateTime dateTime)
    {
        Guard.Against.Null(dateTime);
        Log.Information("DateTimeApi.CheckDateTime()");
        return _dateTimeHandler.CheckDateTime(dateTime);
    }

}