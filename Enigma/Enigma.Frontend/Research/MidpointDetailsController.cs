// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.Research;

public class MidpointDetailsController
{

    public static void ShowHelp()
    {
        DataVault.Instance.CurrentViewBase = "MidpointDetails";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }

}