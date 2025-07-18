// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <summary>Conversions for presentable progressive positions.</summary>
public interface IProgPositionsForPresentationFactory
{
    /// <summary>Convert full point positions to PresentableProgPositions.</summary>
    /// <param name="positions">The positions to convert.</param>
    /// <returns>The resulting PresentableProgPositions.</returns>
    public List<PresentableProgPosition> CreatePresProgPos(Dictionary<ChartPoints, FullPointPos> positions);
    
    /// <summary>Convert progressive point positions to PresentableProgPositions.</summary>
    /// <param name="positions">The positions to convert.</param>
    /// <returns>The resulting PresentableProgPositions.</returns>
    public List<PresentableProgPosition> CreatePresProgPos(Dictionary<ChartPoints, ProgPositions> positions);
}

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
    public List<PresentableProgPosition> CreatePresProgPos(Dictionary<ChartPoints, ProgPositions> positions)
    {
        return (from celPos in positions 
            where celPos.Key.GetDetails().PointCat == PointCats.Common  
            select CreateSinglePos(celPos)).ToList();   
    }

    public List<PresentableProgPosition> CreatePresProgPos(Dictionary<ChartPoints, FullPointPos> positions)
    {
        return (from celPos in positions 
            where celPos.Key.GetDetails().PointCat == PointCats.Common  
            select CreateSinglePos(celPos)).ToList();   
    }
    
    
    private PresentableProgPosition CreateSinglePos(KeyValuePair<ChartPoints, ProgPositions> progPos)
    {
        var longPos = progPos.Value.Longitude;
        var latPos = progPos.Value.Latitude;
        var raPos = progPos.Value.Ra;
        var declPos = progPos.Value.Declination;
        
        var pointGlyph = GlyphsForChartPoints.FindGlyph(progPos.Key);
        var longPosText = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(longPos).longTxt;
        var longGlyph = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(longPos).glyph;
        var latPosText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(latPos);
        var raPosText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(raPos);
        var declPosText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(declPos);
        return new PresentableProgPosition(pointGlyph, longPosText, longGlyph,  latPosText, raPosText, declPosText);
    }
    
    private PresentableProgPosition CreateSinglePos(KeyValuePair<ChartPoints, FullPointPos> pos)
    {
        var longPos = pos.Value.Ecliptical.MainPosSpeed.Position;
        var latPos = pos.Value.Ecliptical.DeviationPosSpeed.Position;
        var raPos = pos.Value.Equatorial.MainPosSpeed.Position;
        var declPos = pos.Value.Equatorial.DeviationPosSpeed.Position;
        
        var pointGlyph = GlyphsForChartPoints.FindGlyph(pos.Key);
        var longPosText = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(longPos).longTxt;
        var longGlyph = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(longPos).glyph;
        var latPosText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(latPos);
        var raPosText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(raPos);
        var declPosText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(declPos);
        return new PresentableProgPosition(pointGlyph, longPosText, longGlyph,  latPosText, raPosText, declPosText);
    }
}