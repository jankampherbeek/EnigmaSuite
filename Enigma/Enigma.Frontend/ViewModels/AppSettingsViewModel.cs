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
       _locationOfDataFiles = model.LocationOfDataFiles();
       _locationOfProjectFiles = model.LocationOfProjectFiles();
       _locationOfDatabase = model.LocationOfDatabase();
       _locationOfExportFiles = model.LocationOfExportFiles();
       _locationOfLogFiles = model.LocationOfLogFiles();
    }
    
    [RelayCommand] private static void Help()
    {
        ShowHelp();
    }
    
    private static void ShowHelp()
    {
        DataVault.Instance.CurrentViewBase = "Settings";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }

}