// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Frontend.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace Enigma.Frontend.Charts;

/// <summary>Shows positions in tabular format.</summary>
public partial class ChartPositionsWindow : Window
{
    private IRosetta _rosetta;

    private ChartPositionsController _controller;
    private readonly string _space = " ";
    private readonly string _newLine = "\n";
    public ChartPositionsWindow(ChartPositionsController controller, IRosetta rosetta)
    {
        InitializeComponent();
        _controller = controller;
        _rosetta = rosetta;

    }

    public void PopulateAll()
    {
        PopulateTexts();
        PopulateHouses();
        PopulateCelPoints();

    }

    private void PopulateTexts()
    {
        FormTitle.Text = _rosetta.TextForId("charts.positions.formtitle");
        ChartData chartData = _controller.GetMeta();
        if (chartData != null)
        {
            ChartName.Text =chartData.ChartMetaData.Name;
            Details.Text = chartData.ChartMetaData.Description + _newLine +
                chartData.ChartLocation.LocationFullName + _newLine +
                ParseDateText(chartData.ChartDateTime.DateText) + _space + ParseTimeText(chartData.ChartDateTime.TimeText) + _newLine +
                _rosetta.TextForId("charts.positions.chartgategory") + _space + chartData.ChartMetaData.ChartCategory + _newLine +
                _rosetta.TextForId("charts.positions.rating") + _space + chartData.ChartMetaData.RoddenRating + _newLine +
                _rosetta.TextForId("charts.positions.source") + _space + chartData.ChartMetaData.Source;


       /*     Description.Text = chartData.ChartMetaData.Description;
            Location.Text = chartData.ChartLocation.LocationFullName;
            DateTime.Text = ParseDateText(chartData.ChartDateTime.DateText) + _space + ParseTimeText(chartData.ChartDateTime.TimeText);
            ChartCartegory.Text = _rosetta.TextForId("charts.positions.chartgategory") + _space + chartData.ChartMetaData.ChartCategory;
            Rating.Text = _rosetta.TextForId("charts.positions.rating") + _space + chartData.ChartMetaData.RoddenRating;
            Source.Text = _rosetta.TextForId("charts.positions.source") + _space + chartData.ChartMetaData.Source; */
        }
    }


    private void PopulateHouses()
    {
        DGHouses.ItemsSource = _controller.GetHousePositionsCurrentChart();
        DGHouses.GridLinesVisibility = DataGridGridLinesVisibility.None;
        DGHouses.Columns[0].Header = "";
        DGHouses.Columns[1].Header = _rosetta.TextForId("charts.positions.datagrid.columns.long");
        DGHouses.Columns[2].Header = "";
        DGHouses.Columns[3].Header = _rosetta.TextForId("charts.positions.datagrid.columns.ra");
        DGHouses.Columns[4].Header = _rosetta.TextForId("charts.positions.datagrid.columns.decl");
        DGHouses.Columns[5].Header = _rosetta.TextForId("charts.positions.datagrid.columns.azimuth");
        DGHouses.Columns[6].Header = _rosetta.TextForId("charts.positions.datagrid.columns.altitude");
        DGHouses.Columns[0].MaxWidth = 80;
        DGHouses.Columns[2].MaxWidth = 20;
        DGHouses.Columns[0].CellStyle = FindResource("nameColumnStyle") as Style;
        DGHouses.Columns[2].CellStyle = FindResource("glyphColumnStyle") as Style;
        DGHouses.HorizontalAlignment = HorizontalAlignment.Right;
    }

    private void PopulateCelPoints()
    {
        DGCelPoints.ItemsSource = _controller.GetCelPointPositionsCurrentChart();
        DGCelPoints.GridLinesVisibility = DataGridGridLinesVisibility.None;
        DGCelPoints.Columns[0].Header = "";
        DGCelPoints.Columns[1].Header = _rosetta.TextForId("charts.positions.datagrid.columns.long");
        DGCelPoints.Columns[2].Header = "";
        DGCelPoints.Columns[3].Header = _rosetta.TextForId("charts.positions.datagrid.columns.longspeed");
        DGCelPoints.Columns[4].Header = _rosetta.TextForId("charts.positions.datagrid.columns.lat");
        DGCelPoints.Columns[5].Header = _rosetta.TextForId("charts.positions.datagrid.columns.latspeed");
        DGCelPoints.Columns[6].Header = _rosetta.TextForId("charts.positions.datagrid.columns.ra");
        DGCelPoints.Columns[7].Header = _rosetta.TextForId("charts.positions.datagrid.columns.raspeed");
        DGCelPoints.Columns[8].Header = _rosetta.TextForId("charts.positions.datagrid.columns.decl");
        DGCelPoints.Columns[9].Header = _rosetta.TextForId("charts.positions.datagrid.columns.declspeed");
        DGCelPoints.Columns[10].Header = _rosetta.TextForId("charts.positions.datagrid.columns.dist");
        DGCelPoints.Columns[11].Header = _rosetta.TextForId("charts.positions.datagrid.columns.distspeed");
        DGCelPoints.Columns[12].Header = _rosetta.TextForId("charts.positions.datagrid.columns.azimuth");
        DGCelPoints.Columns[13].Header = _rosetta.TextForId("charts.positions.datagrid.columns.altitude");

        DGCelPoints.Columns[0].CellStyle = FindResource("glyphColumnStyle") as Style;
        DGCelPoints.Columns[2].CellStyle = FindResource("glyphColumnStyle") as Style;
        DGCelPoints.HorizontalAlignment = HorizontalAlignment.Right;
    }

    private string ParseDateText(string inputDateText)
    {
        int firstStartIndex = inputDateText.IndexOf("[");
        int secondStartIndex = inputDateText.LastIndexOf("[");
        int firstEndIndex = inputDateText.IndexOf("]");
        int secondEndIndex = inputDateText.LastIndexOf("]");
        string monthId = inputDateText.Substring(firstStartIndex + 1, firstEndIndex - firstStartIndex - 1);
        string calendarId = inputDateText.Substring(secondStartIndex + 1, secondEndIndex - secondStartIndex - 1);

        return _rosetta.TextForId("ref.months." + monthId) + inputDateText.Substring(firstEndIndex + 1, secondStartIndex - firstEndIndex - 1) + _rosetta.TextForId("ref.calendar." + calendarId);
    }

    private string ParseTimeText(string inputTimeText)
    {
        int startIndex = inputTimeText.IndexOf("[");
        int endIndex = inputTimeText.LastIndexOf("]");
        string timeZoneId = inputTimeText.Substring(startIndex + 1, endIndex - startIndex - 1);
        return inputTimeText.Substring(0, startIndex - 1) + _rosetta.TextForId(timeZoneId);
    }


    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Hide();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        HelpWindow? helpWindow = App.ServiceProvider.GetService<HelpWindow>();
        if (helpWindow != null)
        {
            helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            helpWindow.SetUri("ChartsPositions");
            helpWindow.ShowDialog();
        }
    }


}
