// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Windows;


namespace Enigma.Frontend.Support;


/// <summary>Show help text.</summary>
public partial class HelpWindow : Window
{
    private IRosetta _rosetta;
    public HelpWindow(IRosetta rosetta)
    {
        InitializeComponent();
        _rosetta = rosetta;
        Title = _rosetta.TextForId("helpwindow.title");
        btnClose.Content = _rosetta.TextForId("common.btnclose");
    }

    /// <summary>Define html file to be shown as help text.</summary>
    /// <param name="helpFile">Name of file withou the .html extension. File should be in the folder E4CUi/res/help/</param>
    public void SetHelpPage(string helpFile)
    {
   string currentDir = AppDomain.CurrentDomain.BaseDirectory;
 /*       int endOfUsableDir = currentDir.IndexOf("bin");
        string usableDir = currentDir.Substring(0, endOfUsableDir);
        string fullPath = usableDir + "res\\help\\" + helpFile + ".html";  */
        string relativePath = currentDir + @"res\help\" + helpFile + ".html";
        //HtmlFrame.Source = new Uri(relativePath, UriKind.Relative);
        HtmlFrame.Source = new Uri(relativePath);
    }

    private void ClickClose(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
