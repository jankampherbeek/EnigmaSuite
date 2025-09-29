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
    private CrossElementPresFactory _crossElementPresFactory;
    
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
        _crossElementPresFactory = new CrossElementPresFactory();
        DefineCrossElementCounts(signCounts, houseCounts, planetsInHouses, signOnCusps);
    }

    private void DefineCrossElementCounts(Dictionary<int, int> signCounts, Dictionary<int, int> houseCounts,Dictionary<ChartPoints, int> planetsInHouses, Dictionary<int, int> signOnCusps)
    {
        var ceCounts = _orchestrator.GetCrossELementCounts();
        _crossesCounts = _crossElementPresFactory.CreatePresCrossesCounts(signCounts, houseCounts, planetsInHouses, signOnCusps);
        _elementsCounts = _crossElementPresFactory.CreatePresElementsCounts(signCounts, planetsInHouses, signOnCusps);
    }


    public List<PresentableCrossElementsCount> GetCrossesCounts()
    {
        return _crossesCounts;
    }

    public List<PresentableCrossElementsCount> GetElementsCounts()
    {
        return _elementsCounts;
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
                pointPos.Key.GetDetails().PointCat == PointCats.Angle)
            {
                pointLongitudes.Add(pointPos.Key, pointPos.Value.Ecliptical.MainPosSpeed.Position);
            } else if (pointPos.Key.GetDetails().PointCat == PointCats.Cusp)
            {
                houseLongitudes.Add(pointPos.Key.GetDetails().CalcId,  pointPos.Value.Ecliptical.MainPosSpeed.Position);
            }
        }
        return new ChartLongitudes(pointLongitudes, houseLongitudes);
    }

    public Dictionary<ChartPoints, double> GetHouseCusps()
    {
        if (_blaPositions == null) throw new InvalidOperationException("No data available for BLA");
        var pointPositions = new Dictionary<ChartPoints, double>();
        foreach (var pointPos in _blaPositions)
        {
            if (pointPos.Key.GetDetails().PointCat != PointCats.Cusp) continue;
            var point = pointPos.Key;
            var pos = pointPos.Value.Ecliptical.MainPosSpeed.Position;
            pointPositions.Add(point, pos);

        }
        return pointPositions;
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
        if (useChiron) points.Add(ChartPoints.Chiron);
        if (useEris) points.Add(ChartPoints.Eris);
        
        return points;
    }

    private HouseSystems DefineHouseSystem(HouseSystems houseSystem)
    {
        return houseSystem;
    }
}