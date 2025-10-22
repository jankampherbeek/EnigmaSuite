// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for Prenatal window</summary>
public partial class PrenatalViewModel : ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.PRENATAL;

    private PreNatalModel _preNatalModel = App.ServiceProvider.GetRequiredService<PreNatalModel>();

    [ObservableProperty] private string _chartName = "Chart Name";
    [ObservableProperty] private List<PresentablePreNatalMoment> _preNatalMoments = new();
    [ObservableProperty] private List<PresentablePreNatalEvent> _preNatalEvents = new();
    
    [RelayCommand]
    private void Help()
    {
        Log.Information("PrenatalViewModel.Help(): send HelpMessage");
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }

    [RelayCommand]
    private void Close()
    {
        Log.Information("PrenatalViewModel.Close(): send CloseMessage");
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }

    public PrenatalViewModel()
    {
        PreNatalMoments = _preNatalModel.GetPrenatalMoments();
        PreNatalEvents = _preNatalModel.GetPrenatalEvents();            
    }
}
