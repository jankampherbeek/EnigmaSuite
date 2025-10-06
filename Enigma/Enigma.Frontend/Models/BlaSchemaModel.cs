// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using Enigma.Api.Calc;
using Enigma.Api.Configuration;
using Enigma.Api.Slices;
using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;
using PresentableDispositorCounts = Enigma.Frontend.Ui.PresentationFactories.PresentableDispositorCounts;

namespace Enigma.Frontend.Ui.Models;

/// <summary>
/// Model for the calculation of BLA items
/// </summary>
public class BlaSchemaModel(IConfigurationApi configApi, IChartAllPositionsApi chartsApi)
{

    private ChartLongitudes _chart;
    private Dictionary<ChartPoints, FullPointPos> ? _blaPositions;
    private BlaSchemaOrchestrator _orchestrator;
    private List<PresentableCrossElementsCount> _crossesCounts;
    private List<PresentableCrossElementsCount> _elementsCounts;
    private List<PresentableQuadrantCount> _quadrantCounts;
    private List<PresentableDispositorCounts> _dispositors;
    private List<PresentableBlaDetails> _blaDetails;
    private BlaDetailsData _blaDetailsData;
    private List<PresentableBlaCycle> _blaCycles;
    private List<PresentableBlaCycle> _blaShortenedCycles;
    private List<PresenTableReinforcementFactor> _blaFactorsInOwnSigns;
    private List<PresenTableReinforcementFactor> _blaFactorsInOwnHouses;
    private List<PresenTableReinforcementFactor> _blaFactorsInOwnMundaneHouses;
    private List<PresenTableReinforcementFactor> _blaHouseLordsInAnalogSigns;
    private List<PresentablePairAnalogHouseSign> _blaPairsAnalogHouseSigns;
    private List<PresentableReception> _receptionsInSigns;
    private List<PresentableReception> _receptionsInHouses;
    private List<PresentableReception> _receptionsInMundaneHouses;
    
    private CrossElementPresFactory _crossElementPresFactory;
    private QuadrantPresFactory _quadrantPresFactory;
    private DispositorPresFactory _dispositorPresFactory;
    private BlaDetailsPresFactory _blaDetailsPresFactory;
    private BlaCyclesPresFactory _blaCyclesPresFactory;
    private BlaReinforcementsPresFactory _blaReinforcementsPresFactory;
    private BlaReceptionsPresFactory _blaReceptionsPresFactory;
    
    public void CreateDataForBla(HouseSystems selectedHouseSystem, bool useChiron, bool useEris)
    {
        var cpRequest = CreateCelPointsRequest(selectedHouseSystem, useChiron, useEris);
        _blaPositions = chartsApi.GetChart(cpRequest);
        _chart = GetChartLongitudes();
        _orchestrator = new BlaSchemaOrchestrator(_chart);
        var signCounts = _orchestrator.GetSignCounts();
        var houseCounts = _orchestrator.GetHouseCounts();
        var planetsInHouses = _orchestrator.GetPlanetsInHouses();
        var signOnCusps = _orchestrator.GetSignsOnCusps();
      //  var dispositorLines = _orchestrator.GetDispositors();
        
        _crossElementPresFactory = new CrossElementPresFactory();
        DefineCrossElementCounts(signCounts, houseCounts, planetsInHouses, signOnCusps);
        _quadrantPresFactory = new QuadrantPresFactory();
        DefineQuadrantCounts(houseCounts);
        _dispositorPresFactory = new DispositorPresFactory();
        DefineDispositorLines();
        _blaDetailsPresFactory = new BlaDetailsPresFactory();
        DefineBlaDetailLines();
        _blaCyclesPresFactory = new BlaCyclesPresFactory();
        DefineCycles();
        DefineShortenedCycles();
        _blaReinforcementsPresFactory = new BlaReinforcementsPresFactory();
        DefineFactorsInOwnSigns();
        DefineFactorsInOwnHouses();
        DefineFactorsInOwnMundaneHouses();
        DefineHouseLordsInAnalogSigns();
        DefinePairsAnalogHouseSign();
        _blaReceptionsPresFactory = new BlaReceptionsPresFactory();
        DefineReceptionsInSigns();
        DefineReceptionsInHouses();
        DefineReceptionInMundaneHouses();
    }

    private void DefineCrossElementCounts(Dictionary<int, int> signCounts, Dictionary<int, int> houseCounts,Dictionary<ChartPoints, int> planetsInHouses, Dictionary<int, int> signOnCusps)
    {
        var ceCounts = _orchestrator.GetCrossELementCounts();
        _crossesCounts = _crossElementPresFactory.CreatePresCrossesCounts(signCounts, houseCounts, planetsInHouses, signOnCusps);
        _elementsCounts = _crossElementPresFactory.CreatePresElementsCounts(signCounts, planetsInHouses, signOnCusps);
    }

