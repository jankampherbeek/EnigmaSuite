// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.PresentationFactories;

public class MidpointForDataGridFactory : IMidpointForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly GlyphsForChartPoints _glyphsForChartPoints;

    public MidpointForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
        _glyphsForChartPoints = new GlyphsForChartPoints();     // TODO 0.3 replace with selection based on config.
    }

    /// <inheritdoc/>
    public List<PresentableMidpoint> CreateMidpointsDataGrid(IEnumerable<BaseMidpoint> midpoints)
    {
        return midpoints.Select(CreatePresMidpoint).ToList();
    }

    /// <inheritdoc/>
    public List<PresentableOccupiedMidpoint> CreateMidpointsDataGrid(IEnumerable<OccupiedMidpoint> midpoints)
    {
        return midpoints.Select(CreatePresMidpoint).ToList();
    }

    private PresentableMidpoint CreatePresMidpoint(BaseMidpoint midpoint)
    {
        char point1Glyph = GlyphsForChartPoints.FindGlyph(midpoint.Point1.Point);
        char point2Glyph = GlyphsForChartPoints.FindGlyph(midpoint.Point2.Point);
        var (longTxt, glyph) = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(midpoint.Position);
        return new PresentableMidpoint(point1Glyph, point2Glyph, longTxt, glyph);

    }

    private PresentableOccupiedMidpoint CreatePresMidpoint(OccupiedMidpoint occMidpoint)
    {
        char point1Glyph = GlyphsForChartPoints.FindGlyph(occMidpoint.Midpoint.Point1.Point);
        char point2Glyph = GlyphsForChartPoints.FindGlyph(occMidpoint.Midpoint.Point2.Point);
        char pointOccGlyph = GlyphsForChartPoints.FindGlyph(occMidpoint.OccupyingPoint.Point);
        string orbText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(occMidpoint.Orb);
        string exactnessText = System.Math.Floor(occMidpoint.Exactness) + " %";
        return new PresentableOccupiedMidpoint(point1Glyph, point2Glyph, pointOccGlyph, orbText, exactnessText);
    }
}


