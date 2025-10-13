// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Enigma.Domain.Constants;
using Enigma.Frontend.Ui.ViewModels;
using Microsoft.Win32;
using ScottPlot;
using ScottPlot.Plottables;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for BLA Schema</summary>
public partial class BlaSchemaWindow
{
    public BlaSchemaWindow()
    {
        InitializeComponent();
        DefineColors();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        // Position window within visible screen area
        PositionWindowWithinScreen();
        
        // Trigger initial population when window loads
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.SetUpBlaSchema();
            UpdateHistograms();
        }
    }
    
    private void PositionWindowWithinScreen()
    {
        // Get the work area (screen minus taskbar)
        var workArea = System.Windows.SystemParameters.WorkArea;
        
        // Set maximum height to work area height to prevent going below taskbar
        this.MaxHeight = workArea.Height;
        
        // Calculate center position
        var centerX = (workArea.Width - this.Width) / 2;
        var centerY = (workArea.Height - this.Height) / 2;
        
        // Ensure window is fully visible
        var left = Math.Max(0, centerX);
        var top = Math.Max(0, centerY);
        
        // If window is too wide, adjust position
        if (this.Width > workArea.Width)
        {
            left = 0;
        }
        
        // If window is too tall, position it at the top
        if (this.Height > workArea.Height)
        {
            top = 0;
        }
        
        // Set window position
        this.Left = left;
        this.Top = top;
    }

    private void BlackMoonCorrectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Trigger population when Black Moon correction selection changes
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.Populate();
            UpdateHistograms();
        }
    }

    private void HouseSystemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Trigger population when House System selection changes
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.Populate();
            UpdateHistograms();
        }
    }

    private void IncludeChironCheckBox_Changed(object sender, RoutedEventArgs e)
    {
        // Trigger population when Include Chiron checkbox changes
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.UpdateChiron();
            UpdateHistograms();
        }
    }

    private void IncludeCeresCheckBox_Changed(object sender, RoutedEventArgs e)
    {
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.UpdateCeres();
            UpdateHistograms();
        }
    }

    private void UseDecanatesCheckBox_Changed(object sender, RoutedEventArgs e)
    {
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.UpdateDecanates();
            UpdateHistograms();
        }
    }
    

    private void UseTrueNodeCheckBox_Changed(object sender, RoutedEventArgs e)
    {
        // Trigger population when Use True node checkbox changes
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.UpdateTrueNode();
            UpdateHistograms();
        }
    }
    
    private void UpdateHistograms()
    {
        if (DataContext is not BlaSchemaViewModel viewModel)
        {
            return;
        }
        
        try
        {
            // update dispositor plot
            if (viewModel.HistogramDispositorValues.Length > 0)
            {
                UpdateSingleHistogram(DispositorsPlot, viewModel.HistogramDispositorValues, viewModel.HistogramDispositorLabels, "Dispositors", "EnigmaAstrologyBLA2");
            }
            
        }
        catch (Exception ex)
        {
            // Log error or show message (you might want to add proper logging here)
            System.Diagnostics.Debug.WriteLine($"Error updating histogram: {ex.Message}");
        }
    }
    
    private void UpdateSingleHistogram(ScottPlot.WPF.WpfPlot plot, double[] values, string[] labels, string title, string fontFamily = null)
    {
        // Clear existing plot
        plot.Plot.Clear();
        
        // Create bar plot data
        var positions = Enumerable.Range(0, values.Length).Select(i => (double)i).ToArray();
        
        // Create bar plot with ScottPlot 5.0 API
        var barPlot = plot.Plot.Add.Bars(positions, values);
        
        // Set axis labels
        plot.Plot.YLabel("Total Count");
        plot.Plot.Title(title);
        
        // Set font for x-axis labels if specified
        if (!string.IsNullOrEmpty(fontFamily) && fontFamily == "EnigmaAstrologyBLA2")
        {
            // Set font name as string for ScottPlot 5.0
            plot.Plot.Axes.Bottom.TickLabelStyle.FontName = "EnigmaAstrologyBLA2";
            // Use larger font size for special font plots (Decans and Dispositors)
            plot.Plot.Axes.Bottom.TickLabelStyle.FontSize = 16;
        }
        else
        {
            // Use smaller font size for regular plots (Elements, Crosses, Quadrants)
            plot.Plot.Axes.Bottom.TickLabelStyle.FontSize = 12;
        }
        
        // Set x-axis tick labels to show the actual names
        if (labels.Length > 0)
        {
            // Align labels with the bars by adjusting position
            var tickPositions = Enumerable.Range(0, labels.Length).Select(i => (double)i).ToArray();
            plot.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(tickPositions, labels);
            
            // Center the labels under each bar
            plot.Plot.Axes.Bottom.TickLabelStyle.Rotation = 0;
            plot.Plot.Axes.Bottom.TickLabelStyle.Alignment = ScottPlot.Alignment.MiddleCenter;
        }
        
        // Auto-scale the plot
        plot.Plot.Axes.AutoScale();
        
        // Refresh the plot
        plot.Refresh();
    }
    
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    private void DefineColors()
    {
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
    }
    
    private void ExportClick(object sender, RoutedEventArgs e)
    {
        var saveDialog = new SaveFileDialog()
        {
            Filter = "PNG Files (*.png)|*.png",
            DefaultExt = ".png",
            FileName = $"BLA_Schema_{DateTime.Now:yyyyMMdd_HHmmss}.png"
        };

        if (saveDialog.ShowDialog() != true) return;
        
        try
        {
            ExportWindowToPng(saveDialog.FileName);
            MessageBox.Show("Export complete!", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Export failed: {ex.Message}", "Export Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void ExportWindowToPng(string filePath)
    {
        // Get the main grid that contains all the content
        var mainGrid = (Grid)this.Content;
        
        // Force layout update to ensure all elements are properly sized
        mainGrid.UpdateLayout();
        this.UpdateLayout();
        
        // Calculate the size of the content we want to export
        var bounds = VisualTreeHelper.GetDescendantBounds(mainGrid);
        var size = new Size(bounds.Width, bounds.Height);
        
        // Create render target bitmap with high DPI for better quality
        const double dpiScale = 2.0;
        var pixelWidth = (int)(size.Width * dpiScale);
        var pixelHeight = (int)(size.Height * dpiScale);
        
        var rtb = new RenderTargetBitmap(
            pixelWidth,
            pixelHeight,
            96 * dpiScale,
            96 * dpiScale,
            PixelFormats.Pbgra32
        );
        
        // Create a drawing visual with white background
        var dv = new DrawingVisual();
        using (var dc = dv.RenderOpen())
        {
            // Fill with white background first
            dc.DrawRectangle(Brushes.White, null, new Rect(0, 0, size.Width, size.Height));
            
            // Render the main grid on top of the white background
            var vb = new VisualBrush(mainGrid);
            vb.Stretch = Stretch.None;
            dc.DrawRectangle(vb, null, new Rect(0, 0, size.Width, size.Height));
        }
        
        // Render the drawing visual
        rtb.Render(dv);
        
        // Save as PNG
        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(rtb));
        
        using (var fs = new FileStream(filePath, FileMode.Create))
        {
            encoder.Save(fs);
        }
    }
}
