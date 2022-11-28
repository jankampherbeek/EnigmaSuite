// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Work.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.Exceptions;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Calc.DateTime;


public class DateTimeHandler : IDateTimeHandler
{
    private readonly IDateTimeCalc _dateTimeCalc;
    private readonly ICheckDateTimeValidator _dateTimeValidator;

    public DateTimeHandler(IDateTimeCalc dateTimeCalc, ICheckDateTimeValidator dateTimeValidator)
    {
        _dateTimeCalc = dateTimeCalc;
        _dateTimeValidator = dateTimeValidator;
    }

    public DateTimeResponse CalcDateTime(DateTimeRequest request)
    {
        SimpleDateTime dateTime = new(0, 0, 0, 0.0, Calendars.Gregorian);
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

    public CheckDateTimeResponse CheckDateTime(CheckDateTimeRequest request)
    {
        bool dateIsValid = true;
        string errorText = "";
        bool success = true;
        try
        {
            dateIsValid = _dateTimeValidator.ValidateDateTime(request.DateTime);
        }
        catch (SwissEphException see)
        {
            errorText = see.Message;
            success = false;
        }
        return new CheckDateTimeResponse(dateIsValid, success, errorText);
    }
}