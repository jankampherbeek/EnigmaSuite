// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Graphics;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Graphics;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Conversions;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>Controller for DeclStrip view</summary>
/// <remarks>This view uses MVC instead of MVVM</remarks>
public class DeclStripCanvasController
{
    private const int INITIAL_DECL_RANGE = 30; 
    private const int DECL_STEP_SIZE = 5;           // add to decl range in one step, if declination is very large. 
    
    private readonly DeclStripMetrics _metrics;
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly double _obliquity;
    public double CanvasWidthSize { get; private set; }
    public double CanvasHeightSize { get; private set; }
    public List<Rectangle> Rectangles { get; } = new();
    public List<Line> Lines { get; } = new();
    public List<TextBlock> DegreeNumbers { get; } = new();
    public List<TextBlock> Directions { get; } = new();
    public List<TextBlock> Glyphs { get; } = new();
    
    private readonly CalculatedChart? _currentChart;
    
    public DeclStripCanvasController(DeclStripMetrics metrics, IDoubleToDmsConversions doubleToDmsConversions)
    {
        _metrics = metrics;
        _doubleToDmsConversions = doubleToDmsConversions;
        var dataVaultCharts = DataVaultCharts.Instance;
        _currentChart = dataVaultCharts.GetCurrentChart();
        if (_currentChart is null)
        {
            const string errorMsg = "No chart available when handling declination strip in DeclStripCanvasController";
            Log.Error(errorMsg);
            throw new EnigmaException(errorMsg);
        }
        _obliquity = _currentChart!.Obliquity; 
    }

    
    /// <summary>Handles resizing of canvas.</summary>
    /// <param name="newHeight">The new height.</param>
    /// <param name="newWidth">The new width.</param>
    public void Resize(double newHeight, double newWidth)
    {
        _metrics.SetSizeFactors(newHeight / 640.0, newWidth / 800.0);
        CanvasWidthSize = _metrics.CanvasWidth;
        CanvasHeightSize = _metrics.CanvasHeight;
        PrepareDraw();
        
    }
    
    /// <summary>Clear and redeine all components for the canvas.</summary>
    public void PrepareDraw()
    {
        DefineDeclRange();
        Rectangles.Clear();
        Lines.Clear();
        DegreeNumbers.Clear();
        Glyphs.Clear();
        Directions.Clear();
        
        HandleRectangles();
        HandleDegreeLines();
        HandleDegreeNumbers();
        HandlePositions();
        HandleDirections();
    }
    
      private void HandleRectangles()
    {
        Rectangle backgroundForDiagram = new()
        {
            Fill = Brushes.LightCyan,
            Height = _metrics.CanvasHeight,
            Width = _metrics.CanvasWidth, 
            Opacity = _metrics.FullOpacity
        };
        Canvas.SetLeft(backgroundForDiagram, 0.0);
        Canvas.SetTop(backgroundForDiagram, 0.0);
        Rectangles.Add(backgroundForDiagram);
        
        double heightForInBoundsRegion = (_obliquity / _metrics.DeclDegreesCount) * _metrics.BarHeight;
        
        Rectangle backgroundForOobRegion = new()
        {
            Fill = Brushes.LightBlue,
            Height = _metrics.CanvasHeight - heightForInBoundsRegion,
            Width = _metrics.CanvasWidth,
            Opacity = _metrics.FullOpacity
        };
        Canvas.SetLeft(backgroundForOobRegion, 0.0);
        Canvas.SetTop(backgroundForOobRegion, 0.0);
        Rectangles.Add(backgroundForOobRegion);

        Rectangle backGroundForBar = new()
        {
            Fill = Brushes.Khaki,
            Height = _metrics.BarHeight,
            Width = _metrics.BarWidth,
            Opacity = _metrics.FullOpacity
        };
        Canvas.SetLeft(backGroundForBar, _metrics.BarX);
        Canvas.SetTop(backGroundForBar, _metrics.BarY);
        Rectangles.Add(backGroundForBar);

        Rectangle backGroundForBottomLine = new()
        {
            Fill = Brushes.Khaki,
            Height = _metrics.NorthSouthBarHeight,
            Width = _metrics.CanvasWidth,
            Opacity = _metrics.FullOpacity
        };
        Canvas.SetLeft(backGroundForBottomLine, _metrics.NorthSouthBarX);
        Canvas.SetTop(backGroundForBottomLine, _metrics.NorthSouthBarY);
        Rectangles.Add(backGroundForBottomLine);
    }

    private void HandleDegreeLines()
    {
        for (int i = 0; i < _metrics.DeclDegreesCount; i++)
        {
            double yValue = _metrics.DegreesBottom + (i * _metrics.DegreeHeight) + _metrics.CanvasHeight - _metrics.BarHeight; 
            Lines.Add(new Line
            {
                X1 = _metrics.BarOffsetLeft,
                Y1 = yValue,
                X2 = _metrics.BarOffsetRight,
                Y2 = yValue,
                Stroke = Brushes.DarkKhaki
            });
        }
    }

