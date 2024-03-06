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
        DrawLines();
    }

    private void DrawPolygons()
    {
       AddToDiagram(new List<UIElement>(_canvasController.Polygons));
    }

    private void DrawLines()
    {
        AddToDiagram((new List<UIElement>(_canvasController.Lines)));
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
    private void AddToDiagram(List<UIElement> uiElements)
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