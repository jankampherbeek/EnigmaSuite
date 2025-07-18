// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for solar input window</summary>
public partial class SolarInputViewModel : ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.SOLAR_INPUT;
    
    private readonly SolarInputModel _model;
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;

    [ObservableProperty] private string _chartName = string.Empty;
    [ObservableProperty] private string _age = string.Empty;
    [ObservableProperty] private bool _siderealReturn = false;
    [ObservableProperty] private string _geoLong = string.Empty;
    [ObservableProperty] private string _geoLat = string.Empty;
    [ObservableProperty] private int _dirLongIndex = 0;
    [ObservableProperty] private int _dirLatIndex = 0;
    [ObservableProperty] private bool _isGeoReadOnly = true;
    [ObservableProperty] private bool _isGeoEnabled = false;
    [ObservableProperty] private string _geoLongValid = "Gray";
    [ObservableProperty] private string _geoLatValid = "Gray";

    public List<string> AllDirectionsForLongitude { get; } = new() { "East", "West" };
    public List<string> AllDirectionsForLatitude { get; } = new() { "North", "South" };

    public SolarInputViewModel()
    {
        _model = App.ServiceProvider.GetRequiredService<SolarInputModel>();
        InitializeData();
        PropertyChanged += OnPropertyChanged;
    }

    private void InitializeData()
    {
        var currentChart = _dataVaultCharts.GetCurrentChart();
        if (currentChart != null)
        {
            ChartName = currentChart.InputtedChartData.MetaData.Name;
            // Initialize with current chart's coordinates
            var location = currentChart.InputtedChartData.Location;
            GeoLong = location.GeoLong.ToString("F0") + ":" + 
                     (location.GeoLong % 1 * 60).ToString("F0") + ":" + 
                     ((location.GeoLong % 1 * 60) % 1 * 60).ToString("F0");
            GeoLat = location.GeoLat.ToString("F0") + ":" + 
                    (location.GeoLat % 1 * 60).ToString("F0") + ":" + 
                    ((location.GeoLat % 1 * 60) % 1 * 60).ToString("F0");
            DirLongIndex = location.GeoLong >= 0 ? 0 : 1; // East : West
            DirLatIndex = location.GeoLat >= 0 ? 0 : 1;   // North : South
            
            // Geographic coordinates are always enabled for relocation
            IsGeoReadOnly = false;
            IsGeoEnabled = true;
        }
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(GeoLong):
                ValidateGeoLong();
                break;
            case nameof(GeoLat):
                ValidateGeoLat();
                break;
        }
    }

    private void ValidateGeoLong()
    {
        // Basic validation - can be enhanced
        GeoLongValid = string.IsNullOrEmpty(GeoLong) ? "Red" : "Gray";
    }

    private void ValidateGeoLat()
    {
        // Basic validation - can be enhanced
        GeoLatValid = string.IsNullOrEmpty(GeoLat) ? "Red" : "Gray";
    }

    [RelayCommand]
    private static void Cancel()
    {
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }

    [RelayCommand]
    private static void Help()
    {
        // TODO: Implement help functionality
        MessageBox.Show("Help for Solar Return calculation will be implemented here.", "Help", 
            MessageBoxButton.OK, MessageBoxImage.Information);
    }

    [RelayCommand]
    private void Calculate()
    {
        if (!ValidateInput())
        {
            MessageBox.Show("Please correct the input errors before calculating.", "Validation Error", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Parse input values
        if (!int.TryParse(Age, out int ageValue))
        {
            MessageBox.Show("Please enter a valid age.", "Validation Error", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Parse geographic coordinates if relocation is enabled
        double longitude = 0, latitude = 0;
        bool relocate = !IsGeoReadOnly;
        
        if (relocate)
        {
            if (!ParseCoordinate(GeoLong, out longitude) || !ParseCoordinate(GeoLat, out latitude))
            {
                MessageBox.Show("Please enter valid geographic coordinates.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            // Apply direction
            if (DirLongIndex == 1) longitude = -longitude; // West
            if (DirLatIndex == 1) latitude = -latitude;   // South
        }
        _model.CalculateSolarReturn(ageValue, !SiderealReturn, relocate, longitude, latitude);
        
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION,ChartsWindowsFlow.SOLAR_RESULTS));
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
        
        
        // Calculate solar return
        //var solarResultsModel = App.ServiceProvider.GetRequiredService<SolarResultsModel>();

        
        // if (success)
        // {
        //     // Close current window
        //     Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is SolarInputWindow)?.Close();
        //     
        //     // Open results window
        //     var resultsWindow = new SolarResultsWindow();
        //     var resultsViewModel = resultsWindow.DataContext as SolarResultsViewModel;
        //     if (resultsViewModel != null)
        //     {
        //         // Initialize the ViewModel with the same model instance that has the calculated data
        //         resultsViewModel.InitializeWithSolarData(solarResultsModel);
        //     }
        //     resultsWindow.Show();
        //     resultsWindow.Populate();
        // }
        // else
        // {
        //     MessageBox.Show("Failed to calculate solar return. Please check your input and try again.", 
        //         "Calculation Error", MessageBoxButton.OK, MessageBoxImage.Error);
        // }
    }

    private bool ParseCoordinate(string coordinate, out double value)
    {
        value = 0;
        if (string.IsNullOrEmpty(coordinate)) return false;
        
        var parts = coordinate.Split(':');
        if (parts.Length != 3) return false;
        
        if (!int.TryParse(parts[0], out int degrees) ||
            !int.TryParse(parts[1], out int minutes) ||
            !int.TryParse(parts[2], out int seconds))
        {
            return false;
        }
        
        value = degrees + minutes / 60.0 + seconds / 3600.0;
        return true;
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrEmpty(Age) || !int.TryParse(Age, out int ageValue) || ageValue <= 0)
        {
            MessageBox.Show("Please enter a valid age (integer > 0).", "Validation Error", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        return true;
    }
} 