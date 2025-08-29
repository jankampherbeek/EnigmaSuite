// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;
using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <summary>
/// Factory for the creation of presentable BLA positions
/// </summary>
public interface IBlaPositionForDataGridFactory
{
    /// <summary>
    /// Create a presentable BLA position
    /// </summary>
    /// <param name="chartDetails">Details for BLA position</param>
    /// <returns>The presentable positions</returns>
    public List<PresentableBlaPosition> CreateBlaPositionsForDataGrid(ChartDetails chartDetails);
}

public class BlaPositionForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions)
    : IBlaPositionForDataGridFactory
{
    private readonly BlaPositionsFactory _blaPositionsFactory = new();
    private readonly HousePositions _housePositions = new();

    public List<PresentableBlaPosition> CreateBlaPositionsForDataGrid(ChartDetails chartDetails)
    {
        List<PresentableBlaPosition> presentableBlaPositions = new();

        foreach (var pos in chartDetails.SignsDecansHouses)
        {
            if (pos.Point.GetDetails().PointCat == PointCats.Common ||
                pos.Point.GetDetails().PointCat == PointCats.Angle)
            {
                var pointGlyph = GlyphsForChartPoints.FindGlyph(pos.Point);
                presentableBlaPositions.Add(CreatePresBlaPosition(pointGlyph, pos.longitude, pos.Decan, pos.House));
            }
        }

        return presentableBlaPositions;
    }

    private PresentableBlaPosition CreatePresBlaPosition(char pointGlyph, double position, int decan, int houseNr)
    {
        var (longTxt, glyph) = doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(position);
        var houseTxt = GetHouseInRomanNumerals(houseNr);
        var decanateGlyph = GetDecanateGlyph(decan);
        return new PresentableBlaPosition(pointGlyph, longTxt, glyph, houseTxt, decanateGlyph);
    }

    private string GetHouseInRomanNumerals(int house)
    {
        return house switch
        {
            1 => "I",
            2 => "II",
            3 => "III",
            4 => "IV",
            5 => "V",
            6 => "VI",
            7 => "VII",
            8 => "VIII",
            9 => "IX",
            10 => "X",
            11 => "XI",
            12 => "XII"
        };
    }

    private char GetDecanateGlyph(int decan)
    {
        return decan switch
        {
            1 => 'f', // Mars
            2 => 'a', // Sun
            3 => 'd', // Venus
            4 => 'c', // Mercury
            5 => 'b', // Moon
            6 => 'h', // Saturn
            7 => 'g', // Jupiter
            _ => '?'
        };
    }
}


public class BlaElementsCrossesForDataGridFactory()
{
    private int _cardinalSCount, _fixedSCount, _mutableSCount, _fireSCount, _earthSCount, _airSCount, _waterSCount;
    private int _cardinalHCount, _fixedHCount, _mutableHCount, _fireHCount, _earthHCount, _airHCount, _waterHCount;
    private int _cardinalCCount, _fixedCCount, _mutableCCount, _fireCCount, _earthCCount, _airCCount, _waterCCount;
 

    public List<PresentableCrossElementsCount> CreatePresCrossesCounts(ChartDetails chartDetails, CalculatedChart calculatedChart)
    {
        ResetCounts();
        CreateBlaItemsForCrosses(chartDetails, calculatedChart);
        List<PresentableCrossElementsCount> counts =
        [
            CreateSinglePresCrossElementsCount("Cardinal", _cardinalSCount, _cardinalHCount, _cardinalCCount),
            CreateSinglePresCrossElementsCount("Fixed", _fixedSCount, _fixedHCount, _fixedCCount),
            CreateSinglePresCrossElementsCount("Mutable", _mutableSCount, _mutableHCount, _mutableCCount),
        ];

        return counts;
    }
    
    
    public List<PresentableCrossElementsCount> CreatePresElementsCounts(ChartDetails chartDetails, CalculatedChart calculatedChart)
    {
        ResetCounts();
        CreateBlaItemsForElements(chartDetails, calculatedChart);
        List<PresentableCrossElementsCount> counts =
        [
            CreateSinglePresCrossElementsCount("Fire", _fireSCount, _fireHCount, _fireCCount),
            CreateSinglePresCrossElementsCount("Earth", _earthSCount, _earthHCount, _earthCCount),
            CreateSinglePresCrossElementsCount("Air", _airSCount, _airHCount, _airCCount),
            CreateSinglePresCrossElementsCount("Water", _waterSCount, _waterHCount, _waterCCount)
        ];

        return counts;
    }

  
    
