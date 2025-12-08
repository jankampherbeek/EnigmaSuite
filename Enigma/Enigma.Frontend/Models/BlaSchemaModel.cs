// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using Enigma.Api.Calc;
using Enigma.Api.Configuration;
using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;
using PresentableBlaPosition = Enigma.Frontend.Ui.PresentationFactories.PresentableBlaPosition;
using PresentableDispositorCounts = Enigma.Frontend.Ui.PresentationFactories.PresentableDispositorCounts;

namespace Enigma.Frontend.Ui.Models;

/// <summary>
/// Model for the calculation of BLA items
/// </summary>
public class BlaSchemaModel(IConfigurationApi configApi, IChartAllPositionsApi chartsApi)
{
    public int HouseIndex { get; }
    public int ApogeeIndex { get; }
    public string ChartName { get; set; }
    private List<HouseSystems> _houseSystems;
    private List<string> _houseSystemNames;
    private List<ApogeeTypes> _apogeeTypes;
    private List<string> _apogeeTypeNames;
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
    private List<PresentableBlaPosition> _blaPositionsForPresentation;
    private List<PresentableHousePosition> _housePositionsForPresentation;
    
    private CrossElementPresFactory _crossElementPresFactory;
    private QuadrantPresFactory _quadrantPresFactory;
    private DispositorPresFactory _dispositorPresFactory;
    private BlaDetailsPresFactory _blaDetailsPresFactory;
    private BlaCyclesPresFactory _blaCyclesPresFactory;
    private BlaReinforcementsPresFactory _blaReinforcementsPresFactory;
    private BlaReceptionsPresFactory _blaReceptionsPresFactory;
    private BlaPositionsPresFactory _blaPositionsPresFactory;

    public void PrepareBlaSchema()
    {
        _blaPositionsPresFactory = new BlaPositionsPresFactory();
        _crossElementPresFactory = new CrossElementPresFactory();
        _quadrantPresFactory = new QuadrantPresFactory();
        _dispositorPresFactory = new DispositorPresFactory();
        _blaDetailsPresFactory = new BlaDetailsPresFactory();
        _blaCyclesPresFactory = new BlaCyclesPresFactory();        
        _blaReinforcementsPresFactory = new BlaReinforcementsPresFactory();
        _blaReceptionsPresFactory = new BlaReceptionsPresFactory();        
        
        CreateHouseSystems();
        CreateApogeeTypes();
    }
    
    public void CreateBlaSchema(bool useChiron, bool useCeres, bool useTrueNode, bool useDecanates, int houseSystemIndex, int apogeeIndex)
    {
        var cpRequest = CreateCelPointsRequest(useChiron, useCeres, useTrueNode, houseSystemIndex, apogeeIndex);
        _blaPositions = chartsApi.GetChart(cpRequest);
        _chart = GetChartLongitudes();
        _orchestrator = new BlaSchemaOrchestrator(_chart, useDecanates);
        var signCounts = _orchestrator.GetSignCounts();
        var houseCounts = _orchestrator.GetHouseCounts();
        var planetsInHouses = _orchestrator.GetPlanetsInHouses();
        var signOnCusps = _orchestrator.GetSignsOnCusps();
        

        DefineBlaPositions();
        DefineHousePositions();
        DefineCrossElementCounts(signCounts, houseCounts, planetsInHouses, signOnCusps);
        DefineQuadrantCounts(houseCounts);
        DefineDispositorLines(useDecanates);
        DefineBlaDetailLines();
        DefineCycles();
        DefineShortenedCycles();
        DefineFactorsInOwnSigns();
        DefineFactorsInOwnHouses();
        DefineFactorsInOwnMundaneHouses();
        DefineHouseLordsInAnalogSigns();
        DefinePairsAnalogHouseSign();
        DefineReceptionsInSigns();
        DefineReceptionsInHouses();
        DefineReceptionInMundaneHouses();
    }

    private void DefineBlaPositions()
    {
        var pointDetails = _orchestrator.GetPointDetails();
        _blaPositionsForPresentation = _blaPositionsPresFactory.GetBlaPositions(pointDetails);
    }

