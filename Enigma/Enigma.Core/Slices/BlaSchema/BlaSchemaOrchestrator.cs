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
        var planetsInSign = pointDetails.ToDictionary(x => x.Point, x => x.Sign);
        var planetsInHouses = HousePositions.DefineHousePositions(chart); // House for each point
        var signsOnCusps = SignsOnCusps. DefineSignsOnCusps(chart.Cusps);        // Signs on cusps (no intercepted signs)
        var (crossesSignHouseCounts, elementsSignHousecounts) = CrossElementCounts.CreateCrossesElementsCounts(signCounts, houseCounts, houseDetails);
        var quadrantCounts = QuadrantPositions.DefineQuadrants(houseDetails);
        var dispositors = Dispositors.CreateDispositors(chart, signCounts, houseCounts, signsOnCusps, planetsInSign, planetsInHouses);
        var decans = BlaDecans.DefineDecans(chart.Points);
        var details = CreateDetails(chart, signsOnCusps);
        
        // Define cyclic connections
        var cyclesData = BlaCycles.CreateCyclesData(planetsInHouses, signsOnCusps);
        
        // Define shortened cycles
        // Define reinforcements

        return new BlaSchemaDataSheet(
            crossesSignHouseCounts,
            elementsSignHousecounts,
            quadrantCounts,
            dispositors,
            decans,
            details,
            cyclesData);

    }

    // TODO move to separate class
    private BlaDetails CreateDetails(ChartLongitudes chart, Dictionary<int, int> signsOnCusps)
    {
        
        var asc = signsOnCusps[1];
        var ascRulers = BlaDomain.RulerPairs()[asc];
        var sisterRulerAsc = ascRulers.SubRuler;
        var sisterSignAsc = (int)Math.Truncate(chart.Points[sisterRulerAsc] / 30.0) + 1;
        var clampedHouses = InterceptedClamped.DefineClampedHouses(chart);
        var interceptedSigns = InterceptedClamped.DefineInterceptedSigns(chart);
        var groundNote = new List<int>();
        groundNote.Add(asc);
        var mundaneHouseAsc = (int)Math.Truncate(asc / 30.0) + 1;
        groundNote.Add(mundaneHouseAsc);
        var sisterSignCusp = 0;
        foreach (var rulerPair in BlaDomain.RulerPairs())
        {
            if (rulerPair.MainRuler == sisterRulerAsc)
            {
                sisterSignCusp = rulerPair.SignIndex;
            }
        }
        groundNote.Add(sisterSignCusp);
        foreach (var cuspSign in signsOnCusps)
        {
            if (cuspSign.Value == asc && cuspSign.Key != 1)
            {
                groundNote.Add(cuspSign.Key);
            }
        }
        var lordAscInHouses = new List<int>();
        // TODO define lord ascendant in houses

        var moonInSign = (int)Math.Truncate(chart.Points[ChartPoints.Moon] / 30.0) + 1;
        
        return new BlaDetails(sisterSignAsc, clampedHouses, interceptedSigns, groundNote, lordAscInHouses, moonInSign);

    }


    
 


    
    
    
}