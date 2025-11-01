// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    private double _actualConceptionJd;
    private double _radixJd;
    private double _endOfPeriodJd;
    private Calendars _calendar;
    public bool useAspects {get; set; }
    public bool useEclipses { get; set; }
    public bool useIngresses {get; set; }
    public bool useRetrogradeDirect{get; set;}
    public List<ChartPoints> selectedFactors {get; set;}
    public List<AspectTypes> selectedAspects {get; set;}
    public string standardConception {get; set;}
    public string actualConception {get; set;}

  //  public FactorSelection? CurrentFactorSelection { get; set; }
    
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

    public void SetInitialConception()
    {
        DefineBaseConceptionJd();
    }
   
    
    public List<PresentablePreNatalMoment> GetPrenatalMoments()
    {
            // TODO exclude points thar are always in aspect

        var typeSettings = new TypeSettings(useAspects, useEclipses, useIngresses, useRetrogradeDirect);
        
        var moments = PreNatalOrchestrator.ConstructPreNatalMoments(selectedFactors, selectedAspects, typeSettings, _actualConceptionJd - MARGIN_BEFORE_CONCEPTION, _endOfPeriodJd);
        _presPreNatalMoments = preNatalPresFactory.GetPreNatalMoments(moments, _actualConceptionJd, _radixJd, _calendar);
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


    public string GetSelectedFactorsGlyphs()
    {
        var sbGlyphs = new StringBuilder();
        foreach (var factor in selectedFactors)
        {
            var glyph = GlyphsForChartPoints.FindGlyph(factor);
            sbGlyphs.Append(glyph);
            sbGlyphs.Append(' ');
        }
        return sbGlyphs.ToString();
    }

    public string GetSelectedAspectsGlyphs()
    {
        var sbGlyphs = new StringBuilder();
        foreach (var aspect in selectedAspects)
        {
            var glyph = aspect.GetDetails().Glyph;
            sbGlyphs.Append(glyph);
            sbGlyphs.Append(' ');
        }
        return sbGlyphs.ToString();
    }
    
    private void DefineBaseConceptionJd()
    {
        _radixJd = _dataVaultCharts.GetCurrentChart().InputtedChartData.FullDateTime.JulianDayForEt;
        _baseConceptionJd = _radixJd - CONCEPTION_PERIOD;
        _actualConceptionJd = _baseConceptionJd;
        standardConception = preNatalPresFactory.JdToDateTimeString(_baseConceptionJd, _calendar);
        actualConception = standardConception;
        _endOfPeriodJd = _baseConceptionJd + MAX_LIFETIME_IN_DAYS;
        _calendar = _dataVaultCharts.GetCurrentChart().InputtedChartData.FullDateTime.DateText.Contains("[g]")? Calendars.Gregorian: Calendars.Julian ;
    }

    public void DefineActualConceptionJd(double eventJd, double momentJd)
    {
        var actConcJd = PreNatalOrchestrator.ActualFromBaseConceptionDate(_baseConceptionJd, eventJd, momentJd);
        _actualConceptionJd = actConcJd;
        actualConception = preNatalPresFactory.JdToDateTimeString(_actualConceptionJd, _calendar);
    }
}