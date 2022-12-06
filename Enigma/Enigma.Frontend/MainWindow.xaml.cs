// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;


namespace Enigma.Frontend.Ui;

/// <summary>Main view for the application.</summary>
public partial class MainWindow : Window
{
    private readonly Rosetta _rosetta = Rosetta.Instance;
    private readonly MainController _controller;

    public MainWindow()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<MainController>();
        PopulateTexts();
    }

    private void ChartsClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowChartsMain();
    }

    private void ResearchClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowResearchMain();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("MainWindow");
        helpWindow.ShowDialog();
    }



    private void ExitClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void PopulateTexts()
    {
        Title = _rosetta.TextForId("mainwindow.title") + " " + EnigmaConstants.ENIGMA_VERSION;
        tbFormTitle.Text = _rosetta.TextForId("mainwindow.formtitle");
        tbSubTitle.Text = _rosetta.TextForId("mainwindow.subtitle");
        tbExplanation.Text = _rosetta.TextForId("mainwindow.explanation");
        tbCharts.Text = _rosetta.TextForId("mainwindow.charts");
        tbResearch.Text = _rosetta.TextForId("mainwindow.research");


        /*    tbChartsTitle.Text = _rosetta.TextForId("mainwindow.chartstitle");
            tbChartsExpl.Text = _rosetta.TextForId("mainwindow.chartsexpl");
            tbNewChart.Text = _rosetta.TextForId("mainwindow.chartsnew");
            btnNewChart.Content = _rosetta.TextForId("mainwindow.btnchartsnew");
            btnSearchChart.Content = _rosetta.TextForId("mainwindow.btnchartssearch");

            tbResearchExpl.Text = _rosetta.TextForId("mainwindow.researchexpl");
            tbResearchTitle.Text = _rosetta.TextForId("mainwindow.researchtitle");
            tbNewData.Text = _rosetta.TextForId("mainwindow.datanew");
            tbNewProject.Text = _rosetta.TextForId("mainwindow.projectnew");
            btnNewData.Content = _rosetta.TextForId("mainwindow.btndatanew");
            btnNewProject.Content = _rosetta.TextForId("mainwindow.btnprojectnew");
            btnSearchProject.Content = _rosetta.TextForId("mainwindow.btnprojectsearch");

            tbPeriodicalTitle.Text = _rosetta.TextForId("mainwindow.periodicaltitle");
            tbPeriodicalExpl.Text = _rosetta.TextForId("mainwindow.periodicalexpl");
            tbNewCycle.Text = _rosetta.TextForId("mainwindow.cyclenew");
            tbNewEphemeris.Text = _rosetta.TextForId("mainwindow.ephemerisnew");
            btnNewCycle.Content = _rosetta.TextForId("mainwindow.btncyclenew");
            btnSearchCycle.Content = _rosetta.TextForId("mainwindow.btncyclesearch");
            btnNewEphemeris.Content = _rosetta.TextForId("mainwindow.btnephemerisnew");


            tbCalculatorsTitle.Text = _rosetta.TextForId("mainwindow.calculatorstitle");
            tbCalculatorsExpl.Text = _rosetta.TextForId("mainwindow.calculatorsexpl");
            btnConversions.Content = _rosetta.TextForId("mainwindow.btnconversions");
            btnParans.Content = _rosetta.TextForId("mainwindow.btnparans");
            btnEvents.Content = _rosetta.TextForId("mainwindow.btnevents");
            btnOthers.Content = _rosetta.TextForId("mainwindow.btnother");  */
        btnExit.Content = _rosetta.TextForId("common.btnexit");
        btnHelp.Content = _rosetta.TextForId("common.btnhelp");
    }

/*    private void PopulateMenu()
    {


        miGeneral.Header = _rosetta.TextForId("mainwindow.menu.migeneral");
        miGeneralSettings.Header = _rosetta.TextForId("mainwindow.menu.migeneral.settings");
        miGeneralConfiguration.Header = _rosetta.TextForId("mainwindow.menu.migeneral.configuration");
        miGeneralClose.Header = _rosetta.TextForId("mainwindow.menu.migeneral.close");

        miCharts.Header = _rosetta.TextForId("mainwindow.menu.micharts");
        miChartsNew.Header = _rosetta.TextForId("mainwindow.menu.micharts.new");
        miChartsOverview.Header = _rosetta.TextForId("mainwindow.menu.micharts.overview");

        miResearch.Header = _rosetta.TextForId("mainwindow.menu.miresearch");
        miResearchDataOverview.Header = _rosetta.TextForId("mainwindow.menu.miresearch.dataoverview");
        miResearchDataImport.Header = _rosetta.TextForId("mainwindow.menu.miresearch.dataimport");
        miResesearchProjectsNew.Header = _rosetta.TextForId("mainwindow.menu.miresearch.projectsnew");
        miResearchProjectsOpen.Header = _rosetta.TextForId("mainwindow.menu.miresearch.projectsopen");

        miPeriodical.Header = _rosetta.TextForId("mainwindow.menu.miperiodical");
        miPeriodicalCycles.Header = _rosetta.TextForId("mainwindow.menu.miperiodical.cycles");
        miPeriodicalEphemeris.Header = _rosetta.TextForId("mainwindow.menu.miperiodical.ephemeris");
        miPeriodicalOccurrences.Header = _rosetta.TextForId("mainwindow.menu.miperiodical.occurrences");

        miCalculations.Header = _rosetta.TextForId("mainwindow.menu.micalculations");

        miHelp.Header = _rosetta.TextForId("mainwindow.menu.mihelp");
        miHelpAbout.Header = _rosetta.TextForId("mainwindow.menu.mihelp.about");
        miHelpPage.Header = _rosetta.TextForId("mainwindow.menu.mihelp.page");
        miHelpManual.Header = _rosetta.TextForId("mainwindow.menu.mihelp.manual");


    }
*/
}
