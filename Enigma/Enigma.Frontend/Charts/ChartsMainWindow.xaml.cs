// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.State;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace Enigma.Frontend.Ui.Charts;


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
        PopulateData();
        DisableOrEnable();
    }

    private void PopulateTexts()
    {
        Title = Rosetta.TextForId("chartsmainwindow.title");
        tbFormTitle.Text = Rosetta.TextForId("chartsmainwindow.formtitle");
        tbAvailable.Text = Rosetta.TextForId("chartsmainwindow.available");
        btnPositions.Content = Rosetta.TextForId("chartsmainwindow.btnpositions");
        btnWheel.Content = Rosetta.TextForId("chartsmainwindow.btnwheel");
        btnNew.Content = Rosetta.TextForId("chartsmainwindow.newchart");
        btnSearch.Content = Rosetta.TextForId("common.btnsearch");
        btnDelete.Content = Rosetta.TextForId("common.btndelete");
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
        miChartsSearch.Header = Rosetta.TextForId("chartsmainwindow.menu.chartsearch");
        miChartsDelete.Header = Rosetta.TextForId("chartsmainwindow.menu.chartdelete");
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

    private void PopulateData()
    {
        tbNrOfCharts.Text = Rosetta.TextForId("chartsmainwindow.nrofcharts") + " " + _controller.NrOfChartsInDatabase().ToString();
        tbLastChart.Text = Rosetta.TextForId("chartsmainwindow.lastchart") + " " + _controller.MostRecentChart();
        tbActiveChart.Text = Rosetta.TextForId("chartsmainwindow.activechart") + " " + _controller.CurrentChartName();
    }

    private void PopulateAvailableCharts()
    {
        dgCurrent.ItemsSource = _controller.AllChartData();
        dgCurrent.GridLinesVisibility = DataGridGridLinesVisibility.None;
        dgCurrent.Columns[0].Header = Rosetta.TextForId("chartsmainwindow.availablechartid");
        dgCurrent.Columns[1].Header = Rosetta.TextForId("chartsmainwindow.availablechartname");
        dgCurrent.Columns[2].Header = Rosetta.TextForId("chartsmainwindow.availablechartdescr");
        dgCurrent.Columns[0].CellStyle = FindResource("nameColumnStyle") as Style;
        dgCurrent.Columns[1].CellStyle = FindResource("nameColumnStyle") as Style;
        dgCurrent.Columns[2].CellStyle = FindResource("nameColumnStyle") as Style;
    }


    private void DisableOrEnable()
    {
        bool able = _dataVault.GetCurrentChart() != null;
        miChartsPositions.IsEnabled = able;
        miChartsWheel.IsEnabled = able;
        miChartsDelete.IsEnabled = able;    
        miAnalysis.IsEnabled = able;
        miAnalysisAspects.IsEnabled = able;
        miAnalysisHarmonics.IsEnabled = able;
        miAnalysisMidpoints.IsEnabled = able;
        btnPositions.IsEnabled = able;
        btnWheel.IsEnabled = able;
        btnDelete.IsEnabled = able;
    }


    private void ShowCurrentChart()
    {
        _controller.ShowCurrentChart();
    }


    private void SearchClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowSearch();
        PopulateData();
        PopulateAvailableCharts();
        DisableOrEnable();
    }

    private void DeleteClick(object sender, RoutedEventArgs e)
    {
        string name = _controller.CurrentChartName();
        if (MessageBox.Show(Rosetta.TextForId("chartsmainwindow.msg.confirmdeletechart").Replace("[name]", name),
                            Rosetta.TextForId("chartsmainwindow.msg.confirmdeletetitle"),
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            if (_controller.DeleteCurrentChart())
            {
                MessageBox.Show(Rosetta.TextForId("chartsmainwindow.msg.deleteresultpositive").Replace("[name]", name), 
                                Rosetta.TextForId("chartsmainwindow.msg.deleteresulttitle"));
            }
            else
            {
                MessageBox.Show(Rosetta.TextForId("chartsmainwindow.msg.deleteresultnegative").Replace("[name]", name),
                                Rosetta.TextForId("chartsmainwindow.msg.deleteresulttitle"));
            }
        }
    }


    private void CloseClick(object sender, RoutedEventArgs e)
    {
        _controller.HandleClose();
        Close();
    }

    private void GeneralConfigurationClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowAstroConfig();
        DisableOrEnable();
        PopulateData();
        PopulateAvailableCharts();
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
        DisableOrEnable();
        PopulateAvailableCharts();
        PopulateData();
    }

    private void NewSelection(object sender, RoutedEventArgs e)
    {
        if (sender is DataGrid dataGrid)
        {
            PresentableChartData? rowView = (PresentableChartData)dataGrid.SelectedItem;
            if (rowView != null)
            {
                int index = int.Parse(rowView.Id);
                _controller.SearchAndSetActiveChart(index);
            }
            PopulateData();
        }
        DisableOrEnable();
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
        ChartsMainController.ShowAbout();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
       ChartsMainController.ShowHelp();
    }

    private void HelpManualClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Help manual not yet implemented.");    // TODO implement handling of click
    }

}
