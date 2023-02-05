// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using System.Windows;

namespace Enigma.Frontend.Ui;

/// <summary>
/// Interaction logic for AboutWindow.xaml
/// </summary>
public partial class AboutWindow : Window
{
    public AboutWindow()
    {
        InitializeComponent();
        PopulateTexts();
    }
    private void PopulateTexts()
    {
        Title = Rosetta.TextForId("aboutwindow.title");
        FormTitle.Text = Rosetta.TextForId("aboutwindow.formtitle");
        Description.Text = Rosetta.TextForId("aboutwindow.description");
        CopyrightTitle.Text = Rosetta.TextForId("aboutwindow.copyright.title");
        CopyrightText.Text = Rosetta.TextForId("aboutwindow.copyright.text");
        MoreInfoTitle.Text = Rosetta.TextForId("aboutwindow.moreinfo.title");
        MoreInfoText.Text = Rosetta.TextForId("aboutwindow.moreinfo.text");
        BtnClose.Content = Rosetta.TextForId("common.btnclose");
    }


    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

}
