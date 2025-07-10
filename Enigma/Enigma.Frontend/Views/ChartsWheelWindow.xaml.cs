// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Enigma.Frontend.Ui.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

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
        if (!_canvasController.NoAspects)
        {
            DrawAspects();            
        }
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

    private void NoAspects_Checked(object sender, RoutedEventArgs e)
    {
        _canvasController.NoAspects = true;
        Populate();
    }
    
    private void NoAspects_UnChecked(object sender, RoutedEventArgs e)
    {
        _canvasController.NoAspects = false;
        Populate();
    }

    private void ExportClick(object sender, RoutedEventArgs e)
    {
        var saveDialog = new SaveFileDialog()
        {
            Filter = "PNG Files (*.png)|*.png",
            DefaultExt = ".png"
        };

        if (saveDialog.ShowDialog() == true)
        {
            var dpiScale = 3.0;
            ExportToPng(WheelCanvas, saveDialog.FileName, dpiScale);
            MessageBox.Show("Export complete!");
        }
        
    }
    
    public static void ExportToPng(Canvas canvas, string filePath, double dpiScale = 1.0, double padding = 10)
{
    // 1. Calculate total content bounds (including negative coordinates)
    Rect totalBounds = new Rect(0, 0, canvas.ActualWidth, canvas.ActualHeight);
    foreach (UIElement child in canvas.Children)
    {
        double left = double.IsNaN(Canvas.GetLeft(child)) ? 0 : Canvas.GetLeft(child);
        double top = double.IsNaN(Canvas.GetTop(child)) ? 0 : Canvas.GetTop(child);
        totalBounds.Union(new Rect(left, top, child.RenderSize.Width, child.RenderSize.Height));
    }

    // 2. Apply padding and ensure minimum dimensions
    totalBounds.Inflate(padding, padding);
    int pixelWidth = Math.Max(1, (int)Math.Ceiling(totalBounds.Width * dpiScale));
    int pixelHeight = Math.Max(1, (int)Math.Ceiling(totalBounds.Height * dpiScale));

    // 3. Handle empty canvas
    if (pixelWidth <= 0 || pixelHeight <= 0)
    {
        MessageBox.Show("Nothing to export - canvas is empty.");
        return;
    }

    // 4. Render with high DPI and offset
    try
    {
        RenderTargetBitmap rtb = new RenderTargetBitmap(
            pixelWidth,
            pixelHeight,
            96 * dpiScale,  // Scaled DPI
            96 * dpiScale,
            PixelFormats.Pbgra32
        );

        DrawingVisual dv = new DrawingVisual();
        using (DrawingContext dc = dv.RenderOpen())
        {
            // Shift content to include top/left padding
            dc.PushTransform(new TranslateTransform(
                -totalBounds.X + padding, 
                -totalBounds.Y + padding
            ));
            VisualBrush vb = new VisualBrush(canvas);
            dc.DrawRectangle(vb, null, new Rect(0, 0, canvas.ActualWidth, canvas.ActualHeight));
        }
        rtb.Render(dv);

        // 5. Save as PNG
        PngBitmapEncoder encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(rtb));
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            encoder.Save(fs);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Export failed: {ex.Message}");
    }
}
    
    
    public static void ExportToPngBak(Canvas canvas, string filePath)
    {
        // 1. Calculate content bounds (including negative coordinates)
        Rect totalBounds = new Rect(0, 0, canvas.ActualWidth, canvas.ActualHeight);
        foreach (UIElement child in canvas.Children)
        {
            double left = Canvas.GetLeft(child);
            double top = Canvas.GetTop(child);
            totalBounds.Union(new Rect(
                double.IsNaN(left) ? 0 : left,    // Handle NaN values
                double.IsNaN(top) ? 0 : top,
                child.RenderSize.Width,
                child.RenderSize.Height
            ));
        }

        // 2. Ensure valid dimensions (minimum 1x1 pixels)
        int pixelWidth = Math.Max(1, (int)Math.Ceiling(totalBounds.Width));
        int pixelHeight = Math.Max(1, (int)Math.Ceiling(totalBounds.Height));

        // 3. Handle empty canvas edge case
        if (pixelWidth <= 0 || pixelHeight <= 0)
        {
            MessageBox.Show("Nothing to export - canvas is empty.");
            return;
        }

        // 4. Render with offset to include top/left content
        try
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap(
                pixelWidth,
                pixelHeight,
                96,  // Standard DPI
                96,
                PixelFormats.Pbgra32
            );

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                dc.PushTransform(new TranslateTransform(-totalBounds.X, -totalBounds.Y));
                VisualBrush vb = new VisualBrush(canvas);
                dc.DrawRectangle(vb, null, new Rect(0, 0, canvas.ActualWidth, canvas.ActualHeight));
            }
            rtb.Render(dv);

            // 5. Save as PNG
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                encoder.Save(fs);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Export failed: {ex.Message}");
        }
    }
}
