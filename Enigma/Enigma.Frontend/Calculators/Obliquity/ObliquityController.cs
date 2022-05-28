// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.Astron;
using Enigma.Core.Calc.Api.DateTime;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.Constants;
using Enigma.Domain.DateTime;
using Enigma.Frontend.InputSupport.Conversions;
using Enigma.Frontend.InputSupport.Validations;
using Enigma.Frontend.UiDomain;
using System.Collections.Generic;

namespace Enigma.Frontend.Calculators.Obliquity;

public class ObliquityController
{

    public string InputDate { get; set; }
    public bool GregorianCalendar { get; set; }
    public bool HistoricalTimeCount { get; set; }
    public ObliquityResult Result { get; set; }
    public List<int> _errorCodes { get; set; }
    private readonly IObliquityApi _obliquityApi;
    private readonly IJulianDayApi _julianDayApi;
    private readonly IValueRangeConverter _valueRangeConverter;
    private readonly IDateValidator _dateValidator;
    private Calendars _cal;
    private int[] _yearMonthDay;

    public ObliquityController(IValueRangeConverter valueRangeConverter, IDateValidator dateValidator, IObliquityApi obliquityApi, IJulianDayApi julianDayApi)
    {
        _valueRangeConverter = valueRangeConverter;
        _dateValidator = dateValidator;
        _obliquityApi = obliquityApi;
        _julianDayApi = julianDayApi;
    }


    public bool ProcessInput()
    {
        _cal = GregorianCalendar ? Calendars.Gregorian : Calendars.Julian;
        _errorCodes = new List<int>();
        bool dateSuccess = HandleDate();


        if (dateSuccess)
        {
            SimpleDateTime dateTime = new(_yearMonthDay[0], _yearMonthDay[1], _yearMonthDay[2], 0.0, _cal);
            JulianDayRequest jdRequest = new(dateTime);
            JulianDayResponse jdResponse = _julianDayApi.getJulianDay(jdRequest);
            double jd = jdResponse.JulDayUt;

            ObliquityRequest oblRequest = new(jd);
            ObliquityResponse oblResponse = _obliquityApi.getObliquity(oblRequest); 
            Result = new ObliquityResult(oblResponse);
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


}