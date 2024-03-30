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
        DrawRectangles();
        DrawPolygons();
        DrawLines();
        DrawDegreeTexts();
        DrawSignGlyphs();
    }


    private void DrawRectangles()
    {
        AddToDiagram(new List<UIElement>(_canvasController.Rectangles));        
    }
    
    private void DrawPolygons()
    {
       AddToDiagram(new List<UIElement>(_canvasController.Polygons));
    }

    private void DrawLines()
    {
        AddToDiagram((new List<UIElement>(_canvasController.Lines)));
    }

    private void DrawDegreeTexts()
    {
        AddToDiagram(new List<UIElement>(_canvasController.VerticalDegreesTexts));
    }

    private void DrawSignGlyphs()
    {
        AddToDiagram(new List<UIElement>(_canvasController.SignGlyphs));
    }
    
    private void AddToDiagram(List<UIElement> uiElements)
    {
        foreach (var uiElement in uiElements)
        {
            DiagramCanvas.Children.Add(uiElement);
        }        
    }


    private void DiagramSizeChanged(object sender, SizeChangedEventArgs e)
    {
        double availHeight = Height - 140.0;
        double minSize = Math.Min(availHeight, Width);
        _canvasController.Resize(minSize);
        DiagramCanvas.Height = _canvasController.CanvasWidthSize;
        DiagramCanvas.Width = _canvasController.CanvasWidthSize;
        DiagramCanvas.Children.Clear();
        Populate();
    }

}