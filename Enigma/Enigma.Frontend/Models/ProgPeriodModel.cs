// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Progressive;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

public class ProgPeriodModel: DateTimeLocationModelBase
{
    private readonly IJulianDayApi _julianDayApi;
    
    public ProgPeriodModel(IDateInputParser dateInputParser, 
        ITimeInputParser timeInputParser, 
        IGeoLongInputParser geoLongInputParser, 
        IGeoLatInputParser geoLatInputParser,
        IJulianDayApi julianDayApi) : 
        base(dateInputParser, timeInputParser, geoLongInputParser, geoLatInputParser)
    {
        _julianDayApi = julianDayApi;
    }
    
    public void CreatePeriodData(string description, string startDateText, string endDateText)
    {
        const double ut = 0.0;
        const string timeText = "00:00:00";
        if (FullDate == null || SecondFullDate == null) return;
        SimpleDateTime startDateTime = new(FullDate.YearMonthDay[0], FullDate.YearMonthDay[1], FullDate.YearMonthDay[2],
            ut, FullDate.Calendar);
        SimpleDateTime endDateTime = new(SecondFullDate.YearMonthDay[0], SecondFullDate.YearMonthDay[1], SecondFullDate.YearMonthDay[2],
            ut, SecondFullDate.Calendar);
        double startJd = _julianDayApi.GetJulianDay(startDateTime).JulDayUt;
        double endJd = _julianDayApi.GetJulianDay(endDateTime).JulDayUt;
        FullDateTime startFullDateTime = new(startDateText, timeText, startJd);
        FullDateTime endFullDateTime = new(endDateText, timeText, endJd);
        ProgPeriod progPeriod = new(0, description, startFullDateTime, endFullDateTime);
        DataVault.Instance.CurrentProgPeriod = progPeriod;
    }
    
    
}