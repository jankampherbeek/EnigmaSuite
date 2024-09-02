// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using Enigma.Api;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Responses;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support.Conversions;
using Enigma.Frontend.Ui.Support.Parsers;

namespace Enigma.Frontend.Ui.Charts.Prog.PrimDir;

public class PrimDirInputModel
{
    private readonly IDateInputParser _dateInputParser;
    private readonly ITextToDateConverter _textToDateConverter;
    private readonly IJulianDayApi _julianDayApi;
    
    public PrimDirInputModel(IDateInputParser dateInputParser, 
        ITextToDateConverter textToDateConverter,
        IJulianDayApi julianDayApi)
    {
        _dateInputParser = dateInputParser;
        _textToDateConverter = textToDateConverter;
        _julianDayApi = julianDayApi;
    }
    
   
    public bool AreDatesValid(string startDateTxt, string endDateTxt, Calendars cal, YearCounts yc)
    {
        try
        {
            bool startDateOk = _textToDateConverter.ConvertText(startDateTxt, cal, out SimpleDate startDate);
            bool endDateOk = _textToDateConverter.ConvertText(endDateTxt, cal, out SimpleDate endDate);
            SimpleDateTime startDateTime = CreateDateTimeNoon(startDate);
            SimpleDateTime endDateTime = CreateDateTimeNoon(endDate);
            JulianDayResponse response = _julianDayApi.GetJulianDay(startDateTime);
            double startJd = response.JulDayEt;
            response = _julianDayApi.GetJulianDay(endDateTime);
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