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

/// <summary>View for declination strip</summary>
public partial class DeclStripWindow
{
    private readonly DeclStripCanvasController _canvasController;
    private const int SPACE_FOR_DESCRIPTION_AND_BUTTONS = 300;
    
    public DeclStripWindow()
    {
        InitializeComponent();
        DefineColors();
        _canvasController = App.ServiceProvider.GetRequiredService<DeclStripCanvasController>();
    }
    
    /*private void window1_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        var a = window1.ActualHeight;
        var b = window1.ActualWidth;
        var c = window1.Height;
        var d = window1.Width;
    }*/
    
    
    
    private void DiagramSizeChanged(object sender, SizeChangedEventArgs e)
    {
        double availHeight = ActualHeight - SPACE_FOR_DESCRIPTION_AND_BUTTONS;
        double availWidth = ActualWidth;
        var height = Height;
        var width = Width;
        _canvasController.Resize(availHeight, availWidth);
        StripCanvas.Height = _canvasController.CanvasHeightSize;
        StripCanvas.Width = _canvasController.CanvasWidthSize;
        StripCanvas.Children.Clear();
        Populate();
    }
    
    private void Populate()
    {
        StripCanvas.Children.Clear();
        _canvasController.PrepareDraw();
        AddToDiagram(new List<UIElement>(_canvasController.Rectangles));
        AddToDiagram(new List<UIElement>(_canvasController.Lines));
        AddToDiagram(new List<UIElement>(_canvasController.DegreeNumbers));
        AddToDiagram(new List<UIElement>(_canvasController.Glyphs));
        AddToDiagram(new List<UIElement>(_canvasController.Directions));
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