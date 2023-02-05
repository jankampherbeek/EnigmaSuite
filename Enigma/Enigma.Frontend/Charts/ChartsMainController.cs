// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Frontend.Ui.Charts.Graphics;
using Enigma.Frontend.Ui.Configuration;
using Enigma.Frontend.Ui.State;
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
    private readonly AboutWindow _aboutWindow = new();
    private AstroConfigWindow _astroConfigWindow = new();

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
            ShowCurrentChart();

        }
    }


    /// <summary>Opens chart wheel for current chart.</summary>
    public void ShowCurrentChart()
    {
        if (_chartsWheel == null || !_chartsWheel.IsVisible)
        {
            _chartsWheel = App.ServiceProvider.GetRequiredService<ChartsWheel>();
            OpenWindow(_chartsWheel);
            _chartsWheel.Populate();
        }
    }

    public void ShowPositions()
    {
        if (_chartPositionsWindow == null || !_chartPositionsWindow.IsVisible)
        {
            _chartPositionsWindow = App.ServiceProvider.GetRequiredService<ChartPositionsWindow>();
            OpenWindow(_chartPositionsWindow);
            _chartPositionsWindow.Populate();
        }
    }

    public void ShowAspects()
    {
        if (_chartAspectsWindow == null || !_chartAspectsWindow.IsVisible)
        {
            _chartAspectsWindow = App.ServiceProvider.GetRequiredService<ChartAspectsWindow>();
            OpenWindow(_chartAspectsWindow);
            _chartAspectsWindow.Populate();
        }
    }




    /// <summary>Show form with midpoints.</summary>
    public void ShowMidpoints()
    {
        if (_chartMidpointsWindow == null || !_chartMidpointsWindow.IsVisible)
        {
            _chartMidpointsWindow = App.ServiceProvider.GetRequiredService<ChartMidpointsWindow>();
            OpenWindow(_chartMidpointsWindow);
            _chartMidpointsWindow.Populate();
        }
    }

    public void ShowHarmonics()
    {
        if (_chartHarmonicsWindow == null || !_chartHarmonicsWindow.IsVisible)
        {
            _chartHarmonicsWindow = App.ServiceProvider.GetRequiredService<ChartHarmonicsWindow>();
            OpenWindow(_chartHarmonicsWindow);
            _chartHarmonicsWindow.Populate();
        }
    }


    public void ShowAbout()
    {
        _aboutWindow.ShowDialog();
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