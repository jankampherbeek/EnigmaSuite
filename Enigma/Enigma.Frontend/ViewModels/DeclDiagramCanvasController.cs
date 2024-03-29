// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.Graphics;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Graphics;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>Controller for DeclDiagram view</summary>
/// <remarks>This view uses MVC instead of MVVM</remarks>
public class DeclDiagramCanvasController
{
    private const int DECL_DEGREES_COUNT = 30;
    private const int LONG_DEGREES_COUNT = 180;
    private readonly DeclDiagramMetrics _metrics;
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly DataVaultCharts _dataVaultCharts;
    private double _obliquity;
    
    

    public List<Rectangle> Rectangles { get; private set; } = new();
    public List<Polygon> Polygons { get; private set; } = new();
    public List<Line> Lines { get; set; } = new();
    public List<TextBlock> VerticalDegreesTexts { get; } = new();
    public List<TextBlock> SignGlyphs { get; set; } = new();
    public double CanvasSize { get; private set; }
    private CalculatedChart? _currentChart;
   
    
    
    public DeclDiagramCanvasController(DeclDiagramMetrics metrics, IDoubleToDmsConversions doubleToDmsConversions)
    {
        _metrics = metrics;
        _doubleToDmsConversions = doubleToDmsConversions;
        _dataVaultCharts = DataVaultCharts.Instance;
        _currentChart = _dataVaultCharts.GetCurrentChart();
        _obliquity = _currentChart.Obliquity; 

    }
    
    
    
    public void Resize(double minSize)
    {
        _metrics.SetSizeFactor(minSize / 740.0);
        CanvasSize = _metrics.CanvasSizeHorizontal;
        PrepareDraw();
    }

    public void PrepareDraw()
    {
        HandleRectangles();
        HandleHorizontalDegreeLines();
        HandleVerticalDegreeLine();
        HandlePolygons();
        HandleSigns();
        HandlePositions();
    }