    private void HandleDegreeNumbers()
    {
        DimTextBlock dimDegreeTextLeft = new(_metrics.DegreeTextsFontFamily, _metrics.DegreeTextSize,
            DeclStripMetrics.DEGREE_TEXT_OPACITY, _metrics.DegreeTextColor);
        for (int i = 0; i < _metrics.DeclDegreesCount; i++)
        {
            int degreeValue = _metrics.DeclDegreesCount - i - 1;
            double xPos = _metrics.BarOffsetLeft + _metrics.DegreeOffsetFromLine;
            double yPos = _metrics.DegreesBottom + _metrics.DegreeOffsetFromLine + ((i - 1) * _metrics.DegreeHeight) + _metrics.CanvasHeight - _metrics.BarHeight; 
            RotateTransform rotateTransform = new(0.0);
            TextBlock degreeText = dimDegreeTextLeft.CreateTextBlock(degreeValue.ToString(), xPos, yPos, rotateTransform);
            DegreeNumbers.Add(degreeText);
        }
    }
    
    
    private void HandlePositions()
    {
        List<GraphicCelPointForDeclDiagram> allPoints = 
            _currentChart!.Positions.Where(pointPosition => 
                pointPosition.Key.GetDetails().PointCat == PointCats.Common 
                || pointPosition.Key.GetDetails().PointCat == PointCats.Angle).
                Select(ConvertFullPosToGraphicForDeclDiagram).ToList();
        double fontSize = _metrics.CelPointGlyphSize;
        DimTextBlock dimTextBlock = new(_metrics.GlyphsFontFamily, fontSize, 1.0, _metrics.CelPointGlyphColor);
        List<GraphicCelPointForDeclDiagram> pointsNorth = new();
        List<GraphicCelPointForDeclDiagram> pointsSouth = new();
        foreach (var point in allPoints)
        {
            if (point.Declination >= 0.0) pointsNorth.Add(point);
            else pointsSouth.Add(point);
        }

        IOrderedEnumerable<GraphicCelPointForDeclDiagram> pointsNorthSorted = pointsNorth.OrderBy(a => a.Declination);
        IOrderedEnumerable<GraphicCelPointForDeclDiagram> pointsSouthSorted = pointsSouth.OrderByDescending(a => a.Declination);

        double xNorth = _metrics.CelPointNorthBaseXPos;
        double margin = _metrics.CelPointMargin;
        int marginFactor = 0;
        double lastDecl = -100.0;
        foreach (var point in pointsNorthSorted)
        {
            if ((point.Declination - lastDecl) < 0.5) marginFactor++;
            else marginFactor = 0;
            double xPos = xNorth - marginFactor * margin;
            double yValue = _metrics.DegreesBottom + ((_metrics.DeclDegreesCount - point.Declination - 1) * _metrics.DegreeHeight) + _metrics.CanvasHeight - _metrics.BarHeight; 
            Glyphs.Add(dimTextBlock.CreateTextBlock(point.Glyph.ToString(), xPos - fontSize / 3, yValue - fontSize / 1.8));
            lastDecl = point.Declination;
            
            Line positionLineNorth = new Line
            {
                X1 = xPos,
                Y1 = yValue,
                X2 = _metrics.BarOffsetLeft,
                Y2 = yValue,
                Stroke = Brushes.DarkCyan,
                StrokeThickness = _metrics.PositionLineStrokeSize,
                Opacity = 0.5
            };
            Lines.Add(positionLineNorth);
            
        }

        double xSouth = _metrics.CelPointSouthBaseXPos;
        marginFactor = 0;
        lastDecl = -100.0;
        foreach (var point in pointsSouthSorted)
        {
            if ((Math.Abs(point.Declination - lastDecl)) < 0.5) marginFactor++;
            else marginFactor = 0;
            double xPos = xSouth + marginFactor * margin;
            double yValue = _metrics.DegreesBottom + ((_metrics.DeclDegreesCount - Math.Abs(point.Declination) - 1) * _metrics.DegreeHeight) + _metrics.CanvasHeight - _metrics.BarHeight; 
            Glyphs.Add(dimTextBlock.CreateTextBlock(point.Glyph.ToString(), xPos - fontSize / 3, yValue - fontSize / 1.8));
            lastDecl = point.Declination;
            
            Line positionLineSouth = new Line
            {
                X1 = xPos,
                Y1 = yValue,
                X2 = _metrics.BarOffsetRight,
                Y2 = yValue,
                Stroke = Brushes.DarkCyan,
                StrokeThickness = _metrics.PositionLineStrokeSize,
                Opacity = 0.5
            };
            Lines.Add(positionLineSouth);
            
        }
        
    }
    
    
    private void HandleDirections() {
        DimTextBlock directionTextBlock = new(_metrics.DegreeTextsFontFamily, _metrics.DegreeTextSize,
            DeclStripMetrics.DEGREE_TEXT_OPACITY, _metrics.DegreeTextColor);
        double xPos = _metrics.LabelNorthXPos;
        double yPos = _metrics.CanvasHeight - _metrics.DegreesBottom;
        RotateTransform rotateTransform = new(0.0);
        TextBlock directionNorthText = directionTextBlock.CreateTextBlock("North", xPos, yPos, rotateTransform);
        Directions.Add(directionNorthText);
        xPos = _metrics.LabelSouthXPos;
        TextBlock directionsSouthText = directionTextBlock.CreateTextBlock("South", xPos, yPos, rotateTransform);
        Directions.Add(directionsSouthText);
        

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
    
    private void DefineDeclRange()
    {
        int range = INITIAL_DECL_RANGE;
        double maxDecl = _currentChart!.Positions.Select(pointPosition => 
            pointPosition.Value.Equatorial.DeviationPosSpeed.Position).Prepend(0.0).Max();
        if (maxDecl > INITIAL_DECL_RANGE)
        {
            double delta = maxDecl - INITIAL_DECL_RANGE;
            int stepCount = (int)delta / DECL_STEP_SIZE + 1;
            range = INITIAL_DECL_RANGE + stepCount * DECL_STEP_SIZE;
        }
        _metrics.DeclDegreesCount = range;
    }
    
    
}