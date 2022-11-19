// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;
using System;
using System.Windows;
using Enigma.Domain.Analysis;
using System.Windows.Controls;

namespace Enigma.Frontend.Ui.Charts;


/// <summary>
/// Interaction logic for ChartMidpointsWindow.xaml
/// </summary>
public partial class ChartMidpointsWindow : Window
{
    private readonly ChartMidpointsController _controller;
    private readonly IRosetta _rosetta;
    private readonly string _emptyHeader = " ";

    public ChartMidpointsWindow(ChartMidpointsController controller, IRosetta rosetta)
    {
        InitializeComponent();
        _controller = controller;
        _rosetta = rosetta;
    }

    public void Populate()
    {
        PopulateTexts();
        PopulateData(MidpointTypes.Dial360);
    }

 
    public void PopulateData(MidpointTypes midpointType)
    {
        string orbSize = _rosetta.TextForId("charts.midpoints.orbsize");
        double actualOrb = 1.6;                             // TODO 0.2.0 retrieve orb from settings
        if (midpointType == MidpointTypes.Dial90) actualOrb /= 4.0;
        if (midpointType == MidpointTypes.Dial45) actualOrb /= 8.0;

        string orbText = _controller.DegreesToDms(actualOrb);
        tbOrbSize.Text = orbSize + "\n" + orbText;
        dgAllMidpoints.ItemsSource = _controller.RetrieveAndFormatMidpoints(midpointType).Item1;
        dgAllMidpoints.GridLinesVisibility = DataGridGridLinesVisibility.None;
        dgAllMidpoints.Columns[0].Header = _emptyHeader;
        dgAllMidpoints.Columns[1].Header = _emptyHeader;
        dgAllMidpoints.Columns[2].Header = _emptyHeader;
        dgAllMidpoints.Columns[3].Header = _rosetta.TextForId("charts.midpoints.header.position");      
        dgAllMidpoints.Columns[4].Header = _emptyHeader;
        dgAllMidpoints.Columns[0].MaxWidth = 20;
        dgAllMidpoints.Columns[1].MaxWidth = 12;
        dgAllMidpoints.Columns[2].MaxWidth = 20;
        dgAllMidpoints.Columns[4].MaxWidth = 20;
        dgAllMidpoints.Columns[0].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgAllMidpoints.Columns[2].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgAllMidpoints.Columns[4].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgAllMidpoints.HorizontalAlignment= HorizontalAlignment.Right;

        dgOccupiedMidpoints.ItemsSource = _controller.RetrieveAndFormatMidpoints(midpointType).Item2;
        dgOccupiedMidpoints.GridLinesVisibility = DataGridGridLinesVisibility.None;
        dgOccupiedMidpoints.Columns[0].Header = _emptyHeader;
        dgOccupiedMidpoints.Columns[1].Header = _emptyHeader;
        dgOccupiedMidpoints.Columns[2].Header = _emptyHeader;
        dgOccupiedMidpoints.Columns[3].Header = _emptyHeader;
        dgOccupiedMidpoints.Columns[4].Header = _emptyHeader;
        dgOccupiedMidpoints.Columns[5].Header = _rosetta.TextForId("charts.midpoints.header.orb");
        dgOccupiedMidpoints.Columns[6].Header = _rosetta.TextForId("charts.midpoints.header.exactness"); 
        dgOccupiedMidpoints.Columns[0].MaxWidth = 20;
        dgOccupiedMidpoints.Columns[1].MaxWidth = 12;
        dgOccupiedMidpoints.Columns[2].MaxWidth = 20;
        dgOccupiedMidpoints.Columns[3].MaxWidth = 12;
        dgOccupiedMidpoints.Columns[4].MaxWidth = 20;
        dgOccupiedMidpoints.Columns[0].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgOccupiedMidpoints.Columns[2].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgOccupiedMidpoints.Columns[4].CellStyle = FindResource("glyphColumnStyle") as Style;
        dgOccupiedMidpoints.HorizontalAlignment= HorizontalAlignment.Right;
    }

    private void PopulateTexts()
    {
        Title = _rosetta.TextForId("charts.midpoints.title");
        tbFormTitle.Text = _rosetta.TextForId("charts.midpoints.formtitle");
        tbSubTitleChartId.Text = _controller.RetrieveChartName();                              
        tbAllMidpoints.Text = _rosetta.TextForId("charts.midpoints.allmidpoints");
        tbOccupiedMidpoints.Text = _rosetta.TextForId("charts.midpoints.occmidpoints");
        tbDialSize.Text = _rosetta.TextForId("charts.midpoints.dialsize");
        rbDial360.Content = _rosetta.TextForId("charts.midpoints.dial360");
        rbDial90.Content = _rosetta.TextForId("charts.midpoints.dial90");
        rbDial45.Content = _rosetta.TextForId("charts.midpoints.dial45");
        btnHelp.Content = _rosetta.TextForId("common.btnhelp");
        btnClose.Content = _rosetta.TextForId("common.btnclose");
    }

    private void RbDial360Checked(object sender, RoutedEventArgs e)
    {
        if (_controller != null) PopulateData(MidpointTypes.Dial360);
    }

    private void RbDial90Checked(object sender, RoutedEventArgs e)
    {
        if (_controller != null) PopulateData(MidpointTypes.Dial90);
    }

    private void RbDial45Checked(object sender, RoutedEventArgs e)
    {
        if (_controller != null) PopulateData(MidpointTypes.Dial45);
    }

    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Hide();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        //  ChartAspectsController.ShowHelp();
    }


}
