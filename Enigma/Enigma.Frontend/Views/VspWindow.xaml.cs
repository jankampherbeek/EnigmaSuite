// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Frontend.Ui.Graphics;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.ViewModels;
using Enigma.Frontend.Ui.Models;
using Microsoft.Extensions.DependencyInjection;
using Enigma.Api.Calc;
using Enigma.Domain.Constants;
using Enigma.Domain.Requests;
using Enigma.Domain.References;
using Enigma.Core.Slices.VenusStarPoint;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for Venus Star Point results</summary>
public partial class VspWindow
{
    private ChartsWheelCanvasController _canvasController;

    public VspWindow()
    {
        InitializeComponent();
        _canvasController = App.ServiceProvider.GetRequiredService<ChartsWheelCanvasController>();
        _canvasController.AllPositions = DataVaultCharts.Instance.GetCurrentChart().Positions;
        // Set up property change handling for the ViewModel
        DataContextChanged += OnDataContextChanged;
        
        // Don't call Populate here - wait for DataContext to be set
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        Console.WriteLine($"OnDataContextChanged called. NewValue: {e.NewValue?.GetType().Name ?? "null"}");
        System.Diagnostics.Debug.WriteLine($"OnDataContextChanged called. NewValue: {e.NewValue?.GetType().Name ?? "null"}");
        
        if (e.NewValue is VspViewModel viewModel)
        {
            Console.WriteLine("VspViewModel detected, setting up and populating");
            System.Diagnostics.Debug.WriteLine("VspViewModel detected, setting up and populating");
            
            // Sync the controller with the ViewModel
            _canvasController.ShowSignBackgroundColors = viewModel.ShowSignBackgroundColors;
            
            // Set up property change notification
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
            
            // Populate the chart after the ViewModel is set
            Populate();
        }
        else
        {
            Console.WriteLine($"DataContext is not VspViewModel: {e.NewValue?.GetType().Name ?? "null"}");
            System.Diagnostics.Debug.WriteLine($"DataContext is not VspViewModel: {e.NewValue?.GetType().Name ?? "null"}");
        }
    }
    
