// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace Enigma.Frontend.Ui.Research.DataFiles;

/// <summary>
/// Interaction logic for DataFilesOverview.xaml
/// </summary>
public partial class DataFilesOverviewWindow : Window
{
    private readonly DataFilesOverviewController _controller;

    public DataFilesOverviewWindow()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<DataFilesOverviewController>();
        PopulateTexts();
        PopulateData();
    }


    private void PopulateTexts()
    {
        FormTitle.Text = Rosetta.TextForId("datafilesoverviewwindow.title");
        btnClose.Content = Rosetta.TextForId("common.btnclose");
        btnHelp.Content = Rosetta.TextForId("common.btnhelp");
    }

    private void PopulateData()
    {
        dgDataNames.ItemsSource = _controller.GetDataNames();
        dgDataNames.GridLinesVisibility = DataGridGridLinesVisibility.None;
        dgDataNames.Columns[0].Header = Rosetta.TextForId("datafilesoverviewwindow.dataname");
        dgDataNames.Columns[0].CellStyle = FindResource("nameColumnStyle") as Style;
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        DataFilesOverviewController.ShowHelp();
    }

    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();

    }

}
