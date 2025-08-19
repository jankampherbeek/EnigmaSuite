// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
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

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for solar results</summary>
public partial class SolarResultsWindow
{
    private ChartsWheelCanvasController _canvasController;

    public SolarResultsWindow()
    {
        InitializeComponent();
        DefineColors();
        _canvasController = App.ServiceProvider.GetRequiredService<ChartsWheelCanvasController>();
        _canvasController.AllPositions = DataVaultProg.Instance.GetCurrentSolar().Positions;
        // Set up property change handling for the ViewModel
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is SolarResultsViewModel viewModel)
        {
            // Sync the controller with the ViewModel
            _canvasController.ShowSignBackgroundColors = viewModel.ShowSignBackgroundColors;
            
            // Set up property change notification
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
    }
    
    private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SolarResultsViewModel.ShowSignBackgroundColors))
        {
            var viewModel = (SolarResultsViewModel)sender;
            _canvasController.ShowSignBackgroundColors = viewModel.ShowSignBackgroundColors;
            Populate();
        }
        else if (e.PropertyName == nameof(SolarResultsViewModel.OrbSolarUnderline))
        {
            // Ensure the aspects tab remains selected after error popup
            MainTabControl.SelectedItem = AspectsTab;
        }
    }
    
    public void Populate()
    {
        WheelCanvas.Children.Clear();
        
        // Get the solar chart from the ViewModel's model
        var viewModel = DataContext as SolarResultsViewModel;
        if (viewModel != null)
        {
            var model = App.ServiceProvider.GetRequiredService<SolarResultsModel>();
            var solarChart = model.GetSolarChart();
            
            if (solarChart != null)
            {
                // Get the current radix chart to access its data
                var currentChart = DataVaultCharts.Instance.GetCurrentChart();
                if (currentChart != null)
                {
                    // Calculate the solar return Julian Day
                    var birthJd = currentChart.InputtedChartData.FullDateTime.JulianDayForEt;
                    var solarReturnJd = birthJd + (model.GetSolarAge() + 1) * EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
                    
                    // Create solar chart metadata
                    var solarLocation = model.GetSolarLocation() ?? currentChart.InputtedChartData.Location;
                    var solarDateTime = CreateSolarDateTime(solarReturnJd, currentChart.InputtedChartData.FullDateTime);
                    var solarMetaData = CreateSolarMetaData(currentChart.InputtedChartData.MetaData, model.GetSolarAge());
                    
                    // Create solar chart data
                    var solarChartData = new ChartData(
                        currentChart.InputtedChartData.Id, // Use same ID as radix
                        solarMetaData,
                        solarLocation,
                        solarDateTime
                    );
                    
                    // Create a CalculatedChart from the solar positions with proper solar metadata
                    var solarCalculatedChart = new CalculatedChart(
                        solarChart,
                        solarChartData,
                        currentChart.Obliquity // Use same obliquity as radix
                    );
                    _canvasController.AllPositions = solarChart;
                }
            }
        }
        
        _canvasController.PrepareDraw();
        DrawChartFrame();
        DrawCusps();
        DrawCelPoints();
        if (!_canvasController.NoAspects)
        {
            DrawAspects();
        }
    }
    
    private FullDateTime CreateSolarDateTime(double solarReturnJd, FullDateTime radixDateTime)
    {
        try
        {
            // Convert Julian Day back to date/time
            var dateTimeApi = App.ServiceProvider.GetRequiredService<IDateTimeApi>();
            var dateTimeRequest = new DateTimeRequest(solarReturnJd, false, radixDateTime.DateText.Contains("Julian") ? Calendars.Julian : Calendars.Gregorian);
            var dateTimeResponse = dateTimeApi.GetDateTime(dateTimeRequest);
            
            var simpleDateTime = dateTimeResponse.DateTime;
            var dateText = $"{simpleDateTime.Year:D4}/{simpleDateTime.Month:D2}/{simpleDateTime.Day:D2}";
            var timeText = $"{simpleDateTime.Ut:F2}";
            return new FullDateTime(dateText, timeText, solarReturnJd);
        }
        catch (Exception)
        {
            // Fallback to radix date/time if conversion fails
            return radixDateTime;
        }
    }
    
    private MetaData CreateSolarMetaData(MetaData radixMetaData, int age)
    {
        var solarName = $"{radixMetaData.Name} Solar Return Age {age}";
        var solarDescription = $"Solar return chart for {radixMetaData.Name} at age {age}";
        return new MetaData(
            solarName,
            solarDescription,
            radixMetaData.Source,
            radixMetaData.LocationName,
            radixMetaData.ChartCategory,
            radixMetaData.RoddenRating
        );
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
        // Account for: Header (~40px) + Chart Name (~30px) + TabControl header (~30px) + 
        // CheckBoxes (~60px) + Export button (~80px) + margins and padding (~100px)
        double reservedHeight = 340.0;
        double availHeight = ActualHeight - reservedHeight;
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
    }
} 