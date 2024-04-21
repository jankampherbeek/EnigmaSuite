// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Enigma.Domain.Constants;
using Enigma.Frontend.Ui.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.Views;

public partial class DeclStripWindow : Window
{
    private readonly DeclStripCanvasController CanvasController;
    
    public DeclStripWindow()
    {
        InitializeComponent();
        DefineColors();
        CanvasController = App.ServiceProvider.GetRequiredService<DeclStripCanvasController>();
    }
    
    private void DiagramSizeChanged(object sender, SizeChangedEventArgs e)
    {
        double availHeight = Height - 300;            // subtract size of rows for description and buttons
        //double availWidth = Width - 280;                // subtract size of right column
        double availWidth = Width;                // subtract size of right column
        CanvasController.Resize(availHeight, availWidth);
        StripCanvas.Height = CanvasController.CanvasHeightSize;
        StripCanvas.Width = CanvasController.CanvasWidthSize;
        StripCanvas.Children.Clear();
        Populate();
    }
    
    private void Populate()
    {
        StripCanvas.Children.Clear();
        CanvasController.PrepareDraw();
        AddToDiagram(new List<UIElement>(CanvasController.Rectangles));
        AddToDiagram(new List<UIElement>(CanvasController.Lines));
        AddToDiagram(new List<UIElement>(CanvasController.DegreeNumbers));
        AddToDiagram(new List<UIElement>(CanvasController.Glyphs));
        AddToDiagram(new List<UIElement>(CanvasController.Directions));
    }


    
    private void AddToDiagram(List<UIElement> uiElements)
    {
        foreach (var uiElement in uiElements)
        {
            StripCanvas.Children.Add(uiElement);
        }        
    }
    
    private void DefineColors()
    {
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
    }
    
}