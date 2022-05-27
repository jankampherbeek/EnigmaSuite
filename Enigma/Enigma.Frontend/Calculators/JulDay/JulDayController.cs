// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.DateTime;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.Constants;
using Enigma.Domain.DateTime;
using Enigma.Frontend.InputSupport.Conversions;
using Enigma.Frontend.InputSupport.Validations;
using Enigma.Frontend.UiDomain;
using System.Collections.Generic;

namespace Enigma.Frontend.Calculators.JulDay;

public class JulDayController
{

    public string InputDate { get; set; }
    public string InputTime { get; set; }
    public bool GregorianCalendar { get; set; }
    public bool HistoricalTimeCount { get; set; } 
    public JulDayResult Result { get; set; }
    public List<int> _errorCodes { get; set; }
    private readonly IJulianDayApi _julianDayApi;
    private readonly IValueRangeConverter _valueRangeConverter;
    private readonly IDateValidator _dateValidator;
    private readonly ITimeValidator _timeValidator;
    private Calendars _cal;
    private int[] _yearMonthDay;
    private double _ut = 0.0;

    public JulDayController(IValueRangeConverter valueRangeConverter, IDateValidator dateValidator, ITimeValidator timeValidator, IJulianDayApi julianDayApi)
    {
        _valueRangeConverter = valueRangeConverter;
        _dateValidator = dateValidator;
        _timeValidator = timeValidator;
        _julianDayApi = julianDayApi;
    }


    public bool ProcessInput()
    {
        _cal = GregorianCalendar ? Calendars.Gregorian : Calendars.Julian;
        _errorCodes = new List<int>();
        bool dateSuccess = HandleDate();
        bool timeSuccess = HandleTime();

        if (dateSuccess && timeSuccess)
        {
            SimpleDateTime dateTime = new(_yearMonthDay[0], _yearMonthDay[1], _yearMonthDay[2], _ut, _cal);
            JulianDayRequest request = new(dateTime);
            JulianDayResponse response = _julianDayApi.getJulianDay(request);
            Result = new JulDayResult(response);
            return true;
        }
        else return false;
    }



    private bool HandleDate()
    {
        FullDate fullDate;
        (int[] dateNumbers, bool dateSuccess) = _valueRangeConverter.ConvertStringRangeToIntRange(InputDate, EnigmaConstants.SEPARATOR_DATE);
        if (dateSuccess)
        {
            YearCounts yearCount = HistoricalTimeCount ? YearCounts.CE : YearCounts.Astronomical;     // TODO handle difference between BCE and CE.
            List<int> errorCodesDate;
            bool dateOk = _dateValidator.CreateCheckedDate(dateNumbers, _cal, yearCount, out fullDate, out errorCodesDate);
            if (dateOk)
            {
                _yearMonthDay = fullDate.YearMonthDay;
            }
            else
            {
                for (int i = 0; i < errorCodesDate.Count; i++)
                {
                    _errorCodes.Add(errorCodesDate[i]);
                }
                dateSuccess = false;
                _errorCodes.Add(ErrorCodes.ERR_INVALID_DATE);
            }
        }
        else _errorCodes.Add(ErrorCodes.ERR_INVALID_DATE);
        return dateSuccess; 
    }

    private bool HandleTime()
    {
        FullTime fullTime;
        (int[] timeNumbers, bool timeSuccess) = _valueRangeConverter.ConvertStringRangeToIntRange(InputTime, EnigmaConstants.SEPARATOR_TIME);
        if (timeSuccess)
        {
            List<int> errorCodesTime;
            int[] lmtOffset = new int[] { 0, 0, 0 };

            bool timeOk = _timeValidator.CreateCheckedTime(timeNumbers, TimeZones.UT, lmtOffset, true, out fullTime, out errorCodesTime);
            if (timeOk)
            {
                int hour = fullTime.HourMinuteSecond[0];
                int minute = fullTime.HourMinuteSecond[1];
                int second = fullTime.HourMinuteSecond[2];
                _ut = (double)hour + (double)minute / EnigmaConstants.MINUTES_PER_HOUR_DEGREE + (double)second / EnigmaConstants.SECONDS_PER_HOUR_DEGREE;
            }
            else
            {
                for (int i = 0; i < errorCodesTime.Count; i++)
                {
                    errorCodesTime.Add(errorCodesTime[i]);
                }
                timeSuccess = false;
                _errorCodes.Add(ErrorCodes.ERR_INVALID_TIME);
            }
        }
        else _errorCodes.Add(ErrorCodes.ERR_INVALID_TIME);
        return timeSuccess;
    }


}