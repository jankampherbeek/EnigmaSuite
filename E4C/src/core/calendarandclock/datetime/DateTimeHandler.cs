// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Shared.Domain;
using E4C.Exceptions;
using E4C.Shared.References;
using E4C.Shared.ReqResp;

namespace E4C.Core.CalendarAndClock.DateTime;

public interface IDateTimeHandler
{
    public DateTimeResponse CalcDateTime(DateTimeRequest request);
}


public class DateTimeHandler : IDateTimeHandler
{
    private readonly IDateTimeCalc _dateTimeCalc;

    public DateTimeHandler(IDateTimeCalc dateTimeCalc) => _dateTimeCalc = dateTimeCalc;

    public DateTimeResponse CalcDateTime(DateTimeRequest request)
    {
        SimpleDateTime dateTime = new SimpleDateTime(0, 0, 0, 0.0, Calendars.Gregorian);
        string errorText = "";
        bool success = true;
        try
        {
            dateTime = _dateTimeCalc.CalcDateTime(request.JulDay, request.Calendar);
        }
        catch (SwissEphException see)
        {
            errorText = see.Message;
            success = false;
        }
        return new DateTimeResponse(dateTime, success, errorText);
    }

}