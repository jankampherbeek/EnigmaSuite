// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Enigma.Domain.Dtos;
using Enigma.Frontend.Ui.Graphics;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>Controller for DeclDiagram view</summary>
/// <remarks>This view uses MVC instead of MVVM</remarks>
public class DeclDiagramCanvasController
{
    private readonly DeclDiagramMetrics _metrics;
    private readonly DataVaultCharts _dataVaultCharts;

    public List<Polygon> Polygons { get; private set; } = new();
    public List<Line> Lines { get; set; } = new();
    public double CanvasSize { get; private set; }
    private CalculatedChart? _currentChart;
   
    
    
    public DeclDiagramCanvasController(DeclDiagramMetrics metrics)
    {
        _dataVaultCharts = DataVaultCharts.Instance;
        _metrics = metrics;
    }
    
    
    
    public void Resize(double minSize)
    {
        _metrics.SetSizeFactor(minSize / 740.0);
        CanvasSize = _metrics.GridSize;
        PrepareDraw();
    }

    public void PrepareDraw()
    {
        _currentChart = _dataVaultCharts.GetCurrentChart();
        HandleDebugLine();
        HandlePolygons();
    }

    private void HandleDebugLine()
    {
        Line debugLine = new Line
        {
            X1 = 10.0,
            Y1 = 10.0,
            X2 = 900.0,
            Y2 = 10.0
        };
        debugLine.Stroke = Brushes.Aqua;
        debugLine.StrokeThickness = 4;
        Lines.Add(debugLine);
    }
    
    private void HandlePolygons()
    {
        double obliquity = _dataVaultCharts.GetCurrentChart().Obliquity;
        Tuple<List<Point>, List<Point>> polygonPoints = _metrics.GetPolygonPoints(obliquity);
        List<Point> pointsCompletePolygonNorth = polygonPoints.Item1;
        pointsCompletePolygonNorth.Add(pointsCompletePolygonNorth[0]);    // close polygon
        List<Point> pointsCompletePolygonSouth = polygonPoints.Item2;
        pointsCompletePolygonSouth.Add(pointsCompletePolygonSouth[0]);    // close polygon
        Polygon polygonNorth = new()
        {
            Stroke = Brushes.Coral,
            Fill = Brushes.Bisque,
            StrokeThickness = 1
        };
        Polygon polygonSouth = new()
        {
            Stroke = Brushes.Teal,
            Fill = Brushes.Azure,
            StrokeThickness = 1
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
}