    private void HandleRectangles()
    {
        Rectangle backgroundForDiagram = new()
        {
            Fill = Brushes.LightBlue,
            Height = _metrics.CanvasSizeVertical,
            Width = _metrics.CanvasSizeHorizontal, 
            Opacity = 1.0 
        };
        Canvas.SetLeft(backgroundForDiagram, 0.0);
        Canvas.SetTop(backgroundForDiagram, 0.0);
        Rectangles.Add(backgroundForDiagram);

        double sizeFor60DegreesDeclination = _metrics.CanvasSizeVertical - _metrics.DeclDegreeTopOffset -
                                             _metrics.DeclDegreeBottomOffset;
        double heightForInBoundsRegion = (_obliquity / DECL_DEGREES_COUNT) * sizeFor60DegreesDeclination;
        double offsetBgInBoundsRegion = (_metrics.CanvasSizeVertical - heightForInBoundsRegion) / 2.0;
        
        Rectangle backgroundForInBoundsRegion = new()
        {
            Fill = Brushes.LightCyan,
            Height = heightForInBoundsRegion,
            Width = _metrics.CanvasSizeHorizontal,
            Opacity = 1
        };
        Canvas.SetLeft(backgroundForInBoundsRegion, 0.0);
        Canvas.SetTop(backgroundForInBoundsRegion, offsetBgInBoundsRegion);
        
        Rectangles.Add(backgroundForInBoundsRegion);

        for (int i = 0; i < 3; i++)
        {
            double xPos = _metrics.DiagramOffsetLeft + (i * _metrics.SignWidth * 2);
            double yPosTop = 0.0;
            double yPosBottom = _metrics.CanvasSizeVertical;
            Rectangle signBarTop = new()
            {
                Fill = Brushes.Khaki,
                Height = offsetBgInBoundsRegion,
                Width = _metrics.SignWidth,
                Opacity = 0.5
            };
            Canvas.SetLeft(signBarTop, xPos);
            Canvas.SetTop(signBarTop, yPosTop);
            Rectangles.Add(signBarTop);
            yPosBottom = _metrics.CanvasSizeVertical - offsetBgInBoundsRegion;
            Rectangle signBarBottom = new()
            {
                Fill = Brushes.Khaki,
                Height = offsetBgInBoundsRegion,
                Width = _metrics.SignWidth,
                Opacity = 0.5
            };
            Canvas.SetLeft(signBarBottom, xPos);
            Canvas.SetTop(signBarBottom, yPosBottom);

            Rectangles.Add(signBarBottom);
            yPosBottom = offsetBgInBoundsRegion;
            Rectangle signBarCenter = new()
            {
                Fill = Brushes.Khaki,
                Height = _metrics.CanvasSizeVertical - (2 * offsetBgInBoundsRegion),
                Width = _metrics.SignWidth,
                Opacity = 0.5
            };
            Canvas.SetLeft(signBarCenter, xPos);
            Canvas.SetTop(signBarCenter, yPosBottom);
            Rectangles.Add(signBarCenter);

        }
        
        
        
    }
    
    
    private void HandleHorizontalDegreeLines()
    {
        Line topDegreeLine = new Line
        {
            X1 = _metrics.DiagramOffsetLeft,
            Y1 = _metrics.LongDegreeTopOffset,
            X2 = _metrics.DiagramWidth + _metrics.DiagramOffsetLeft,
            Y2 = _metrics.LongDegreeTopOffset,
            Stroke = Brushes.Black
        };
        Lines.Add(topDegreeLine);
        Line bottomDegreeLine = new Line
        {
            X1 = _metrics.DiagramOffsetLeft,
            Y1 = _metrics.CanvasSizeVertical - _metrics.LongDegreeBottomOffset,
            X2 = _metrics.DiagramWidth + _metrics.DiagramOffsetLeft,
            Y2 = _metrics.CanvasSizeVertical - _metrics.LongDegreeBottomOffset,
            Stroke = Brushes.Black
        };
        Lines.Add(bottomDegreeLine);

        double horizontalDegreeInterval = _metrics.DiagramWidth / LONG_DEGREES_COUNT;
        // top line
        double xStart = _metrics.DiagramOffsetLeft;
        double yStart = _metrics.LongDegreeTopOffset;
        double yEnd = yStart + _metrics.DegreeSizeSmall;
        double yEnd5Degrees = yStart + _metrics.DegreeSizeLarge;
        for (int i = 0; i <= LONG_DEGREES_COUNT; i++)
        {
            double xPos = xStart + i * horizontalDegreeInterval;
            Line degreeIndicatorLine = new Line
            {
                X1 = xPos,
                Y1 = yStart,
                X2 = xPos,
                Y2 = i % 5 == 0 ? yEnd5Degrees : yEnd,
                Stroke = Brushes.Black
            };
            Lines.Add(degreeIndicatorLine);
        }
        // bottom line
        yStart = _metrics.CanvasSizeVertical - _metrics.LongDegreeBottomOffset;
        yEnd = yStart - _metrics.DegreeSizeSmall;
        yEnd5Degrees = yStart - _metrics.DegreeSizeLarge;
        for (int i = 0; i <= LONG_DEGREES_COUNT; i++)
        {
            double xPos = xStart + i * horizontalDegreeInterval;
            Line degreeIndicatorLine = new Line
            {
                X1 = xPos,
                Y1 = yStart,
                X2 = xPos,
                Y2 = i % 5 == 0 ? yEnd5Degrees : yEnd,
                Stroke = Brushes.Black
            };
            Lines.Add(degreeIndicatorLine);
        }
        
        
    }

