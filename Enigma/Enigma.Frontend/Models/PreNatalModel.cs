// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using Enigma.Api.Persistency;
using Enigma.Core.Slices.PreNatal;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.Models;

public sealed class PreNatalModel(PreNatalPresFactory preNatalPresFactory, IEventDataPersistencyApi eventDataPersistencyApi, IEventDataConverter eventDataConverter)
{
    private double _baseConceptionJd;
    private double _radixJd;
    private double _endOfPeriodJd;
    private Calendars _calendar;
    private const double CONCEPTION_PERIOD = 273.217;
    private const double MAX_LIFETIME_IN_DAYS = 390.31;  // corresponds to 100 years
    private const double MARGIN_BEFORE_CONCEPTION = 30.0;
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;
    
    private List<PresentablePreNatalMoment> _presPreNatalMoments;
    private List<PresentablePreNatalEvent> _presPreNatalEvents;

    /// <summary>
    /// Get the current chart name
    /// </summary>
    /// <returns>Name of the current chart or empty string if no chart</returns>
    public string GetCurrentChartName()
    {
        var currentChart = _dataVaultCharts.GetCurrentChart();
        return currentChart?.InputtedChartData.MetaData.Name ?? "";
    }
 

    public List<PresentablePreNatalMoment> GetPrenatalMoments()
    {
        
        DefineBaseConceptionJd();
        var factors = new List<ChartPoints>  // TODO use selected factors
        {
            ChartPoints.Sun,
       //     ChartPoints.Moon,
            ChartPoints.Mercury,
            ChartPoints.Venus,
            ChartPoints.Mars,
            ChartPoints.Jupiter,
            ChartPoints.Saturn,
            ChartPoints.Uranus,
            ChartPoints.Neptune,
            ChartPoints.Pluto
        };
        var aspects = new List<AspectTypes>    // TODO use selected aspects
        {
            AspectTypes.Conjunction,
            AspectTypes.Sextile,
            AspectTypes.Square,
            AspectTypes.Triangle,
            AspectTypes.Opposition,
            AspectTypes.Inconjunct
        };
        var moments = PreNatalOrchestrator.ConstructPreNatalMoments(factors, aspects, _baseConceptionJd - MARGIN_BEFORE_CONCEPTION, _endOfPeriodJd);
        _presPreNatalMoments = preNatalPresFactory.GetPreNatalMoments(moments, _baseConceptionJd, _calendar);
        return _presPreNatalMoments;

    }

    public List<PresentablePreNatalEvent> GetPrenatalEvents()
    {
        var events = new List<ProgEvent>();
        var currentChart = _dataVaultCharts.GetCurrentChart();
        long chartId = currentChart.InputtedChartData.Id;
        var persistableEventData = eventDataPersistencyApi.SearchEventData(chartId);
        foreach (ProgEvent? progEventData in persistableEventData.Select(item => eventDataConverter.FromPersistableEventData(item)))
        {
            events.Add(progEventData);
        }
        _presPreNatalEvents = preNatalPresFactory.GetPreNatalEvents(events, _calendar);
        return _presPreNatalEvents;
    }
    
    
    private void DefineBaseConceptionJd()
    {
        _radixJd = _dataVaultCharts.GetCurrentChart().InputtedChartData.FullDateTime.JulianDayForEt;
        _baseConceptionJd = _radixJd - CONCEPTION_PERIOD;
        _endOfPeriodJd = _baseConceptionJd + MAX_LIFETIME_IN_DAYS;
        _calendar = _dataVaultCharts.GetCurrentChart().InputtedChartData.FullDateTime.DateText.Contains("[g]")? Calendars.Gregorian: Calendars.Julian ;
    }
    
}