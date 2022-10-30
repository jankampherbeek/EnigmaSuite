// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Interfaces;
using System.Windows;

namespace Enigma.Frontend;

/// <summary>
/// Interaction logic for AboutWindow.xaml
/// </summary>
public partial class AboutWindow : Window
{
    private readonly IRosetta _rosetta;
    public AboutWindow(IRosetta rosetta)
    {
        InitializeComponent();
        _rosetta = rosetta;
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
