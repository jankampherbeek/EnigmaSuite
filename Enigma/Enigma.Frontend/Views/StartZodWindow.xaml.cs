// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Enigma.Api.Calc;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Frontend.Ui.Graphics;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for Startpoint Zodiac</summary>
public partial class StartZodWindow
{
    private ChartsWheelCanvasController _canvasController;
    
    public StartZodWindow()
    {
        InitializeComponent();
        DefineColors();
    }


    private void DefineColors()
    {
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
        ChartNameText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
        _canvasController = App.ServiceProvider.GetRequiredService<ChartsWheelCanvasController>();
        _canvasController.AllPositions = DataVaultCharts.Instance.GetAltChart().Positions;
        
        // Set up property change handling for the ViewModel
        if (DataContext is StartZodViewModel viewModel)
        {
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.OldValue is StartZodViewModel oldViewModel)
        {
            oldViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
        
        if (e.NewValue is StartZodViewModel newViewModel)
        {
            newViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
    }

    private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(StartZodViewModel.SelectedChartPoint))
        {
            // Update the chart when user selects a different planet
            var model = App.ServiceProvider.GetRequiredService<StartZodModel>();
            if (model.AltChart != null)
            {
                _canvasController.AllPositions = model.AltChart.Positions;
                Populate();
            }
        }
    }

    public void Populate()
    {
        WheelCanvas.Children.Clear();

        // Get the solar chart from the ViewModel's model
        var viewModel = DataContext as StartZodViewModel;
        if (viewModel != null)
        {
            var model = App.ServiceProvider.GetRequiredService<StartZodModel>();
            var altChart = model.AltChart;

            if (altChart != null)
            {
                // // Get the current radix chart to access its data
                // var currentChart = DataVaultCharts.Instance.GetCurrentChart();
                // if (currentChart != null)
                // {
                    // // Calculate the solar return Julian Day
                    // var birthJd = currentChart.InputtedChartData.FullDateTime.JulianDayForEt;
                    // var solarReturnJd = birthJd + (model.GetSolarAge() + 1) * EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
                    //
                    // // Create solar chart metadata
                    // var solarLocation = model.GetSolarLocation() ?? currentChart.InputtedChartData.Location;
                    // var solarDateTime = CreateSolarDateTime(solarReturnJd, currentChart.InputtedChartData.FullDateTime);
                    // var solarMetaData =
                    //     CreateSolarMetaData(currentChart.InputtedChartData.MetaData, model.GetSolarAge());
                    //
                    // // Create solar chart data
                    // var solarChartData = new ChartData(
                    //     currentChart.InputtedChartData.Id, // Use same ID as radix
                    //     solarMetaData,
                    //     solarLocation,
                    //     solarDateTime
                    // );
                    //
                    // // Create a CalculatedChart from the solar positions with proper solar metadata
                    // var solarCalculatedChart = new CalculatedChart(
                    //     altChart,
                    //     solarChartData,
                    //     currentChart.Obliquity // Use same obliquity as radix
                    // );
                    _canvasController.AllPositions = altChart.Positions;
                // }
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

    // private FullDateTime CreateSolarDateTime(double solarReturnJd, FullDateTime radixDateTime)
    // {
    //     try
    //     {
    //         // Convert Julian Day back to date/time
    //         var dateTimeApi = App.ServiceProvider.GetRequiredService<IDateTimeApi>();
    //         var dateTimeRequest = new DateTimeRequest(solarReturnJd, false,
    //             radixDateTime.DateText.Contains("Julian") ? Calendars.Julian : Calendars.Gregorian);
    //         var dateTimeResponse = dateTimeApi.GetDateTime(dateTimeRequest);
    //
    //         var simpleDateTime = dateTimeResponse.DateTime;
    //         var dateText = $"{simpleDateTime.Year:D4}/{simpleDateTime.Month:D2}/{simpleDateTime.Day:D2}";
    //         var timeText = $"{simpleDateTime.Ut:F2}";
    //         return new FullDateTime(dateText, timeText, solarReturnJd);
    //     }
    //     catch (Exception)
    //     {
    //         // Fallback to radix date/time if conversion fails
    //         return radixDateTime;
    //     }
    // }

    // private MetaData CreateSolarMetaData(MetaData radixMetaData, int age)
    // {
    //     var solarName = $"{radixMetaData.Name} Solar Return Age {age}";
    //     var solarDescription = $"Solar return chart for {radixMetaData.Name} at age {age}";
    //     return new MetaData(
    //         solarName,
    //         solarDescription,
    //         radixMetaData.Source,
    //         radixMetaData.LocationName,
    //         radixMetaData.ChartCategory,
    //         radixMetaData.RoddenRating
    //     );
    // }

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
}