    private void ResetCounts()
    {
        _cardinalSCount = _fixedSCount = _mutableSCount = _fireSCount = _earthSCount = _airSCount = _waterSCount = 0;
        _cardinalHCount = _fixedHCount = _mutableHCount = _fireHCount = _earthHCount = _airHCount = _waterHCount = 0;
        _cardinalCCount = _fixedCCount = _mutableCCount = _fireCCount = _earthCCount = _airCCount = _waterCCount = 0;
    }
    
    
    private void CreateBlaItemsForCrosses(ChartDetails chartDetails, CalculatedChart calculatedChart)
    {
        foreach (var pos in chartDetails.SignsDecansHouses)
        {
            switch (pos.Sign)
            {
                case 1 or 4 or 7 or 10:
                    _cardinalSCount++;
                    break;
                case 2 or 5 or 8 or 11:
                    _fixedSCount++;
                    break;
                case 3 or 6 or 9 or 12:
                    _mutableSCount++;
                    break;
            }
        }

        foreach (var pos in chartDetails.SignsDecansHouses)
        {
            if (pos.Point.GetDetails().PointCat == PointCats.Common)
            {
                switch (pos.House)
                {
                    case 1 or 4 or 7 or 10:
                        _cardinalHCount++;
                        break;
                    case 2 or 5 or 8 or 11:
                        _fixedHCount++;
                        break;
                    case 3 or 6 or 9 or 12:
                        _mutableHCount++;
                        break;
                }
            }
        }

        var houseRulingSigns = SignOnCusps(calculatedChart);
        var pointsInHouses = CalcNrOfPointsInHouses(chartDetails); 
        
        foreach (var hrs in houseRulingSigns)
        {
            var count = 0;
            var sign = hrs.Value;
            foreach (var pih in pointsInHouses)
            {
                if (pih.Key == hrs.Key) count = pih.Value;
            }
            switch (sign)
            {
                case 1 or 4 or 7 or 10:
                    _cardinalCCount += count;
                    break;
                case 2 or 5 or 8 or 11:
                    _fixedCCount += count;;
                    break;
                case 3 or 6 or 9 or 12:
                    _mutableCCount += count;
                    break;
            }
        }
    }

    
       public void CreateBlaItemsForElements(ChartDetails chartDetails, CalculatedChart calculatedChart)
    {
        foreach (var pos in chartDetails.SignsDecansHouses)
        {
            switch (pos.Sign)
            {
                case 1 or 5 or 9:
                    _fireSCount++;
                    break;
                case 2 or 6 or 10:
                    _earthSCount++;
                    break;
                case 3 or 7 or 11:
                    _airSCount++;
                    break;
                case 4 or 8 or 12:
                    _waterSCount++;
                    break;
            }
        }

        foreach (var pos in chartDetails.SignsDecansHouses)
        {
            if (pos.Point.GetDetails().PointCat == PointCats.Common)
            {
                switch (pos.House)
                {
                    case 1 or 5 or 9:
                        _fireHCount++;
                        break;
                    case 2 or 6 or 10:
                        _earthHCount++;
                        break;
                    case 3 or 7 or 11:
                        _airHCount++;
                        break;
                    case 4 or 8 or 12:
                        _waterHCount++;
                        break;
                }
            }
        }

        var houseRulingSigns = SignOnCusps(calculatedChart);
        var pointsInHouses = CalcNrOfPointsInHouses(chartDetails); 
        
        foreach (var hrs in houseRulingSigns)
        {
            var count = 0;
            var sign = hrs.Value;
            foreach (var pih in pointsInHouses)
            {
                if (pih.Key == hrs.Key) count = pih.Value;
            }
            switch (sign)
            {
                case 1 or 5 or 9:
                    _fireCCount += count;
                    break;
                case 2 or 6 or 10:
                    _earthCCount += count;;
                    break;
                case 3 or 7 or 11:
                    _airCCount += count;
                    break;
                case 4 or 8 or 12:
                    _waterCCount += count;; 
                    break;
            }
        }
    }
    
    
    
    
    /// <summary>
    /// Create dictionary with house number (key) and eclitpical sign (1..12, value)
    /// </summary>
    /// <param name="calculatedChart">The actual chart</param>
    /// <returns>The dictionary</returns>
    private Dictionary<int, int> SignOnCusps(CalculatedChart calculatedChart)
    {
        Dictionary<int, double> houseLongitudes = new();
        Dictionary<int, int> houseSigns = new();
        foreach (var pos in calculatedChart.Positions)
        {
            if (pos.Key.GetDetails().PointCat != PointCats.Cusp) continue;
            var longitude = pos.Value.Ecliptical.MainPosSpeed.Position;
            switch (pos.Key)
            {
                case ChartPoints.Cusp1: houseLongitudes.Add(1, longitude); break;
                case ChartPoints.Cusp2: houseLongitudes.Add(2, longitude); break;
                case ChartPoints.Cusp3: houseLongitudes.Add(3, longitude); break;
                case ChartPoints.Cusp4: houseLongitudes.Add(4, longitude); break;
                case ChartPoints.Cusp5: houseLongitudes.Add(5, longitude); break;
                case ChartPoints.Cusp6: houseLongitudes.Add(6, longitude); break;
                case ChartPoints.Cusp7: houseLongitudes.Add(7, longitude); break;
                case ChartPoints.Cusp8: houseLongitudes.Add(8, longitude); break;
                case ChartPoints.Cusp9: houseLongitudes.Add(9, longitude); break;
                case ChartPoints.Cusp10: houseLongitudes.Add(10, longitude); break;
                case ChartPoints.Cusp11: houseLongitudes.Add(11, longitude); break;
                case ChartPoints.Cusp12: houseLongitudes.Add(12, longitude); break;
            }
        }

        foreach (var cuspLong in houseLongitudes)
        {
            var signIndex = (int)Math.Round(cuspLong.Value / 30.0) + 1;
            houseSigns.Add(cuspLong.Key, signIndex);
        }
        return houseSigns;
    }
    

