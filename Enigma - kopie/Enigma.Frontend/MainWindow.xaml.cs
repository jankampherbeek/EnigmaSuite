// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;


namespace Enigma.Frontend;

/// <summary>Main view for the application.</summary>
public partial class MainWindow : Window
{
    private IRosetta _rosetta;
    private MainController _controller;

    public MainWindow(MainController controller, IRosetta rosetta)
    {
        InitializeComponent();
        _controller = controller;
        _rosetta = rosetta;
        PopulateTexts();
        PopulateMenu();
    }

    private void GeneralSettingsClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowAppSettings();
    }

    private void GeneralConfigurationClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowAstroConfig();
    }

    private void GeneralCloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void ProjectsNewClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Projects - New not yet implemented.");
    }


    private void ProjectsOpenClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Projects - Open not yet implemented.");
    }

    private void DataOverviewClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowDataOverview();
    }

    private void DataImportClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowDataImport();
    }

    private void DataExportClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowDataExport();
    }

    private void PeriodicalCyclesClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Periodical - cycles not yet implemented.");
    }

    private void PeriodicalEphemerisClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Periodical - ephemeris not yet implemented.");
    }

    private void PeriodicalOccurrencesClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Periodical - occurrences not yet implemented.");
    }

    private void AdhocChartClick(object sender, RoutedEventArgs e)
    {
        _controller.NewChart();
    }

    private void AdhocCalculationsClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Ad hoc - calculations not yet implemented.");
    }

    private void HelpAboutClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowAbout();
    }

    private void HelpPageClick(object sender, RoutedEventArgs e)
    {
        HelpWindow? helpWindow = App.ServiceProvider.GetService<HelpWindow>();
        if (helpWindow != null)
        {
            helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            helpWindow.SetHelpPage("MainWindow");
            helpWindow.ShowDialog();
        }
    }

    private void HelpManualClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Help - Manual not yet implemented.");
    }


    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }


    private void PopulateTexts()
    {
        FormTitle.Text = _rosetta.TextForId("mainwindow.formtitle");
        ProjectsSubTitle.Text = _rosetta.TextForId("mainwindow.projectssubtitle");
        BtnNewProject.Content = _rosetta.TextForId("mainwindow.btnnewproject");
        BtnNewChart.Content = _rosetta.TextForId("mainwindow.btnnewchart");
        BtnOpenProject.Content = _rosetta.TextForId("mainwindow.btnopenproject");
        BtnClose.Content = _rosetta.TextForId("common.btnclose");
        BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
    }

    private void PopulateMenu()
    {
        miGeneral.Header = _rosetta.TextForId("mainwindow.menu.migeneral");
        miGeneralSettings.Header = _rosetta.TextForId("mainwindow.menu.migeneral.settings");
        miGeneralConfiguration.Header = _rosetta.TextForId("mainwindow.menu.migeneral.configuration");

        miGeneralClose.Header = _rosetta.TextForId("mainwindow.menu.migeneral.close");
        miProjects.Header = _rosetta.TextForId("mainwindow.menu.miprojects");
        miProjectsNew.Header = _rosetta.TextForId("mainwindow.menu.miprojects.new");
        miProjectsOpen.Header = _rosetta.TextForId("mainwindow.menu.miprojects.open");
        miData.Header = _rosetta.TextForId("mainwindow.menu.midata");
        miDataImport.Header = _rosetta.TextForId("mainwindow.menu.midata.import");
        miDataExport.Header = _rosetta.TextForId("mainwindow.menu.midata.export");
        miDataOverview.Header = _rosetta.TextForId("mainwindow.menu.midata.overview");
        miPeriodical.Header = _rosetta.TextForId("mainwindow.menu.miperiodical");
        miPeriodicalCycles.Header = _rosetta.TextForId("mainwindow.menu.miperiodical.cycles");
        miPeriodicalEphemeris.Header = _rosetta.TextForId("mainwindow.menu.miperiodical.ephemeris");
        miPeriodicalOccurrences.Header = _rosetta.TextForId("mainwindow.menu.miperiodical.occurrences");
        miAdhoc.Header = _rosetta.TextForId("mainwindow.menu.miadhoc");
        miAdhocChart.Header = _rosetta.TextForId("mainwindow.menu.miadhoc.chart");
        miAdhocCalculations.Header = _rosetta.TextForId("mainwindow.menu.miadhoc.calculations");
        miHelp.Header = _rosetta.TextForId("mainwindow.menu.mihelp");
        miHelpAbout.Header = _rosetta.TextForId("mainwindow.menu.mihelp.about");
        miHelpPage.Header = _rosetta.TextForId("mainwindow.menu.mihelp.page");
        miHelpManual.Header = _rosetta.TextForId("mainwindow.menu.mihelp.manual");
    }

}
