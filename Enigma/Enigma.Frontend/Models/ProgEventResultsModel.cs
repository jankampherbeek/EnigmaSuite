// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Windows.Documents;
using Enigma.Api.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.ViewModels;
using Serilog;

namespace Enigma.Frontend.Ui.Models;

public class ProgEventResultsModel
{
    private IDescriptiveChartText _descriptiveChartText;
    private ICalcTransitsEventApi _calcTransitsEventApi;
    private IProgAspectsApi _progAspectsApi;
    private readonly IConfigPreferencesConverter _configPrefsConverter;
    private readonly IProgPositionsForPresentationFactory _progPosPresFactory;
    private readonly IProgAspectForPresentationFactory _progAspectPresFactory;
    public string MethodName { get; set; }
    public string Details { get; set; }
    public string EventDescription { get; set; }
    public string EventDateTime { get; set; }
    public Dictionary<ChartPoints, double> _progPositions = null;
    private Dictionary<ChartPoints, ProgPositions> _transitProgPositions = new();
    //public readonly List<PresentableProgPosition> presProgPositions;
    public List<PresentableProgAspect> presProgAspects;
    //private readonly Dictionary<ChartPoints, ProgPositions> _progPositions;
    private readonly List<DefinedAspect> _progAspects;
    private readonly DataVault _dataVault = DataVault.Instance;
    
    public ProgEventResultsModel(IDescriptiveChartText descriptiveChartText, 
        ICalcTransitsEventApi calcTransitsEventApi,
        IProgAspectsApi progAspectsApi,
        IConfigPreferencesConverter configPreferencesConverter,
        IProgPositionsForPresentationFactory progPosPresFactory,
        IProgAspectForPresentationFactory progAspectForPresentationFactory)
    {
        _descriptiveChartText = descriptiveChartText;
        _calcTransitsEventApi = calcTransitsEventApi;
        _progAspectsApi = progAspectsApi;
        _configPrefsConverter = configPreferencesConverter;
        _progPosPresFactory = progPosPresFactory;
        _progAspectPresFactory = progAspectForPresentationFactory;
        MethodName = DefineMethodName();
        Details = DefineDetails();
        EventDescription = DefineEventDescription();
        EventDateTime = DefineEventDateTime();
    }

    public List<PresentableProgPosition> HandleTransits()
    {
        _transitProgPositions = CalculateTransits();
        // TODO make the following a separate method
        _progPositions = new();
        foreach (var transitProgPos in _transitProgPositions)
        {
            _progPositions.Add(transitProgPos.Key, transitProgPos.Value.Longitude);
        }
        List<PresentableProgPosition> presTransitPositions = _progPosPresFactory.CreatePresProgPos(_transitProgPositions);
        return presTransitPositions;
    }


    public void HandleAspects(ProgresMethods progMethod)
    {
        CalculatedChart? radix = DataVault.Instance.GetCurrentChart();
        if (radix != null)
        {
            Dictionary<ChartPoints, FullPointPos> fullPositions = radix.Positions;
            Dictionary<ChartPoints, double> radixPositions = 
                fullPositions.ToDictionary(fullPos => fullPos.Key, 
                    fullPos => fullPos.Value.Ecliptical.MainPosSpeed.Position);
            ConfigProg configProg = CurrentConfig.Instance.GetConfigProg();
            AstroConfig astroConfig = CurrentConfig.Instance.GetConfig();
            Dictionary<AspectTypes, AspectConfigSpecs> configAspects = astroConfig.Aspects;
            List<AspectTypes> selectedAspects = 
                configAspects.Select(configAspect => configAspect.Key).ToList();
            double orb = progMethod switch
            {
                ProgresMethods.Transits => configProg.ConfigTransits.Orb,
                ProgresMethods.Primary => configProg.ConfigPrimDir.Orb,
                ProgresMethods.Secundary => configProg.ConfigSecDir.Orb,
                ProgresMethods.Symbolic => configProg.ConfigSymDir.Orb,
                ProgresMethods.Solar => 0.0,                // No orb for solar
                ProgresMethods.Undefined => throw new ArgumentOutOfRangeException(nameof(progMethod), progMethod, null),
                _ => throw new ArgumentOutOfRangeException(nameof(progMethod), progMethod, null)
            };
            ProgAspectsRequest request = new(radixPositions, _progPositions, selectedAspects, orb);
            ProgAspectsResponse response = _progAspectsApi.FindProgAspects(request);
            if (response.ResultCode == ResultCodes.OK)
            {
                presProgAspects = _progAspectPresFactory.CreatePresProgAspect(response.Aspects);
            }
         
            // TODO decouple the presentation part
        }
    }
    
    
    
    private string DefineMethodName()
    {
        var method = _dataVault.CurrentProgresMethod;
        return method switch
        {
            ProgresMethods.Transits => "Transits",
            ProgresMethods.Primary => "Primary directions",
            ProgresMethods.Undefined => "No progressive method defined",
            ProgresMethods.Secundary => "Secundary directions",
            ProgresMethods.Symbolic => "Symbolic directions",
            ProgresMethods.Solar => "Solar return",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private string DefineDetails()
    {
        var chart = _dataVault.GetCurrentChart();
        var config = CurrentConfig.Instance.GetConfig();
        return chart != null
            ? _descriptiveChartText.ShortDescriptiveText(config, chart.InputtedChartData.MetaData)
            : "";
    }

    private string DefineEventDescription()
    {
        return _dataVault.CurrentProgEvent != null ? _dataVault.CurrentProgEvent.Description : "No description for event.";
    }

    private string DefineEventDateTime()
    {
        if (_dataVault.CurrentProgEvent == null) return "No date and time for event.";
        FullDateTime fullDate = _dataVault.CurrentProgEvent.DateTime;
        return fullDate.DateText + "\n" + fullDate.TimeText;

    }

    private Dictionary<ChartPoints, ProgPositions> CalculateTransits()
    {
        Dictionary<ChartPoints, ProgPositions> positions = new();
        ProgEvent? progEvent = _dataVault.CurrentProgEvent;
        if (progEvent == null) return positions;
        double jdUt = _dataVault.CurrentProgEvent.DateTime.JulianDayForEt;   // TODO check ET vs UT!
        Location location = _dataVault.CurrentProgEvent.Location;
        ConfigProgTransits configTransits = CurrentConfig.Instance.GetConfigProg().ConfigTransits;
        AstroConfig configRadix = CurrentConfig.Instance.GetConfig();
        TransitsEventRequest request = new(jdUt, location, configTransits, configRadix.Ayanamsha, configRadix.ObserverPosition);
        TransitsEventResponse response = _calcTransitsEventApi.CalcTransits(request);
        if (response.ResultCode == 0) positions = response.Positions;
        else
        {
            Log.Error("Resultcode when calculating transits {ResultCode}", response.ResultCode);
        }

        return positions;
    }

    private List<DefinedAspect> FindProgAspects()
    {
        
       // ProgAspectsRequest request = new(ChartPoints, ProgPoints, new List<AspectTypes>(, orb))
       return new List<DefinedAspect>();
    }
    
    
    
}