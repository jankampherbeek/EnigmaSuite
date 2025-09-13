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
        for (int i = 1; i < 13; i++)
        {
            houseDetails.Add(BlaDetailsFactory.CreateBlaHouseDetails(i, chart));
        }

        var houseCounts =  HousePositions.DefineHouseCounts(pointDetails);      // Number of points in houses
        var signCounts = SignPositions.DefineSignCounts(pointDetails);          // Number of points in signs
//        var planetsInSign = SignPositions.DefineSignCounts(pointDetails);       // Sign for each point   
        var planetsInSign = pointDetails.ToDictionary(x => x.Point, x => x.Sign);
        var planetsInHouses = HousePositions.DefineHousePositions(chart); // House for each point
        var signsOnCusps = SignsOnCusps. DefineSignsOnCusps(chart.Cusps);        // Signs on cusps (no intercepted signs)
        var (crossesSignHouseCounts, elementsSignHousecounts) = CrossElementCounts.CreateCrossesElementsCounts(signCounts, houseCounts, houseDetails);
        var quadrantCounts = QuadrantPositions.DefineQuadrants(houseDetails);
        
        // =======================
        
        // Define dispositors
        var dispositors = Dispositors.CreateDispositors(chart, signCounts, houseCounts, signsOnCusps, planetsInSign, planetsInHouses);
       // var dispositors = new List<BlaDispositorLine>();
       
        
        // Define decans
        // Define details (Sister sign asc etc.)
        // Define cyclic connections
        // Define shortened cycles
        // Define reinforcements

        return new BlaSchemaDataSheet(
            crossesSignHouseCounts,
            elementsSignHousecounts,
            quadrantCounts,
            dispositors);

    }

    
    // TODO: create a record like BlaPositions with ChartPoint, longitude, sign, house, ruledSigns, ruledHouses
    // See ChartDetailsFactory for examples


   
    
  
    
 


    
    
    
}