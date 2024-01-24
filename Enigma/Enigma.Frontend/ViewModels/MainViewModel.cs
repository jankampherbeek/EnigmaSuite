// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.IO;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Api;
using Enigma.Api.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Views;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for the startscreen</summary>
/// <remarks>There is no model for this ViewModel</remarks>
public partial class MainViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = GeneralWindowsFlow.MAIN;

    [ObservableProperty] private string _versionText = "Current version is up to date";
    private bool _isVersionOk = true;
    public SolidColorBrush VersionTextBrush => _isVersionOk ? Brushes.White : Brushes.Orange;
    public MainViewModel()
    {
        HandleCheckNewVersion();
        HandleCheckDirForSettings();
        HandleCheckRdbms();
    }
    
    
    // Startup checks 
    private static void HandleCheckDirForSettings()        // todo 0.2 move to backend
    {
        ApplicationSettings settings = ApplicationSettings.Instance;
        if (!Directory.Exists(ApplicationSettings.LocationEnigmaRoot)) Directory.CreateDirectory(ApplicationSettings.LocationEnigmaRoot);
        if (!Directory.Exists(ApplicationSettings.LocationExportFiles)) Directory.CreateDirectory(ApplicationSettings.LocationExportFiles);
        if (!Directory.Exists(settings.LocationProjectFiles)) Directory.CreateDirectory(settings.LocationProjectFiles);
        if (!Directory.Exists(ApplicationSettings.LocationDataFiles)) Directory.CreateDirectory(ApplicationSettings.LocationDataFiles);
        if (!Directory.Exists(ApplicationSettings.LocationDatabase)) Directory.CreateDirectory(ApplicationSettings.LocationDatabase);
        if (!Directory.Exists(ApplicationSettings.LocationLogFiles)) Directory.CreateDirectory(ApplicationSettings.LocationLogFiles);
    }

    private static bool HandleCheckRdbms()
    {
        IRdbmsPrepApi rdbmsPrepApi = App.ServiceProvider.GetRequiredService<IRdbmsPrepApi>();
        return rdbmsPrepApi.PrepareRdbms();
    }
    
    private void HandleCheckNewVersion()
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
            Log.Information("New release found");
            VersionText = "A new release is available.\nPlease check: https://radixpro.com/rel/releaseinfo.html";
            _isVersionOk = false;
        }
    }
    
    
    
    
    [RelayCommand]
    private static void ChartsModule()
    {
        WeakReferenceMessenger.Default.Send(new OpenMessage("MainView", "ChartsMain"));
        
    }

    [RelayCommand]
    private static void ResearchModule()
    {
        WeakReferenceMessenger.Default.Send(new OpenMessage("MainView", "ResearchMain"));
    }
    

    
    [RelayCommand]
    private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
    
    
}