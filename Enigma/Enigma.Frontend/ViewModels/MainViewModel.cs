// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for the startscreen</summary>
/// <remarks>There is no model for this ViewModel</remarks>
public partial class MainViewModel: ObservableObject
{

    private IWindowsFlow _generalWindowsFlow;

    public MainViewModel()
    {
        _generalWindowsFlow = App.ServiceProvider.GetRequiredService<IWindowsFlow>();
    }
    
    [RelayCommand]
    private static void ChartsModule()
    {
        new ChartsMainWindow().ShowDialog();
        Application.Current.Shutdown(0);
    }

    [RelayCommand]
    private static void ResearchModule()
    {
        WeakReferenceMessenger.Default.Send(new OpenMessage("MainView", "ResearchMain"));
    }
    
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
    
    
    [RelayCommand]
    private static void Help()
    {
        DataVaultGeneral.Instance.CurrentViewBase = "Main";
        new HelpWindow().ShowDialog();
    }
    
    
}