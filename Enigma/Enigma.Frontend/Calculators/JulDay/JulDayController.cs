// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.DateTime;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.Constants;
using Enigma.Domain.DateTime;
using Enigma.Frontend.UiDomain;

namespace Enigma.Frontend.Calculators.JulDay;

public class JulDayController
{

    public string InputDate { get; set; }
    public string InputTime { get; set; }
    public bool GregorianCalendar { get; set; }
    public bool HistoricalTimeCount { get; set; }
    public JulDayResult Result { get; set; }
    private IJulianDayApi _julianDayApi;

    public JulDayController(IJulianDayApi julianDayApi)
    {
        _julianDayApi = julianDayApi;
    }


    public bool ProcessInput()
    {
        // TODO: check inputValues
        // TODO: handle Yearcount
        string[] inputDateTexts = InputDate.Split('/');
        string[] inputTimeTexts = InputTime.Split(':');
        int year = int.Parse(inputDateTexts[0]);
        int month = int.Parse(inputDateTexts[1]);
        int day = int.Parse((inputDateTexts[2]));
        int hour = int.Parse(inputTimeTexts[0]);
        int minute = int.Parse(inputTimeTexts[1]);
        int second = int.Parse(inputTimeTexts[2]);
        double ut = hour / EnigmaConstants.HOURS_PER_DAY + minute / EnigmaConstants.MINUTES_PER_DAY + second / EnigmaConstants.SECONDS_PER_DAY;
        Calendars calendar = GregorianCalendar ? Calendars.Gregorian : Calendars.Julian;

        SimpleDateTime dateTime = new SimpleDateTime(year, month, day, ut, calendar);
        JulianDayRequest request = new JulianDayRequest(dateTime);

        JulianDayResponse response = _julianDayApi.getJulianDay(request);

        Result = new JulDayResult(response);
        return true;
    }


}