// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Enigma.Domain.Constants;
using Enigma.Frontend.Ui.ViewModels;
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
}
