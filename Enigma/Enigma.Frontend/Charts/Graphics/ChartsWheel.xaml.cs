// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Enigma.Frontend.Ui.Charts.Graphics;

public partial class ChartsWheel : Window
{
    private readonly ChartsWheelController _controller;

    public ChartsWheel(ChartsWheelController controller)
    {
        InitializeComponent();
        _controller = controller;
    }

    public void Populate()
    {
        PopulateTexts();
        wheelCanvas.Children.Clear();
        _controller.PrepareDraw();
        DrawChartFrame();
        DrawCusps();
        DrawCelPoints();
        DrawAspects();
    }


    private void PopulateTexts()
    {
        tbDetails.Text = _controller.DescriptiveText();
        Title = Rosetta.TextForId("charts.wheel.title");
        btnClose.Content = Rosetta.TextForId("common.btnclose");
        btnHelp.Content = Rosetta.TextForId("common.btnhelp");


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
            wheelCanvas.Children.Add(textBlock);
        }
    }

    private void AddToWheel(List<Line> lines)
    {
        foreach (var line in lines)
        {
            wheelCanvas.Children.Add(line);
        }
    }

    private void AddToWheel(List<Ellipse> circles)
    {
        foreach (var circle in circles)
        {
            wheelCanvas.Children.Add(circle);
        }
    }


    private void WheelGrid_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        double availHeight = Height - 120.0;
        double minSize = Math.Min(availHeight, Width);
        _controller.Resize(minSize);
        wheelCanvas.Height = _controller.CanvasSize;
        wheelCanvas.Width = _controller.CanvasSize;
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
