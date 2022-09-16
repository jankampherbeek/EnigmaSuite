// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Configuration.Domain;

namespace Enigma.Frontend.Settings;


public class AppSettingsController
{
    private ApplicationSettings _applicationSettings;

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

}