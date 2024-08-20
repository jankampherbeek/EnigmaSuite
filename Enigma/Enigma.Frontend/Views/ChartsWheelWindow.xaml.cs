// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Windows;
using Enigma.Frontend.Ui.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for chart wheel</summary>
/// <remarks>Still using MVC instead of MVVM for this view as binding multiple visuals with a canvas is rather challenging</remarks>
public partial class ChartsWheelWindow
{
    private ChartsWheelCanvasController _canvasController;

    public ChartsWheelWindow()
    {
        InitializeComponent();
        _canvasController = App.ServiceProvider.GetRequiredService<ChartsWheelCanvasController>();
    }

    public void Populate()
    {
        WheelCanvas.Children.Clear();
        _canvasController.PrepareDraw();
        DrawChartFrame();
        DrawCusps();
        DrawCelPoints();
        DrawAspects();
    }

   

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

    private void AddToWheel(List<UIElement> uiElements)
    {
        foreach (var uiElement in uiElements)
        {
            WheelCanvas.Children.Add(uiElement);
        }        
    }

    private void WheelGrid_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        double availHeight = ActualHeight - 120.0;
        double minSize = Math.Min(availHeight, ActualWidth);
        _canvasController.Resize(minSize);
        WheelCanvas.Height = _canvasController.CanvasSize;
        WheelCanvas.Width = _canvasController.CanvasSize;
        Populate();
    }

    private void NoTime_Checked(object sender, RoutedEventArgs e)
    {
        _canvasController.NoTime = true;
        Populate();
    }

    private void NoTime_Unchecked(object sender, RoutedEventArgs e)
    {
        _canvasController.NoTime = false;
        Populate();
    }
    

}
