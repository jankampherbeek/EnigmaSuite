// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.DateTime;
using Enigma.Domain.Exceptions;

namespace Enigma.Core.Calc.DateTime.DateTimeFromJd;

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