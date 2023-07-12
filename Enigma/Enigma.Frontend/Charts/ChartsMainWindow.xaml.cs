// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using System.Windows.Controls;
using Enigma.Domain.Charts;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.State;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.Charts;

/// <summary>Main window with dashboard for charts.</summary>
public partial class ChartsMainWindow
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
        TbFormTitle.Text = Rosetta.TextForId("chartsmainwindow.formtitle");
        TbAvailable.Text = Rosetta.TextForId("chartsmainwindow.available");
        BtnPositions.Content = Rosetta.TextForId("chartsmainwindow.btnpositions");
        BtnWheel.Content = Rosetta.TextForId("chartsmainwindow.btnwheel");
        BtnNew.Content = Rosetta.TextForId("chartsmainwindow.newchart");
        BtnSearch.Content = Rosetta.TextForId("common.btnsearch");
        BtnDelete.Content = Rosetta.TextForId("common.btndelete");
        BtnHelp.Content = Rosetta.TextForId("common.btnhelp");
        BtnClose.Content = Rosetta.TextForId("common.btnclose");
    }

    private void PopulateMenu()
    {
        MiGeneral.Header = Rosetta.TextForId("chartsmainwindow.menu.general");
        MiGeneralClose.Header = Rosetta.TextForId("chartsmainwindow.menu.close");
        MiGeneralConfiguration.Header = Rosetta.TextForId("chartsmainwindow.menu.migeneral.configuration");
        MiGeneralSettings.Header = Rosetta.TextForId("chartsmainwindow.menu.migeneral.settings");

        MiCharts.Header = Rosetta.TextForId("chartsmainwindow.menu.charts");
        MiChartsNew.Header = Rosetta.TextForId("chartsmainwindow.menu.newchart");
        MiChartsSearch.Header = Rosetta.TextForId("chartsmainwindow.menu.chartsearch");
        MiChartsDelete.Header = Rosetta.TextForId("chartsmainwindow.menu.chartdelete");
        MiChartsWheel.Header = Rosetta.TextForId("chartsmainwindow.menu.wheel");
        MiChartsPositions.Header = Rosetta.TextForId("chartsmainwindow.menu.positions");

        MiAnalysis.Header = Rosetta.TextForId("chartsmainwindow.menu.analysis");
        MiAnalysisAspects.Header = Rosetta.TextForId("chartsmainwindow.menu.aspects");
        MiAnalysisHarmonics.Header = Rosetta.TextForId("chartsmainwindow.menu.harmonics");
        MiAnalysisMidpoints.Header = Rosetta.TextForId("chartsmainwindow.menu.midpoints");

        MiProgressive.Header = Rosetta.TextForId("chartsmainwindow.menu.progressive");
        MiProgressiveNewEvent.Header = Rosetta.TextForId("chartsmainwindow.menu.progressivenewevent");
        MiProgressiveSearchEvent.Header = Rosetta.TextForId("chartsmainwindow.menu.progressivesearchevent");
        MiProgressiveNewDaterange.Header = Rosetta.TextForId("chartsmainwindow.menu.progressivedaterange");

        MiProgressivePd.Header = Rosetta.TextForId("chartsmainwindow.menu.progressivepd");
        MiProgressiveSd.Header = Rosetta.TextForId("chartsmainwindow.menu.progressivesp");
        MiProgressiveTrans.Header = Rosetta.TextForId("chartsmainwindow.menu.progressivetrans");
        MiProgressiveSym.Header = Rosetta.TextForId("chartsmainwindow.menu.progressivesym");
        MiProgressiveSolar.Header = Rosetta.TextForId("chartsmainwindow.menu.progressivesolar");

        MiHelp.Header = Rosetta.TextForId("chartsmainwindow.menu.help");
        MiHelpAbout.Header = Rosetta.TextForId("chartsmainwindow.menu.helpabout");
        MiHelpPage.Header = Rosetta.TextForId("chartsmainwindow.menu.helppage");
        MiHelpManual.Header = Rosetta.TextForId("chartsmainwindow.menu.manual");
    }

    private void PopulateData()
    {
        TbNrOfCharts.Text = Rosetta.TextForId("chartsmainwindow.nrofcharts") + " " + _controller.NrOfChartsInDatabase();
        TbLastChart.Text = Rosetta.TextForId("chartsmainwindow.lastchart") + " " + _controller.MostRecentChart();
        TbActiveChart.Text = Rosetta.TextForId("chartsmainwindow.activechart") + " " + _controller.CurrentChartName();
    }

    private void PopulateAvailableCharts()
    {
        DgCurrent.ItemsSource = _controller.AllChartData();
        DgCurrent.GridLinesVisibility = DataGridGridLinesVisibility.None;
        DgCurrent.Columns[0].Header = Rosetta.TextForId("chartsmainwindow.availablechartid");
        DgCurrent.Columns[1].Header = Rosetta.TextForId("chartsmainwindow.availablechartname");
        DgCurrent.Columns[2].Header = Rosetta.TextForId("chartsmainwindow.availablechartdescr");
        DgCurrent.Columns[0].CellStyle = FindResource("nameColumnStyle") as Style;
        DgCurrent.Columns[1].CellStyle = FindResource("nameColumnStyle") as Style;
        DgCurrent.Columns[2].CellStyle = FindResource("nameColumnStyle") as Style;
    }


    private void DisableOrEnable()
    {
        bool able = _dataVault.GetCurrentChart() != null;
        MiChartsPositions.IsEnabled = able;
        MiChartsWheel.IsEnabled = able;
        MiChartsDelete.IsEnabled = able;
        MiAnalysis.IsEnabled = able;
        MiAnalysisAspects.IsEnabled = able;
        MiAnalysisHarmonics.IsEnabled = able;
        MiAnalysisMidpoints.IsEnabled = able;
        BtnPositions.IsEnabled = able;
        BtnWheel.IsEnabled = able;
        BtnDelete.IsEnabled = able;
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
            MessageBox.Show(
                _controller.DeleteCurrentChart()
                    ? Rosetta.TextForId("chartsmainwindow.msg.deleteresultpositive").Replace("[name]", name)
                    : Rosetta.TextForId("chartsmainwindow.msg.deleteresultnegative").Replace("[name]", name),
                Rosetta.TextForId("chartsmainwindow.msg.deleteresulttitle"));
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
            var rowView = (PresentableChartData)dataGrid.SelectedItem;
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


    private void NewEventClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowInputEvent();
    }

    private void SearchEventClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowSearchEvent();
    }

    private void DaterangeClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowInputDaterange();
    }


    private void PrimDirClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowInputPrimDir();
    }

    private void SecProgClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowInputSecProg();
    }

    private void TransClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowInputTransProg();
    }

    private void SymDirClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowInputSymProg();
    }

    private void SolarClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowInputSolarProg();
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
        MessageBox.Show(Rosetta.TextForId("helpwindow.manual"));
    }
}