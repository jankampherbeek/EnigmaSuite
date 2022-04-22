// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using E4C.Core.CalendarAndClock.CheckDateTime;
using E4C.Shared.ReqResp;

namespace E4C.core.api.datetime;

/// <summary>API for checking date and time values.</summary>
public interface ICheckDateTimeApi
{

    /// <summary>Api call to check if a given date time is valid.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null or if SimpleDateTime in the request is null.</remarks> 
    /// <returns>Response with an indication if the date and time are valid and an indication if errors did occur.</returns>
    public CheckDateTimeResponse checkDateTime(CheckDateTimeRequest request);
}


/// <inheritdoc/>
public class CheckDateTimeApi : ICheckDateTimeApi
{
    private readonly ICheckDateTimeHandler _checkDateTimeHandler;

    public CheckDateTimeApi(ICheckDateTimeHandler checkDateTimeHandler) => _checkDateTimeHandler = checkDateTimeHandler;

    public CheckDateTimeResponse checkDateTime(CheckDateTimeRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.DateTime);
        return _checkDateTimeHandler.CheckDateTime(request);
    }

}