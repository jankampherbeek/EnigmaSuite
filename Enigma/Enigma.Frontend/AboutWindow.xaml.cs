// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using System.Windows;

namespace Enigma.Frontend.Ui;

/// <summary>
/// Interaction logic for AboutWindow.xaml
/// </summary>
public partial class AboutWindow : Window
{
    private readonly Rosetta _rosetta = Rosetta.Instance;
    public AboutWindow()
    {
        InitializeComponent();
        PopulateTexts();
    }
    private void PopulateTexts()
    {
        Title.Text = _rosetta.TextForId("aboutwindow.title");
        Description.Text = _rosetta.TextForId("aboutwindow.description");
        CopyrightTitle.Text = _rosetta.TextForId("aboutwindow.copyright.title");
        CopyrightText.Text = _rosetta.TextForId("aboutwindow.copyright.text");
        MoreInfoTitle.Text = _rosetta.TextForId("aboutwindow.moreinfo.title");
        MoreInfoText.Text = _rosetta.TextForId("aboutwindow.moreinfo.text");
        BtnClose.Content = _rosetta.TextForId("common.btnclose");
    }


    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

}
