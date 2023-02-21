// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
namespace Enigma.Frontend.Ui.Configuration;

/// <summary>
/// Interaction logic for AppSettingsWindow.xaml
/// </summary>
public partial class AppSettingsWindow : Window
{
    private readonly AppSettingsController _controller;


    public AppSettingsWindow()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<AppSettingsController>();
        PopulateTexts();
    }
    private void PopulateTexts()
    {
        Title = Rosetta.TextForId("appsettingswindow.title");
        FormTitle.Text = Rosetta.TextForId("appsettingswindow.formtitle");
        Explanation.Text = Rosetta.TextForId("appsettingswindow.explanation");
        LocData.Text = Rosetta.TextForId("appsettingswindow.locdata");
        LocExport.Text = Rosetta.TextForId("appsettingswindow.locexport");
        LocProject.Text = Rosetta.TextForId("appsettingswindow.locproject");
        LocSwissEph.Text = Rosetta.TextForId("appsettingswindow.locswisseph");
        LocDatabase.Text = Rosetta.TextForId("appsettingswindow.locdatabase");
        BtnHelp.Content = Rosetta.TextForId("common.btnhelp");
        BtnClose.Content = Rosetta.TextForId("common.btnclose");
        LocDataValue.Text = _controller.LocationOfDataFiles();
        LocExportValue.Text = _controller.LocationOfExportFiles();
        LocProjectValue.Text = _controller.LocationOfProjectFiles();
        LocSwissEphValue.Text = _controller.LocationOfSeFiles();
        LocDatabaseValue.Text = _controller.LocationOfDatabase();
    }

    public void BtnCloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    public void BtnHelpClick(object sender, RoutedEventArgs e)
    {
        AppSettingsController.ShowHelp();
    }

}
