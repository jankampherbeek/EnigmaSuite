// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using System.Windows;
using System.Windows.Controls;

namespace Enigma.Frontend.Ui.Charts;

/// <summary>
/// Interaction logic for ChartAspects.xaml
/// </summary>
public partial class ChartAspectsWindow : Window
{

    private readonly ChartAspectsController _controller;
    private readonly string _emptyHeader = " ";
    public ChartAspectsWindow(ChartAspectsController controller)
    {
        InitializeComponent();
        _controller = controller;
    }

    public void Populate()
    {
        PopulateTexts();
        PopulateData();
    }


    private void PopulateTexts()
    {
        this.Title = Rosetta.TextForId("charts.aspects.title");
        FormTitle.Text = Rosetta.TextForId("charts.aspects.formtitle");
        SubTitleChartId.Text = _controller.GetChartIdName();
        BtnHelp.Content = Rosetta.TextForId("common.btnhelp");
        BtnClose.Content = Rosetta.TextForId("common.btnclose");
    }


    private void PopulateData()
    {
        tbDescriptionText.Text = _controller.DescriptiveText();
        dgSsAspects.ItemsSource = _controller.GetPresentableAspectsForChartPoints();
        dgSsAspects.GridLinesVisibility = DataGridGridLinesVisibility.None;
        dgSsAspects.Columns[0].Header = Rosetta.TextForId("charts.aspects.datagrid.columns.point1");
        dgSsAspects.Columns[1].Header = _emptyHeader;
        dgSsAspects.Columns[2].Header = Rosetta.TextForId("charts.aspects.datagrid.columns.aspect");
        dgSsAspects.Columns[3].Header = _emptyHeader;
        dgSsAspects.Columns[4].Header = Rosetta.TextForId("charts.aspects.datagrid.columns.point2");
        dgSsAspects.Columns[5].Header = _emptyHeader;
        dgSsAspects.Columns[6].Header = Rosetta.TextForId("charts.aspects.datagrid.columns.orb");
        dgSsAspects.Columns[7].Header = Rosetta.TextForId("charts.aspects.datagrid.columns.exactness");


        dgSsAspects.Columns[1].MaxWidth = 20;
        dgSsAspects.Columns[3].MaxWidth = 20;
        dgSsAspects.Columns[5].MaxWidth = 20;
        dgSsAspects.Columns[0].CellStyle = FindResource("NameColumnStyle") as Style;
        dgSsAspects.Columns[2].CellStyle = FindResource("NameColumnStyle") as Style;
        dgSsAspects.Columns[4].CellStyle = FindResource("NameColumnStyle") as Style;
        dgSsAspects.Columns[1].CellStyle = FindResource("GlyphColumnStyle") as Style;
        dgSsAspects.Columns[3].CellStyle = FindResource("GlyphColumnStyle") as Style;
        dgSsAspects.Columns[5].CellStyle = FindResource("GlyphColumnStyle") as Style;
        dgSsAspects.HorizontalAlignment = HorizontalAlignment.Right;
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
