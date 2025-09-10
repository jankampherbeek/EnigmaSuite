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

        var houseCounts =  HousePositions.DefineHouseCounts(chart);             // Number of points in houses
        var signCounts = SignPositions.DefineSignCounts(chart);                 // Number of points in signs
        var planetsInSign = SignPositions.CreatePointsInSign(chart);      // Sign for each point   
        var planetsInHouses = HousePositions.DefineHousePositions(chart); // House for each point
        var signsOnCusps = SignsOnCusps.DefineSignsOnCusps(chart.Cusps);        // Signs on cusps (no intercepted signs)
        var crossesElementsCounts =   
            CrossElementCounts.CreateCrossesElementsCounts(signCounts, houseCounts, signsOnCusps); // Crosses and elements counts
        var crossesSignHouseCounts = crossesElementsCounts.Item1;
        var elementsSignHousecounts = crossesElementsCounts.Item2;; 
        var quadrantCounts = QuadrantPositions.DefineQuadrants(chart);
    
        
        
        // Define dispositors
        var dispositors = Dispositors.CreateDispositors(chart, signCounts, houseCounts, signsOnCusps, planetsInSign, planetsInHouses);
        
        
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