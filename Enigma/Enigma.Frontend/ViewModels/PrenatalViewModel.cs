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
    [ObservableProperty] private bool _useAspects;
    [ObservableProperty] private bool _useEclipses;
    [ObservableProperty] private bool _useIngresses;
    [ObservableProperty] private bool _useRetrogradeDirect;
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
        UseAspects = true;
        UseEclipses = true;
        UseIngresses = false;
        UseRetrogradeDirect = false;
        
      
    }

    public void Populate()
    {
        _preNatalModel.useAspects = UseAspects;
        _preNatalModel.useEclipses = UseEclipses;
        _preNatalModel.useIngresses = UseIngresses;
        _preNatalModel.useRetrogradeDirect = UseRetrogradeDirect;
        PreNatalMoments = _preNatalModel.GetPrenatalMoments();
        PreNatalEvents = _preNatalModel.GetPrenatalEvents();              
    }
    
    public void UpdateAspects(bool useIt)
    {
        _useAspects = useIt;
        Populate();
    }
    
    public void UpdateEclipses(bool useIt)
    {
        _useEclipses = useIt;
        Populate();
    }
    
    public void UpdateRetrogradeDirect(bool useIt)
    {
        _useRetrogradeDirect = useIt;
        Populate();
    }
    
    public void UpdateIngresses(bool useIt)
    {
        _useIngresses = useIt;
        Populate();
    }
    
}
