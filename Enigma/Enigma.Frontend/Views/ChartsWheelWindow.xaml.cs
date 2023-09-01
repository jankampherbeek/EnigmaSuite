// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Enigma.Frontend.Ui.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for chart wheel</summary>
/// <remarks>Still using MVC instead of MVVM for this view as binding multiple visuals with a canvas is rather challenging</remarks>
public partial class ChartsWheelWindow
{
    private readonly ChartsWheelController _controller;

    public ChartsWheelWindow()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<ChartsWheelController>();
    }

    public void Populate()
    {
        PopulateTexts();
        WheelCanvas.Children.Clear();
        _controller.PrepareDraw();
        DrawChartFrame();
        DrawCusps();
        DrawCelPoints();
        DrawAspects();
    }


    private void PopulateTexts()
    {
        TbDetails.Text = _controller.DescriptiveText();
    }

    private void DrawChartFrame()
    {
        AddToWheel(_controller.WheelCircles);
        AddToWheel(_controller.SignSeparators);
        AddToWheel(_controller.SignGlyphs);
        AddToWheel(_controller.DegreeLines);
    }


    private void DrawCusps()
    {
        AddToWheel(_controller.CuspLines);
        AddToWheel(_controller.CuspCardinalLines);
        AddToWheel(_controller.CuspTexts);
        AddToWheel(_controller.CuspCardinalIndicators);
    }

    private void DrawCelPoints()
    {
        AddToWheel(_controller.CelPointGlyphs);
        AddToWheel(_controller.CelPointConnectLines);
        AddToWheel(_controller.CelPointTexts);
    }

    private void DrawAspects()
    {
        AddToWheel(_controller.AspectLines);
    }

    private void AddToWheel(List<TextBlock> textBlocks)
    {
        foreach (var textBlock in textBlocks)
        {
            WheelCanvas.Children.Add(textBlock);
        }
    }

    private void AddToWheel(List<Line> lines)
    {
        foreach (var line in lines)
        {
            WheelCanvas.Children.Add(line);
        }
    }

    private void AddToWheel(List<Ellipse> circles)
    {
        foreach (var circle in circles)
        {
            WheelCanvas.Children.Add(circle);
        }
    }

    private void WheelGrid_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        double availHeight = Height - 120.0;
        double minSize = Math.Min(availHeight, Width);
        _controller.Resize(minSize);
        WheelCanvas.Height = _controller.CanvasSize;
        WheelCanvas.Width = _controller.CanvasSize;
        Populate();
    }


    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        ChartsWheelController.ShowHelp();
    }

}
