// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Interfaces;
using Enigma.Frontend.Interfaces;
using Enigma.InputSupport.Interfaces;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Enigma.Frontend.Charts.Graphics;


public class ChartsWheelSolSysPoints : IChartsWheelSolSysPoints
{

    private readonly IRangeCheck _rangeCheck;
    private readonly ISortedGraphicSolSysPointsFactory _sortedGraphicSolSysPointsFactory;
    private readonly ISolarSystemPointSpecifications _solarSystemPointSpecifications;
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;

    public ChartsWheelSolSysPoints(ISortedGraphicSolSysPointsFactory sortedGraphicSolSysPointsFactory,
        ISolarSystemPointSpecifications solarSystemPointSpecifications,
        IRangeCheck rangeCheck,
        IDoubleToDmsConversions doubleToDmsConversions)
    {
        _rangeCheck = rangeCheck;
        _sortedGraphicSolSysPointsFactory = sortedGraphicSolSysPointsFactory;
        _solarSystemPointSpecifications = solarSystemPointSpecifications;
        _doubleToDmsConversions = doubleToDmsConversions;
    }



    public List<TextBlock> CreateSolSysPointGlyphs(ChartsWheelMetrics metrics, List<FullSolSysPointPos> solSysPoints, Point centerPoint, double longAscendant)
    {
        List<TextBlock> glyphs = new();

        List<GraphicSolSysPointPositions> graphicSolSysPointsPositions = _sortedGraphicSolSysPointsFactory.CreateSortedList(solSysPoints, longAscendant, metrics.MinDistance);
        DimPoint dimPoint = new(centerPoint);
        double fontSize = metrics.SolSysPointGlyphSize;
        foreach (var graphPoint in graphicSolSysPointsPositions)
        {
            double angle = graphPoint.PlotPos;
            Point point1 = dimPoint.CreatePoint(angle, metrics.SolSysPointGlyphRadius);
            TextBlock glyph = new()
            {
                Text = _solarSystemPointSpecifications.DetailsForPoint(graphPoint.SolSysPoint).DefaultGlyph,
                FontFamily = metrics.GlyphsFontFamily,
                FontSize = fontSize,
                Foreground = new SolidColorBrush(metrics.SolSysPointColor)
            };
            Canvas.SetLeft(glyph, point1.X - (fontSize / 3));
            Canvas.SetTop(glyph, point1.Y - (fontSize / 1.8));
            glyphs.Add(glyph);
        }
        return glyphs;
    }

    public List<Line> CreateSolSysPointConnectLines(ChartsWheelMetrics metrics, List<FullSolSysPointPos> solSysPoints, Point centerPoint, double longAscendant)
    {
        List<Line> connectLines = new();
        List<GraphicSolSysPointPositions> graphicSolSysPointsPositions = _sortedGraphicSolSysPointsFactory.CreateSortedList(solSysPoints, longAscendant, metrics.MinDistance);
        DimLine dimLine = new();
        DimPoint dimPoint = new(centerPoint);
        foreach (var graphPoint in graphicSolSysPointsPositions)
        {
            Point point1 = dimPoint.CreatePoint(graphPoint.PlotPos, metrics.OuterConnectionRadius);
            Point point2 = dimPoint.CreatePoint(graphPoint.MundanePos, metrics.OuterAspectRadius);
            Line connectionLine = dimLine.CreateLine(point1, point2, metrics.ConnectLineSize, metrics.SolSysPointConnectLineColor, metrics.SolSysPointConnectLineOpacity);
            connectLines.Add(connectionLine);
        }
        return connectLines;
    }

    public List<TextBlock> CreateSolSysPointTexts(ChartsWheelMetrics metrics, List<FullSolSysPointPos> solSysPoints, Point centerPoint, double longAscendant)
    {
        List<TextBlock> texts = new();
        List<GraphicSolSysPointPositions> graphicSolSysPointsPositions = _sortedGraphicSolSysPointsFactory.CreateSortedList(solSysPoints, longAscendant, metrics.MinDistance);
        DimPoint dimPoint = new(centerPoint);
        foreach (var graphPoint in graphicSolSysPointsPositions)
        {
            double angle = graphPoint.PlotPos < 180.0 ? graphPoint.PlotPos - 1.5 : graphPoint.PlotPos + 1.5;
            string posDmText = _doubleToDmsConversions.ConvertDoubleToDmInSignNoGlyph(graphPoint.EclipticPos);
            Point point1 = angle < 180.0 ? dimPoint.CreatePoint(angle, metrics.SolSysPointTextRadius + metrics.SolSysPointTextEastOffset) : dimPoint.CreatePoint(angle, metrics.SolSysPointTextRadius + metrics.SolSysPointTextWestOffset);
            TextBlock posText = new()
            {
                Text = posDmText,
                FontFamily = metrics.PositionTextsFontFamily,
                FontSize = metrics.PositionTextSize,
                Foreground = new SolidColorBrush(metrics.SolSysPointTextColor)
            };
            RotateTransform rotateTransform = new();
            double rotateAngle = graphPoint.PlotPos < 180.0 ? graphPoint.PlotPos - 90.0 : graphPoint.PlotPos - 270.0;
            double swapAngle = 90.0 - rotateAngle;
            rotateTransform.Angle = _rangeCheck.InRange360(270.0 + swapAngle);
            posText.RenderTransform = rotateTransform;
            Canvas.SetLeft(posText, point1.X - metrics.GlyphXOffset);
            Canvas.SetTop(posText, point1.Y - metrics.GlyphYOffset);
            texts.Add(posText);
        }
        return texts;
    }

}