    private void DefineQuadrantCounts(Dictionary<int, int> houseCounts)
    {
        var qCounts = _orchestrator.GetQuadrantCounts();
        _quadrantCounts = _quadrantPresFactory.CreateQuadrantCounts(qCounts);
    }

    private void DefineDispositorLines()
    {
        var dLines = _orchestrator.GetDispositors();
        _dispositors = _dispositorPresFactory.CreatePresDispositorCounts(dLines);
    }

    private void DefineBlaDetailLines()
    {
        var bladLines = _orchestrator.GetDetails();
        _blaDetails = _blaDetailsPresFactory.CreateBlaDetails(bladLines);
    }

    private void DefineCycles()
    {
        var cycles = _orchestrator.GetCycles();
        _blaCycles = _blaCyclesPresFactory.CreateBlaCycles(cycles);
    }

    private void DefineShortenedCycles()
    {
        var shortenedCycles = _orchestrator.GetShortenedCycles();
        _blaShortenedCycles = _blaCyclesPresFactory.CreateShortenedCycles(shortenedCycles);   
    }

    private void DefineFactorsInOwnSigns()
    {
        var factors = _orchestrator.GetPointsInOwnSign();
        _blaFactorsInOwnSigns = _blaReinforcementsPresFactory.CreateReinforcementFactors(factors);
    }

    private void DefineFactorsInOwnHouses()
    {
        var factors = _orchestrator.GetPointsInOwnHouse();
        _blaFactorsInOwnHouses = _blaReinforcementsPresFactory.CreateReinforcementFactors(factors);   
    }

    private void DefineFactorsInOwnMundaneHouses()
    {
        var factors = _orchestrator.GetPointsInOwnMundaneHouse();
        _blaFactorsInOwnMundaneHouses = _blaReinforcementsPresFactory.CreateReinforcementFactors(factors);  
    }

    private void DefineHouseLordsInAnalogSigns()
    {
        var factors = _orchestrator.GetRulersInHouseAsSign();
        _blaHouseLordsInAnalogSigns = _blaReinforcementsPresFactory.CreateReinforcementFactors(factors); 
    }

    private void DefinePairsAnalogHouseSign()
    {
        var pairs = _orchestrator.GetFactorPairsAnalogHouseSigns();
        _blaPairsAnalogHouseSigns = _blaReinforcementsPresFactory.CreatePairsWithAnalogHouseSigns(pairs);
    }

    private void DefineReceptionsInSigns()
    {
        var rec = _orchestrator.GetReceptionsInSigns();
        _receptionsInSigns = _blaReceptionsPresFactory.CreateReceptionsInSigns(rec);
    }


    private void DefineReceptionsInHouses()
    {
        var rec = _orchestrator.GetReceptionsInHouses();
        _receptionsInHouses = _blaReceptionsPresFactory.CreateReceptionsInHouses(rec);   
    }

    private void DefineReceptionInMundaneHouses()
    {
        var rec = _orchestrator.GetReceptionsInMundaneHouses();
        _receptionsInMundaneHouses = _blaReceptionsPresFactory.CreateReceptionsInHouses(rec);
    }
    
    
    public List<PresentableCrossElementsCount> GetCrossesCounts()
    {
        return _crossesCounts;
    }

    public List<PresentableCrossElementsCount> GetElementsCounts()
    {
        return _elementsCounts;
    }

    public List<PresentableQuadrantCount> GetQuadrantCounts()
    {
        return _quadrantCounts;
    }

    public List<PresentableDispositorCounts> GetDispositors()
    {
        return _dispositors;
    }

    public List<PresentableBlaDetails> GetBlaDetails()
    {
        return _blaDetails;
    }

    public List<PresentableBlaCycle> GetBlaCycles()
    {
        return _blaCycles;
    }

    public List<PresentableBlaCycle> GetBlaShortenedCycles()
    {
        return _blaShortenedCycles;   
    }

    public List<PresenTableReinforcementFactor> GetBlaFactorsInOwnSigns()
    {
        return _blaFactorsInOwnSigns;  
    }
    
    public List<PresenTableReinforcementFactor> GetBlaFactorsInOwnHouses()
    {
        return _blaFactorsInOwnHouses;  
    }

    public List<PresenTableReinforcementFactor> GetBlaFactorsInOwnMundaneHouses()
    {
        return _blaFactorsInOwnMundaneHouses; 
    }

    public List<PresenTableReinforcementFactor> GetBlaHouseLordsInAnalogSigns()
    {
        return _blaHouseLordsInAnalogSigns;
    }

