// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Enigma.Frontend.Ui.ViewModels;
using Microsoft.Extensions.DependencyInjection;


namespace Enigma.Frontend.Ui.Views;

public partial class DeclDiagramWindow
{
    public DeclDiagramWindow()
    {
        InitializeComponent();
        _canvasController = App.ServiceProvider.GetRequiredService<DeclDiagramCanvasController>();
    }
    
   private readonly DeclDiagramCanvasController _canvasController;


    public void Populate()
    {
        DiagramCanvas.Children.Clear();
        _canvasController.PrepareDraw();
        DrawPolygons();
        DrawTestLine();

    }

    private void DrawPolygons()
    {
        AddToWheel(new List<UIElement>(_canvasController.Polygons));
    }

    private void DrawTestLine()
    {
        Line line1 = new()
        {
            X1 = 100,
            X2 = 150,
            Y1 = 300,
            Y2 = 500,
            Stroke = new SolidColorBrush(Colors.Aqua),
            StrokeThickness = 6
        };
        Line line2 = new()
        {
            X1 = 120,
            X2 = 170,
            Y1 = 350,
            Y2 = 540,
            Stroke = new SolidColorBrush(Colors.Magenta),
            StrokeThickness = 6
        };
        List<Line> lines = new();
        lines.Add(line1);
        lines.Add(line2);
        AddToWheel(new List<UIElement>(lines));
    }
    
    
/*
    private void DrawChartFrame()
    {
        AddToWheel(new List<UIElement>(_canvasController.WheelCircles));
        AddToWheel(new List<UIElement>(_canvasController.SignSeparators));
        AddToWheel(new List<UIElement>(_canvasController.SignGlyphs));
        AddToWheel(new List<UIElement>(_canvasController.DegreeLines));
    }


    private void DrawCusps()
    {
        AddToWheel(new List<UIElement>(_canvasController.CuspLines));
        AddToWheel(new List<UIElement>(_canvasController.CuspCardinalLines));
        AddToWheel(new List<UIElement>(_canvasController.CuspTexts));
        AddToWheel(new List<UIElement>(_canvasController.CuspCardinalIndicators));
    }

    private void DrawCelPoints()
    {
        AddToWheel(new List<UIElement>(_canvasController.CelPointGlyphs));
        AddToWheel(new List<UIElement>(_canvasController.CelPointConnectLines));
        AddToWheel(new List<UIElement>(_canvasController.CelPointTexts));
    }

    private void DrawAspects()
    {
        AddToWheel(new List<UIElement>(_canvasController.AspectLines));
    }
*/
    private void AddToWheel(List<UIElement> uiElements)
    {
        foreach (var uiElement in uiElements)
        {
            DiagramCanvas.Children.Add(uiElement);
        }        
    }


    private void DiagramGridSizeChanged(object sender, SizeChangedEventArgs e)
    {
        double availHeight = Height - 120.0;
        double minSize = Math.Min(availHeight, Width);
        _canvasController.Resize(minSize);
        DiagramCanvas.Height = _canvasController.CanvasSize;
        DiagramCanvas.Width = _canvasController.CanvasSize;
        Populate();
    }

}