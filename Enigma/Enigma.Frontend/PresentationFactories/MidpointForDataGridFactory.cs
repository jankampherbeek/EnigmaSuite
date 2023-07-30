// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;

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
    public List<PresentableMidpoint> CreateMidpointsDataGrid(List<BaseMidpoint> midpoints)
    {
        List<PresentableMidpoint> presentableMidpoints = new();
        foreach (var midpoint in midpoints)
        {
            presentableMidpoints.Add(CreatePresMidpoint(midpoint));
        }
        return presentableMidpoints;
    }

    /// <inheritdoc/>
    public List<PresentableOccupiedMidpoint> CreateMidpointsDataGrid(List<OccupiedMidpoint> midpoints)
    {
        List<PresentableOccupiedMidpoint> presentableOccMidpoints = new();
        foreach (var midpoint in midpoints)
        {
            presentableOccMidpoints.Add(CreatePresMidpoint(midpoint));
        }
        return presentableOccMidpoints;
    }

    private PresentableMidpoint CreatePresMidpoint(BaseMidpoint midpoint)
    {
        char point1Glyph = _glyphsForChartPoints.FindGlyph(midpoint.Point1.Point);
        char point2Glyph = _glyphsForChartPoints.FindGlyph(midpoint.Point2.Point);
        var (longTxt, glyph) = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(midpoint.Position);
        return new PresentableMidpoint(point1Glyph, point2Glyph, longTxt, glyph);

    }

    private PresentableOccupiedMidpoint CreatePresMidpoint(OccupiedMidpoint occMidpoint)
    {
        char point1Glyph = _glyphsForChartPoints.FindGlyph(occMidpoint.Midpoint.Point1.Point);
        char point2Glyph = _glyphsForChartPoints.FindGlyph(occMidpoint.Midpoint.Point2.Point);
        char pointOccGlyph = _glyphsForChartPoints.FindGlyph(occMidpoint.OccupyingPoint.Point);
        string orbText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(occMidpoint.Orb);
        string exactnessText = System.Math.Floor(occMidpoint.Exactness) + " %";
        return new PresentableOccupiedMidpoint(point1Glyph, point2Glyph, pointOccGlyph, orbText, exactnessText);
    }
}