    public List<PresentablePairAnalogHouseSign> GetBlaPairsAnalogHouseSigns()
    {
        return _blaPairsAnalogHouseSigns;
    }

    public List<PresentableReception> GetReceptionsInSigns()
    {
        return _receptionsInSigns;
    }

    public List<PresentableReception> GetReceptionsInHouses()
    {
        return _receptionsInHouses;
    }

    public List<PresentableReception> GetReceptionsInMundaneHouses()
    {
        return _receptionsInMundaneHouses;
    }
    
    
    
    private ChartLongitudes GetChartLongitudes()
    {
        if (_blaPositions == null) throw new InvalidOperationException("No data available for BLA");
        var currentChart = DataVaultCharts.Instance.GetCurrentChart();
        if (currentChart == null) throw new InvalidOperationException("No current chart available");
        var pointLongitudes = new Dictionary<ChartPoints, double>();
        var houseLongitudes = new Dictionary<int, double>();
        foreach (var pointPos in _blaPositions)
        {
            if (pointPos.Key.GetDetails().PointCat == PointCats.Common ||
                pointPos.Key.GetDetails().PointCat == PointCats.Angle ||
                pointPos.Key.GetDetails().PointCat == PointCats.Lots)
            {
                pointLongitudes.Add(pointPos.Key, pointPos.Value.Ecliptical.MainPosSpeed.Position);
            } else if (pointPos.Key.GetDetails().PointCat == PointCats.Cusp)
            {
                houseLongitudes.Add(pointPos.Key.GetDetails().CalcId,  pointPos.Value.Ecliptical.MainPosSpeed.Position);
            }
        }
        return new ChartLongitudes(pointLongitudes, houseLongitudes);
    }

    private CelPointsRequest CreateCelPointsRequest(HouseSystems selectedHouseSystem, bool useChiron, bool useEris)
    {
        var calcPrefs = DefineCalcPrefs(selectedHouseSystem, useChiron, useEris);
        var chart = DataVaultCharts.Instance.GetCurrentChart() ?? throw new InvalidOperationException();
        var request = new CelPointsRequest(chart.InputtedChartData.FullDateTime.JulianDayForEt, chart.InputtedChartData.Location, calcPrefs);
        return request;
    }
    
    private CalculationPreferences DefineCalcPrefs(HouseSystems selectedHouseSystem, bool useChiron, bool useEris)
    {
        var config = configApi.GetCurrentConfiguration();
        var chartPoints = DefineChartPoints(useChiron, useEris);
        const ZodiacTypes zodiacType = ZodiacTypes.Tropical;
        const Ayanamshas ayanamsha = Ayanamshas.None;
        const CoordinateSystems coordSystem = CoordinateSystems.Ecliptical;
        var observerPos = config.ObserverPosition;
        const ProjectionTypes projectionType = ProjectionTypes.TwoDimensional;
        var apogeeType = config.ApogeeType;
        var oscillate = config.OscillateNodes;
        CalculationPreferences calcPrefs = new(chartPoints, zodiacType, ayanamsha, coordSystem, observerPos, projectionType,selectedHouseSystem, apogeeType, oscillate);
        return calcPrefs;
    }


    private static List<ChartPoints> DefineChartPoints(bool useChiron, bool useEris)
    {
        var points = new List<ChartPoints>();
        points.Add(ChartPoints.Sun);
        points.Add(ChartPoints.Moon);
        points.Add(ChartPoints.Mercury);
        points.Add(ChartPoints.Venus);
        points.Add(ChartPoints.Mars);
        points.Add(ChartPoints.Jupiter);
        points.Add(ChartPoints.Saturn);
        points.Add(ChartPoints.Uranus);
        points.Add(ChartPoints.Neptune);
        points.Add(ChartPoints.Pluto);
        points.Add(ChartPoints.NorthNode);     // TODO add support for true north and south node
        points.Add(ChartPoints.SouthNode);
        points.Add(ChartPoints.PersephoneCarteret);
        points.Add(ChartPoints.VulcanusCarteret);
        points.Add(ChartPoints.ApogeeMean);
        points.Add(ChartPoints.ApogeeCorrected);
        points.Add(ChartPoints.BlackSun);
        points.Add(ChartPoints.Diamond);
        points.Add(ChartPoints.Priapus);   
        points.Add(ChartPoints.PriapusCorrected);
        points.Add(ChartPoints.Dragon);
        points.Add(ChartPoints.Beast);
        points.Add(ChartPoints.FortunaNoSect);
        if (useChiron) points.Add(ChartPoints.Chiron);
        if (useEris) points.Add(ChartPoints.Eris);
        
        return points;
    }
    
}