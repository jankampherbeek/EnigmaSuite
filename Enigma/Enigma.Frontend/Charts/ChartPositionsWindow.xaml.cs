// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Enigma.Frontend.Ui.Charts;

/// <summary>Shows positions in tabular format.</summary>
public partial class ChartPositionsWindow : Window
{
    private readonly ChartPositionsController _controller;
    private readonly string _space = " ";
    private readonly string _newLine = "\n";
    private ChartData? _chartData;
    public ChartPositionsWindow()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<ChartPositionsController>();
    }

    public void PopulateAll()
    {
        PopulateTexts();
        PopulateHouses();
        PopulateCelPoints();

    }

    private void PopulateTexts()
    {
        FormTitle.Text = Rosetta.TextForId("charts.positions.formtitle");
        _chartData = _controller.GetMeta();
        if (_chartData != null)
        {
            ChartName.Text = _chartData.MetaData.Name;
            Details.Text = _chartData.MetaData.Description + _newLine +
                _chartData.Location.LocationFullName + _newLine +
                ParseDateText(_chartData.FullDateTime.DateText) + _space + ParseTimeText(_chartData.FullDateTime.TimeText) + _newLine +
                Rosetta.TextForId("charts.positions.chartgategory") + _space + _chartData.MetaData.ChartCategory + _newLine +
                Rosetta.TextForId("charts.positions.rating") + _space + _chartData.MetaData.RoddenRating + _newLine +
                Rosetta.TextForId("charts.positions.source") + _space + _chartData.MetaData.Source;
        }
    }


    private void PopulateHouses()
    {
        DGHouses.ItemsSource = _controller.GetHousePositionsCurrentChart();
        DGHouses.GridLinesVisibility = DataGridGridLinesVisibility.None;
        DGHouses.Columns[0].Header = "";
        DGHouses.Columns[1].Header = Rosetta.TextForId("charts.positions.datagrid.columns.long");
        DGHouses.Columns[2].Header = "";
        DGHouses.Columns[3].Header = Rosetta.TextForId("charts.positions.datagrid.columns.ra");
        DGHouses.Columns[4].Header = Rosetta.TextForId("charts.positions.datagrid.columns.decl");
        DGHouses.Columns[5].Header = Rosetta.TextForId("charts.positions.datagrid.columns.azimuth");
        DGHouses.Columns[6].Header = Rosetta.TextForId("charts.positions.datagrid.columns.altitude");
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
        DGCelPoints.Columns[1].Header = Rosetta.TextForId("charts.positions.datagrid.columns.long");
        DGCelPoints.Columns[2].Header = "";
        DGCelPoints.Columns[3].Header = Rosetta.TextForId("charts.positions.datagrid.columns.longspeed");
        DGCelPoints.Columns[4].Header = Rosetta.TextForId("charts.positions.datagrid.columns.lat");
        DGCelPoints.Columns[5].Header = Rosetta.TextForId("charts.positions.datagrid.columns.latspeed");
        DGCelPoints.Columns[6].Header = Rosetta.TextForId("charts.positions.datagrid.columns.ra");
        DGCelPoints.Columns[7].Header = Rosetta.TextForId("charts.positions.datagrid.columns.raspeed");
        DGCelPoints.Columns[8].Header = Rosetta.TextForId("charts.positions.datagrid.columns.decl");
        DGCelPoints.Columns[9].Header = Rosetta.TextForId("charts.positions.datagrid.columns.declspeed");
        DGCelPoints.Columns[10].Header = Rosetta.TextForId("charts.positions.datagrid.columns.dist");
        DGCelPoints.Columns[11].Header = Rosetta.TextForId("charts.positions.datagrid.columns.distspeed");
        DGCelPoints.Columns[12].Header = Rosetta.TextForId("charts.positions.datagrid.columns.azimuth");
        DGCelPoints.Columns[13].Header = Rosetta.TextForId("charts.positions.datagrid.columns.altitude");

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

        return string.Concat(Rosetta.TextForId("ref.months." + monthId), inputDateText.AsSpan(firstEndIndex + 1, secondStartIndex - firstEndIndex - 1), Rosetta.TextForId("ref.calendar." + calendarId));
    }

    private string ParseTimeText(string inputTimeText)
    {
        int startIndex = inputTimeText.IndexOf("[");
        int endIndex = inputTimeText.LastIndexOf("]");
        string timeZoneId = inputTimeText.Substring(startIndex + 1, endIndex - startIndex - 1);
        return string.Concat(inputTimeText.AsSpan(0, startIndex - 1), Rosetta.TextForId(timeZoneId));
    }


    private void AspectsClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowAspects();
    }

    private void DeclinationsClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Declinations not yet implemented.");
    }

    private void HarmonicsClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Harmonics not yet implemented.");
    }

    private void MidpointsClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Midpoints not yet implemented.");
    }

    private void PrimaryClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Primary directions not yet implemented.");
    }

    private void SecundaryClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Secundary progressions not yet implemented.");
    }

    private void TransitsClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Transits not yet implemented.");
    }

    private void SolarreturnClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Solar return not yet implemented.");
    }

    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        HelpWindow? helpWindow = App.ServiceProvider.GetService<HelpWindow>();
        if (helpWindow != null)
        {
            helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            helpWindow.SetHelpPage("ChartsPositions");
            helpWindow.ShowDialog();
        }
    }


}
