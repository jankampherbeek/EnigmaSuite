// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Enigma.Domain.Constants;
using Enigma.Frontend.Ui.ViewModels;
using Microsoft.Extensions.DependencyInjection;


namespace Enigma.Frontend.Ui.Views;

public partial class DeclDiagramWindow
{
    public DeclDiagramWindow()
    {
        InitializeComponent();
        DefineColors();
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
        double availHeight = Height - 200.0;            // subtract size of rows for description and buttons
        double availWidth = Width - 280;                // subtract size of right column
        _canvasController.Resize(availHeight, availWidth);
        DiagramCanvas.Height = _canvasController.CanvasHeightSize;
        DiagramCanvas.Width = _canvasController.CanvasWidthSize;
        DiagramCanvas.Children.Clear();
        Populate();
    }

    private void DefineColors()
    {
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
    }
    
}