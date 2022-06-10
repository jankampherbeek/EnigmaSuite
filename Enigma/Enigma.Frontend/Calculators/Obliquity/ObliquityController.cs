// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.Astron;
using Enigma.Core.Calc.Api.DateTime;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.Constants;
using Enigma.Domain.DateTime;
using Enigma.Frontend.InputSupport.InputParsers;
using Enigma.Frontend.UiDomain;
using System.Collections.Generic;

namespace Enigma.Frontend.Calculators.Obliquity;

public class ObliquityController
{

    public string InputDate { get; set; }
    public Calendars Calendar { get; set; }
    public YearCounts YearCount { get; set; }
    public ObliquityResult Result { get; set; }
    public List<int> _errorCodes { get; set; }
    private readonly IObliquityApi _obliquityApi;
    private readonly IJulianDayApi _julianDayApi;
    private readonly IDateInputParser _dateInputParser;

    public ObliquityController(IDateInputParser dateInputParser, IObliquityApi obliquityApi, IJulianDayApi julianDayApi)
    {
        _dateInputParser = dateInputParser;
        _obliquityApi = obliquityApi;
        _julianDayApi = julianDayApi;
    }


    public bool ProcessInput()
    {
        _errorCodes = new List<int>();
        FullDate? fullDate;
        bool dateSuccess = _dateInputParser.HandleGeoLong(InputDate, Calendar, YearCount, out fullDate);
        if (!dateSuccess) _errorCodes.Add(ErrorCodes.ERR_INVALID_DATE);
        if (dateSuccess && fullDate != null)
        {
            SimpleDateTime dateTime = new(fullDate.YearMonthDay[0], fullDate.YearMonthDay[1], fullDate.YearMonthDay[2], 0.0, Calendar);
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


}