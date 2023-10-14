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
    private readonly IDescriptiveChartText _descriptiveChartText;
    private readonly ICalcTransitsEventApi _calcTransitsEventApi;
    private readonly ICalcSecDirEventApi _calcSecDirEventApi;
    private readonly IProgAspectsApi _progAspectsApi;
    private readonly IProgPositionsForPresentationFactory _progPosPresFactory;
    private readonly IProgAspectForPresentationFactory _progAspectPresFactory;
    public string MethodName { get; set; }
    public string Details { get; set; }
    public string EventDescription { get; set; }
    public string EventDateTime { get; set; }
    private Dictionary<ChartPoints, double> _progPositions = new();
    public List<PresentableProgAspect> PresProgAspects;
    public List<PresentableProgPosition> PresProgPositions;
    private readonly DataVault _dataVault = DataVault.Instance;
    
    public ProgEventResultsModel(IDescriptiveChartText descriptiveChartText, 
        ICalcTransitsEventApi calcTransitsEventApi,
        ICalcSecDirEventApi calcSecDirEventApi,
        IProgAspectsApi progAspectsApi,
        IProgPositionsForPresentationFactory progPosPresFactory,
        IProgAspectForPresentationFactory progAspectForPresentationFactory)
    {
        _descriptiveChartText = descriptiveChartText;
        _calcTransitsEventApi = calcTransitsEventApi;
        _calcSecDirEventApi = calcSecDirEventApi;
        _progAspectsApi = progAspectsApi;
        _progPosPresFactory = progPosPresFactory;
        _progAspectPresFactory = progAspectForPresentationFactory;
        MethodName = DefineMethodName();
        Details = DefineDetails();
        EventDescription = DefineEventDescription();
        EventDateTime = DefineEventDateTime();
    }


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
     
     
     private void HandleAspects(ProgresMethods progMethod)
     {
         CalculatedChart? radix = DataVault.Instance.GetCurrentChart();
         if (radix == null) return;
         Dictionary<ChartPoints, double> radixPositions = DefineRadixPositions(radix);
         ConfigProg configProg = CurrentConfig.Instance.GetConfigProg();
         AstroConfig astroConfig = CurrentConfig.Instance.GetConfig();
         Dictionary<AspectTypes, AspectConfigSpecs> configAspects = astroConfig.Aspects;
         List<AspectTypes> selectedAspects = (from configAspect in configAspects 
             where configAspect.Value.IsUsed select configAspect.Key).ToList();
         double orb = DefineOrb(progMethod, configProg); 
         ProgAspectsRequest request = new(radixPositions, _progPositions, selectedAspects, orb);
         ProgAspectsResponse response = _progAspectsApi.FindProgAspects(request);
         if (response.ResultCode == ResultCodes.OK)
         {
             PresProgAspects = CreatePresentableProgAspects(response.Aspects);
         }
     }
     
     private Dictionary<ChartPoints, double> CreateProgPositions(Dictionary<ChartPoints, ProgPositions> calculatedPositions)
     {
         return calculatedPositions.ToDictionary(calcPos => calcPos.Key, 
             calcPos => calcPos.Value.Longitude);
     }
     
     private List<PresentableProgPosition> CreatePresentableProgPositions(Dictionary<ChartPoints, ProgPositions> positions)
     {
         return _progPosPresFactory.CreatePresProgPos(positions);
     }

     private List<PresentableProgAspect> CreatePresentableProgAspects(List<DefinedAspect> aspects)
     {
         return _progAspectPresFactory.CreatePresProgAspect(aspects);
     }
     
    private Dictionary<ChartPoints, double> DefineRadixPositions(CalculatedChart radix)
    {
        Dictionary<ChartPoints, FullPointPos> fullPositions = radix.Positions;
        return (from fullPos in fullPositions let cPoint = fullPos.Key 
            where cPoint.GetDetails().PointCat == PointCats.Common || cPoint.GetDetails().PointCat == PointCats.Angle 
            select fullPos).ToDictionary(fullPos => fullPos.Key, 
            fullPos => fullPos.Value.Ecliptical.MainPosSpeed.Position);
    }

    private double DefineOrb(ProgresMethods progMethod,  ConfigProg configProg)
    {
        return progMethod switch
        {
            ProgresMethods.Transits => configProg.ConfigTransits.Orb,
            ProgresMethods.Primary => configProg.ConfigPrimDir.Orb,
            ProgresMethods.Secundary => configProg.ConfigSecDir.Orb,
            ProgresMethods.Symbolic => configProg.ConfigSymDir.Orb,
            ProgresMethods.Solar => 0.0,                // No orb for solar
            ProgresMethods.Undefined => throw new ArgumentOutOfRangeException(nameof(progMethod), progMethod, null),
            _ => throw new ArgumentOutOfRangeException(nameof(progMethod), progMethod, null)
        };
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
        ProgRealPointsResponse response = _calcTransitsEventApi.CalcTransits(request);
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
        ProgEvent? progEvent = _dataVault.CurrentProgEvent;
        if (progEvent == null) return positions;
        double jdEvent = _dataVault.CurrentProgEvent.DateTime.JulianDayForEt;   // TODO check ET vs UT!
        double jdRadix = _dataVault.GetCurrentChart().InputtedChartData.FullDateTime.JulianDayForEt;
        Location location = _dataVault.CurrentProgEvent.Location;
        ConfigProgSecDir configSecDir = CurrentConfig.Instance.GetConfigProg().ConfigSecDir;
        AstroConfig configRadix = CurrentConfig.Instance.GetConfig();
        SecDirEventRequest request = new(jdRadix, jdEvent, location, configSecDir, configRadix.Ayanamsha, configRadix.ObserverPosition);
        ProgRealPointsResponse response = _calcSecDirEventApi.CalcSecDir(request);
        if (response.ResultCode == 0) positions = response.Positions;
        else
        {
            Log.Error("Resultcode when calculating secundary directions {ResultCode}", response.ResultCode);
        }
        return positions;
    }
    
    
}