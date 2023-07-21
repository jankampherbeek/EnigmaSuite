// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.IO;
using Enigma.Api.Interfaces;
using Enigma.Domain.Communication;
using Enigma.Domain.Configuration;
using Enigma.Domain.Constants;
using Enigma.Domain.Exceptions;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.Models;

public class StartModel
{
    
    public void HandleCheckDirForSettings()        // todo 0.2 move to backend
    {
        ApplicationSettings settings = ApplicationSettings.Instance;
        if (!Directory.Exists(settings.LocationEnigmaRoot)) Directory.CreateDirectory(settings.LocationEnigmaRoot);
        if (!Directory.Exists(settings.LocationExportFiles)) Directory.CreateDirectory(settings.LocationExportFiles);
        if (!Directory.Exists(settings.LocationProjectFiles)) Directory.CreateDirectory(settings.LocationProjectFiles);
        if (!Directory.Exists(settings.LocationDataFiles)) Directory.CreateDirectory(settings.LocationDataFiles);
        if (!Directory.Exists(settings.LocationDatabase)) Directory.CreateDirectory(settings.LocationDatabase);
        if (!Directory.Exists(settings.LocationLogFiles)) Directory.CreateDirectory(settings.LocationLogFiles);
    }

    public void HandleCheckForConfig()
    {
        bool result = true;
        if (!File.Exists(EnigmaConstants.CONFIG_LOCATION))
        {
            IConfigurationApi configApi = App.ServiceProvider.GetRequiredService<IConfigurationApi>();
            AstroConfig config = configApi.GetDefaultConfiguration();
            result = configApi.WriteConfig(config);
        }

        if (result) return;
        const string errorText = "Could not start Enigma Astrology Research. There is a problem with the configuration file. Please check if you have write access to the disk";
        Log.Error("StartWindow.xaml.cs.HandleCheckForConfig(): " + errorText);
        throw new EnigmaException(errorText);
    }

    public void HandleCheckNewVersion()
    {
        ICommunicationApi communicationApi = App.ServiceProvider.GetRequiredService<ICommunicationApi>();
        ReleaseInfo releaseInfo = communicationApi.LatestAvaialableRelease();
        if (releaseInfo.Version == "")
        {
            Log.Error("Could not check for updates as creating an internet connection failed");
        }
        else
        {
            Log.Information("Info about latest release : " + releaseInfo);
            if (releaseInfo.Version != EnigmaConstants.ENIGMA_VERSION)
            {
                Log.Information("New release found, showing downloadpage");
                HelpWindow helpWindow = new(); 
                // TODO 0.2 find a solution for the line with releaseinfo
                // helpWindow.SetExternalPage("https://radixpro.com/rel/releaseinfo.html");
                helpWindow.ShowDialog();
            }
        }
    }
    
}