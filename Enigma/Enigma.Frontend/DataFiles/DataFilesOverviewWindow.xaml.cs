// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Interfaces;
using Enigma.Frontend.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace Enigma.Frontend.DataFiles;

/// <summary>
/// Interaction logic for DataFilesOverview.xaml
/// </summary>
public partial class DataFilesOverviewWindow : Window
{
    private DataFilesOverviewController _controller;

    private IRosetta _rosetta;
    public DataFilesOverviewWindow()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<DataFilesOverviewController>();
        _rosetta = App.ServiceProvider.GetRequiredService<IRosetta>();
        PopulateTexts();
        PopulateData();
    }


    private void PopulateTexts()
    {
        FormTitle.Text = _rosetta.TextForId("datafilesoverviewwindow.title");
        btnClose.Content = _rosetta.TextForId("common.btnclose");
        btnHelp.Content = _rosetta.TextForId("common.btnhelp");
    }

    private void PopulateData()
    {
        dgDataNames.ItemsSource = _controller.GetDataNames();
        dgDataNames.GridLinesVisibility = DataGridGridLinesVisibility.None;
        dgDataNames.Columns[0].Header = _rosetta.TextForId("datafilesoverviewwindow.dataname");
        dgDataNames.Columns[0].CellStyle = FindResource("nameColumnStyle") as Style;
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        HelpWindow? helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("DataFilesOverview");
        helpWindow.ShowDialog();
    }

    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();

    }

}
