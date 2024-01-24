// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using Enigma.Api.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using Serilog;

namespace Enigma.Frontend.Ui.Models;

public class ProgEventResultsModel
{
    private readonly IProgSecDirEventApi _progSecDirEventApi;
    private readonly IProgSymDirEventApi _progSymDirEventApi;
    private readonly IProgTransitsEventApi _progTransitsEventApi;
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;
    private readonly DataVaultProg _dataVaultProg = DataVaultProg.Instance;
    private readonly IDescriptiveChartText _descriptiveChartText;
    private readonly IProgAspectForPresentationFactory _progAspectPresFactory;
    private readonly IProgAspectsApi _progAspectsApi;
    private readonly IProgPositionsForPresentationFactory _progPosPresFactory;
    private Dictionary<ChartPoints, double> _progPositions = new();
    public List<PresentableProgAspect> PresProgAspects = new();
    public List<PresentableProgPosition> PresProgPositions = new();

    public ProgEventResultsModel(IDescriptiveChartText descriptiveChartText,
        IProgTransitsEventApi progTransitsEventApi,
        IProgSecDirEventApi progSecDirEventApi,
        IProgSymDirEventApi progSymDirEventApi,
        IProgAspectsApi progAspectsApi,
        IProgPositionsForPresentationFactory progPosPresFactory,
        IProgAspectForPresentationFactory progAspectForPresentationFactory)
    {
        _descriptiveChartText = descriptiveChartText;
        _progTransitsEventApi = progTransitsEventApi;
        _progSecDirEventApi = progSecDirEventApi;
        _progSymDirEventApi = progSymDirEventApi;
        _progAspectsApi = progAspectsApi;
        _progPosPresFactory = progPosPresFactory;
        _progAspectPresFactory = progAspectForPresentationFactory;
        MethodName = DefineMethodName();
        Details = DefineDetails();
        EventDescription = DefineEventDescription();
        EventDateTime = DefineEventDateTime();
    }

    public string MethodName { get; }
    public string Details { get; }
    public string EventDescription { get; }
    public string EventDateTime { get; }


    public void HandleTransits()
    {
        Dictionary<ChartPoints, ProgPositions> calculatedPositions = CalculateTransits();
        _progPositions = CreateProgPositions(calculatedPositions);
        PresProgPositions = CreatePresentableProgPositions(calculatedPositions);
        HandleAspects(ProgresMethods.Transits);
    }

    public void HandleSecDir()
    {
        Dictionary<ChartPoints, ProgPositions> calculatedPositions = CalculateSecDir();
        _progPositions = CreateProgPositions(calculatedPositions);
        PresProgPositions = CreatePresentableProgPositions(calculatedPositions);
        HandleAspects(ProgresMethods.Secundary);
    }

    public void HandleSymDir()
    {
        Dictionary<ChartPoints, ProgPositions> calculatedPositions = CalculateSymDir();
        _progPositions = CreateProgPositions(calculatedPositions);
        List<PresentableProgPosition> rawPresProgPositions = CreatePresentableProgPositions(calculatedPositions);
        PresProgPositions = new();
        foreach (var presProgPos in rawPresProgPositions)
        {
            PresProgPositions.Add(new PresentableProgPosition(presProgPos.PointGlyph, presProgPos.Longitude, presProgPos.SignGlyph, "-", "-", "-"));            
        }
        HandleAspects(ProgresMethods.Symbolic);
    }

    private void HandleAspects(ProgresMethods progMethod)
    {
        CalculatedChart? radix = DataVaultCharts.Instance.GetCurrentChart();
        if (radix == null) return;
        Dictionary<ChartPoints, double> radixPositions = DefineRadixPositions(radix);
        ConfigProg configProg = CurrentConfig.Instance.GetConfigProg();
        AstroConfig astroConfig = CurrentConfig.Instance.GetConfig();
        Dictionary<AspectTypes, AspectConfigSpecs> configAspects = astroConfig.Aspects;
        List<AspectTypes> selectedAspects = (from configAspect in configAspects
            where configAspect.Value.IsUsed
            select configAspect.Key).ToList();
        double orb = DefineOrb(progMethod, configProg);
        ProgAspectsRequest request = new(radixPositions, _progPositions, selectedAspects, orb);
        ProgAspectsResponse response = _progAspectsApi.FindProgAspects(request);
        if (response.ResultCode == ResultCodes.OK)
        {
            PresProgAspects = CreatePresentableProgAspects(response.Aspects);
        }
    }

    private static Dictionary<ChartPoints, double> CreateProgPositions(
        Dictionary<ChartPoints, ProgPositions> calculatedPositions)
    {
        return calculatedPositions.ToDictionary(calcPos => calcPos.Key,
            calcPos => calcPos.Value.Longitude);
    }

    private List<PresentableProgPosition> CreatePresentableProgPositions(
        Dictionary<ChartPoints, ProgPositions> positions)
    {
        return _progPosPresFactory.CreatePresProgPos(positions);
    }

    private List<PresentableProgAspect> CreatePresentableProgAspects(List<DefinedAspect> aspects)
    {
        return _progAspectPresFactory.CreatePresProgAspect(aspects);
    }

    private static Dictionary<ChartPoints, double> DefineRadixPositions(CalculatedChart radix)
    {
        Dictionary<ChartPoints, FullPointPos> fullPositions = radix.Positions;
        return (from fullPos in fullPositions
            let cPoint = fullPos.Key
            where cPoint.GetDetails().PointCat == PointCats.Common || cPoint.GetDetails().PointCat == PointCats.Angle
            select fullPos).ToDictionary(fullPos => fullPos.Key,
            fullPos => fullPos.Value.Ecliptical.MainPosSpeed.Position);
    }

