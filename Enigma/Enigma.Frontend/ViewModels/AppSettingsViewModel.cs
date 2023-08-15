// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for application settings</summary>
public partial class AppSettingsViewModel: ObservableObject
{

    [ObservableProperty] private string _locationOfDataFiles;
    [ObservableProperty] private string _locationOfProjectFiles;
    [ObservableProperty] private string _locationOfDatabase;
    [ObservableProperty] private string _locationOfExportFiles;
    [ObservableProperty] private string _locationOfLogFiles;
    
    public AppSettingsViewModel()
    {
       AppSettingsModel model = App.ServiceProvider.GetRequiredService<AppSettingsModel>();
       _locationOfDataFiles = AppSettingsModel.LocationOfDataFiles();
       _locationOfProjectFiles = model.LocationOfProjectFiles();
       _locationOfDatabase = AppSettingsModel.LocationOfDatabase();
       _locationOfExportFiles = AppSettingsModel.LocationOfExportFiles();
       _locationOfLogFiles = AppSettingsModel.LocationOfLogFiles();
    }
    
    [RelayCommand] private static void Help()
    {
        DataVault.Instance.CurrentViewBase = "Settings";
        new HelpWindow().ShowDialog();
    }


}