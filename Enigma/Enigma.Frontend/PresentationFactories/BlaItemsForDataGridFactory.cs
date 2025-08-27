// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using Enigma.Core.Slices.BlaSchema;
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


public class BlaPositionForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions): IBlaPositionForDataGridFactory
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
    public List<PresentableCrossElementsCount> CreateBlaItemsForElementsCrosses(ChartDetails chartDetails)
    {

        foreach (var pos in chartDetails.SignsDecansHouses)
        {
            var factor = (int)Math.Round(pos.longitude/12) + 1;
            switch (factor)
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

            switch (factor)
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
            var house= pos.House;
            switch (house)
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

            switch (house)
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
        return CreatePresCrossElementsCounts();
    }

    private List<PresentableCrossElementsCount> CreatePresCrossElementsCounts()
    {

        List<PresentableCrossElementsCount> counts = new();
        var hCusp = 1;
        counts.Add(CreateSinglePresCrossElementsCount("Cardinal", _cardinalSCount, _cardinalHCount, hCusp));
        counts.Add(CreateSinglePresCrossElementsCount("Fixed", _fixedSCount, _fixedHCount, hCusp));
        counts.Add(CreateSinglePresCrossElementsCount("Mutable", _mutableSCount, _mutableHCount, hCusp));
        counts.Add(CreateSinglePresCrossElementsCount("Fire", _fireSCount, _fireHCount, hCusp));
        counts.Add(CreateSinglePresCrossElementsCount("Earth", _earthSCount, _earthHCount, hCusp));
        counts.Add(CreateSinglePresCrossElementsCount("Air", _airSCount, _airHCount, hCusp));
        counts.Add(CreateSinglePresCrossElementsCount("Water", _waterSCount, _waterHCount, hCusp));
        return counts;
    }

    private PresentableCrossElementsCount CreateSinglePresCrossElementsCount(string name, int sCount, int hCount, int hcusp)
    {
        const string spacer = "";
        var sum = sCount + hCount;
        var total = sum + hcusp;
        var count = new PresentableCrossElementsCount(name, sCount, hCount, spacer, sum, spacer, hcusp, spacer, total);
        return count;
    }
    
    
}