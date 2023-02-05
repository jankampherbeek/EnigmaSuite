// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.State;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;


namespace Enigma.Frontend.Ui.Charts;

// TODO 0.1 finish ChartsMainWindow


/// <summary>Main window with dashboard for charts.</summary>
public partial class ChartsMainWindow : Window
{

    private readonly ChartsMainController _controller;
    private readonly DataVault _dataVault = DataVault.Instance;

    public ChartsMainWindow()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<ChartsMainController>();
        PopulateTexts();
        PopulateMenu();
        DisableOrEnable(false);
    }

    private void PopulateTexts()
    {
        Title = Rosetta.TextForId("chartsmainwindow.title");
        tbFormTitle.Text = Rosetta.TextForId("chartsmainwindow.formtitle");
        tbCurrent.Text = Rosetta.TextForId("chartsmainwindow.current");
        btnWheel.Content = Rosetta.TextForId("chartsmainwindow.btnwheel");
        btnPositions.Content = Rosetta.TextForId("chartsmainwindow.btnpositions");
        btnNew.Content = Rosetta.TextForId("chartsmainwindow.newchart");
        btnHelp.Content = Rosetta.TextForId("common.btnhelp");
        btnClose.Content = Rosetta.TextForId("common.btnclose");
    }

    private void PopulateMenu()
    {
        miGeneral.Header = Rosetta.TextForId("chartsmainwindow.menu.general");
        miGeneralClose.Header = Rosetta.TextForId("chartsmainwindow.menu.close");
        miGeneralConfiguration.Header = Rosetta.TextForId("chartsmainwindow.menu.migeneral.configuration");
        miGeneralSettings.Header = Rosetta.TextForId("chartsmainwindow.menu.migeneral.settings");
        miCharts.Header = Rosetta.TextForId("chartsmainwindow.menu.charts");
        miChartsNew.Header = Rosetta.TextForId("chartsmainwindow.menu.newchart");
        miChartsWheel.Header = Rosetta.TextForId("chartsmainwindow.menu.wheel");
        miChartsPositions.Header = Rosetta.TextForId("chartsmainwindow.menu.positions");
        miAnalysis.Header = Rosetta.TextForId("chartsmainwindow.menu.analysis");
        miAnalysisAspects.Header = Rosetta.TextForId("chartsmainwindow.menu.aspects");
        miAnalysisHarmonics.Header = Rosetta.TextForId("chartsmainwindow.menu.harmonics");
        miAnalysisMidpoints.Header = Rosetta.TextForId("chartsmainwindow.menu.midpoints");

        miHelp.Header = Rosetta.TextForId("chartsmainwindow.menu.help");
        miHelpAbout.Header = Rosetta.TextForId("chartsmainwindow.menu.helpabout");
        miHelpPage.Header = Rosetta.TextForId("chartsmainwindow.menu.helppage");
        miHelpManual.Header = Rosetta.TextForId("chartsmainwindow.menu.manual");




    }

    private void DisableOrEnable(bool able)
    {
        miChartsPositions.IsEnabled = able;
        miChartsWheel.IsEnabled = able;
        miAnalysis.IsEnabled = able;
        miAnalysisAspects.IsEnabled = able;
        miAnalysisHarmonics.IsEnabled = able;
        miAnalysisMidpoints.IsEnabled = able;
        btnPositions.IsEnabled = able;
        btnWheel.IsEnabled = able;
    }


    private void ShowCurrentChart()
    {
        _controller.ShowCurrentChart();
    }


    private void CloseClick(object sender, RoutedEventArgs e)
    {
        // TODO check if chart was saved 0.1.0
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



    private void ChartsNewClick(object sender, RoutedEventArgs e)
    {
        _controller.NewChart();
        bool newChartAdded = _dataVault.GetNewChartAdded();
        DisableOrEnable(newChartAdded);
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
        MessageBox.Show("Help about not yet implemented.");     // TODO 0.1 implement handling of click
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Help not yet implemented.");           // TODO implement handling of click
    }

    private void HelpManualClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Help manual not yet implemented.");    // TODO implement handling of click
    }




}
