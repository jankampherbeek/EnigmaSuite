// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.IO;
using Enigma.Api.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.Models;

public class StartModel
{
    
    public static void HandleCheckDirForSettings()        // todo 0.2 move to backend
    {
        ApplicationSettings settings = ApplicationSettings.Instance;
        if (!Directory.Exists(ApplicationSettings.LocationEnigmaRoot)) Directory.CreateDirectory(ApplicationSettings.LocationEnigmaRoot);
        if (!Directory.Exists(ApplicationSettings.LocationExportFiles)) Directory.CreateDirectory(ApplicationSettings.LocationExportFiles);
        if (!Directory.Exists(settings.LocationProjectFiles)) Directory.CreateDirectory(settings.LocationProjectFiles);
        if (!Directory.Exists(ApplicationSettings.LocationDataFiles)) Directory.CreateDirectory(ApplicationSettings.LocationDataFiles);
        if (!Directory.Exists(ApplicationSettings.LocationDatabase)) Directory.CreateDirectory(ApplicationSettings.LocationDatabase);
        if (!Directory.Exists(ApplicationSettings.LocationLogFiles)) Directory.CreateDirectory(ApplicationSettings.LocationLogFiles);
    }

    public static void HandleCheckForConfig()
    {
        bool resultConfig = true;
        bool resultConfigProg = true;
        if (!File.Exists(EnigmaConstants.CONFIG_LOCATION))
        {
            IConfigurationApi configApi = App.ServiceProvider.GetRequiredService<IConfigurationApi>();
            AstroConfig config = configApi.GetDefaultConfiguration();
            resultConfig = configApi.WriteConfig(config);
        }
        if (!File.Exists(EnigmaConstants.CONFIG_PROG_LOCATION))
        {
            IConfigurationApi configApi = App.ServiceProvider.GetRequiredService<IConfigurationApi>();
            ConfigProg config = configApi.GetDefaultProgConfiguration();
            resultConfigProg = configApi.WriteConfig(config);
        }

        if (resultConfig && resultConfigProg) return;
        const string errorText = "Could not start Enigma Astrology Research. " +
                                 "There is a problem with one opr more of the configuration files. " +
                                 "Please check if you have write access to the disk";
        Log.Error("StartWindow.xaml.cs.HandleCheckForConfig(): {EText}", errorText);
        throw new EnigmaException(errorText);
    }

    public static void HandleCheckNewVersion()
    {
        ICommunicationApi communicationApi = App.ServiceProvider.GetRequiredService<ICommunicationApi>();
        ReleaseInfo releaseInfo = communicationApi.LatestAvaialableRelease();
        if (releaseInfo.Version == "")
        {
            Log.Error("Could not check for updates as creating an internet connection failed");
        }
        else
        {
            Log.Information("Info about latest release : {Info}", releaseInfo);
            if (releaseInfo.Version == EnigmaConstants.ENIGMA_VERSION) return;
            Log.Information("New release found, showing downloadpage");
            HelpWindow helpWindow = new(); 
            // TODO 0.2 find a solution for the line with releaseinfo
            // helpWindow.SetExternalPage("https://radixpro.com/rel/releaseinfo.html");
            helpWindow.ShowDialog();
        }
    }
    
}