// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Charts;
using Enigma.Frontend.Ui.Charts.Graphics;
using Enigma.Frontend.Ui.Configuration;
using Enigma.Frontend.Ui.State;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.Ui.Charts;

/// <summary>Controller for main chart window.</summary>
public class ChartsMainController
{
    private ChartsWheel? _chartsWheel;
    private ChartPositionsWindow? _chartPositionsWindow;
    private ChartAspectsWindow? _chartAspectsWindow;
    private ChartMidpointsWindow? _chartMidpointsWindow;
    private ChartHarmonicsWindow? _chartHarmonicsWindow;
    private List<Window> _openWindows = new();
    private readonly DataVault _dataVault = DataVault.Instance;



    public void ShowAppSettings()
    {
        AppSettingsWindow appSettingsWindow = new();
        appSettingsWindow.ShowDialog();
    }

    public void ShowAstroConfig()
    {
        AstroConfigWindow astroConfigWindow = new();
        astroConfigWindow.ShowDialog();
    }

    public void NewChart()
    {
        ChartDataInputWindow chartDataInputWindow = new();
        chartDataInputWindow.ShowDialog();
        if (_dataVault.GetNewChartAdded())
        {
            // enable relevant menuitems and buttons
            ShowCurrentChart();

        }
    }


    /// <summary>Opens chart wheel for current chart.</summary>
    public void ShowCurrentChart()
    {
        _chartsWheel = App.ServiceProvider.GetRequiredService<ChartsWheel>();
        _openWindows.Add(_chartsWheel);
        _chartsWheel.Show();
        _chartsWheel.DrawChart();

    }

    public void ShowPositions()
    {
        _chartPositionsWindow = App.ServiceProvider.GetRequiredService<ChartPositionsWindow>();
        _openWindows.Add(_chartPositionsWindow);
        _chartPositionsWindow.Show();
        _chartPositionsWindow.PopulateAll();

    }

    public void ShowAspects()
    {
        _chartAspectsWindow = App.ServiceProvider.GetRequiredService<ChartAspectsWindow>();
        _openWindows.Add(_chartAspectsWindow);
        _chartAspectsWindow.Show();
        _chartAspectsWindow.Populate();
    }


    /// <summary>Show form with midpoints.</summary>
    public void ShowMidpoints()
    {
        _chartMidpointsWindow = App.ServiceProvider.GetRequiredService<ChartMidpointsWindow>();
        _openWindows.Add(_chartMidpointsWindow);
        _chartMidpointsWindow.Show();
        _chartMidpointsWindow.Populate();
    }

    public void ShowHarmonics()
    {
        _chartHarmonicsWindow = App.ServiceProvider.GetRequiredService<ChartHarmonicsWindow>();
        _openWindows.Add(_chartHarmonicsWindow);
        _chartHarmonicsWindow.Show();
        _chartHarmonicsWindow.Populate();

    }

    public void AddCalculatedChart(CalculatedChart newChart)
    {
        // AllCurrentCharts.AddChart(newChart, true, false);
    }


    public void ShowAbout()
    {
        AboutWindow aboutWindow = new();    // todo make specific for charts
        aboutWindow.ShowDialog();
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