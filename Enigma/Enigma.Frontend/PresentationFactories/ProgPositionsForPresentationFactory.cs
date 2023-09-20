// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <inheritdoc/>
public sealed class ProgPositionsForPresentationFactory:IProgPositionsForPresentationFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly GlyphsForChartPoints _glyphsForChartPoints;

    public ProgPositionsForPresentationFactory(IDoubleToDmsConversions doubleToDmsConversions,
        GlyphsForChartPoints glyphsForChartPoints)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
        _glyphsForChartPoints = glyphsForChartPoints;
    }
    
    /// <inheritdoc/>
    public List<PresentableProgPosition> CreatePresProgPos(Dictionary<ChartPoints, FullPointPos> positions)
    {
        return (from celPos in positions 
            where celPos.Key.GetDetails().PointCat == PointCats.Common  
            select CreateSinglePos(celPos)).ToList();   
    }

    
    private PresentableProgPosition CreateSinglePos(KeyValuePair<ChartPoints, FullPointPos> commonPos)
    {
        char pointGlyph = GlyphsForChartPoints.FindGlyph(commonPos.Key);
        
        double longPos = commonPos.Value.Ecliptical.MainPosSpeed.Position;
        double longPosSpeed = commonPos.Value.Ecliptical.MainPosSpeed.Speed;
        double declPos = commonPos.Value.Equatorial.DeviationPosSpeed.Position;
        double declSpeed = commonPos.Value.Equatorial.DeviationPosSpeed.Speed;
        
        string longPosText = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(longPos).longTxt;
        string longPosSpeedText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(longPosSpeed);
        char longGlyph = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(longPos).glyph;
        string declPosText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(declPos);
        string declSpeedText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(declSpeed);
        return new PresentableProgPosition(pointGlyph, longPosText, longGlyph,  longPosSpeedText, declPosText, declSpeedText);
    }
    
}