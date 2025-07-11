// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Enigma.Frontend.Ui.Graphics;

/// <summary>
/// Export a canvas to an image
/// </summary>
public class CanvasExporter
{

    /// <summary>
    /// Export a canvas to an image at the filesystem at a user defined location. The image format is png.
    /// </summary>
    /// <param name="canvas">The canvas with the image</param>
    public static void WriteCanvasToPng(Canvas canvas)
    {
        var saveDialog = new SaveFileDialog()
        {
            Filter = "PNG Files (*.png)|*.png",
            DefaultExt = ".png"
        };

        if (saveDialog.ShowDialog() != true) return;
        const double dpiScale = 3.0;
        ExportToPng(canvas, saveDialog.FileName, dpiScale);
        MessageBox.Show("Export complete!");


    }

    private static void ExportToPng(Canvas canvas, string filePath, double dpiScale = 1.0, double padding = 10)
    {
        // Calculate total content bounds (including negative coordinates)
        var totalBounds = new Rect(0, 0, canvas.ActualWidth, canvas.ActualHeight);
        foreach (UIElement child in canvas.Children)
        {
            var left = double.IsNaN(Canvas.GetLeft(child)) ? 0 : Canvas.GetLeft(child);
            var top = double.IsNaN(Canvas.GetTop(child)) ? 0 : Canvas.GetTop(child);
            totalBounds.Union(new Rect(left, top, child.RenderSize.Width, child.RenderSize.Height));
        }

        // Apply padding and ensure minimum dimensions
        totalBounds.Inflate(padding, padding);
        var pixelWidth = Math.Max(1, (int)Math.Ceiling(totalBounds.Width * dpiScale));
        var pixelHeight = Math.Max(1, (int)Math.Ceiling(totalBounds.Height * dpiScale));

        // Handle empty canvas
        if (pixelWidth <= 0 || pixelHeight <= 0)
        {
            MessageBox.Show("Nothing to export - there is no image.");
            return;
        }

        // Render with high DPI and offset
        try
        {
            var rtb = new RenderTargetBitmap(
                pixelWidth,
                pixelHeight,
                96 * dpiScale, // Scaled DPI
                96 * dpiScale,
                PixelFormats.Pbgra32
            );

            var dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {
                // Shift content to include top/left padding
                dc.PushTransform(new TranslateTransform(
                    -totalBounds.X + padding,
                    -totalBounds.Y + padding
                ));
                var vb = new VisualBrush(canvas);
                dc.DrawRectangle(vb, null, new Rect(0, 0, canvas.ActualWidth, canvas.ActualHeight));
            }

            rtb.Render(dv);

            // Save as PNG
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));
            using (var fs = new FileStream(filePath, FileMode.Create))
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