    private void HandleVerticalDegreeLine()
    {
        
        // Left bar
        Line verticalDegreesLineLeft = new Line
        {
            X1 = _metrics.DeclDegreeLeftOffset,
            Y1 = _metrics.DeclDegreeTopOffset,
            X2 = _metrics.DeclDegreeLeftOffset,
            Y2 = _metrics.CanvasSizeVertical - _metrics.DeclDegreeBottomOffset,
            Stroke = Brushes.Black
        };
        Lines.Add(verticalDegreesLineLeft);
        double lengthOfDegreeLine =
            _metrics.CanvasSizeVertical - _metrics.DeclDegreeTopOffset - _metrics.DeclDegreeBottomOffset;
        double verticalDegreeInterval = lengthOfDegreeLine / (DECL_DEGREES_COUNT * 2);
        double xStart = _metrics.DeclDegreeLeftOffset;
        double xEnd = xStart + _metrics.DegreeSizeSmall;
        double xEnd5Degrees = xStart + _metrics.DegreeSizeLarge;
        for (int i = 0; i <= DECL_DEGREES_COUNT * 2; i++)
        {
            double yPos = _metrics.DeclDegreeTopOffset + i * verticalDegreeInterval;
            Line degreeIndicatorLine = new Line
            {
                X1 = xStart,
                Y1 = yPos,
                X2 = i % 5 == 0 ? xEnd5Degrees : xEnd,
                Y2 = yPos,
                Stroke = Brushes.Black
            };
            Lines.Add(degreeIndicatorLine);
               
        }
        DimTextBlock dimDegreeTextLeft = new(_metrics.DegreeTextsFontFamily, _metrics.DegreeTextSize,
            _metrics.DegreeTextOpacity, _metrics.DegreeTextColor); 
        for (int i = 0; i < 7; i++)
        {
            int degreeValue = 30 - i * 10;
            double xPos = _metrics.DeclDegreeLeftOffset - _metrics.DeclDegreeCharacterLeftOffset;
            double yPos = _metrics.DeclDegreeTopOffset + i * verticalDegreeInterval * 10 - 8;
            RotateTransform rotateTransform = new(0.0);
            TextBlock degreeText = dimDegreeTextLeft.CreateTextBlock(degreeValue.ToString(), xPos, yPos, rotateTransform);
            VerticalDegreesTexts.Add(degreeText);
        }

        // Rightbar
        
        Line verticalDegreesLineRight = new Line
        {
            X1 = _metrics.CanvasSizeHorizontal - _metrics.DeclDegreeRightOffset,
            Y1 = _metrics.DeclDegreeTopOffset,
            X2 = _metrics.CanvasSizeHorizontal - _metrics.DeclDegreeRightOffset,
            Y2 = _metrics.CanvasSizeVertical - _metrics.DeclDegreeBottomOffset,
            Stroke = Brushes.Black
        };
        Lines.Add(verticalDegreesLineRight);
        xStart = _metrics.CanvasSizeHorizontal - _metrics.DeclDegreeRightOffset;
        xEnd = xStart - _metrics.DegreeSizeSmall;
        xEnd5Degrees = xStart - _metrics.DegreeSizeLarge;
        for (int i = 0; i <= DECL_DEGREES_COUNT * 2; i++)
        {
            double yPos = _metrics.DeclDegreeTopOffset + i * verticalDegreeInterval;
            Line degreeIndicatorLine = new Line
            {
                X1 = xStart,
                Y1 = yPos,
                X2 = i % 5 == 0 ? xEnd5Degrees : xEnd,
                Y2 = yPos,
                Stroke = Brushes.Black
            };
            Lines.Add(degreeIndicatorLine);
               
        }
        DimTextBlock dimDegreeTextRight = new(_metrics.DegreeTextsFontFamily, _metrics.DegreeTextSize,
            _metrics.DegreeTextOpacity, _metrics.DegreeTextColor); 
        for (int i = 0; i < 7; i++)
        {
            int degreeValue = 30 - i * 10;
            double xPos = _metrics.CanvasSizeHorizontal - _metrics.DeclDegreeRightOffset + _metrics.DeclDegreeCharacterRightOffset;
            double yPos = _metrics.DeclDegreeTopOffset + i * verticalDegreeInterval * 10 - 8;
            RotateTransform rotateTransform = new(0.0);
            TextBlock degreeText = dimDegreeTextRight.CreateTextBlock(degreeValue.ToString(), xPos, yPos, rotateTransform);
            VerticalDegreesTexts.Add(degreeText);
        }

    }
    
