// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using Enigma.Api.Calc;
using Enigma.Core.Slices.ProgCalendar;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support.Parsers;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.Models;

public class ProgCalModel(IJulianDayApi jdApi)
{
    private ProgCalItemPresFactory _progCalItemPresFactory = App.ServiceProvider.GetRequiredService<ProgCalItemPresFactory>();
    private readonly IDateInputParser _dateInputParser = App.ServiceProvider.GetRequiredService<IDateInputParser>();
    public bool useSecundary { get; set; }
    public bool useParallels { get; set; }
    public bool useExtPeriod { get; set; }
    public string ChartName { get; set; } = string.Empty;
    public List<PresentableProgCalItem> allItems { get; set;}
    public List<PresentableProgCalPeriod> allPeriods { get; set;}
    private FullDate? _fullDate;
    
    
    
    public void DefineProgCal()
    {
        var startDateItems = _fullDate.YearMonthDay;   
        var startDate = new SimpleDateTime(startDateItems[0], startDateItems[1], startDateItems[2],0.0, Calendars.Gregorian);
        var startJd = jdApi.GetJulianDay(startDate).JulDayEt;
        var periodLength = useExtPeriod ? 100: 33;
        
        AstroConfig radixConfig = CurrentConfig.Instance.GetConfig();
        ConfigProg progConfig = CurrentConfig.Instance.GetConfigProg();
        var radixPoints = new List<ChartPoints>();
        var radixPointSpecs = radixConfig.ChartPoints;
        foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs?> radixPointSpec in radixPointSpecs)
        {
            radixPoints.Add(radixPointSpec.Key);
        }
        var aspects = new List<AspectTypes>();
        foreach (KeyValuePair<AspectTypes, AspectConfigSpecs?> aspectSpec in radixConfig.Aspects)
        {
            if (!aspectSpec.Value.IsUsed) continue;
            aspects.Add(aspectSpec.Key);
        }

        var transitPoints = new List<ChartPoints>();
        var transitPointSpecs = progConfig.ConfigTransits.ProgPoints;
        foreach (KeyValuePair<ChartPoints, ProgPointConfigSpecs?> transitPointSpec in transitPointSpecs)
        {
            if (!transitPointSpec.Value.IsUsed) continue;
            transitPoints.Add(transitPointSpec.Key);
        }

        var secundaryPoints = new List<ChartPoints>();
        var secundaryPointSpects = progConfig.ConfigSecDir.ProgPoints;
        foreach (KeyValuePair<ChartPoints, ProgPointConfigSpecs?> secundaryPointSpec in secundaryPointSpects)
        {
            if (!secundaryPointSpec.Value.IsUsed) continue;
            secundaryPoints.Add(secundaryPointSpec.Key);
        }
        
        var calcChart = DataVaultCharts.Instance.GetCurrentChart();
        ChartName = calcChart?.InputtedChartData.MetaData.Name ?? "";
        
        var orbAspects = 1.0;
        var orbParallels = 0.25;
        var progTypes = new List<ProgressionTypes>() {ProgressionTypes.Transit };
        if (useSecundary) progTypes.Add(ProgressionTypes.Secundary);
        var declParallels = new List<DeclinationParallels>();
        if (useParallels)
        {
            declParallels.Add(DeclinationParallels.Parallel);
            declParallels.Add(DeclinationParallels.ContraParallel);
        }
        var request = new ProgCalRequest(startJd, periodLength, calcChart, progTypes, transitPoints, secundaryPoints, radixPoints, aspects,
            declParallels, orbAspects, orbParallels);
        var response = ProgCalOrchestrator.DefineProgressiveCalendar(request);

        allItems = _progCalItemPresFactory.CreatePresProgCalItems(response.Matches);
        allPeriods = _progCalItemPresFactory.CreatePresProgCalPeriods(response.PeriodMatches);
        
    }

    public bool CheckDate(string inputDate)
    {
        bool isValid = _dateInputParser.HandleDate(inputDate, Calendars.Gregorian, YearCounts.CE, out FullDate? fullDate);
        if (isValid) _fullDate = fullDate;
        return isValid;
    }
    
    /// <summary>
    /// Get the current chart name
    /// </summary>
    /// <returns>Name of the current chart or empty string if no chart</returns>
    public string GetCurrentChartName()
    {
        var currentChart = DataVaultCharts.Instance.GetCurrentChart();
        return currentChart?.InputtedChartData.MetaData.Name ?? "";
    }
    
}