    private void DefineHousePositions()
    {
        var houseDetails = _orchestrator.GetHouseDetails();
        _housePositionsForPresentation = _blaPositionsPresFactory.GetHousePositions(houseDetails);   
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

    private void DefineDispositorLines(bool useDecanates)
    {
        var dLines = _orchestrator.GetDispositors(useDecanates);
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

    public List<string> GetHouseSystemNames()
    {
        return _houseSystemNames;
    }

    public List<string> GetApogeeTypeNames()
    {
        return _apogeeTypeNames;
    }
    
    public List<PresentableBlaPosition> GetBlaPositions()
    {
        return _blaPositionsForPresentation;   
    }

    public List<PresentableHousePosition> GetHousePositions()
    {
        return _housePositionsForPresentation;  
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


    private void CreateHouseSystems()
    {
        _houseSystems =
        [
            HouseSystems.Alcabitius,
            HouseSystems.Campanus,
            HouseSystems.Koch,
            HouseSystems.Krusinski,
            HouseSystems.Placidus,
            HouseSystems.Porphyri,
            HouseSystems.Regiomontanus,
            HouseSystems.TopoCentric
        ];
        _houseSystemNames =
        [
            "Alcabitius",
            "Campanus",
            "Koch",
            "Krusinski",
            "Placidus",
            "Porphyri",
            "Regiomontanus",
            "Topo-centric"
        ];
    }

    private void CreateApogeeTypes()
    {
        _apogeeTypes =
        [
            ApogeeTypes.Corrected,
            ApogeeTypes.Duval,
            ApogeeTypes.Interpolated
        ];
        _apogeeTypeNames =
        [
            "Corrected SE",
            "Corrected Duval",
            "Interpolated"
        ];
    }
    
    private ChartLongitudes GetChartLongitudes()
    {
        if (_blaPositions == null) throw new InvalidOperationException("No data available for ILA");
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

    private CelPointsRequest CreateCelPointsRequest(bool useChiron, bool useCeres, 
        bool useTrueNode, int houseSystemIndex, int apogeeIndex)
    {
        var calcPrefs = DefineCalcPrefs( useChiron, useCeres, useTrueNode, houseSystemIndex, apogeeIndex);
        var chart = DataVaultCharts.Instance.GetCurrentChart() ?? throw new InvalidOperationException();
        ChartName = chart.InputtedChartData.MetaData.Name;
        var request = new CelPointsRequest(chart.InputtedChartData.FullDateTime.JulianDayForEt, chart.InputtedChartData.Location, calcPrefs);
        return request;
    }
    
    private CalculationPreferences DefineCalcPrefs(bool useChiron, bool useCeres, 
        bool useTrueNode, int houseSystemIndex, int apogeeIndex)
    {
        var selectedHouseSystem = _houseSystems[houseSystemIndex];
        var config = configApi.GetCurrentConfiguration();
        var chartPoints = DefineChartPoints(useChiron, useCeres, useTrueNode);
        const ZodiacTypes zodiacType = ZodiacTypes.Tropical;
        const Ayanamshas ayanamsha = Ayanamshas.None;
        const CoordinateSystems coordSystem = CoordinateSystems.Ecliptical;
        var observerPos = config.ObserverPosition;
        const ProjectionTypes projectionType = ProjectionTypes.TwoDimensional;
        
        var apogeeType = _apogeeTypes[apogeeIndex];
        var oscillate = useTrueNode;
        CalculationPreferences calcPrefs = new(chartPoints, zodiacType, ayanamsha, coordSystem, observerPos, projectionType,selectedHouseSystem, apogeeType, oscillate);
        return calcPrefs;
    }


    private static List<ChartPoints> DefineChartPoints(bool useChiron, bool useCeres, bool useTrueNode)
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
        var node = ChartPoints.NorthNode;
        if (useTrueNode) node = ChartPoints.TrueNode;
        points.Add(node);
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
        if (useCeres) points.Add(ChartPoints.Ceres);
        
        return points;
    }
    
}