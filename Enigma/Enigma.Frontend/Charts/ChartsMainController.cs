// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Api.Interfaces;
using Enigma.Domain.Charts;
using Enigma.Domain.Persistency;
using Enigma.Frontend.Ui.Charts.Graphics;
using Enigma.Frontend.Ui.Charts.Progressive;
using Enigma.Frontend.Ui.Charts.Progressive.InputTransits;
using Enigma.Frontend.Ui.Configuration;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using Enigma.Frontend.Ui.Charts.Progressive.InputEvent;
using Enigma.Frontend.Ui.Charts.Progressive.InputPeriod;
using Enigma.Frontend.Ui.Charts.Shared;
using Enigma.Frontend.Ui.Views;
using JetBrains.Annotations;

namespace Enigma.Frontend.Ui.Charts;

/// <summary>Controller for main chart window.</summary>
public sealed class ChartsMainController
{
    private ChartsWheel? _chartsWheel;
 //   private ChartPositionsWindow? _chartPositionsWindow;
 //   private ChartHarmonicsWindow? _chartHarmonicsWindow;
    private readonly List<Window> _openWindows = new();
    private readonly DataVault _dataVault = DataVault.Instance;
    private AstroConfigWindow _astroConfigWindow = new();
    private SearchChartWindow _searchChartWindow = new();
    private ProgInputEvent _inputEventWindow = new();
    private ProgInputPeriod _inputDaterangeWindow = new();
   // private RadixAspectsWindow? _aspectsWindow;
   // private RadixMidpointsWindow? _midpointsWindow;    
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
        //AppSettingsWindow _appSettingsWindow = new();
        AppSettingsWindow _appSettingsWindow = new AppSettingsWindow();
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
        RadixDataInputWindow window = new();
        window.ShowDialog();
        if (!_dataVault.GetNewChartAdded()) return;
        int newIndex = SaveCurrentChart();
        if (_dataVault.GetCurrentChart() == null || _dataVault.GetCurrentChart()!.InputtedChartData == null) return;
        _dataVault.GetCurrentChart()!.InputtedChartData.Id = newIndex;
        ShowCurrentChart();

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

    public int SaveCurrentChart()
    {
        int newIndex = -1;
        var currentChart = _dataVault.GetCurrentChart();
        if (currentChart != null)
        {
            ChartData chartData = currentChart.InputtedChartData;
            PersistableChartData persistableChartData = _chartDataConverter.ToPersistableChartData(chartData);
            newIndex = _chartDataPersistencyApi.AddChartData(persistableChartData);
        }
        return newIndex;
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

    public void ShowPositions(){
        new RadixPositionsWindow().Show();
    }

    public void ShowAspects()
    {
        RadixAspectsWindow aspectsWindow = new();
        aspectsWindow.Show();
    }

    /// <summary>Show form with midpoints.</summary>
    public void ShowMidpoints()
    {
        RadixMidpointsWindow midpointsWindow = new();
        midpointsWindow.Show();
    }

    public void ShowHarmonics()
    {
        RadixHarmonicsWindow harmonicsWindow = new();
        harmonicsWindow.Show();
    }


    public void ShowInputEvent()
    {
        _inputEventWindow = App.ServiceProvider.GetRequiredService<ProgInputEvent>();
        OpenWindow(_inputEventWindow);
        // todo handle input event
    }

    public void ShowSearchEvent()
    {
        // todo create and handle search event
        //CalYearCountWindow tempWindow = new();
        //LmtWindow tempWindow = new();
        //tempWindow.ShowDialog();
    }

    public void ShowInputDaterange()
    {
        _inputDaterangeWindow = App.ServiceProvider.GetRequiredService<ProgInputPeriod>();
        OpenWindow(_inputDaterangeWindow);
        // todo handle input event
    }
    
    
    
    public void ShowInputPrimDir()
    {
        ChartProgPrimInput primInput = new ChartProgPrimInput();
        OpenWindow(primInput);
        primInput.Populate();
    }


    public void ShowInputSecProg()
    {
        ChartProgSecInput secInput = new ChartProgSecInput();
        OpenWindow(secInput);
        secInput.Populate();
    }

    public void ShowInputTransProg()
    {
        ProgInputTransits transInput = new();
        OpenWindow(transInput);
        transInput.Populate();
    }


    public void ShowInputSymProg()
    {
        ChartProgSymInput symInput = new ChartProgSymInput();
        OpenWindow(symInput);
        symInput.Populate();
    }
    public void ShowInputSolarProg()
    {
        ChartProgSolarInput solarInput = new ChartProgSolarInput();
        OpenWindow(solarInput);
        solarInput.Populate();
    }

    public static void ShowAbout()
    {
        DataVault.Instance.CurrentViewBase = "AboutCharts";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }

    public static void ShowHelp()
    {
        DataVault.Instance.CurrentViewBase = "ChartsMain";
        HelpWindow helpWindow = new();
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


    /// <summary>Closes all child windows of main chart window. Clears all charts in DataVault.</summary>
    public void HandleClose()
    {
        _dataVault.ClearExistingCharts();
        foreach (Window window in _openWindows)
        {
            window.Close();
        }
    }

}