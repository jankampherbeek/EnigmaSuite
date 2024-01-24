// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for application settings</summary>
public partial class AppSettingsViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = GeneralWindowsFlow.APP_SETTINGS;
    [ObservableProperty] private string _locationOfDataFiles;
    [ObservableProperty] private string _locationOfProjectFiles;
    [ObservableProperty] private string _locationOfDatabase;
    [ObservableProperty] private string _locationOfExportFiles;
    [ObservableProperty] private string _locationOfLogFiles;
    [ObservableProperty] private string _locationOfDocs;
    
    public AppSettingsViewModel()
    {
       AppSettingsModel model = App.ServiceProvider.GetRequiredService<AppSettingsModel>();
       _locationOfDataFiles = AppSettingsModel.LocationOfDataFiles();
       _locationOfProjectFiles = model.LocationOfProjectFiles();
       _locationOfDatabase = AppSettingsModel.LocationOfDatabase();
       _locationOfExportFiles = AppSettingsModel.LocationOfExportFiles();
       _locationOfLogFiles = AppSettingsModel.LocationOfLogFiles();
       _locationOfDocs = AppSettingsModel.LocationOfDocs();
    }

    [RelayCommand] private static void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand] private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }


}