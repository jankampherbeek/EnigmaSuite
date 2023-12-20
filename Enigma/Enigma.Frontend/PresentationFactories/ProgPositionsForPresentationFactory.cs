// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
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
    public List<PresentableProgPosition> CreatePresProgPos(Dictionary<ChartPoints, ProgPositions> positions)
    {
        return (from celPos in positions 
            where celPos.Key.GetDetails().PointCat == PointCats.Common  
            select CreateSinglePos(celPos)).ToList();   
    }

    
    private PresentableProgPosition CreateSinglePos(KeyValuePair<ChartPoints, ProgPositions> progPos)
    {
        double longPos = progPos.Value.Longitude;
        double latPos = progPos.Value.Latitude;
        double raPos = progPos.Value.Ra;
        double declPos = progPos.Value.Declination;
        
        char pointGlyph = GlyphsForChartPoints.FindGlyph(progPos.Key);
        string longPosText = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(longPos).longTxt;
        char longGlyph = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(longPos).glyph;
        string latPosText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(latPos);
        string raPosText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(raPos);
        string declPosText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(declPos);
        return new PresentableProgPosition(pointGlyph, longPosText, longGlyph,  latPosText, raPosText, declPosText);
    }
    
}