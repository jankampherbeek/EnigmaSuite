// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Data.Common;
using Enigma.Api.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.Presentables;
using Enigma.Domain.Progressive;
using Enigma.Domain.References;
using Enigma.Domain.RequestResponse;
using Enigma.Domain.Requests;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.ViewModels;

namespace Enigma.Frontend.Ui.Models;

public class ProgEventResultsModel
{
    private IDescriptiveChartText _descriptiveChartText;
    private ICalcTransitsEventApi _calcTransitsEventApi;
    private IProgAspectsApi _progAspectsApi;
    private readonly IConfigPreferencesConverter _configPrefsConverter;
    private readonly IProgPositionsForPresentationFactory _progPosPresFactory;
    public string MethodName { get; set; }
    public string Details { get; set; }
    public string EventDescription { get; set; }
    public string EventDateTime { get; set; }
    public readonly List<PresentableProgPosition> presProgPositions;
    public readonly List<PresentableProgAspect> presProgAspects;
    private readonly Dictionary<ChartPoints, FullPointPos> _progPositions;
    private readonly List<DefinedAspect> _progAspects;
    private readonly DataVault _dataVault = DataVault.Instance;
    
    public ProgEventResultsModel(IDescriptiveChartText descriptiveChartText, 
        ICalcTransitsEventApi calcTransitsEventApi,
        IProgAspectsApi progAspectsApi,
        IConfigPreferencesConverter configPreferencesConverter,
        IProgPositionsForPresentationFactory progPosPresFactory)
    {
        _descriptiveChartText = descriptiveChartText;
        _calcTransitsEventApi = calcTransitsEventApi;
        _progAspectsApi = progAspectsApi;
        _configPrefsConverter = configPreferencesConverter;
        _progPosPresFactory = progPosPresFactory;
        MethodName = DefineMethodName();
        Details = DefineDetails();
        EventDescription = DefineEventDescription();
        EventDateTime = DefineEventDateTime();
        _progPositions = CalculateTransits();
        _progAspects = FindProgAspects();
        presProgPositions = progPosPresFactory.CreatePresProgPos(_progPositions);
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

    private Dictionary<ChartPoints, FullPointPos> CalculateTransits()
    {
        Dictionary<ChartPoints, FullPointPos> positions = new();
        ProgEvent? progEvent = _dataVault.CurrentProgEvent;
        if (progEvent == null) return _progPositions;
        double jdUt = _dataVault.CurrentProgEvent.DateTime.JulianDayForEt;   // TODO check ET vs UT!
        Location location = _dataVault.CurrentProgEvent.Location;
        CalculationPreferences prefs = _configPrefsConverter.RetrieveCalculationPreferences();
        TransitsEventRequest request = new(jdUt, location, prefs);
        return _calcTransitsEventApi.CalculateTransits(request);
    }

    private List<DefinedAspect> FindProgAspects()
    {
        
       // ProgAspectsRequest request = new(ChartPoints, ProgPoints, new List<AspectTypes>(, orb))
       return new List<DefinedAspect>();
    }
    
    
    
}