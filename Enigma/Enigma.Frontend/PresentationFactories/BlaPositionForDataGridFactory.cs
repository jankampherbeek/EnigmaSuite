// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
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
