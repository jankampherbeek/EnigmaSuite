// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using Enigma.Api;
using Enigma.Api.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Responses;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support.Conversions;
using Enigma.Frontend.Ui.Support.Parsers;

namespace Enigma.Frontend.Ui.Charts.Prog.PrimDir;

public class PrimDirInputModel(
    IDateInputParser dateInputParser,
    ITextToDateConverter textToDateConverter,
    IJulianDayApi julianDayApi)
{
    private readonly IDateInputParser _dateInputParser = dateInputParser;


    public bool AreDatesValid(string startDateTxt, string endDateTxt, Calendars cal, YearCounts yc)
    {
        try
        {
            bool startDateOk = textToDateConverter.ConvertText(startDateTxt, cal, out SimpleDate startDate);
            bool endDateOk = textToDateConverter.ConvertText(endDateTxt, cal, out SimpleDate endDate);
            SimpleDateTime startDateTime = CreateDateTimeNoon(startDate);
            SimpleDateTime endDateTime = CreateDateTimeNoon(endDate);
            JulianDayResponse response = julianDayApi.GetJulianDay(startDateTime);
            double startJd = response.JulDayEt;
            response = julianDayApi.GetJulianDay(endDateTime);
            double endJd = response.JulDayEt;
            bool sequenceOk = startJd < endJd;
            double radixJd = DataVaultCharts.Instance.GetCurrentChart().InputtedChartData.FullDateTime.JulianDayForEt;
            bool positiveAge = startJd > radixJd;
            return startDateOk && endDateOk && sequenceOk && positiveAge;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    

    private SimpleDateTime CreateDateTimeNoon(SimpleDate sDate)
    {
        return new SimpleDateTime(sDate.Year, sDate.Month, sDate.Day, 0.0, sDate.Calendar);
    }
    
}