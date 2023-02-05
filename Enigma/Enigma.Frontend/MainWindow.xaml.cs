// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
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
        MainController.HelpClick();
    }



    private void ExitClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void PopulateTexts()
    {
        Title = Rosetta.TextForId("mainwindow.title") + " " + EnigmaConstants.ENIGMA_VERSION;
        tbFormTitle.Text = Rosetta.TextForId("mainwindow.formtitle");
        tbSubTitle.Text = Rosetta.TextForId("mainwindow.subtitle");
        tbExplanation.Text = Rosetta.TextForId("mainwindow.explanation");
  //      tbCharts.Text = Rosetta.TextForId("mainwindow.charts");
  //      tbResearch.Text = Rosetta.TextForId("mainwindow.research");


        /*    tbChartsTitle.Text = Rosetta.TextForId("mainwindow.chartstitle");
            tbChartsExpl.Text = Rosetta.TextForId("mainwindow.chartsexpl");
            tbNewChart.Text = Rosetta.TextForId("mainwindow.chartsnew");
            btnNewChart.Content = Rosetta.TextForId("mainwindow.btnchartsnew");
            btnSearchChart.Content = Rosetta.TextForId("mainwindow.btnchartssearch");

            tbResearchExpl.Text = Rosetta.TextForId("mainwindow.researchexpl");
            tbResearchTitle.Text = Rosetta.TextForId("mainwindow.researchtitle");
            tbNewData.Text = Rosetta.TextForId("mainwindow.datanew");
            tbNewProject.Text = Rosetta.TextForId("mainwindow.projectnew");
            btnNewData.Content = Rosetta.TextForId("mainwindow.btndatanew");
            btnNewProject.Content = Rosetta.TextForId("mainwindow.btnprojectnew");
            btnSearchProject.Content = Rosetta.TextForId("mainwindow.btnprojectsearch");

            tbPeriodicalTitle.Text = Rosetta.TextForId("mainwindow.periodicaltitle");
            tbPeriodicalExpl.Text = Rosetta.TextForId("mainwindow.periodicalexpl");
            tbNewCycle.Text = Rosetta.TextForId("mainwindow.cyclenew");
            tbNewEphemeris.Text = Rosetta.TextForId("mainwindow.ephemerisnew");
            btnNewCycle.Content = Rosetta.TextForId("mainwindow.btncyclenew");
            btnSearchCycle.Content = Rosetta.TextForId("mainwindow.btncyclesearch");
            btnNewEphemeris.Content = Rosetta.TextForId("mainwindow.btnephemerisnew");


            tbCalculatorsTitle.Text = Rosetta.TextForId("mainwindow.calculatorstitle");
            tbCalculatorsExpl.Text = Rosetta.TextForId("mainwindow.calculatorsexpl");
            btnConversions.Content = Rosetta.TextForId("mainwindow.btnconversions");
            btnParans.Content = Rosetta.TextForId("mainwindow.btnparans");
            btnEvents.Content = Rosetta.TextForId("mainwindow.btnevents");
            btnOthers.Content = Rosetta.TextForId("mainwindow.btnother");  */
        btnExit.Content = Rosetta.TextForId("common.btnexit");
        btnHelp.Content = Rosetta.TextForId("common.btnhelp");
    }

    /*    private void PopulateMenu()
        {


            miGeneral.Header = Rosetta.TextForId("mainwindow.menu.migeneral");
            miGeneralSettings.Header = Rosetta.TextForId("mainwindow.menu.migeneral.settings");
            miGeneralConfiguration.Header = Rosetta.TextForId("mainwindow.menu.migeneral.configuration");
            miGeneralClose.Header = Rosetta.TextForId("mainwindow.menu.migeneral.close");

            miCharts.Header = Rosetta.TextForId("mainwindow.menu.micharts");
            miChartsNew.Header = Rosetta.TextForId("mainwindow.menu.micharts.new");
            miChartsOverview.Header = Rosetta.TextForId("mainwindow.menu.micharts.overview");

            miResearch.Header = Rosetta.TextForId("mainwindow.menu.miresearch");
            miResearchDataOverview.Header = Rosetta.TextForId("mainwindow.menu.miresearch.dataoverview");
            miResearchDataImport.Header = Rosetta.TextForId("mainwindow.menu.miresearch.dataimport");
            miResesearchProjectsNew.Header = Rosetta.TextForId("mainwindow.menu.miresearch.projectsnew");
            miResearchProjectsOpen.Header = Rosetta.TextForId("mainwindow.menu.miresearch.projectsopen");

            miPeriodical.Header = Rosetta.TextForId("mainwindow.menu.miperiodical");
            miPeriodicalCycles.Header = Rosetta.TextForId("mainwindow.menu.miperiodical.cycles");
            miPeriodicalEphemeris.Header = Rosetta.TextForId("mainwindow.menu.miperiodical.ephemeris");
            miPeriodicalOccurrences.Header = Rosetta.TextForId("mainwindow.menu.miperiodical.occurrences");

            miCalculations.Header = Rosetta.TextForId("mainwindow.menu.micalculations");

            miHelp.Header = Rosetta.TextForId("mainwindow.menu.mihelp");
            miHelpAbout.Header = Rosetta.TextForId("mainwindow.menu.mihelp.about");
            miHelpPage.Header = Rosetta.TextForId("mainwindow.menu.mihelp.page");
            miHelpManual.Header = Rosetta.TextForId("mainwindow.menu.mihelp.manual");


        }
    */
}