    private double DefineOrb(ProgresMethods progMethod, ConfigProg configProg)
    {
        return progMethod switch
        {
            ProgresMethods.Transits => configProg.ConfigTransits.Orb,
            ProgresMethods.Secundary => configProg.ConfigSecDir.Orb,
            ProgresMethods.Symbolic => configProg.ConfigSymDir.Orb,
            ProgresMethods.Undefined => throw new ArgumentOutOfRangeException(nameof(progMethod), progMethod, null),
            _ => throw new ArgumentOutOfRangeException(nameof(progMethod), progMethod, null)
        };
    }

    private string DefineMethodName()
    {
        var method = _dataVaultProg.CurrentProgresMethod;
        return method switch
        {
            ProgresMethods.Transits => "Transits",
            ProgresMethods.Undefined => "No progressive method defined",
            ProgresMethods.Secundary => "Secundary directions",
            ProgresMethods.Symbolic => "Symbolic directions",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private string DefineDetails()
    {
        var chart = _dataVaultCharts.GetCurrentChart();
        var config = CurrentConfig.Instance.GetConfig();
        return chart != null
            ? _descriptiveChartText.ShortDescriptiveText(config, chart.InputtedChartData.MetaData)
            : "";
    }

    private string DefineEventDescription()
    {
        return _dataVaultProg.CurrentProgEvent != null
            ? _dataVaultProg.CurrentProgEvent.Description
            : "No description for event.";
    }

    private string DefineEventDateTime()
    {
        if (_dataVaultProg.CurrentProgEvent == null) return "No date and time for event.";
        FullDateTime fullDate = _dataVaultProg.CurrentProgEvent.DateTime;
        return fullDate.DateText + "\n" + fullDate.TimeText;
    }

    private Dictionary<ChartPoints, ProgPositions> CalculateTransits()
    {
        Dictionary<ChartPoints, ProgPositions> positions = new();
        ProgEvent? progEvent = _dataVaultProg.CurrentProgEvent;
        if (progEvent == null) return positions;
        double jdUt = _dataVaultProg.CurrentProgEvent.DateTime.JulianDayForEt; // TODO check ET vs UT!
        Location location = _dataVaultProg.CurrentProgEvent.Location;
        ConfigProgTransits configTransits = CurrentConfig.Instance.GetConfigProg().ConfigTransits;
        AstroConfig configRadix = CurrentConfig.Instance.GetConfig();
        TransitsEventRequest request = new(jdUt, location, configTransits, configRadix.Ayanamsha,
            configRadix.ObserverPosition);
        ProgRealPointsResponse response = _progTransitsEventApi.CalcTransits(request);
        if (response.ResultCode == 0) positions = response.Positions;
        else
        {
            Log.Error("Resultcode when calculating transits {ResultCode}", response.ResultCode);
        }
        return positions;
    }

    private Dictionary<ChartPoints, ProgPositions> CalculateSecDir()
    {
        Dictionary<ChartPoints, ProgPositions> positions = new();
        ProgEvent? progEvent = _dataVaultProg.CurrentProgEvent;
        if (progEvent == null) return positions;
        double jdEvent = _dataVaultProg.CurrentProgEvent.DateTime.JulianDayForEt; // TODO check ET vs UT!
        double jdRadix = _dataVaultCharts.GetCurrentChart().InputtedChartData.FullDateTime.JulianDayForEt;
        Location location = _dataVaultProg.CurrentProgEvent.Location;
        ConfigProgSecDir configSecDir = CurrentConfig.Instance.GetConfigProg().ConfigSecDir;
        AstroConfig configRadix = CurrentConfig.Instance.GetConfig();
        SecDirEventRequest request = new(jdRadix, jdEvent, location, configSecDir, configRadix.Ayanamsha,
            configRadix.ObserverPosition);
        ProgRealPointsResponse response = _progSecDirEventApi.CalcSecDir(request);
        if (response.ResultCode == 0) positions = response.Positions;
        else
        {
            Log.Error("Resultcode when calculating secundary directions {ResultCode}", response.ResultCode);
        }
        return positions;
    }

    private Dictionary<ChartPoints, ProgPositions> CalculateSymDir()
    {
        Dictionary<ChartPoints, ProgPositions> positions = new();
        ProgEvent? progEvent = _dataVaultProg.CurrentProgEvent;
        if (progEvent == null) return positions;
        double jdEvent = _dataVaultProg.CurrentProgEvent.DateTime.JulianDayForEt; // TODO check ET vs UT!
        double jdRadix = _dataVaultCharts.GetCurrentChart().InputtedChartData.FullDateTime.JulianDayForEt;
        Location location = _dataVaultProg.CurrentProgEvent.Location;
        ConfigProgSymDir configSymDir = CurrentConfig.Instance.GetConfigProg().ConfigSymDir;
        AstroConfig configRadix = CurrentConfig.Instance.GetConfig();
        var fullPositions = DataVaultCharts.Instance.GetCurrentChart().Positions;
        Dictionary<ChartPoints, double> radixPositions = 
            fullPositions.ToDictionary(fullPos => fullPos.Key, 
                fullPos => fullPos.Value.Ecliptical.MainPosSpeed.Position);
        SymDirEventRequest request = new(jdRadix, jdEvent, configSymDir, radixPositions);
        ProgRealPointsResponse response = _progSymDirEventApi.CalcSymDir(request);
        if (response.ResultCode == 0) positions = response.Positions;
        else
        {
            Log.Error("Resultcode when calculating symbolic directions {ResultCode}", response.ResultCode);
        }
        return positions;
    }
    
    
}