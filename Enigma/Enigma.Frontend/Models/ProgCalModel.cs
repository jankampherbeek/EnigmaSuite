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
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

public class ProgCalModel(IJulianDayApi jdApi)
{
    
    
    public void TestAspects()
    {
        var startDate = new SimpleDateTime(2025, 11, 1, 0.0, Calendars.Gregorian);
        var startJd = jdApi.GetJulianDay(startDate).JulDayEt;
        var endDate = new SimpleDateTime(2026, 11, 1, 0.0, Calendars.Gregorian);
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
        var declEvents = new List<DeclinationEvents>();
        var declParallels = new List<DeclinationParallels>();
        var progTypes = new List<ProgressionTypes>() { ProgressionTypes.Transit, ProgressionTypes.Secundary };
        var request = new ProgCalRequest(startJd, endJd, calcChart, progTypes, progPoints, radixPoints, aspects,
            declEvents, declParallels);
        var response = ProgCalOrchestrator.DefineProgressiveCalendar(request);
        foreach (var aspect in response.Aspects)
        {
            Console.WriteLine($@"Aspect: type: {aspect.ProgType}, jd: {aspect.Jd}, radixPoint: {aspect.RadixPoint}, progPoint: {aspect.ProgPoint}, aspect: {aspect.Aspect}, longitude prog: {aspect.ProgLongitude}");
        }
    }
    
}