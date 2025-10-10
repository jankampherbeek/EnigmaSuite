// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Core.Slices.BlaSchema;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Frontend.Ui.PresentationFactories;


public record PresentableBlaPosition(char Factor, string Position, char Sign, string House);

/// <summary>
/// Factory for presentable planetary positions 
/// </summary>
public class BlaPositionsPresFactory
{
    public List<PresentableBlaPosition> GetBlaPositions(List<BlaPointDetails> pointDetails)
    {
        DoubleToDmsConversions _doubleToDmsConversions = new();
        
        var positions = new List<PresentableBlaPosition>();
        foreach (var pd in pointDetails)
        {
            var factorGlyph = GlyphsForChartPoints.FindGlyph(pd.Point);
            var factorPosText = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(pd.Longitude).longTxt;
            char longGlyph = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(pd.Longitude).glyph;
            positions.Add(new PresentableBlaPosition(factorGlyph, factorPosText, longGlyph, pd.House.ToString()));
        }

        return positions;
    } 
    
}