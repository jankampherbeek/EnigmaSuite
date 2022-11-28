// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Ui.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace Enigma.Frontend.Ui.Charts;

/// <summary>
/// Interaction logic for ChartAspects.xaml
/// </summary>
public partial class ChartAspectsWindow : Window
{

    private readonly ChartAspectsController _controller;
    private readonly IRosetta _rosetta;
    private readonly string _emptyHeader = " ";
    public ChartAspectsWindow(ChartAspectsController controller, IRosetta rosetta)
    {
        InitializeComponent();
        _controller = controller;
        _rosetta = rosetta;
    }

    public void Populate()
    {
        PopulateTexts();
        PopulateCelPointAspects();
        PopulateMundaneAspects();
    }


    private void PopulateTexts()
    {
        this.Title = _rosetta.TextForId("charts.aspects.formtitle");
        FormTitle.Text = _rosetta.TextForId("charts.aspects.formtitle");
        SubTitleChartId.Text = _controller.GetChartIdName();
        BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
        BtnClose.Content = _rosetta.TextForId("common.btnclose");
    }


    private void PopulateCelPointAspects()
    {
        dgSsAspects.ItemsSource = _controller.GetPresentableAspectsForCelPoints();
        dgSsAspects.GridLinesVisibility = DataGridGridLinesVisibility.None;
        dgSsAspects.Columns[0].Header = _emptyHeader;
        dgSsAspects.Columns[1].Header = _emptyHeader;
        dgSsAspects.Columns[2].Header = _emptyHeader;
        dgSsAspects.Columns[3].Header = _rosetta.TextForId("charts.aspects.datagrid.columns.orb");
        dgSsAspects.Columns[4].Header = _rosetta.TextForId("charts.aspects.datagrid.columns.exactness");
        dgSsAspects.Columns[0].MaxWidth = 20;
        dgSsAspects.Columns[1].MaxWidth = 20;
        dgSsAspects.Columns[2].MaxWidth = 20;
        dgSsAspects.Columns[0].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgSsAspects.Columns[1].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgSsAspects.Columns[2].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgSsAspects.HorizontalAlignment = HorizontalAlignment.Right;
    }

    private void PopulateMundaneAspects()
    {
        dgMuAspects.ItemsSource = _controller.GetPresentableAspectsForMundanePoints();
        dgMuAspects.GridLinesVisibility = DataGridGridLinesVisibility.None;
        dgMuAspects.Columns[0].Header = _emptyHeader;
        dgMuAspects.Columns[1].Header = _emptyHeader;
        dgMuAspects.Columns[2].Header = _emptyHeader;
        dgMuAspects.Columns[3].Header = _rosetta.TextForId("charts.aspects.datagrid.columns.orb");
        dgMuAspects.Columns[4].Header = _rosetta.TextForId("charts.aspects.datagrid.columns.exactness");
        dgMuAspects.Columns[0].MaxWidth = 80;
        dgMuAspects.Columns[1].MaxWidth = 20;
        dgMuAspects.Columns[2].MaxWidth = 20;
        dgMuAspects.Columns[0].CellStyle = FindResource("nameColumnStyle") as Style;
        dgMuAspects.Columns[1].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgMuAspects.Columns[2].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgMuAspects.HorizontalAlignment = HorizontalAlignment.Right;
    }

    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Hide();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        ChartAspectsController.ShowHelp();
    }
}
