// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using Serilog;
using System;
using System.IO;
using System.Windows;


namespace Enigma.Frontend.Ui.Support;


/// <summary>Show help text.</summary>
public partial class HelpWindow : Window
{
    private readonly Rosetta _rosetta = Rosetta.Instance;
    public HelpWindow()
    {
        InitializeComponent();
        Title = _rosetta.TextForId("helpwindow.title");
        btnClose.Content = _rosetta.TextForId("common.btnclose");
    }

    /// <summary>Define html file to be shown as help text.</summary>
    /// <param name="helpFile">Name of file withou the .html extension. File should be in the folder E4CUi/res/help/</param>
    public void SetHelpPage(string helpFile)
    {
        string currentDir = AppDomain.CurrentDomain.BaseDirectory;
        string relativePath = currentDir + @"res\help\" + helpFile + ".html";
        if (File.Exists(relativePath))
        {
            HtmlFrame.Source = new Uri(relativePath);
        }
        else
        {
            Log.Error("Could not find helpfile {rp}. This results in a blank help screen but the program continues.", relativePath);
        }
    }

    private void ClickClose(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
