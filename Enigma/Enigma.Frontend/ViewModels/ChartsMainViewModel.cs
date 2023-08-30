// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Charts;
using Enigma.Frontend.Ui.Charts.Graphics;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for main charts screen</summary>
public partial class ChartsMainViewModel: ObservableObject
{
    private readonly ChartsMainModel _model;
    private readonly DataVault _dataVault;
    private readonly List<Window> _openWindows = new();
    [NotifyCanExecuteChangedFor(nameof(DeleteChartCommand))]
    [NotifyCanExecuteChangedFor(nameof(ShowWheelCommand))]
    [NotifyCanExecuteChangedFor(nameof(ShowPositionsCommand))]
    [NotifyCanExecuteChangedFor(nameof(AspectsCommand))]
    [NotifyCanExecuteChangedFor(nameof(MidpointsCommand))]
    [NotifyCanExecuteChangedFor(nameof(HarmonicsCommand))]
    [NotifyCanExecuteChangedFor(nameof(ProgressionsCommand))]
    [NotifyPropertyChangedFor(nameof(SelectedChart))]
    [ObservableProperty] private int _chartIndex = -1;
    [ObservableProperty] private string _nrOfChartsInDatabase = string.Empty;
    [ObservableProperty] private string _lastAddedChart = string.Empty;
    [ObservableProperty] private string _currentlySelectedChart = string.Empty;
    [ObservableProperty] private ObservableCollection<PresentableChartData> _availableCharts;
    [NotifyPropertyChangedFor(nameof(ChartIndex))]
    [ObservableProperty] private PresentableChartData? _selectedChart;
    public ChartsMainViewModel()
    {
        _model = App.ServiceProvider.GetRequiredService<ChartsMainModel>();
        _dataVault = DataVault.Instance;
        _availableCharts = new ObservableCollection<PresentableChartData>(_model.AvailableCharts());
        PopulateData();
    }
    
    private void OpenWindow(Window window)
    {
        if (!_openWindows.Contains(window))
        {
            _openWindows.Add(window);
        }
        window.Show();
    }

    [RelayCommand]
    private void ItemChanged()
    {
        SelectedChart = AvailableCharts[ChartIndex];
        //_dataVault.SetCurrentChart(ChartIndex);
        _dataVault.SetIndexCurrentChart(ChartIndex);
        PopulateData();
        PopulateAvailableCharts();
    }

    [RelayCommand]
    private void PopulateData()
    {
        NrOfChartsInDatabase = "Nr. of charts in database : " + _model.CountPersistedCharts();
        LastAddedChart = "Last added to database : " + _model.MostRecentChart();
        CurrentlySelectedChart = "Currently selected : " + _model.CurrentChartName();

        SelectedChart = _model.CurrentChart();
    }

    private void PopulateAvailableCharts()
    {
        AvailableCharts = new ObservableCollection<PresentableChartData>(_model.AvailableCharts());
        
    }
    
    
    [RelayCommand]
    private static void AppSettings()
    {
        AppSettingsWindow appSettingsWindow = new();
        appSettingsWindow.ShowDialog();
    }
        
    [RelayCommand]
    private static void Configuration()
    {
        ConfigurationWindow configWindow = new();
        configWindow.ShowDialog();
    }

    [RelayCommand]
    private void NewChart()
    {
        RadixDataInputWindow dataInputWindow = new();
        dataInputWindow.ShowDialog();
        if (!_dataVault.GetNewChartAdded()) return;
        int newIndex = _model.SaveCurrentChart();
        if (_dataVault.GetCurrentChart() == null) return;
        _dataVault.GetCurrentChart()!.InputtedChartData.Id = newIndex;
        PopulateData();
        PopulateAvailableCharts();
    }

    [RelayCommand(CanExecute = nameof(IsChartSelected))]
    private void Progressions()
    {
        OpenWindow(new ProgressiveMainWindow());
    }
    
    [RelayCommand]
    private void SearchChart()
    {
        RadixSearchWindow window = new();
        window.ShowDialog();
        PopulateData();
        PopulateAvailableCharts();
    }

    [RelayCommand(CanExecute = nameof(IsChartSelected))]
    private void DeleteChart()
    {
        var currentChart = _dataVault.GetCurrentChart();
        string name = currentChart != null ? currentChart.InputtedChartData.MetaData.Name : "";
        if (MessageBox.Show("Do you want to delete the chart for  "+ name + " from the database?",
                "Delete chart",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            MessageBox.Show(
                _model.DeleteCurrentChart()
                    ? "The chart for "+ name + " was succesfully deleted."
                    : "The chart for  "+ name + " was not found and could not be deleted.",
                "Result of delete");
    }

    [RelayCommand(CanExecute = nameof(IsChartSelected))]
    private void ShowWheel()
    {
        ShowCurrentChart();
    }
    
    [RelayCommand(CanExecute = nameof(IsChartSelected))]
    private void ShowPositions()
    {
        OpenWindow(new RadixPositionsWindow());
    }

    [RelayCommand(CanExecute = nameof(IsChartSelected))]
    private void Aspects()
    {
        OpenWindow(new RadixAspectsWindow());
    }

    [RelayCommand(CanExecute = nameof(IsChartSelected))]
    private void Harmonics()
    {
        OpenWindow((new RadixHarmonicsWindow()));
    }
    
    [RelayCommand(CanExecute = nameof(IsChartSelected))]
    private void Midpoints()
    {
        OpenWindow(new RadixMidpointsWindow());
    }

    
    [RelayCommand]
    private void About()
    {
        _dataVault.CurrentViewBase = "AboutCharts";
        new HelpWindow().ShowDialog();
    }
    
    [RelayCommand]
    private void Help()
    {
        _dataVault.CurrentViewBase = "ChartsMain";
        new HelpWindow().ShowDialog();
    }


    [RelayCommand]
    private static void UserManual()
    {
        DataVault.Instance.CurrentViewBase = "UserManual";
        new HelpWindow().ShowDialog();
    }
    
    /// <summary>Closes all child windows of main chart window. Clears all charts in DataVault.</summary>
    [RelayCommand]
    private void HandleClose()
    {
        _dataVault.ClearExistingCharts();
        foreach (Window window in _openWindows)
        {
            window.Close();
        }
    }
    

    private bool IsChartSelected()
    {
        return ChartIndex >= 0;
    }
    
    
    /// <summary>Opens chart wheel for current chart.</summary>
    private void ShowCurrentChart()
    {
        ChartsWheel wheel = new();
        OpenWindow(wheel);
        wheel.Populate();
    }
    

}