// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        // Trigger initial population when window loads
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.Populate();
            UpdateHistograms();
        }
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
            viewModel.Populate();
            UpdateHistograms();
        }
    }

    private void IncludeErisCheckBox_Changed(object sender, RoutedEventArgs e)
    {
        // Trigger population when Include Eris checkbox changes
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.Populate();
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
            // Update Crosses Plot
            if (viewModel.HistogramCrossesValues.Length > 0)
            {
                UpdateSingleHistogram(CrossesPlot, viewModel.HistogramCrossesValues, viewModel.HistogramCrossesLabels, "Crosses");
            }
            
            // Update Elements Plot
            if (viewModel.HistogramElementsValues.Length > 0)
            {
                UpdateSingleHistogram(ElementsPlot, viewModel.HistogramElementsValues, viewModel.HistogramElementsLabels, "Elements");
            }
            // Update quadrants Plot
            if (viewModel.HistogramQuadrantValues.Length > 0)
            {
                UpdateSingleHistogram(QuadrantsPlot, viewModel.HistogramQuadrantValues, viewModel.HistogramQuadrantLabels, "Quadrants");
            }
            // update decns plot
            if (viewModel.HistogramDecanValues.Length > 0)
            {
                UpdateSingleHistogram(DecansPlot, viewModel.HistogramDecanValues, viewModel.HistogramDecanLabels, "Decans", "EnigmaAstrologyBLA2");
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
}
