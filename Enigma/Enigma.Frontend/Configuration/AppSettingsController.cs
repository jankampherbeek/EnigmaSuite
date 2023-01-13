// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Configuration;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Enigma.Frontend.Ui.Configuration;


public class AppSettingsController
{
    private readonly ApplicationSettings _applicationSettings;

    public AppSettingsController()
    {
        _applicationSettings = ApplicationSettings.Instance;
    }


    public string LocationOfDataFiles()
    {
        return _applicationSettings.LocationDataFiles;
    }

    public string LocationOfProjectFiles()
    {
        return _applicationSettings.LocationProjectFiles;
    }

    public string LocationOfExportFiles()
    {
        return _applicationSettings.LocationExportFiles;
    }

    public string LocationOfSeFiles()
    {
        return _applicationSettings.LocationSeFiles;
    }

    public static void ShowHelp()
    {
        HelpWindow? helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("Settings");
        helpWindow.ShowDialog();
    }
}