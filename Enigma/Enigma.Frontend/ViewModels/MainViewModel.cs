// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for the startscreen</summary>
/// <remarks>There is no model for this ViewModel</remarks>
public partial class MainViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = GeneralWindowsFlow.MAIN;
    private GeneralWindowsFlow _generalWindowsFlow;

    public MainViewModel()
    {
        _generalWindowsFlow = App.ServiceProvider.GetRequiredService<GeneralWindowsFlow>();
    }
    
    [RelayCommand]
    private static void ChartsModule()
    {
        /*
        new ChartsMainWindow().ShowDialog();
        Application.Current.Shutdown(0);
        */
        WeakReferenceMessenger.Default.Send(new OpenMessage("MainView", "ChartsMain"));
        
    }

    [RelayCommand]
    private static void ResearchModule()
    {
        WeakReferenceMessenger.Default.Send(new OpenMessage("MainView", "ResearchMain"));
    }
    /*
    [RelayCommand]
    private static void CyclesModule()
    {
        new CyclesMainWindow().ShowDialog();
    }
    
    [RelayCommand]
    private static void CalculateModule()
    {
        new CalculateMainWindow().ShowDialog();
    }
*/

    
    [RelayCommand]
    private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
    
    
}