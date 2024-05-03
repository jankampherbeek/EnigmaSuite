// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
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
        CanvasController = App.ServiceProvider.GetRequiredService<DeclDiagramCanvasController>();
    }
    
   private readonly DeclDiagramCanvasController CanvasController;


    public void Populate()
    {
        CanvasController.HidePositionLines = CboxHidePositionLines.IsChecked == true;
        DiagramCanvas.Children.Clear();
        CanvasController.PrepareDraw();
        DrawRectangles();
        DrawPolygons();
        DrawLines();
        DrawDegreeTexts();
        DrawSignGlyphs();
    }

    public void CheckBoxPositionLinesClick(object sender, RoutedEventArgs e)
    {
        Populate();
    }

    private void DrawRectangles()
    {
        AddToDiagram(new List<UIElement>(CanvasController.Rectangles));        
    }
    
    private void DrawPolygons()
    {
       AddToDiagram(new List<UIElement>(CanvasController.Polygons));
    }

    private void DrawLines()
    {
        AddToDiagram((new List<UIElement>(CanvasController.Lines)));
    }

    private void DrawDegreeTexts()
    {
        AddToDiagram(new List<UIElement>(CanvasController.VerticalDegreesTexts));
    }

    private void DrawSignGlyphs()
    {
        AddToDiagram(new List<UIElement>(CanvasController.SignGlyphs));
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
        double availHeight = ActualHeight - 200.0;            // subtract size of rows for description and buttons
        double availWidth = ActualWidth - 280;                // subtract size of right column
        CanvasController.Resize(availHeight, availWidth);
        DiagramCanvas.Height = CanvasController.CanvasHeightSize;
        DiagramCanvas.Width = CanvasController.CanvasWidthSize;
        DiagramCanvas.Children.Clear();
        Populate();
    }

    private void DefineColors()
    {
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
    }
    
}