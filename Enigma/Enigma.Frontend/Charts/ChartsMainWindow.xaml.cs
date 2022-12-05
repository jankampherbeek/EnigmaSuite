// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Charts;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;


namespace Enigma.Frontend.Ui;

/// <summary>Main window with dashboard for charts.</summary>
public partial class ChartsMainWindow : Window
{

    private ChartsMainController _controller;
    private Rosetta _rosetta = Rosetta.Instance;


    public ChartsMainWindow()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<ChartsMainController>();
        PopulateTexts();
        PopulateMenu();
   //     ShowCurrentChart();
    }

    private void PopulateTexts()
    {
        Title = _rosetta.TextForId("chartsmainwindow.title");
        tbFormTitle.Text = _rosetta.TextForId("chartsmainwindow.formtitle");
        tbCurrent.Text = _rosetta.TextForId("chartsmainwindow.current");
        tbTags.Text = _rosetta.TextForId("chartsmainwindow.tags");
        btnWheel.Content = _rosetta.TextForId("chartsmainwindow.btnwheel");
        btnPositions.Content = _rosetta.TextForId("chartsmainwindow.btnpositions");
        btnSave.Content = _rosetta.TextForId("common.btnsave");
        btnNew.Content = _rosetta.TextForId("chartsmainwindow.newchart");
        btnOverview.Content = _rosetta.TextForId("chartsmainwindow.overviewcharts");
        btnSearch.Content = _rosetta.TextForId("common.btnsearch");
        btnAddTag.Content = _rosetta.TextForId("chartsmainwindow.btnaddtag");
        btnHelp.Content = _rosetta.TextForId("common.btnhelp");
        btnClose.Content = _rosetta.TextForId("common.btnclose");
    }

    private void PopulateMenu()
    {
        miGeneral.Header = _rosetta.TextForId("chartsmainwindow.menu.general");
        miGeneralClose.Header = _rosetta.TextForId("chartsmainwindow.menu.close");
        miGeneralConfiguration.Header = _rosetta.TextForId("chartsmainwindow.menu.migeneral.configuration");
        miGeneralSettings.Header = _rosetta.TextForId("chartsmainwindow.menu.migeneral.settings");
        miCharts.Header = _rosetta.TextForId("chartsmainwindow.menu.charts");
        miChartsWheel.Header = _rosetta.TextForId("chartsmainwindow.menu.wheel");
        miChartsPositions.Header = _rosetta.TextForId("chartsmainwindow.menu.positions");
        miChartsNew.Header = _rosetta.TextForId("chartsmainwindow.menu.newchart");
        miChartsOverview.Header = _rosetta.TextForId("chartsmainwindow.menu.chartsoverview");
        miChartsSave.Header = _rosetta.TextForId("chartsmainwindow.menu.save");
        miAnalysis.Header = _rosetta.TextForId("chartsmainwindow.menu.analysis");
        miAnalysisAspects.Header = _rosetta.TextForId("chartsmainwindow.menu.aspects");
        miAnalysisHarmonics.Header = _rosetta.TextForId("chartsmainwindow.menu.harmonics");
        miAnalysisMidpoints.Header = _rosetta.TextForId("chartsmainwindow.menu.midpoints");

        miHelp.Header = _rosetta.TextForId("chartsmainwindow.menu.help");
        miHelpAbout.Header = _rosetta.TextForId("chartsmainwindow.menu.helpabout");
        miHelpPage.Header = _rosetta.TextForId("chartsmainwindow.menu.helppage");
        miHelpManual.Header = _rosetta.TextForId("chartsmainwindow.menu.manual");
    }


    private void ShowCurrentChart()
    {
        _controller.ShowCurrentChart();
    }


    private void CloseClick(object sender, RoutedEventArgs e)
    {
        // TODO check if chart was saved 0.2.0
        _controller.HandleClose();
        Close();
    }

    private void GeneralConfigurationClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowAstroConfig();
    }

    private void GeneralSettingsClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowAppSettings();
    }


    private void ShowWheelClick(object sender, RoutedEventArgs e)
    {
        ShowCurrentChart();
    }

    private void ShowPositionsClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowPositions();
    }

    private void ChartsSaveClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Save not yet implemented.");           // TODO implement handling of click
    }

    private void ChartsNewClick(object sender, RoutedEventArgs e)
    {
        _controller.NewChart();
    }

    private void ChartsOverviewClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Charts overview not yet implemented.");    // TODO implement handling of click
    }

    private void AspectsClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowAspects();
    }

    private void HarmonicsClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowHarmonics();
    }

    private void MidpointsClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowMidpoints();
    }

    private void HelpAboutClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Help about not yet implemented.");     // TODO implement handling of click
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Help not yet implemented.");           // TODO implement handling of click
    }

    private void HelpManualClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Help manual not yet implemented.");    // TODO implement handling of click
    }

    private void SearchClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Search not yet implemented.");         // TODO implement handling of click
    }

    private void AddTagClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Add Tag not yet implemented.");        // TODO implement handling of click
    }


}
