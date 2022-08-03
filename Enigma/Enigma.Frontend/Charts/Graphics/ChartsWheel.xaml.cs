// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Enigma.Frontend.Charts.Graphics;

public partial class ChartsWheel : Window
{
    private ChartsWheelController _controller;
   

    public ChartsWheel(ChartsWheelController controller) 
    {
        InitializeComponent();
        _controller = controller;
    }

    public void DrawChart()
    {
        wheelCanvas.Children.Clear();
        _controller.PrepareDraw();
        DrawChartFrame();
        DrawCusps();
        DrawSolSysPoints();
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

    private void DrawSolSysPoints()
    {
        AddToWheel(_controller.SolSysPointGlyphs);
        AddToWheel(_controller.SolSysPointConnectLines);
        AddToWheel(_controller.SolSysPointTexts);
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


    private void wheelGrid_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        double minSize = Math.Min(Height, Width);
        _controller.Resize(minSize);
        wheelCanvas.Height = _controller.CanvasSize;
        wheelCanvas.Width = _controller.CanvasSize;
        DrawChart();
    }

}
