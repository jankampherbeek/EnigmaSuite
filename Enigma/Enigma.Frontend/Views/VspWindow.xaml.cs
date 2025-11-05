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
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for Venus Star Point results</summary>
public partial class VspWindow
{
    private ChartsWheelCanvasController _canvasController;

    private double leftAnchorPoint = 0.0;
    public VspWindow()
    {
        InitializeComponent();
        DefineColors();
        _canvasController = App.ServiceProvider.GetRequiredService<ChartsWheelCanvasController>();
        _canvasController.AllPositions = DataVaultCharts.Instance.GetCurrentChart().Positions;
        var viewModel = DataContext as VspViewModel;

        if (viewModel != null)
        {
            if (viewModel.VspPositions != null && viewModel.VspPositions.Count > 0)
            {
                var lap = viewModel.VspPositions[2].Longitude + 90.0;
                if (lap < 0.0) lap += 360.0;
                leftAnchorPoint = lap;
                _canvasController.LeftAnchorPoint = lap;
            }

        }

        // Set up property change handling for the ViewModel
        DataContextChanged += OnDataContextChanged;
        
        // Don't call Populate here - wait for DataContext to be set
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        
        if (e.NewValue is VspViewModel viewModel)
        {
            // Sync the controller with the ViewModel
            _canvasController.ShowSignBackgroundColors = viewModel.ShowSignBackgroundColors;
            
            // Set up property change notification
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
            
            // Populate the chart after the ViewModel is set
            Populate();
        }
        else
        {
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
        else if (e.PropertyName == nameof(VspViewModel.VspPositions))
        {
            // When VspPositions changes (e.g., when Prenatal checkbox is toggled), refresh the chart wheel
            Populate();
        }
    }
    
    public void Populate()
    {
        // Check if ViewModel is properly set
        var viewModel = DataContext as VspViewModel;
        
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
        DrawVspPoints();
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
        var viewModel = DataContext as VspViewModel;
        
        if (viewModel != null)
        {
            if (viewModel.VspPositions != null && viewModel.VspPositions.Count > 0)
            {
                // First, create and add VSP connection lines (drawn first, behind everything)
                var vspLines = CreateVspConnectionLines(viewModel.VspPositions);
                AddToWheel(vspLines);
                
                // Then, create and add VSP background circles (drawn second, behind text)
                var vspCircles = CreateVspCircles(viewModel.VspPositions);
                AddToWheel(vspCircles);
                
                // Finally, create and add VSP text (drawn last, on top)
                var vspTexts = CreateVspTexts(viewModel.VspPositions);
                AddToWheel(vspTexts);
            }
            else
            {
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
    
    private List<UIElement> CreateVspCircles(List<PresentableVspPosition> vspPositions)
    {
        var vspCircles = new List<UIElement>();
        var centerPoint = new Point(_canvasController.CanvasSize / 2, _canvasController.CanvasSize / 2);
        // var ascendantLongitude = _canvasController.NoTime ? 0.0 : 
        //     DataVaultCharts.Instance.GetCurrentChart()?.Positions[ChartPoints.Ascendant].Ecliptical.MainPosSpeed.Position ?? 0.0;
        foreach (var vspPosition in vspPositions)
        {
            // Calculate the angle for positioning
            double angle = vspPosition.Longitude - leftAnchorPoint + 90.0;
            if (angle < 0.0) angle += 360.0;
            if (angle >= 360.0) angle -= 360.0;
            
            // Position the circle on the VSP radius
            var dimPoint = new DimPoint(centerPoint);
            var point = dimPoint.CreatePoint(angle, _canvasController.Metrics.VspRadius);
            
            // Create the light blue background circle
            var enLargeFactor = 1.8;
            var fillColor = new SolidColorBrush(Color.FromRgb(173, 216, 230)); // Light blue color
            if (vspPosition == vspPositions[2]) fillColor = new SolidColorBrush(Color.FromRgb(255, 153, 153));
            //
            //
            // if (vspPosition == vspPositions[2])
            // {
            //     enLargeFactor = 3.6;
            // }
            var backgroundCircle = new Ellipse
            {

                Width = _canvasController.Metrics.VspTextSize * enLargeFactor,
                Height = _canvasController.Metrics.VspTextSize * enLargeFactor,
                Fill = fillColor
            };
            
            // Position the circle on the canvas
            Canvas.SetLeft(backgroundCircle, point.X - backgroundCircle.Width / 2);
            Canvas.SetTop(backgroundCircle, point.Y - backgroundCircle.Height / 2);
            
            vspCircles.Add(backgroundCircle);
        }
        
        return vspCircles;
    }
    
    private List<UIElement> CreateVspTexts(List<PresentableVspPosition> vspPositions)
    {
        var vspTexts = new List<UIElement>();
        var centerPoint = new Point(_canvasController.CanvasSize / 2, _canvasController.CanvasSize / 2);
        // var ascendantLongitude = _canvasController.NoTime ? 0.0 : 
        //     DataVaultCharts.Instance.GetCurrentChart()?.Positions[ChartPoints.Ascendant].Ecliptical.MainPosSpeed.Position ?? 0.0;
        foreach (var vspPosition in vspPositions)
        {
            // Calculate the angle for positioning
            double angle = vspPosition.Longitude - leftAnchorPoint + 90.0;
            if (angle < 0.0) angle += 360.0;
            if (angle >= 360.0) angle -= 360.0;
            
            // Position the text on the VSP radius
            var dimPoint = new DimPoint(centerPoint);
            var point = dimPoint.CreatePoint(angle, _canvasController.Metrics.VspRadius);
            
            // Calculate longitude in degrees and minutes within the sign (original format for wheel)
            double longitudeInSign = vspPosition.Longitude % 30.0;
            int degrees = (int)longitudeInSign;
            int minutes = (int)((longitudeInSign - degrees) * 60.0);
            
            // Create the text block for the VSP number and longitude (original format)
            var textSize = 0.8;
            var textColor = Colors.Red;
            if (vspPosition == vspPositions[2]) textColor = Colors.Blue;
            var textBlock = new TextBlock
            {
                Text = $"{vspPosition.SequenceId}\n{degrees}°{minutes:D2}'",
                FontFamily = _canvasController.Metrics.PositionTextsFontFamily,
                FontSize = _canvasController.Metrics.VspTextSize * textSize, // Slightly smaller to fit both lines
                Foreground =   new SolidColorBrush(textColor), // Make it red for visibility
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center
            };
            
            // Position the text on the canvas - center it properly in the circle
            Canvas.SetLeft(textBlock, point.X - (_canvasController.Metrics.VspTextSize * 1.8) / 2);
            Canvas.SetTop(textBlock, point.Y - (_canvasController.Metrics.VspTextSize * 1.8) / 2);
            
            vspTexts.Add(textBlock);
        }
        
        return vspTexts;
    }
    
    private List<UIElement> CreateVspConnectionLines(List<PresentableVspPosition> vspPositions)
    {
        var vspLines = new List<UIElement>();
        var centerPoint = new Point(_canvasController.CanvasSize / 2, _canvasController.CanvasSize / 2);
        // var ascendantLongitude = _canvasController.NoTime ? 0.0 : 
        //     DataVaultCharts.Instance.GetCurrentChart()?.Positions[ChartPoints.Ascendant].Ecliptical.MainPosSpeed.Position ?? 0.0;
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
                double angle1 = currentVsp.Longitude - leftAnchorPoint + 90.0;
                if (angle1 < 0.0) angle1 += 360.0;
                if (angle1 >= 360.0) angle1 -= 360.0;
                
                double angle2 = nextVsp.Longitude - leftAnchorPoint + 90.0;
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
                    Stroke = new SolidColorBrush(Color.FromRgb(173, 216, 230)), // Light blue color
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
 
    
    private void ExportClick(object sender, RoutedEventArgs e)
    {
        CanvasExporter.WriteCanvasToPng(WheelCanvas);
    }
    
    private void DefineColors()
    {
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
        ChartNameText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
        ExplanationText.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.DESCRIPTION_BLOCK_COLOR)!;
     }
}
