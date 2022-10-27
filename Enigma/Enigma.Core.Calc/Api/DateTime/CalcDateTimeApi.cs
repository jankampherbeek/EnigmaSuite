// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Calc.DateTime.DateTimeFromJd;
using Enigma.Core.Calc.ReqResp;

namespace Enigma.Core.Calc.Api.DateTime;

/// <summary>API for calculating date and time.</summary>
public interface ICalcDateTimeApi
{

    /// <summary>Api call to calculate date and time.</summary>
    /// <param name="request">DateTimeRequest with the value of a Julian Day number and the calendar that is used.</param>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks> 
    /// <returns>Response with validation and an instance of SimpleDateTime.</returns>
    public DateTimeResponse getDateTime(DateTimeRequest request);

}


/// <inheritdoc/>
public class CalcDateTimeApi : ICalcDateTimeApi
{
    private readonly IDateTimeHandler _dateTimeHandler;

    public CalcDateTimeApi(IDateTimeHandler dateTimeHandler) => _dateTimeHandler = dateTimeHandler;

    public DateTimeResponse getDateTime(DateTimeRequest request)
    {
        Guard.Against.Null(request);
        return _dateTimeHandler.CalcDateTime(request);
    }

}