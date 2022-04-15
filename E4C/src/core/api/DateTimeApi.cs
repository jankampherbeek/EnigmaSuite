// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using E4C.core.calendarandclock.checkdatetime;
using E4C.core.calendarandclock.datetime;
using E4C.core.calendarandclock.julday;
using E4C.shared.reqresp;

namespace E4C.core.api;

/// <summary>API for date and time calculations and validations.</summary>
public interface IDateTimeApi
{

    /// <summary>Api call to calculate a Julian Day Number.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null or if SimpleDateTime in the request is null.</remarks> 
    /// <returns>Response with validation and a value for a Julian Day number.</returns>
    public JulianDayResponse getJulianDay(JulianDayRequest request);


    /// <summary>Api call to calculate date and time.</summary>
    /// <param name="request">DateTimeRequest with the value of a Julian Day number and the calendar that is used.</param>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks> 
    /// <returns>Response with validation and an instance of SimpleDateTime.</returns>
    public DateTimeResponse getDateTime(DateTimeRequest request);


    /// <summary>Api call to check if a given date time is valid.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null or if SimpleDateTime in the request is null.</remarks> /// 
    /// <returns>Response with an indication if the date and time are valid and an indication if errors did occur.</returns>
    public CheckDateTimeResponse checkDateTime(CheckDateTimeRequest request);
}


/// <inheritdoc/>
public class DateTimeApi : IDateTimeApi
{
    private readonly IJulDayHandler _julDayHandler;
    private readonly IDateTimeHandler _dateTimeHandler;
    private readonly ICheckDateTimeHandler _checkDateTimeHandler;

    public DateTimeApi(IJulDayHandler julDayHandler, IDateTimeHandler dateTimeHandler, ICheckDateTimeHandler checkDateTimeHandler)
    {
        _julDayHandler = julDayHandler;
        _dateTimeHandler = dateTimeHandler;
        _checkDateTimeHandler = checkDateTimeHandler;
    }

    public CheckDateTimeResponse checkDateTime(CheckDateTimeRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.DateTime);
        return _checkDateTimeHandler.CheckDateTime(request);
    }

    public DateTimeResponse getDateTime(DateTimeRequest request)
    {
        Guard.Against.Null(request);
        return _dateTimeHandler.CalcDateTime(request);
    }

    public JulianDayResponse getJulianDay(JulianDayRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.DateTime);
        return _julDayHandler.CalcJulDay(request);
    }
}