    private PresentableCrossElementsCount CreateSinglePresCrossElementsCount(string name, int sCount, int hCount,
        int hcusp)
    {
        var sum = sCount + hCount;
        var total = sum + hcusp;
        var count = new PresentableCrossElementsCount(name, sCount, hCount, sum, hcusp, total);
        return count;
    }

    private static Dictionary<int, int> CalcNrOfPointsInHouses(ChartDetails chartDetails)
    {
        var counts = new Dictionary<int, int>
        {
            { 1, 0 },
            { 2, 0 },
            { 3, 0 },
            { 4, 0 },
            { 5, 0 },
            { 6, 0 },
            { 7, 0 },
            { 8, 0 },
            { 9, 0 },
            { 10, 0 },
            { 11, 0 },
            { 12, 0 }
        };

        foreach (var point in chartDetails.Houses)
        {
            if (point.Key.GetDetails().PointCat != PointCats.Common) continue;
            var houseNr = point.Value;
            counts[houseNr]++;
        }
        return counts;
    }
}


public record PresentableQuadrantCount(int Quadrant, int Count);

public class BlaPresQuadrantCountFactory()
{
    public List<PresentableQuadrantCount> CreatePresQuadrants(ChartDetails chartDetails)
    {
        var presQuadrants = new List<PresentableQuadrantCount>();
        foreach (var qData in chartDetails.QuadrantCounts)
        {
            presQuadrants.Add(new PresentableQuadrantCount(qData.Key, qData.Value));    
        }
        return presQuadrants;
    }    
}


public record PresentableDecanCount(string Decan, int Count);

public class BlaPresDecanCountFactory()
{
    public List<PresentableDecanCount> CreatePresDecans(ChartDetails chartDetails)
    {
        var counts = new int[7];
        foreach (var sdh in chartDetails.SignsDecansHouses)
        {
            
            counts[sdh.Decan - 1]++;
        }
        var presDecans = new List<PresentableDecanCount>
        {
            new PresentableDecanCount(FindGlyphForDecan(1),counts[0]),
            new PresentableDecanCount(FindGlyphForDecan(2),counts[1]),
            new PresentableDecanCount(FindGlyphForDecan(3),counts[2]),
            new PresentableDecanCount(FindGlyphForDecan(4),counts[3]),
            new PresentableDecanCount(FindGlyphForDecan(5),counts[4]),
            new PresentableDecanCount(FindGlyphForDecan(6),counts[5]),
            new PresentableDecanCount(FindGlyphForDecan(7),counts[6])
        };
        return presDecans;   
    }
    
    private string FindGlyphForDecan(int decan)
    {
        switch (decan)
        {
            case 1: return "f";  // Mars
            case 2: return "a";  // Sun
            case 3: return "d";  // Venus
            case 4: return "c";  // Mercury
            case 5: return "b";  // Moon
            case 6: return "h";  // Saturn
            case 7: return "g";  // Jupiter
            default: return " ";

        }
    }
}