    private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(VspViewModel.ShowSignBackgroundColors))
        {
            var viewModel = (VspViewModel)sender;
            _canvasController.ShowSignBackgroundColors = viewModel.ShowSignBackgroundColors;
            Populate();
        }
    }
    
    public void Populate()
    {
        Console.WriteLine("=== Populate() called ===");
        System.Diagnostics.Debug.WriteLine("=== Populate() called ===");
        
        // Check if ViewModel is properly set
        var viewModel = DataContext as VspViewModel;
        Console.WriteLine($"Populate: ViewModel is {viewModel != null}");
        System.Diagnostics.Debug.WriteLine($"Populate: ViewModel is {viewModel != null}");
        
        WheelCanvas.Children.Clear();
        
        // Get the current chart from DataVaultCharts (same as SolarResultsWindow)
        var currentChart = DataVaultCharts.Instance.GetCurrentChart();
        if (currentChart != null)
        {
            _canvasController.AllPositions = currentChart.Positions;
        }
        
        _canvasController.PrepareDraw();
        DrawChartFrame();
        DrawCusps();
        DrawCelPoints();
        Console.WriteLine("About to call DrawVspPoints()");
        System.Diagnostics.Debug.WriteLine("About to call DrawVspPoints()");
        DrawVspPoints();
        Console.WriteLine("DrawVspPoints() completed");
        System.Diagnostics.Debug.WriteLine("DrawVspPoints() completed");
        Console.WriteLine($"Total children in WheelCanvas: {WheelCanvas.Children.Count}");
        System.Diagnostics.Debug.WriteLine($"Total children in WheelCanvas: {WheelCanvas.Children.Count}");
    }
    

    
    private void DrawChartFrame()
    {
        AddToWheel(new List<UIElement>(_canvasController.WheelCircles));
        AddToWheel(new List<UIElement>(_canvasController.SignBackgroundSectors));
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
    
    private void DrawVspPoints()
    {
        Console.WriteLine("=== DrawVspPoints() called ===");
        System.Diagnostics.Debug.WriteLine("=== DrawVspPoints() called ===");
        var viewModel = DataContext as VspViewModel;
        Console.WriteLine($"ViewModel: {viewModel != null}");
        System.Diagnostics.Debug.WriteLine($"ViewModel: {viewModel != null}");
        
        if (viewModel != null)
        {
            Console.WriteLine($"VspPositions: {viewModel.VspPositions != null}, Count: {viewModel.VspPositions?.Count ?? 0}");
            System.Diagnostics.Debug.WriteLine($"VspPositions: {viewModel.VspPositions != null}, Count: {viewModel.VspPositions?.Count ?? 0}");
            
            if (viewModel.VspPositions != null && viewModel.VspPositions.Count > 0)
            {
                Console.WriteLine($"VSP Positions count: {viewModel.VspPositions.Count}");
                System.Diagnostics.Debug.WriteLine($"VSP Positions count: {viewModel.VspPositions.Count}");
                var vspTexts = CreateVspTexts(viewModel.VspPositions);
                Console.WriteLine($"VSP Texts created: {vspTexts.Count}");
                System.Diagnostics.Debug.WriteLine($"VSP Texts created: {vspTexts.Count}");
                AddToWheel(vspTexts);
                
                // Create and add VSP connection lines
                var vspLines = CreateVspConnectionLines(viewModel.VspPositions);
                Console.WriteLine($"VSP Lines created: {vspLines.Count}");
                System.Diagnostics.Debug.WriteLine($"VSP Lines created: {vspLines.Count}");
                AddToWheel(vspLines);
            }
            else
            {
                Console.WriteLine("VSP ViewModel or VspPositions is null or empty");
                System.Diagnostics.Debug.WriteLine("VSP ViewModel or VspPositions is null or empty");
            }
        }
    }
    
    
    private void AddToWheel(List<UIElement> uiElements)
    {
        foreach (var uiElement in uiElements)
        {
            WheelCanvas.Children.Add(uiElement);
        }
    }
    
    private List<UIElement> CreateVspTexts(List<PresentableVspPosition> vspPositions)
    {
        var vspTexts = new List<UIElement>();
        var centerPoint = new Point(_canvasController.CanvasSize / 2, _canvasController.CanvasSize / 2);
        var ascendantLongitude = _canvasController.NoTime ? 0.0 : 
            DataVaultCharts.Instance.GetCurrentChart()?.Positions[ChartPoints.Ascendant].Ecliptical.MainPosSpeed.Position ?? 0.0;
        
        System.Diagnostics.Debug.WriteLine($"Canvas Size: {_canvasController.CanvasSize}");
        System.Diagnostics.Debug.WriteLine($"Center Point: {centerPoint}");
        System.Diagnostics.Debug.WriteLine($"Ascendant Longitude: {ascendantLongitude}");
        System.Diagnostics.Debug.WriteLine($"VSP Radius: {_canvasController.Metrics.VspRadius}");
        System.Diagnostics.Debug.WriteLine($"VSP Text Size: {_canvasController.Metrics.VspTextSize}");
        
        foreach (var vspPosition in vspPositions)
        {
            System.Diagnostics.Debug.WriteLine($"Processing VSP {vspPosition.SequenceId} at longitude {vspPosition.Longitude}");
            
            // Calculate the angle for positioning
            // Longitude starts at 0° Aries, and 9 o'clock is the ascendant
            // The difference between 0° Aries and ascendant is 360 - longitude asc
            double angle = vspPosition.Longitude - ascendantLongitude + 90.0;
            if (angle < 0.0) angle += 360.0;
            if (angle >= 360.0) angle -= 360.0;
            
            System.Diagnostics.Debug.WriteLine($"Calculated angle: {angle}");
            
            // Create the text block for the VSP number
            var textBlock = new TextBlock
            {
                Text = vspPosition.SequenceId.ToString(),
                FontFamily = _canvasController.Metrics.PositionTextsFontFamily,
                FontSize = _canvasController.Metrics.VspTextSize,
                Foreground = new SolidColorBrush(Colors.Red), // Make it red for visibility
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            
            // Position the text on the VSP radius
            var dimPoint = new DimPoint(centerPoint);
            var point = dimPoint.CreatePoint(angle, _canvasController.Metrics.VspRadius);
            
            System.Diagnostics.Debug.WriteLine($"Positioned at point: {point}");
            
            Canvas.SetLeft(textBlock, point.X - textBlock.FontSize / 3);
            Canvas.SetTop(textBlock, point.Y - textBlock.FontSize / 1.8);
            
            System.Diagnostics.Debug.WriteLine($"Canvas position: Left={Canvas.GetLeft(textBlock)}, Top={Canvas.GetTop(textBlock)}");
            System.Diagnostics.Debug.WriteLine($"Canvas bounds: Width={WheelCanvas.Width}, Height={WheelCanvas.Height}");
            System.Diagnostics.Debug.WriteLine($"Text block bounds: Width={textBlock.ActualWidth}, Height={textBlock.ActualHeight}");
            
            vspTexts.Add(textBlock);
        }
        
        return vspTexts;
    }
    
    private List<UIElement> CreateVspConnectionLines(List<PresentableVspPosition> vspPositions)
    {
        var vspLines = new List<UIElement>();
        var centerPoint = new Point(_canvasController.CanvasSize / 2, _canvasController.CanvasSize / 2);
        var ascendantLongitude = _canvasController.NoTime ? 0.0 : 
            DataVaultCharts.Instance.GetCurrentChart()?.Positions[ChartPoints.Ascendant].Ecliptical.MainPosSpeed.Position ?? 0.0;
        
        // Sort VSP positions by sequence ID to ensure correct order (1, 2, 3, 4, 5)
        var sortedVspPositions = vspPositions.OrderBy(vsp => vsp.SequenceId).ToList();
        
        if (sortedVspPositions.Count < 2)
        {
            return vspLines; // Need at least 2 points to draw lines
        }
        
        // Define the specific connection sequence: 1→3→5→2→4
        int[] connectionSequence = { 1, 3, 5, 2, 4 };
        
        // Create lines connecting VSPs in the specified sequence: 1→3→5→2→4
        for (int i = 0; i < connectionSequence.Length; i++)
        {
            int currentSequenceId = connectionSequence[i];
            int nextSequenceId = connectionSequence[(i + 1) % connectionSequence.Length]; // Wrap around to first point
            
            // Find the VSP positions for the current and next sequence IDs
            var currentVsp = sortedVspPositions.FirstOrDefault(vsp => vsp.SequenceId == currentSequenceId);
            var nextVsp = sortedVspPositions.FirstOrDefault(vsp => vsp.SequenceId == nextSequenceId);
            
            if (currentVsp != null && nextVsp != null)
            {
                // Calculate angles for both points
                double angle1 = currentVsp.Longitude - ascendantLongitude + 90.0;
                if (angle1 < 0.0) angle1 += 360.0;
                if (angle1 >= 360.0) angle1 -= 360.0;
                
                double angle2 = nextVsp.Longitude - ascendantLongitude + 90.0;
                if (angle2 < 0.0) angle2 += 360.0;
                if (angle2 >= 360.0) angle2 -= 360.0;
                
                // Calculate positions on the VSP radius (at the circle edge)
                var dimPoint = new DimPoint(centerPoint);
                var point1 = dimPoint.CreatePoint(angle1, _canvasController.Metrics.VspRadius);
                var point2 = dimPoint.CreatePoint(angle2, _canvasController.Metrics.VspRadius);
                
                // Adjust line endpoints to start from the circle edge rather than center of numbers
                // Move the line endpoints slightly inward from the VSP radius to avoid overlapping with text
                double lineRadius = _canvasController.Metrics.VspRadius - 8.0; // Move 8 pixels inward from VSP radius
                var linePoint1 = dimPoint.CreatePoint(angle1, lineRadius);
                var linePoint2 = dimPoint.CreatePoint(angle2, lineRadius);
                
                // Create the line
                var line = new Line
                {
                    X1 = linePoint1.X,
                    Y1 = linePoint1.Y,
                    X2 = linePoint2.X,
                    Y2 = linePoint2.Y,
                    Stroke = new SolidColorBrush(Colors.Blue),
                    StrokeThickness = 6.0,
                    Opacity = 0.8
                };
                
                System.Diagnostics.Debug.WriteLine($"Created VSP line from {currentVsp.SequenceId} to {nextVsp.SequenceId}: ({point1.X:F1}, {point1.Y:F1}) to ({point2.X:F1}, {point2.Y:F1})");
                
                vspLines.Add(line);
            }
        }
        
        return vspLines;
    }
    
    private void WheelGrid_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        // Account for: Header (~40px) + Chart Name (~30px) + margins and padding (~100px)
        double reservedHeight = 170.0;
        double availHeight = ActualHeight - reservedHeight;
        double minSize = Math.Min(availHeight, ActualWidth / 2); // Half width since we have two columns
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
    
  private void SignColors_Checked(object sender, RoutedEventArgs e)
    {
        _canvasController.ShowSignBackgroundColors = false;
        Populate();
    }
    
    private void SignColors_Unchecked(object sender, RoutedEventArgs e)
    {
        _canvasController.ShowSignBackgroundColors = true;
        Populate();
    }
    
    private void Prenatal_Checked(object sender, RoutedEventArgs e)
    {
        var viewModel = DataContext as VspViewModel;
        viewModel?.UpdatePrenatal(true);
    }
    
    private void Prenatal_Unchecked(object sender, RoutedEventArgs e)
    {
        var viewModel = DataContext as VspViewModel;
        viewModel?.UpdatePrenatal(false);
    }
    
    private void ExportClick(object sender, RoutedEventArgs e)
    {
        CanvasExporter.WriteCanvasToPng(WheelCanvas);
    }
}
