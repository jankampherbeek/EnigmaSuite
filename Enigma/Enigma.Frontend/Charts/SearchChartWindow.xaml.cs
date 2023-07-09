// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Frontend.Helpers.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace Enigma.Frontend.Ui.Charts;

/// <summary>
/// Interaction logic for SearchCriteria.xaml
/// </summary>
public partial class SearchChartWindow : Window
{
    private readonly SearchChartController _controller;



    public SearchChartWindow()
    {
        InitializeComponent();
        PopulateTexts();
        _controller = App.ServiceProvider.GetRequiredService<SearchChartController>();

    }


    public void BtnCloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    public void BtnHelpClick(object sender, RoutedEventArgs e)
    {
        SearchChartController.ShowHelp();
    }

    public void BtnSearchClick(object sender, RoutedEventArgs e)
    {
        _controller.PerformSearch(tboxSearchArgument.Text);
        PopulateData();
    }

    public void ChartSelectedClick(object sender, RoutedEventArgs e)
    {
        if (sender is DataGrid dataGrid)
        {
            PresentableChartData? rowView = (PresentableChartData)dataGrid.SelectedItem;
            if (rowView != null)
            {
                _controller.AddFoundChartToDataVault(rowView);
                Close();
            }
        }
    }



    private void PopulateTexts()
    {
        Title = Rosetta.TextForId("searchchartwindow.title");
        tbFormTitle.Text = Rosetta.TextForId("searchchartwindow.formtitle");
        tbExplanation.Text = Rosetta.TextForId("searchchartwindow.explanation");
        tbSearchResults.Text = Rosetta.TextForId("searchchartwindow.searchresults");
        btnClose.Content = Rosetta.TextForId("common.btnclose");
        btnHelp.Content = Rosetta.TextForId("common.btnhelp");
        btnSearch.Content = Rosetta.TextForId("searchchartwindow.btnsearch");
    }

    private void PopulateData()
    {
        dgSearchResults.ItemsSource = _controller.SearchedChartData();
        dgSearchResults.GridLinesVisibility = DataGridGridLinesVisibility.None;
        dgSearchResults.Columns[0].Header = Rosetta.TextForId("searchchartwindow.colid");
        dgSearchResults.Columns[1].Header = Rosetta.TextForId("searchchartwindow.colname");
        dgSearchResults.Columns[2].Header = Rosetta.TextForId("searchchartwindow.coldescription");
        dgSearchResults.Columns[0].CellStyle = FindResource("nameColumnStyle") as Style;
        dgSearchResults.Columns[1].CellStyle = FindResource("nameColumnStyle") as Style;
        dgSearchResults.Columns[2].CellStyle = FindResource("nameColumnStyle") as Style;
    }



}



