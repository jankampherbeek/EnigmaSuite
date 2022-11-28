// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Frontend.Ui.Charts;
using Enigma.Frontend.Ui.Charts.Graphics;
using Enigma.Frontend.Ui.DataFiles;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.ResearchProjects;
using Enigma.Frontend.Ui.Settings;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui;


public class MainController
{
    private readonly IRosetta _rosetta;
    private readonly DataVault _dataVault;


    public CurrentCharts AllCurrentCharts { get; set; }         // Todo move to ChartsMainController


    public MainController(IRosetta rosetta, ChartsWheel chartsWheel)
    {
        _rosetta = rosetta;
        _dataVault = DataVault.Instance;

        AllCurrentCharts = new CurrentCharts();
    }

    public void NewChart()
    {
        ChartDataInputWindow chartDataInputWindow = new();
        chartDataInputWindow.ShowDialog();
        if (_dataVault.GetNewChartAdded())
        {
            ChartsMainWindow chartsMainWindow = new();
            chartsMainWindow.Show();
        }
    }


    public void NewProject()
    {
        ProjectInputWindow projectInputWindow = new();
        projectInputWindow.ShowDialog();
    }

    public void ShowProjectsOpen()
    {
        ProjectsOverviewWindow projectsOverviewWindow = new();
        projectsOverviewWindow.ShowDialog();
    }

    public void ShowAbout()
    {
        AboutWindow aboutWindow = new(_rosetta);
        aboutWindow.ShowDialog();
    }

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

    public void ShowDataOverview()
    {
        DataFilesOverviewWindow dataFilesOverviewWindow = new();
        dataFilesOverviewWindow.ShowDialog();
    }

    public void ShowDataExport()
    {
        DataFilesExportWindow dataFilesExportWindow = new();
        dataFilesExportWindow.ShowDialog();
    }

    public void ShowDataImport()
    {
        DataFilesImportWindow dataFilesImportWindow = new();
        dataFilesImportWindow.ShowDialog();
    }

    public void AddCalculatedChart(CalculatedChart newChart)
    {
        AllCurrentCharts.AddChart(newChart, true, false);
    }

    private static void ShowPositions()
    {
        ChartPositionsWindow chartPositionsWindow = new();
        chartPositionsWindow.Show();
        chartPositionsWindow.PopulateAll();
    }


}