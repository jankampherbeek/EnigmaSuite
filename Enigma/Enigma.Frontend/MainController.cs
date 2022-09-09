// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain;
using Enigma.Frontend.Charts;
using Enigma.Frontend.Charts.Graphics;
using Enigma.Frontend.DataFiles;
using Enigma.Frontend.Settings;
using Enigma.Frontend.UiDomain;

namespace Enigma.Frontend;


public class MainController
{
    private readonly AboutWindow _aboutWindow;
    private readonly ChartDataInputWindow _chartDataInputWindow;
    private readonly ChartPositionsWindow _chartPositionsWindow;
    private readonly ChartsWheel _chartsWheel;
    private readonly DataFilesOverviewWindow _dataFilesOverviewWindow;
    private readonly DataFilesImportWindow _dataFilesImportWindow;
    private readonly DataFilesExportWindow _dataFilesExportWindow;
    private readonly AppSettingsWindow _appSettingsWindow;
    private readonly AstroConfigWindow _astroConfigWindow;

    public CurrentCharts AllCurrentCharts { get; set; }


    public MainController(AboutWindow aboutWindow, ChartDataInputWindow chartDataInputWindow, ChartPositionsWindow chartPositionsWindow, ChartsWheel chartsWheel,
        DataFilesOverviewWindow dataFilesOverviewWindow, DataFilesImportWindow dataFilesImportWindow, DataFilesExportWindow dataFilesExportWindow,
        AppSettingsWindow appSettingsWindow, AstroConfigWindow astroConfigWindow)
    {
        _aboutWindow = aboutWindow;
        _chartDataInputWindow = chartDataInputWindow;
        _chartPositionsWindow = chartPositionsWindow;
        _chartsWheel = chartsWheel;
        _dataFilesOverviewWindow = dataFilesOverviewWindow;
        _dataFilesImportWindow = dataFilesImportWindow;
        _dataFilesExportWindow = dataFilesExportWindow;
        _appSettingsWindow = appSettingsWindow;
        _astroConfigWindow = astroConfigWindow;
        AllCurrentCharts = new CurrentCharts();
    }

    public void NewChart()
    {
        _chartDataInputWindow.ShowDialog();
        ShowWheel();
        ShowPositions();

    }

    public void ShowAbout()
    {
        _aboutWindow.ShowDialog();
    }

    public void ShowAppSettings()
    {
        _appSettingsWindow.ShowDialog();
    }

    public void ShowAstroConfig()
    {
        _astroConfigWindow.ShowDialog();
    }

    public void ShowDataOverview()
    {
        _dataFilesOverviewWindow.ShowDialog();
    }

    public void ShowDataExport()
    {
        _dataFilesExportWindow.ShowDialog();
    }

    public void ShowDataImport()
    {
        _dataFilesImportWindow.ShowDialog();
    }

    public void AddCalculatedChart(CalculatedChart newChart)
    {
        AllCurrentCharts.AddChart(newChart, true, false);
    }

    private void ShowWheel()
    {
        _chartsWheel.Show();
        _chartsWheel.DrawChart();
    }
    private void ShowPositions()
    {
        _chartPositionsWindow.ShowDialog();
        _chartPositionsWindow.PopulateAll();
    }


}