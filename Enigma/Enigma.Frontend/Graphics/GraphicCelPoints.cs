// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Enigma.Domain.Dtos;
using Enigma.Domain.Graphics;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Frontend.Ui.Graphics;

/// <summary>Graphic representations for celestial points.</summary>
public interface IGraphicCelPoints
{
    /// <summary>Create a graphic representation of a celestial point glyph in a chart wheel.</summary>
    /// <param name="metrics">Container for sizes.</param>
    /// <param name="commonPoints">Points to show.</param>
    /// <param name="centerPoint">Center point of the wheel.</param>
    /// <param name="longAscendant">Longitude of the ascendant.</param>
    /// <returns>Textblocks with the glyphs and positions.</returns>
    public List<TextBlock> CreateCelPointGlyphsForWheel(ChartsWheelMetrics metrics, 
        Dictionary<ChartPoints, FullPointPos> commonPoints, Point centerPoint, double longAscendant);
    
    /// <summary>Create lines that connect glyphs for celestial points to the exact position in the ecliptic.</summary>
    /// <param name="metrics">Container for sizes.</param>
    /// <param name="celPoints">Points for which a connectline should be constructed.</param>
    /// <param name="centerPoint">Center point of the wheel.</param>
    /// <param name="longAscendant">Longitude of the ascendants.</param>
    /// <returns>The lines for the connection between glyph and position.</returns>
    public List<Line> CreateCelPointConnectLines(ChartsWheelMetrics metrics, 
        Dictionary<ChartPoints, FullPointPos> celPoints, Point centerPoint, double longAscendant);
    
    /// <summary>Create position texts for celestial points in a chart wheel.</summary>
    /// <param name="metrics">Container for sizes.</param>
    /// <param name="celPoints">Points for which a text should be constructed.</param>
    /// <param name="centerPoint">Center point of the wheel.</param>
    /// <param name="longAscendant">Longitude of the ascendant.</param>
    /// <returns>Textblocks with the texts and positions.</returns>
    public List<TextBlock> CreateCelPointTextsForWheel(ChartsWheelMetrics metrics, 
        Dictionary<ChartPoints, FullPointPos> celPoints, Point centerPoint, double longAscendant);
    
    
}

/// <inheritdoc/>
public sealed class GraphicCelPoints : IGraphicCelPoints
{
    private readonly ISortedGraphicCelPointsFactory _sortedGraphicCelPointsFactory;
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly GlyphsForChartPoints _glyphsForChartPoints;

    public GraphicCelPoints(ISortedGraphicCelPointsFactory sortedGraphicCelPointsFactory,
        IDoubleToDmsConversions doubleToDmsConversions)
    {
        _sortedGraphicCelPointsFactory = sortedGraphicCelPointsFactory;
        _doubleToDmsConversions = doubleToDmsConversions;
        _glyphsForChartPoints = new GlyphsForChartPoints();
    }


    /// <inheritdoc/>
    public List<TextBlock> CreateCelPointGlyphsForWheel(ChartsWheelMetrics metrics, Dictionary<ChartPoints, FullPointPos> commonPoints, Point centerPoint, double longAscendant)
    {
        List<TextBlock> glyphs = new();

        List<GraphicCelPointForWheelPositions> graphicSolCelPointsPositions = _sortedGraphicCelPointsFactory.CreateSortedListForWheel(commonPoints, longAscendant, ChartsWheelMetrics.MinDistance);
        DimPoint dimPoint = new(centerPoint);
        double fontSize = metrics.CelPointGlyphSize;
        foreach (var graphPoint in graphicSolCelPointsPositions)
        {
            double angle = graphPoint.PlotPos;
            Point point1 = dimPoint.CreatePoint(angle, metrics.CelPointGlyphRadius);
            TextBlock glyph = new()
            {
                Text = GlyphsForChartPoints.FindGlyph(graphPoint.CelPoint).ToString(),
                FontFamily = metrics.GlyphsFontFamily,
                FontSize = fontSize,
                Foreground = new SolidColorBrush(metrics.CelPointColor)
            };
            Canvas.SetLeft(glyph, point1.X - fontSize / 3);
            Canvas.SetTop(glyph, point1.Y - fontSize / 1.8);
            glyphs.Add(glyph);
        }
        return glyphs;
    }


    /// <inheritdoc/>
    public List<Line> CreateCelPointConnectLines(ChartsWheelMetrics metrics, Dictionary<ChartPoints, FullPointPos> celPoints, Point centerPoint, double longAscendant)
    {
        List<GraphicCelPointForWheelPositions> graphicCelPointsPositions = _sortedGraphicCelPointsFactory.CreateSortedListForWheel(celPoints, longAscendant, ChartsWheelMetrics.MinDistance);
        DimPoint dimPoint = new(centerPoint);
        return (from graphPoint in graphicCelPointsPositions 
            let point1 = dimPoint.CreatePoint(graphPoint.PlotPos, metrics.OuterConnectionRadius) 
            let point2 = dimPoint.CreatePoint(graphPoint.MundanePos, metrics.OuterAspectRadius) 
            select DimLine.CreateLine(point1, point2, metrics.ConnectLineSize, metrics.CelPointConnectLineColor, ChartsWheelMetrics.CelPointConnectLineOpacity)).ToList();
    }

    /// <inheritdoc/>
    public List<TextBlock> CreateCelPointTextsForWheel(ChartsWheelMetrics metrics, Dictionary<ChartPoints, FullPointPos> celPoints, Point centerPoint, double longAscendant)
    {
        List<TextBlock> texts = new();
        List<GraphicCelPointForWheelPositions> graphicCelPointsPositions = _sortedGraphicCelPointsFactory.CreateSortedListForWheel(celPoints, longAscendant, ChartsWheelMetrics.MinDistance);
        DimPoint dimPoint = new(centerPoint);
        foreach (var graphPoint in graphicCelPointsPositions)
        {
            double angle = graphPoint.PlotPos < 180.0 ? graphPoint.PlotPos - 1.5 : graphPoint.PlotPos + 1.5;
            string posDmText = _doubleToDmsConversions.ConvertDoubleToDmInSignNoGlyph(graphPoint.EclipticPos);
            Point point1 = angle < 180.0 ? dimPoint.CreatePoint(angle, metrics.CelPointTextRadius + metrics.CelPointTextEastOffset) : dimPoint.CreatePoint(angle, metrics.CelPointTextRadius + metrics.CelPointTextWestOffset);
            TextBlock posText = new()
            {
                Text = posDmText,
                FontFamily = metrics.PositionTextsFontFamily,
                FontSize = metrics.PositionTextSize,
                Foreground = new SolidColorBrush(metrics.CelPointTextColor)
            };
            RotateTransform rotateTransform = new();
            double rotateAngle = graphPoint.PlotPos < 180.0 ? graphPoint.PlotPos - 90.0 : graphPoint.PlotPos - 270.0;
            double swapAngle = 90.0 - rotateAngle;
            rotateTransform.Angle = 270.0 + swapAngle;
            if (rotateTransform.Angle >= 360.0) rotateTransform.Angle -= 360.0;

            posText.RenderTransform = rotateTransform;
            Canvas.SetLeft(posText, point1.X - metrics.GlyphXOffset);
            Canvas.SetTop(posText, point1.Y - metrics.GlyphYOffset);
            texts.Add(posText);
        }
        return texts;
    }

}
