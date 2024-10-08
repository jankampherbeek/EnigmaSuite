﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <summary>Prepare midpoint values to be shown in a datagrid.</summary>
public interface IMidpointForDataGridFactory
{
    /// <summary>Builds a presentable midpoint to be used in a grid.</summary>
    /// <param name="midpoints">Calculated midpoints.</param>
    /// <returns>Presentable midpoints.</returns>
    List<PresentableMidpoint> CreateMidpointsDataGrid(IEnumerable<BaseMidpoint> midpoints);

    /// <summary>Builds a presentable occupied midpoint to be used in a grid.</summary>
    /// <param name="midpoints">Occupied midpoints.</param>
    /// <returns>Presentable occupied midpoints.</returns>
    List<PresentableOccupiedMidpoint> CreateMidpointsDataGrid(IEnumerable<OccupiedMidpoint> midpoints);
}




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
        return new PresentableOccupiedMidpoint(point1Glyph, point2Glyph, pointOccGlyph, orbText, occMidpoint.Exactness);
    }
}


