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
public partial class SearchChartWindow
{
    private readonly SearchChartController _controller;



    public SearchChartWindow()
    {
        InitializeComponent();
        PopulateTexts();
        _controller = App.ServiceProvider.GetRequiredService<SearchChartController>();

    }


    private void BtnCloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void BtnHelpClick(object sender, RoutedEventArgs e)
    {
        SearchChartController.ShowHelp();
    }

    private void BtnSearchClick(object sender, RoutedEventArgs e)
    {
        _controller.PerformSearch(TboxSearchArgument.Text);
        PopulateData();
    }

    private void ChartSelectedClick(object sender, RoutedEventArgs e)
    {
        if (sender is not DataGrid dataGrid) return;
        PresentableChartData? rowView = (PresentableChartData)dataGrid.SelectedItem;
        if (rowView == null) return;
        _controller.AddFoundChartToDataVault(rowView);
        Close();
    }



    private void PopulateTexts()
    {
        Title = Rosetta.TextForId("searchchartwindow.title");
        TbFormTitle.Text = Rosetta.TextForId("searchchartwindow.formtitle");
        TbExplanation.Text = Rosetta.TextForId("searchchartwindow.explanation");
        TbSearchResults.Text = Rosetta.TextForId("searchchartwindow.searchresults");
        BtnClose.Content = Rosetta.TextForId("common.btnclose");
        BtnHelp.Content = Rosetta.TextForId("common.btnhelp");
        BtnSearch.Content = Rosetta.TextForId("searchchartwindow.btnsearch");
    }

    private void PopulateData()
    {
        DgSearchResults.ItemsSource = _controller.SearchedChartData();
        DgSearchResults.GridLinesVisibility = DataGridGridLinesVisibility.None;
        DgSearchResults.Columns[0].Header = Rosetta.TextForId("searchchartwindow.colid");
        DgSearchResults.Columns[1].Header = Rosetta.TextForId("searchchartwindow.colname");
        DgSearchResults.Columns[2].Header = Rosetta.TextForId("searchchartwindow.coldescription");
        DgSearchResults.Columns[0].CellStyle = FindResource("NameColumnStyle") as Style;
        DgSearchResults.Columns[1].CellStyle = FindResource("NameColumnStyle") as Style;
        DgSearchResults.Columns[2].CellStyle = FindResource("NameColumnStyle") as Style;
    }



}



