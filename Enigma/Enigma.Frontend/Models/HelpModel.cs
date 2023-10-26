// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.IO;
using Enigma.Frontend.Ui.State;
using Serilog;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for help files</summary>
public class HelpModel
{
    public Uri? HtmlUri { get; } = HelpPageUri();

    /// <summary>Define html file to be shown as help text.</summary>
    private static Uri? HelpPageUri()
    {
        string currentViewBase = DataVaultGeneral.Instance.CurrentViewBase;  // Name of file without the .html extension. File should be in the folder res/help/

        string currentDir = AppDomain.CurrentDomain.BaseDirectory;
        string relativePath = currentDir + @"res\help\" + currentViewBase + ".html";    
        if (File.Exists(relativePath))
        {
            return new Uri(relativePath);
        }
        Log.Error("Could not find helpfile {RelPath}. This results in a blank help screen but the program continues", relativePath);
        return null;
    }

}