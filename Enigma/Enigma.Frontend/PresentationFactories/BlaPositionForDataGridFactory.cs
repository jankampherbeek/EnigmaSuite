// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
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
    /// <param name="positionList">Chartpoints and longituded</param>
    /// <returns>The presentable positions</returns>
    public List<PresentableBlaPosition> CreateBlaPositionForDataGrid(List<KeyValuePair<ChartPoints, double>> positionList);
}


public class BlaPositionForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions): IBlaPositionForDataGridFactory
{
    public List<PresentableBlaPosition> CreateBlaPositionForDataGrid(List<KeyValuePair<ChartPoints, double>> positionList)
    {
        List<PresentableBlaPosition> presentableBlaPositions = new();
        foreach (var position in positionList)
        {
            var pointGlyph = GlyphsForChartPoints.FindGlyph(position.Key);
            presentableBlaPositions.Add(CreatePresBlaPosition(pointGlyph, position.Value));
        }
        return presentableBlaPositions;
    }

    // TODO add creation of house nr and decanate
    private PresentableBlaPosition CreatePresBlaPosition(char pointGlyph, double position)    
    {
        var (longTxt, glyph) = doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(position);
        var houseNr = 5;
        var decanateGlyph = 'a';
        return new PresentableBlaPosition(pointGlyph, longTxt, glyph, houseNr, decanateGlyph);
    }
    
}