    private void HandlePolygons()
    {
        Tuple<List<Point>, List<Point>> polygonPoints = DefinePolygonPoints();
        List<Point> pointsCompletePolygonNorth = polygonPoints.Item1;
        pointsCompletePolygonNorth.Add(pointsCompletePolygonNorth[0]);    // close polygon
        List<Point> pointsCompletePolygonSouth = polygonPoints.Item2;
        pointsCompletePolygonSouth.Add(pointsCompletePolygonSouth[0]);    // close polygon
        Polygon polygonNorth = new()
        {
            Stroke = Brushes.Coral,
            Fill = Brushes.Bisque,
            //Fill = Brushes.Coral,
            StrokeThickness = 1,
            Opacity = 0.5
        };
        Polygon polygonSouth = new()
        {
            Stroke = Brushes.Teal,
            Fill = Brushes.Azure,
            //Fill = Brushes.Teal,
            StrokeThickness = 1,
            Opacity = 0.5
        };
        
        PointCollection pointCollectionNorth = new PointCollection();
        foreach (Point point in pointsCompletePolygonNorth)
        {
            pointCollectionNorth.Add(point);
        }
        PointCollection pointCollectionSouth = new PointCollection();
        foreach (Point point in pointsCompletePolygonSouth)
        {
            pointCollectionSouth.Add(point);
        }

        polygonNorth.Points = pointCollectionNorth;
        polygonSouth.Points = pointCollectionSouth;
        Polygons = new List<Polygon>
        {
            polygonNorth,
            polygonSouth
        };
    }

    private void HandlePositions()
    {
        List<GraphicCelPointForDeclDiagram> allPoints = 
            _currentChart.Positions.Where(pointPosition => 
                pointPosition.Key.GetDetails().PointCat == PointCats.Common 
                || pointPosition.Key.GetDetails().PointCat == PointCats.Angle).
                Select(pointPosition => 
                    ConvertFullPosToGraphicForDeclDiagram(pointPosition)).ToList();
        double sizeOfHalfDiagram = (_metrics.CanvasSizeVertical / 2) - _metrics.DeclDegreeTopOffset;
        double fontSize = _metrics.CelPointGlyphSize;
        DimTextBlock dimTextBlock = new(_metrics.GlyphsFontFamily, fontSize, 1.0, Colors.DarkSlateBlue);
        
        foreach (var point in allPoints)
        {
            double declDegreeSize = sizeOfHalfDiagram / 30.0;
            double longDegreeSize = _metrics.DiagramWidth / 180.0;
            double longitude = point.Longitude;
            double declination = point.Declination;
            double degreeLongLinePosition = (longitude < 180.0) ? longitude : 360 - longitude;
            double degreeDeclLinePosition =  30 - declination;
            double yBorderForLongitude = point.Longitude < 180.0 ?_metrics.LongDegreeTopOffset : _metrics.CanvasSizeVertical - _metrics.LongDegreeBottomOffset;
            
            double horizontalDistanceFromLeft = degreeLongLinePosition * longDegreeSize;
            double verticalDistanceFromTop = degreeDeclLinePosition * declDegreeSize;
            double xPos = _metrics.DiagramOffsetLeft + horizontalDistanceFromLeft;
            double yPos = _metrics.DeclDegreeTopOffset + verticalDistanceFromTop;
            Line declPositionLine = new Line
            {
                X1 = _metrics.DeclDegreeLeftOffset,
                Y1 = yPos,
                X2 = _metrics.CanvasSizeHorizontal - _metrics.DeclDegreeRightOffset,
                Y2 = yPos,
                Stroke = Brushes.Goldenrod,
                Opacity = 0.7
            };
            Lines.Add(declPositionLine);
            Line longPositionLine = new Line
            {
                X1 = xPos,
                Y1 = yBorderForLongitude,
                X2 = xPos,
                Y2 = yPos,
                Stroke = Brushes.Goldenrod,
                Opacity = 0.7
            };
            Lines.Add(longPositionLine);
            string glyph = point.Glyph.ToString();
            SignGlyphs.Add(dimTextBlock.CreateTextBlock(glyph, xPos - fontSize / 3, yPos - fontSize / 1.8));        
        }
    }

    
    private void HandleSigns()
    {
        // draw separator lines
        double xPosStart = _metrics.DiagramOffsetLeft;
        double yPos = _metrics.LongDegreeTopOffset;

        for (int i = 0; i <= 6; i++)
        {
            double xPos = xPosStart + i * _metrics.SignWidth;
            Line signSeparatorLineTop = new Line
            {
                X1 = xPos,
                Y1 = 0,
                X2 = xPos,
                Y2 = yPos,
                Stroke = Brushes.Black
            };
            Lines.Add(signSeparatorLineTop);
        }
        for (int i = 0; i <= 6; i++)
        {
            double xPos = xPosStart + i * _metrics.SignWidth;
            Line signSeparatorLineBottom = new Line
            {
                X1 = xPos,
                Y1 = _metrics.CanvasSizeVertical - _metrics.LongDegreeBottomOffset,
                X2 = xPos,
                Y2 = _metrics.CanvasSizeVertical,
                Stroke = Brushes.Black
            };
            Lines.Add(signSeparatorLineBottom);
        }
        
        // draw sign glyphs
        string[] glyphs = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=" };
        double fontSize = _metrics.SignGlyphSize;
        DimTextBlock dimTextBlock = new(_metrics.GlyphsFontFamily, fontSize, 1.0, Colors.DarkSlateBlue);
        xPosStart = _metrics.DiagramOffsetLeft + _metrics.SignWidth / 2.0;
        yPos = _metrics.LongDegreeTopOffset / 2.0;
        for (int i = 0; i < 6; i++)
        {
            double xPos = xPosStart + i * _metrics.SignWidth;            
            SignGlyphs.Add(dimTextBlock.CreateTextBlock(glyphs[i], xPos - fontSize / 3, yPos - fontSize / 1.8));
        }
        yPos = _metrics.CanvasSizeVertical - _metrics.LongDegreeBottomOffset / 2.0;
        for (int i = 0; i < 6; i++)
        {
            double xPos = xPosStart + i * _metrics.SignWidth;            
            SignGlyphs.Add(dimTextBlock.CreateTextBlock(glyphs[11 - i], xPos - fontSize / 3, yPos - fontSize / 1.8));
        }
        
        
    }
    
    
    

