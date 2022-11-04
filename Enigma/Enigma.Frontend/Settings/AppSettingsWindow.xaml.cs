// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Ui.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Enigma.Frontend.Ui.Settings;

/// <summary>
/// Interaction logic for AppSettingsWindow.xaml
/// </summary>
public partial class AppSettingsWindow : Window
{
    private readonly AppSettingsController _controller;
    private readonly IRosetta _rosetta;


    public AppSettingsWindow()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<AppSettingsController>();
        _rosetta = App.ServiceProvider.GetRequiredService<IRosetta>();
        PopulateTexts();
    }
    private void PopulateTexts()
    {
        FormTitle.Text = _rosetta.TextForId("appsettingswindow.title");
        Explanation.Text = _rosetta.TextForId("appsettingswindow.explanation");
        LocData.Text = _rosetta.TextForId("appsettingswindow.locdata");
        LocExport.Text = _rosetta.TextForId("appsettingswindow.locexport");
        LocProject.Text = _rosetta.TextForId("appsettingswindow.locproject");
        LocSwissEph.Text = _rosetta.TextForId("appsettingswindow.locswisseph");
        BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
        BtnClose.Content = _rosetta.TextForId("common.btnclose");
        LocDataValue.Text = _controller.LocationOfDataFiles();
        LocExportValue.Text = _controller.LocationOfExportFiles();
        LocProjectValue.Text = _controller.LocationOfProjectFiles();
        LocSwissEphValue.Text = _controller.LocationOfSeFiles();
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
