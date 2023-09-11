// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Exceptions;
using Enigma.Domain.References;
using Enigma.Domain.RequestResponse;
using Enigma.Domain.Requests;
using Serilog;

namespace Enigma.Core.Calc.DateTime;


public sealed class DateTimeHandler : IDateTimeHandler
{
    private readonly IDateTimeCalc _dateTimeCalc;
    private readonly IDateTimeValidator _dateTimeValidator;

    public DateTimeHandler(IDateTimeCalc dateTimeCalc, IDateTimeValidator dateTimeValidator)
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
            Log.Error("DateTimeHandler.CalcDateTime() encountered an error : {Error}", errorText);
            success = false;
        }
        return new DateTimeResponse(dateTime, success, errorText);
    }

    public bool CheckDateTime(SimpleDateTime dateTime)
    {
        bool dateIsValid;
        try
        {
            dateIsValid = _dateTimeValidator.ValidateDateTime(dateTime);
        }
        catch (SwissEphException see)
        {
            Log.Error("DateTimeHandler.CheckDateTime() encountered an error : {Error}", see.Message);
            dateIsValid = false;
        }
        return dateIsValid;
    }
}