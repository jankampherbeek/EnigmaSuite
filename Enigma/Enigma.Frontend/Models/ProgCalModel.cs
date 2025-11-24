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
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.Models;

public class ProgCalModel(IJulianDayApi jdApi)
{
    private ProgCalItemPresFactory _progCalItemPresFactory = App.ServiceProvider.GetRequiredService<ProgCalItemPresFactory>();
    
    public List<PresentableProgCalItem> allItems { get; set;}
    public List<PresentableProgCalPeriod> allPeriods { get; set;}    
    
    public void DefineProgCal()
    {
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
        
        
        var startDate = new SimpleDateTime(2025, 11, 1, 0.0, Calendars.Gregorian);
        var startJd = jdApi.GetJulianDay(startDate).JulDayEt;
        var endDate = new SimpleDateTime(2025, 12, 1, 0.0, Calendars.Gregorian);
        var endJd = jdApi.GetJulianDay(endDate).JulDayEt;
        var calcChart = DataVaultCharts.Instance.GetCurrentChart();
        
        var declParallels = new List<DeclinationParallels>
        {
            DeclinationParallels.ContraParallel,
            DeclinationParallels.Parallel
        };
        var orbAspects = 1.0;
        var orbParallels = 0.25;
        var progTypes = new List<ProgressionTypes>() {ProgressionTypes.Secundary, ProgressionTypes.Transit };
        var request = new ProgCalRequest(startJd, endJd, calcChart, progTypes, transitPoints, secundaryPoints, radixPoints, aspects,
            declParallels, orbAspects, orbParallels);
        var response = ProgCalOrchestrator.DefineProgressiveCalendar(request);

        allItems = _progCalItemPresFactory.CreatePresProgCalItems(response.Matches);
        allPeriods = _progCalItemPresFactory.CreatePresProgCalPeriods(response.PeriodMatches);
        
    }
    
}