    private GraphicCelPointForDeclDiagram ConvertFullPosToGraphicForDeclDiagram(
        KeyValuePair<ChartPoints, FullPointPos> pointPosition)
    {
        char glyph = GlyphsForChartPoints.FindGlyph(pointPosition.Key);
        double longitude = pointPosition.Value.Ecliptical.MainPosSpeed.Position;
        double declination = pointPosition.Value.Equatorial.DeviationPosSpeed.Position;
        double declSpeed = pointPosition.Value.Equatorial.DeviationPosSpeed.Speed;
        string longitudeText = _doubleToDmsConversions.ConvertDoubleToDmInSignNoGlyph(longitude);
        string declinationText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(declination);
        return new GraphicCelPointForDeclDiagram(glyph, longitude, declination, declSpeed, longitudeText,
            declinationText);
    }

    
    
    public Tuple<List<Point>, List<Point>> DefinePolygonPoints()
    {
        List<Point> PolygonPointsNorth = new List<Point>();
        List<Point> PolygonPointsSouth = new List<Point>();
        double longDegreeSize = _metrics.DiagramWidth / LONG_DEGREES_COUNT;
        double declDegreeSize = _metrics.DeclinationBarHeight / (DECL_DEGREES_COUNT * 2);
        double sizeFor60DegreesDeclination = _metrics.CanvasSizeVertical - _metrics.DeclDegreeTopOffset -
                                             _metrics.DeclDegreeBottomOffset;
        double heightForInBoundsRegion = (_obliquity / DECL_DEGREES_COUNT) * sizeFor60DegreesDeclination;
        double offsetInBoundsRegion = (_metrics.CanvasSizeVertical - heightForInBoundsRegion) / 2.0;
        double horizontalOffset =  _metrics.DiagramOffsetLeft;
        for (int i = 0; i <= 180; i++)
        {
            double decl = CalcDecl(i, _obliquity);
            double declOffsetNorth = _metrics.CanvasSizeVertical / 2.0 - (decl * declDegreeSize);
            double declOffsetSouth = _metrics.CanvasSizeVertical / 2.0 + (decl * declDegreeSize);
            double longOffset = horizontalOffset + (i * longDegreeSize);
            PolygonPointsNorth.Add(new Point(longOffset, declOffsetNorth));
            PolygonPointsSouth.Add(new Point(longOffset, declOffsetSouth));
        }
        return new Tuple<List<Point>, List<Point>>(PolygonPointsNorth, PolygonPointsSouth);
    }

    private double CalcDecl(double longitude, double obliquity)
    {
        
        return  MathExtra.RadToDeg(Math.Asin(Math.Sin(MathExtra.DegToRad(longitude)) * Math.Sin(MathExtra.DegToRad(obliquity))));
    }
    
}



