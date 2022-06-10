// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.DateTime;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.Constants;
using Enigma.Domain.DateTime;
using Enigma.Frontend.InputSupport.Conversions;
using Enigma.Frontend.InputSupport.InputParsers;
using Enigma.Frontend.UiDomain;
using System.Collections.Generic;

namespace Enigma.Frontend.Calculators.JulDay;

public class JulDayController
{

    public string InputDate { get; set; }
    public string InputTime { get; set; }
    public Calendars Calendar { get; set; }
    public YearCounts YearCount { get; set; } 
    public JulDayResult Result { get; set; }
    public List<int> _errorCodes { get; set; }
    private readonly IJulianDayApi _julianDayApi;
    private readonly IDateInputParser _dateInputParser;
    private readonly ITimeInputParser _timeInputParser;

    public JulDayController(IDateInputParser dateInputParser, ITimeInputParser timeInputParser, IJulianDayApi julianDayApi)
    {
        _dateInputParser = dateInputParser;
        _timeInputParser = timeInputParser;
        _julianDayApi = julianDayApi;
    }


    public bool ProcessInput()
    {
        TimeZones timeZone = TimeZones.UT;
        double lmtOffset = 0.0;
        _errorCodes = new List<int>();
        FullDate? fullDate;
        FullTime? fullTime;
        bool dateSuccess = _dateInputParser.HandleGeoLong(InputDate, Calendar, YearCount, out fullDate);
        bool timeSuccess = _timeInputParser.HandleTime(InputTime, timeZone, lmtOffset, out fullTime);

        if (!dateSuccess) _errorCodes.Add(ErrorCodes.ERR_INVALID_DATE);
        if (!timeSuccess) _errorCodes.Add(ErrorCodes.ERR_INVALID_TIME);

        if (dateSuccess && timeSuccess && fullDate != null && fullTime != null)
        {
            double ut = fullTime.Ut + (fullTime.CorrectionForDay * 24.0);

            SimpleDateTime dateTime = new(fullDate.YearMonthDay[0], fullDate.YearMonthDay[1], fullDate.YearMonthDay[2], ut, Calendar);
            JulianDayRequest request = new(dateTime);
            JulianDayResponse response = _julianDayApi.getJulianDay(request);
            Result = new JulDayResult(response);
            return true;
        }
        else
        {   
            _errorCodes.Add(ErrorCodes.ERR_INVALID_DATE);
            return false;
        }
    }


}