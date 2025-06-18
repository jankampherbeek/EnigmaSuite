// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.IO;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Api;
using Enigma.Core.Persistency;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Windows;
using Enigma.Api.Persistency;
using Microsoft.Win32;


namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for the startscreen</summary>
/// <remarks>There is no model for this ViewModel</remarks>
public partial class MainViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = GeneralWindowsFlow.MAIN;
    private GeneralWindowsFlow _generalWindowsFlow;

    [ObservableProperty] private string _versionText = "Current version is up to date";
    private bool _isVersionOk = true;
    public SolidColorBrush VersionTextBrush => _isVersionOk ? Brushes.White : Brushes.Orange;
    public MainViewModel()
    {
        _generalWindowsFlow = App.ServiceProvider.GetRequiredService<GeneralWindowsFlow>();
        HandleCheckNewVersion();
    //    HandleCheckDirForSettings();   // TODO, obsolete, remove
        HandleCheckRdbms();
        HandleCheckConfig();
        if (!HandleCheckSettings())
        {
            MessageBox.Show("You did not define a work folder. Please restart Enigma and try again.");
            Environment.Exit(1);
        }
        Rosetta.Instance.SetLanguage("en");
    }

    private void HandleCheckRdbms()
    {
        try
        {
            // Ensure the database directory exists
            Directory.CreateDirectory(ApplicationSettings.LocationDatabase);
            
            // Initialize the database
            IRdbmsPreparator rdbmsPreparator = App.ServiceProvider.GetRequiredService<IRdbmsPreparator>();
            if (!rdbmsPreparator.PreparaDatabase())
            {
                Log.Error("Failed to prepare database");
            }
        }
        catch (Exception ex)
        {
            Log.Error("Error initializing database: {Message}", ex.Message);
            // Continue with application startup even if database initialization fails
        }
    }

    private void HandleCheckConfig()
    {
        const string generalConfig = "enigmacfgdelta.json";
        const string progConfig = "enigmaprogcfgdelta.json";
        const string previousFolder = @"c:\enigma_ar";
        var sep = Path.DirectorySeparatorChar;
        var folder = ApplicationSettings.LocationEnigmaRoot;
        Log.Information($"Checking for config in {folder}");
        if (!File.Exists(folder + sep + generalConfig))
        {
            Log.Information($"Config not found: {folder + sep + generalConfig} ");
            if (File.Exists(previousFolder + sep + generalConfig))
            {
                Log.Information($"Previous config found in {previousFolder}");
                File.Copy(previousFolder + sep + generalConfig, folder + sep + generalConfig);
                Log.Information("Config copied");
            }
        }
        Log.Information($"Checking for prog config in {folder}");
        if (!File.Exists(folder + sep + progConfig))
        {
            Log.Information($"Prog config not found: {folder + sep + generalConfig} ");
            if (File.Exists(previousFolder + sep + progConfig))
            {
                Log.Information($"Previous prog config found in {previousFolder}");                
                File.Copy(previousFolder + sep + progConfig, folder + sep + progConfig);
                Log.Information("Prog config copied");                
            }
        }
        
        
    }
    
    
    private void HandleCheckNewVersion()
    {
        var communicationApi = App.ServiceProvider.GetRequiredService<ICommunicationApi>();
        var releaseInfo = communicationApi.LatestAvaialableRelease();
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
    
    private static void HandleCheckDirForSettings()     
    {
        if (!Directory.Exists(ApplicationSettings.LocationEnigmaRoot)) Directory.CreateDirectory(ApplicationSettings.LocationEnigmaRoot);
        if (!Directory.Exists(ApplicationSettings.LocationExportFiles)) Directory.CreateDirectory(ApplicationSettings.LocationExportFiles);
        if (!Directory.Exists(ApplicationSettings.LocationDatabase)) Directory.CreateDirectory(ApplicationSettings.LocationDatabase);
        if (!Directory.Exists(ApplicationSettings.LocationDocs)) Directory.CreateDirectory(ApplicationSettings.LocationDocs);
        if (!Directory.Exists(ApplicationSettings.LocationProjectFiles)) Directory.CreateDirectory(ApplicationSettings.LocationProjectFiles);
        if (!Directory.Exists(ApplicationSettings.LocationDataFiles)) Directory.CreateDirectory(ApplicationSettings.LocationDataFiles);
        if (!Directory.Exists(ApplicationSettings.LocationLogFiles)) Directory.CreateDirectory(ApplicationSettings.LocationLogFiles);
    }

    private static string GetWorkfolderPath()
    {
        MessageBox.Show("This is the first time that you start this version of Enigma. \n" +
                       "After closing this popup, you need to select a folder where you want to save results form working with Enigma.\n" +
                       "Please click OK and define a folder in the next screen.");
    
        // var dialog = new Microsoft.Win32.OpenFileDialog
        // {
        //     Title = "Select folder for Enigma work files",
        //     Filter = "Folders|*.none",
        //     CheckFileExists = false,
        //     CheckPathExists = true,
        //     FileName = "Select Folder",
        //     InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        // };
        //
        // if (dialog.ShowDialog() == true)
        // {
        //     return Path.GetDirectoryName(dialog.FileName) ?? string.Empty;
        // }
        var dialog = new OpenFolderDialog()
        {
            Title = "Select folder for Enigma work files",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };
        if (dialog.ShowDialog() == true)
        {
            return dialog.FolderName; // Should not be empty
        }
        return string.Empty;
    }
    
    
    // Checks if minimal settings are available.
    private static bool HandleCheckSettings()
    {
        try
        {
            var settingsApi = App.ServiceProvider.GetRequiredService<ISettingsApi>();
            var workfolderExists = settingsApi.SettingExists("workfolder");
            if (workfolderExists) return true;

            Log.Warning("Workfolder setting not found in database");
            var workfolderPath = GetWorkfolderPath();
            if (string.IsNullOrEmpty(workfolderPath))
            {
                MessageBox.Show("No workfolder selected. The application may not function correctly.", 
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (settingsApi.InsertSetting("workfolder", workfolderPath))
            {
                MessageBox.Show("Workfolder has been set successfully.", 
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            
            MessageBox.Show("Failed to save workfolder setting.", 
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
        catch (Exception ex)
        {
            Log.Error("Error checking settings: {Message}", ex.Message);
            MessageBox.Show($"Error checking settings: {ex.Message}", 
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
    }
    
    
    [RelayCommand]
    private static void ChartsModule()
    {
        Log.Information("MainViewModel.ChartsModule(): send OpenMessage for ChartsMain"); 
        WeakReferenceMessenger.Default.Send(new OpenMessage("MainView", "ChartsMain"));
        
    }

    [RelayCommand]
    private static void ResearchModule()
    {
        Log.Information("MainViewModel.ResearchModule(): send OpenMessage for ResearchMain");
        WeakReferenceMessenger.Default.Send(new OpenMessage("MainView", "ResearchMain"));
    }
    

    [RelayCommand]
    private static void CyclesModule()
    {
        Log.Information("MainViewModel.CyclesModule(): send OpenMessage for CyclesMain");
        WeakReferenceMessenger.Default.Send(new OpenMessage("MainView", "CyclesMain"));
    }
  
    [RelayCommand]
    private static void Help()
    {
        Log.Information("MainViewModel.Help(): send HelpMessage");
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
    
}