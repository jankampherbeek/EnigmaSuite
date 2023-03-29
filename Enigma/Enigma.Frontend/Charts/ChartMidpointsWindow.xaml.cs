// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using System.Windows;
using System.Windows.Controls;

namespace Enigma.Frontend.Ui.Charts;


/// <summary>
/// Interaction logic for ChartMidpointsWindow.xaml
/// </summary>
public partial class ChartMidpointsWindow : Window
{
    private readonly ChartMidpointsController _controller;
    private readonly string _emptyHeader = " ";

    public ChartMidpointsWindow(ChartMidpointsController controller)
    {
        InitializeComponent();
        _controller = controller;
    }

    public void Populate()
    {
        PopulateTexts();
        PopulateData(360.0);
    }



    // TODO split in two methodes
    public void PopulateData(double dialSize)
    {
        tbDescriptionText.Text = _controller.DescriptiveText();
        string orbSize = Rosetta.TextForId("charts.midpoints.orbsize");
        double actualOrb = 1.6;                             // TODO 0.2.0 retrieve Orb from settings
        string orbText = _controller.DegreesToDms(actualOrb);
        tbOrbSize.Text = orbSize + "\n" + orbText;
        dgAllMidpoints.ItemsSource = _controller.RetrieveAndFormatMidpoints(360.0).Item1;
        dgAllMidpoints.GridLinesVisibility = DataGridGridLinesVisibility.None;
        dgAllMidpoints.Columns[0].Header = _emptyHeader;
        dgAllMidpoints.Columns[1].Header = _emptyHeader;
        dgAllMidpoints.Columns[2].Header = _emptyHeader;
        dgAllMidpoints.Columns[3].Header = Rosetta.TextForId("charts.midpoints.header.position");
        dgAllMidpoints.Columns[4].Header = _emptyHeader;
        dgAllMidpoints.Columns[0].MaxWidth = 20;
        dgAllMidpoints.Columns[1].MaxWidth = 12;
        dgAllMidpoints.Columns[2].MaxWidth = 20;
        dgAllMidpoints.Columns[4].MaxWidth = 20;
        dgAllMidpoints.Columns[0].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgAllMidpoints.Columns[2].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgAllMidpoints.Columns[4].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgAllMidpoints.HorizontalAlignment = HorizontalAlignment.Right;

        dgOccupiedMidpoints.ItemsSource = _controller.RetrieveAndFormatMidpoints(dialSize).Item2;
        dgOccupiedMidpoints.GridLinesVisibility = DataGridGridLinesVisibility.None;
        dgOccupiedMidpoints.Columns[0].Header = _emptyHeader;
        dgOccupiedMidpoints.Columns[1].Header = _emptyHeader;
        dgOccupiedMidpoints.Columns[2].Header = _emptyHeader;
        dgOccupiedMidpoints.Columns[3].Header = _emptyHeader;
        dgOccupiedMidpoints.Columns[4].Header = _emptyHeader;
        dgOccupiedMidpoints.Columns[5].Header = Rosetta.TextForId("charts.midpoints.header.orb");
        dgOccupiedMidpoints.Columns[6].Header = Rosetta.TextForId("charts.midpoints.header.exactness");
        dgOccupiedMidpoints.Columns[0].MaxWidth = 20;
        dgOccupiedMidpoints.Columns[1].MaxWidth = 12;
        dgOccupiedMidpoints.Columns[2].MaxWidth = 20;
        dgOccupiedMidpoints.Columns[3].MaxWidth = 12;
        dgOccupiedMidpoints.Columns[4].MaxWidth = 20;
        dgOccupiedMidpoints.Columns[0].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgOccupiedMidpoints.Columns[2].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgOccupiedMidpoints.Columns[4].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgOccupiedMidpoints.HorizontalAlignment = HorizontalAlignment.Right;
    }

    private void PopulateTexts()
    {
        Title = Rosetta.TextForId("charts.midpoints.title");
        tbFormTitle.Text = Rosetta.TextForId("charts.midpoints.formtitle");
        tbSubTitleChartId.Text = _controller.RetrieveChartName();
        tbAllMidpoints.Text = Rosetta.TextForId("charts.midpoints.allmidpoints");
        tbOccupiedMidpoints.Text = Rosetta.TextForId("charts.midpoints.occmidpoints");
        tbDialSize.Text = Rosetta.TextForId("charts.midpoints.dialsize");
        rbDial360.Content = Rosetta.TextForId("charts.midpoints.dial360");
        rbDial90.Content = Rosetta.TextForId("charts.midpoints.dial90");
        rbDial45.Content = Rosetta.TextForId("charts.midpoints.dial45");
        btnHelp.Content = Rosetta.TextForId("common.btnhelp");
        btnClose.Content = Rosetta.TextForId("common.btnclose");
    }

    private void RbDial360Checked(object sender, RoutedEventArgs e)
    {
        if (_controller != null) PopulateData(360.0);
    }

    private void RbDial90Checked(object sender, RoutedEventArgs e)
    {
        if (_controller != null) PopulateData(90.0);
    }

    private void RbDial45Checked(object sender, RoutedEventArgs e)
    {
        if (_controller != null) PopulateData(45.0);
    }

    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        ChartMidpointsController.ShowHelp();
    }


}
