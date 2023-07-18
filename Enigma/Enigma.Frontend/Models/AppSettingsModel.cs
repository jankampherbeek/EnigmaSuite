// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;

namespace Enigma.Frontend.Ui.Models;

public sealed class AppSettingsModel
{
    private readonly ApplicationSettings _applicationSettings;
    
    public AppSettingsModel()
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

    public string LocationOfLogFiles()
    {
        return _applicationSettings.LocationLogFiles;
    }

    public string LocationOfDatabase()
    {
        return _applicationSettings.LocationDatabase;
    }


}