// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Serilog;

namespace Enigma.Api;


/// <summary>API for handling date and time.</summary>
public interface IDateTimeApi
{
    /// <summary>Api call to calculate date and time.</summary>
    /// <param name="request">DateTimeRequest with the value of a Julian Day number and the calendar that is used.</param>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks> 
    /// <returns>Response with validation and an instance of SimpleDateTime.</returns>
    public DateTimeResponse GetDateTime(DateTimeRequest request);

    /// <summary>Checks if a given date and time is possible.</summary>
    /// <param name="dateTime">Date and time.</param>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks> 
    /// <returns>True if date and tme are valid.</returns>
    public bool CheckDateTime(SimpleDateTime dateTime);
}


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