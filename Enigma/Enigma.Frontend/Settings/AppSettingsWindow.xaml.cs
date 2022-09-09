// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Support;
using System.Windows;

namespace Enigma.Frontend.Settings;

/// <summary>
/// Interaction logic for AppSettingsWindow.xaml
/// </summary>
public partial class AppSettingsWindow : Window
{
    private AppSettingsController _controller;
    private IRosetta _rosetta;

    public AppSettingsWindow(AppSettingsController controller, IRosetta rosetta)
    {
        InitializeComponent();
        _controller = controller;
        _rosetta = rosetta;
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



}
