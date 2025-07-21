// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System;
using System.Linq;
using Enigma.Api.Analysis;
using Enigma.Api.Slices;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Serilog;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for solar results</summary>
public sealed class SolarResultsModel(
    SolarService solarService,
    ICelPointForDataGridFactory celPointForDataGridFactory,
    IAspectForDataGridFactory aspectForDataGridFactory,
    IProgPositionsForPresentationFactory progPositionsForPresentationFactory,
    IDescriptiveChartText descriptiveChartText,
    ICalculationPreferencesCreator prefsCreator,
    IProgAspectsApi progAspectsApi,
    IProgPositionsForPresentationFactory progPosPresFactory,
    IProgAspectForPresentationFactory progAspectForPresentationFactory)
{
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;
    private readonly SolarService _solarService = solarService;
    private readonly ICelPointForDataGridFactory _celPointForDataGridFactory = celPointForDataGridFactory;
    private readonly IAspectForDataGridFactory _aspectForDataGridFactory = aspectForDataGridFactory;
    private readonly IDescriptiveChartText _descriptiveChartText = descriptiveChartText;
    private readonly ICalculationPreferencesCreator _prefsCreator = prefsCreator;
    private Dictionary<ChartPoints, FullPointPos>? _solarChart;
    private int _solarAge;
    private Location? _solarLocation;
    public double SolarOrb { get; } = 2.5; // todo use configuration to define default orb for solar
    
    public List<PresentableProgAspect> SolarAspects = new();

   
    
    
    /// <summary>
    /// Get the current chart name
    /// </summary>
    /// <returns>Name of the current chart or empty string if no chart</returns>
    public string GetChartName()
    {
        var currentChart = _dataVaultCharts.GetCurrentChart();
        return currentChart?.InputtedChartData.MetaData.Name ?? "";
    }

    /// <summary>
    /// Get the solar age used for calculation
    /// </summary>
    /// <returns>The age used for solar return calculation</returns>
    public int GetSolarAge()
    {
        return _solarAge;
    }

    /// <summary>
    /// Get the solar location used for calculation
    /// </summary>
    /// <returns>The location used for solar return calculation</returns>
    public Location? GetSolarLocation()
    {
        return _solarLocation;
    }

    /// <summary>
    /// Get solar positions in presentable format
    /// </summary>
    /// <returns>List of presentable solar positions</returns>
    public List<PresentableProgPosition> GetSolarPositions()
    {
        _solarChart = DataVaultProg.Instance.GetCurrentSolar();
        if (_solarChart == null)
        {
            Log.Warning("SolarResultsModel.GetSolarPositions: No solar chart calculated");
            return new List<PresentableProgPosition>();
        }

        return progPositionsForPresentationFactory.CreatePresProgPos(_solarChart);
    }

    // /// <summary>
    // /// Get solar aspects with radix in presentable format
    // /// </summary>
    // /// <returns>List of presentable solar aspects</returns>
    // public List<PresentableProgAspect> GetSolarAspects()
    // {
    //     if (_solarChart == null)
    //     {
    //         Log.Warning("SolarResultsModel.GetSolarAspects: No solar chart calculated");
    //         return new List<PresentableProgAspect>();
    //     }
    //
    //     var radixChart = _dataVaultCharts.GetCurrentChart();
    //     if (radixChart == null)
    //     {
    //         Log.Warning("SolarResultsModel.GetSolarAspects: No radix chart available");
    //         return new List<PresentableProgAspect>();
    //     }
    //
    //     // Create aspect request for solar vs radix
    //     var config = CurrentConfig.Instance.GetConfig();
    //
    //
    //     // Create aspect request comparing solar chart with radix chart
    //     var aspectRequest = new AspectRequest(_solarChart, config);
    //
    //     // TODO: Implement solar aspects calculation using the API
    //     // For now, return empty list - this would need to be implemented in the API layer
    //     return new List<PresentableProgAspect>();
    // }

    /// <summary>
    /// Create solar chart data for aspect calculations
    /// </summary>
    /// <param name="radixChart">The radix chart</param>
    /// <returns>ChartData for the solar return</returns>
    private ChartData CreateSolarChartData(CalculatedChart radixChart)
    {
        var birthJd = radixChart.InputtedChartData.FullDateTime.JulianDayForEt;
        var solarReturnJd = birthJd + (_solarAge + 1) * EnigmaConstants.TROPICAL_YEAR_IN_DAYS;

        // Create solar chart metadata
        var solarMetaData = new MetaData(
            $"{radixChart.InputtedChartData.MetaData.Name} Solar Return Age {_solarAge}",
            $"Solar return chart for {radixChart.InputtedChartData.MetaData.Name} at age {_solarAge}",
            radixChart.InputtedChartData.MetaData.Source,
            radixChart.InputtedChartData.MetaData.LocationName,
            radixChart.InputtedChartData.MetaData.ChartCategory,
            radixChart.InputtedChartData.MetaData.RoddenRating
        );

        // Create solar date/time
        var solarDateTime = new FullDateTime(
            $"{DateTime.Now.Year:D4}/{DateTime.Now.Month:D2}/{DateTime.Now.Day:D2}",
            $"{DateTime.Now.Hour:F2}",
            solarReturnJd
        );

        return new ChartData(
            radixChart.InputtedChartData.Id,
            solarMetaData,
            _solarLocation ?? radixChart.InputtedChartData.Location,
            solarDateTime
        );
    }

    public void HandleAspects(double orb)
    {
        CalculatedChart? radix = DataVaultCharts.Instance.GetCurrentChart();
        if (radix == null) return;
        Dictionary<ChartPoints, double> radixPositions = DefineRadixPositions(radix.Positions);
        Dictionary<ChartPoints, double> solarPositions = DefineRadixPositions(_solarChart);
     //   ConfigProg configProg = CurrentConfig.Instance.GetConfigProg();
        AstroConfig astroConfig = CurrentConfig.Instance.GetConfig();
        Dictionary<AspectTypes, AspectConfigSpecs> configAspects = astroConfig.Aspects;
        List<AspectTypes> selectedAspects = (from configAspect in configAspects
            where configAspect.Value.IsUsed
            select configAspect.Key).ToList();
        ProgAspectsRequest request = new(radixPositions, solarPositions, selectedAspects, orb);
        ProgAspectsResponse response = progAspectsApi.FindProgAspects(request);
        if (response.ResultCode == ResultCodes.OK)
        {
            SolarAspects = CreatePresentableProgAspects(response.Aspects);
        }
    }

    private List<PresentableProgAspect> CreatePresentableProgAspects(List<DefinedAspect> aspects)
    {
        return progAspectForPresentationFactory.CreatePresProgAspect(aspects);
    }
    

    
    private static Dictionary<ChartPoints, double> DefineRadixPositions(Dictionary<ChartPoints, FullPointPos> positions)
    {
    //    Dictionary<ChartPoints, FullPointPos> fullPositions = radix.Positions;
        return (from fullPos in positions
            let cPoint = fullPos.Key
            where cPoint.GetDetails().PointCat == PointCats.Common || cPoint.GetDetails().PointCat == PointCats.Angle
            select fullPos).ToDictionary(fullPos => fullPos.Key,
            fullPos => fullPos.Value.Ecliptical.MainPosSpeed.Position);
    }
    
    private List<PresentableProgPosition> CreatePresentableProgPositions(
        Dictionary<ChartPoints, ProgPositions> positions)
    {
        return progPosPresFactory.CreatePresProgPos(positions);
    }
    
    
    /// <summary>
    /// Get the calculated solar chart
    /// </summary>
    /// <returns>Solar chart positions or null if not calculated</returns>
    public Dictionary<ChartPoints, FullPointPos>? GetSolarChart()
    {
        return _solarChart;
    }
}