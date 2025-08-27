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
            UpdateHistogram();
        }
    }

    private void BlackMoonCorrectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Trigger population when Black Moon correction selection changes
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.Populate();
            UpdateHistogram();
        }
    }

    private void HouseSystemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Trigger population when House System selection changes
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.Populate();
            UpdateHistogram();
        }
    }

    private void IncludeChironCheckBox_Changed(object sender, RoutedEventArgs e)
    {
        // Trigger population when Include Chiron checkbox changes
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.Populate();
            UpdateHistogram();
        }
    }

    private void IncludeErisCheckBox_Changed(object sender, RoutedEventArgs e)
    {
        // Trigger population when Include Eris checkbox changes
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.Populate();
            UpdateHistogram();
        }
    }
    
    private void UpdateHistogram()
    {
        if (DataContext is not BlaSchemaViewModel viewModel || 
            viewModel.HistogramValues == null || 
            viewModel.HistogramValues.Length == 0)
        {
            return;
        }
        
        try
        {
            // Clear existing plot
            HistogramPlot.Plot.Clear();
            
            // Create bar plot data
            var positions = Enumerable.Range(0, viewModel.HistogramValues.Length).Select(i => (double)i).ToArray();
            var values = viewModel.HistogramValues;
            var labels = viewModel.HistogramLabels;
            
                         // Create bar plot with ScottPlot 5.0 API
             var barPlot = HistogramPlot.Plot.Add.Bars(positions, values);
             
             // Set axis labels (no title to save space)
             HistogramPlot.Plot.YLabel("Total Count");
             
             // Set x-axis tick labels to show the actual names
             if (labels.Length > 0)
             {
                 // Align labels with the bars by adjusting position
                 var tickPositions = Enumerable.Range(0, labels.Length).Select(i => (double)i).ToArray();
                 HistogramPlot.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(tickPositions, labels);
                 
                 // Center the labels under each bar
                 HistogramPlot.Plot.Axes.Bottom.TickLabelStyle.Rotation = 0;
                 HistogramPlot.Plot.Axes.Bottom.TickLabelStyle.Alignment = ScottPlot.Alignment.MiddleCenter;
             }
            
            // Auto-scale the plot
            HistogramPlot.Plot.Axes.AutoScale();
            
            // Refresh the plot
            HistogramPlot.Refresh();
        }
        catch (Exception ex)
        {
            // Log error or show message (you might want to add proper logging here)
            System.Diagnostics.Debug.WriteLine($"Error updating histogram: {ex.Message}");
        }
    }
}
