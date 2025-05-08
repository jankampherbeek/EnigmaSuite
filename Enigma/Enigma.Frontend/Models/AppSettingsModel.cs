// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Frontend.Ui.Models;

/// <summary>ViewModel for application settings</summary>
public sealed class AppSettingsModel
{
    private readonly ApplicationSettings _appSettings = ApplicationSettings.Instance;

    public static string LocationOfDataFiles()
    {
        return ApplicationSettings.LocationDataFiles;
    }

    public static string LocationOfProjectFiles()
    {
        return ApplicationSettings.LocationProjectFiles;
    }

    public static string LocationOfLogFiles()
    {
        return ApplicationSettings.LocationLogFiles;
    }
    
    
}