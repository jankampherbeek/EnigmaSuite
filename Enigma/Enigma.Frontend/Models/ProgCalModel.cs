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
    
    public List<PresentableProgCalItem> TestProgCal()
    {
        var startDate = new SimpleDateTime(2025, 11, 1, 0.0, Calendars.Gregorian);
        var startJd = jdApi.GetJulianDay(startDate).JulDayEt;
        var endDate = new SimpleDateTime(2026, 1, 1, 0.0, Calendars.Gregorian);
        var endJd = jdApi.GetJulianDay(endDate).JulDayEt;
        var calcChart = DataVaultCharts.Instance.GetCurrentChart();
        var progPoints = new List<ChartPoints>()
        {
            ChartPoints.Sun,
            ChartPoints.Moon,
            ChartPoints.Mercury,
            ChartPoints.Venus,
            ChartPoints.Mars,
            ChartPoints.Jupiter,
            ChartPoints.Saturn
        };
        var radixPoints = new List<ChartPoints>()
        {
            ChartPoints.Sun,
            ChartPoints.Moon,
            ChartPoints.Mercury,
            ChartPoints.Venus,
            ChartPoints.Mars,
            ChartPoints.Jupiter,
            ChartPoints.Saturn,
            ChartPoints.Mc,
            ChartPoints.Mars
        };
        var aspects = new List<AspectTypes>()
        {
            AspectTypes.Conjunction,
            AspectTypes.Opposition,
            AspectTypes.Square,
            AspectTypes.Triangle,
            AspectTypes.Sextile
        };
        var declParallels = new List<DeclinationParallels>
        {
            DeclinationParallels.ContraParallel,
            DeclinationParallels.Parallel
        };
        var orbAspects = 1.0;
        var orbParallels = 0.25;
        var progTypes = new List<ProgressionTypes>() { ProgressionTypes.Transit, ProgressionTypes.Secundary };
        var request = new ProgCalRequest(startJd, endJd, calcChart, progTypes, progPoints, radixPoints, aspects,
            declParallels, orbAspects, orbParallels);
        var response = ProgCalOrchestrator.DefineProgressiveCalendar(request);

        var result = _progCalItemPresFactory.CreatePresProgCalItems(response);
        Console.WriteLine(result.Count);
        return result;
    }
    
}