// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Diagnostics.CodeAnalysis;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.BlaSchema;

/// <summary>
/// Orchestrator for BLA Schema
/// </summary>
public class BlaSchemaOrchestrator
{

    public BlaSchemaDataSheet CreateBlaSchema(ChartLongitudes chart)
    {
        var pointDetails = new List<BlaPointDetails>();
        foreach (var point in chart.Points)
        {
            pointDetails.Add(BlaDetailsFactory.CreateBlaPointDetails(point.Key, chart));
        }
        var houseDetails = new List<BlaHouseDetails>();
        for (var i = 1; i < 13; i++)
        {
            houseDetails.Add(BlaDetailsFactory.CreateBlaHouseDetails(i, chart));
        }

        var houseCounts =  HousePositions.DefineHouseCounts(pointDetails);      // Number of points in houses
        var signCounts = SignPositions.DefineSignCounts(pointDetails);          // Number of points in signs
        var planetsInSigns = pointDetails.ToDictionary(x => x.Point, x => x.Sign);
        var planetsInHouses = HousePositions.DefineHousePositions(chart); // House for each point
        var signsOnCusps = SignsOnCusps. DefineSignsOnCusps(chart.Cusps);        // Signs on cusps (no intercepted signs)
        var (crossesSignHouseCounts, elementsSignHousecounts) = CrossElementCounts.CreateCrossesElementsCounts(signCounts, houseCounts, houseDetails);
        var quadrantCounts = QuadrantPositions.DefineQuadrants(houseDetails);
        var dispositors = Dispositors.CreateDispositors(chart, signCounts, houseCounts, signsOnCusps, planetsInSigns, planetsInHouses);
        var decans = BlaDecans.DefineDecans(chart.Points);
        var details = BlaDetails.CreateDetails(chart, signsOnCusps, planetsInHouses);
        var cyclesData = BlaCycles.CreateCyclesData(planetsInHouses, signsOnCusps);
        var shortenedCyclesData = BlaCycles.CreateShortenedCyclesData(planetsInHouses, signsOnCusps);
        var reinforcements = BlaReinforcements.CreateReinforcements(planetsInSigns, planetsInHouses, signsOnCusps);

        return new BlaSchemaDataSheet(
            crossesSignHouseCounts,
            elementsSignHousecounts,
            quadrantCounts,
            dispositors,
            decans,
            details,
            cyclesData,
            shortenedCyclesData,
            reinforcements);

    }

 

    
 


    
    
    
}