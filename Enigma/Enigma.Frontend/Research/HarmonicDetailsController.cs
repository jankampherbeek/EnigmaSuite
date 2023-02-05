// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Enigma.Frontend.Ui.Research;

public class HarmonicDetailsController
{

    public static void ShowHelp()
    {
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("HarmonicDetails");
        helpWindow.ShowDialog();
    }
}