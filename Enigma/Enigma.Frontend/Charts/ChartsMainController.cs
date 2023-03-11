// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Api.Interfaces;
using Enigma.Domain.Charts;
using Enigma.Domain.Persistency;
using Enigma.Frontend.Ui.Charts.Graphics;
using Enigma.Frontend.Ui.Configuration;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.Ui.Charts;

/// <summary>Controller for main chart window.</summary>
public sealed class ChartsMainController
{
    private ChartsWheel? _chartsWheel;
    private ChartPositionsWindow? _chartPositionsWindow;
    private ChartAspectsWindow? _chartAspectsWindow;
    private ChartMidpointsWindow? _chartMidpointsWindow;
    private ChartHarmonicsWindow? _chartHarmonicsWindow;
    private readonly List<Window> _openWindows = new();
    private readonly DataVault _dataVault = DataVault.Instance;
    private readonly AppSettingsWindow _appSettingsWindow = new();
    private AstroConfigWindow _astroConfigWindow = new();
    private SearchChartWindow _searchChartWindow = new();
    private readonly IChartDataPersistencyApi _chartDataPersistencyApi;
    private readonly IChartDataForDataGridFactory _chartDataForDataGridFactory;
    private readonly IChartDataConverter _chartDataConverter;
    private readonly IChartCalculation _chartCalculation;

    public ChartsMainController(IChartDataPersistencyApi chartDataPersistencyApi, IChartDataForDataGridFactory chartDataForDataGridFactory, IChartDataConverter chartDataConverter, IChartCalculation chartCalculation)
    {
        _chartDataPersistencyApi = chartDataPersistencyApi;
        _chartDataForDataGridFactory = chartDataForDataGridFactory;
        _chartDataConverter = chartDataConverter;
        _chartCalculation = chartCalculation;
    }

    public void ShowAppSettings()
    {
        _appSettingsWindow.ShowDialog();
    }

    public void ShowAstroConfig()
    {
        if (!_astroConfigWindow.IsLoaded)
        {
            _astroConfigWindow = new();
        }
        _astroConfigWindow.ShowDialog();
    }

    public void NewChart()
    {
        ChartDataInputWindow chartDataInputWindow = new();
        chartDataInputWindow.ShowDialog();
        if (_dataVault.GetNewChartAdded())
        {
            // enable relevant menuitems and buttons
            SaveCurrentChart();
            ShowCurrentChart();
        }
    }

    public List<PresentableChartData> AllChartData()
    {
        List<PresentableChartData> allCharts = _chartDataForDataGridFactory.CreateChartDataForDataGrid(_dataVault.GetAllCharts());
        return allCharts;
    }

    public int NrOfChartsInDatabase()
    {
        return _chartDataPersistencyApi.NumberOfRecords();
    }

    public string MostRecentChart()
    {
        int highestIndex = _chartDataPersistencyApi.HighestIndex();
        if (highestIndex != 0)
        {
            PersistableChartData? chartData = _chartDataPersistencyApi.ReadChartData(highestIndex);
            if (chartData != null) { return chartData.Name; }
        }
        return "-";
    }

    public void SaveCurrentChart()
    {
        var currentChart = _dataVault.GetCurrentChart();
        if (currentChart != null)
        {
            ChartData chartData = currentChart.InputtedChartData;
            PersistableChartData persistableChartData = _chartDataConverter.ToPersistableChartData(chartData);
            var _ = _chartDataPersistencyApi.AddChartData(persistableChartData);
        }

    }

    public bool DeleteCurrentChart()
    {
        var currentChart = _dataVault.GetCurrentChart();
        if (currentChart != null)
        {
            int id = currentChart.InputtedChartData.Id;
            return _chartDataPersistencyApi.DeleteChartData(id);
        }
        return false;
    }

    /// <summary>Opens chart wheel for current chart.</summary>
    public void ShowCurrentChart()
    {
        _chartsWheel = App.ServiceProvider.GetRequiredService<ChartsWheel>();
        OpenWindow(_chartsWheel);
        _chartsWheel.Populate();
    }

    public void SearchAndSetActiveChart(int id)
    {
        CalculatedChart? calculatedChart = null;
        List<CalculatedChart> allCharts = _dataVault.GetAllCharts();
        foreach (var calcChart in allCharts)
        {
            if (calcChart.InputtedChartData.Id == id)
            {
                // recalculate to effectuate possible changes in config.
                calculatedChart = _chartCalculation.CalculateChart(calcChart.InputtedChartData);
            }
        }
        if (calculatedChart != null)
        {
            _dataVault.AddNewChart(calculatedChart);
            _dataVault.SetNewChartAdded(true);
        }
    }


    public string CurrentChartName()
    {
        var currentChart = _dataVault.GetCurrentChart();
        return currentChart != null ? currentChart.InputtedChartData.MetaData.Name : "";
    }

    public void ShowPositions()
    {
        _chartPositionsWindow = App.ServiceProvider.GetRequiredService<ChartPositionsWindow>();
        OpenWindow(_chartPositionsWindow);
        _chartPositionsWindow.Populate();

    }

    public void ShowAspects()
    {
        _chartAspectsWindow = App.ServiceProvider.GetRequiredService<ChartAspectsWindow>();
        OpenWindow(_chartAspectsWindow);
        _chartAspectsWindow.Populate();
    }

    /// <summary>Show form with midpoints.</summary>
    public void ShowMidpoints()
    {
        _chartMidpointsWindow = App.ServiceProvider.GetRequiredService<ChartMidpointsWindow>();
        OpenWindow(_chartMidpointsWindow);
        _chartMidpointsWindow.Populate();
    }

    public void ShowHarmonics()
    {
        _chartHarmonicsWindow = App.ServiceProvider.GetRequiredService<ChartHarmonicsWindow>();
        OpenWindow(_chartHarmonicsWindow);
        _chartHarmonicsWindow.Populate();
    }


    public void ShowAbout()
    {
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("AboutCharts");
        helpWindow.ShowDialog();
    }


    public void ShowSearch()
    {
        _searchChartWindow = new();
        _searchChartWindow.ShowDialog();

    }

    private void OpenWindow(Window window)
    {
        if (!_openWindows.Contains(window))
        {
            _openWindows.Add(window);
        }
        window.Show();
    }


    /// <summary>Closes all child windows of main chart window.</summary>
    public void HandleClose()
    {
        foreach (Window window in _openWindows)
        {
            window.Close();
        }
    }

}