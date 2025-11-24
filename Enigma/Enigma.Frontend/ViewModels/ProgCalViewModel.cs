// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Core.Slices.ProgCalendar;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for Progressive Calendar window</summary>
public partial class ProgCalViewModel : ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.PROG_CAL;

    private ProgCalModel _model = App.ServiceProvider.GetRequiredService<ProgCalModel>();
    
    [ObservableProperty] private string _chartName = "Chart Name";
    [ObservableProperty] private List<PresentableProgCalItem> _progCalItems;
    [ObservableProperty] private List<PresentableProgCalPeriod> _progCalPeriods;
    [ObservableProperty] private bool _useAspects;
    [ObservableProperty] private bool _useParallels;
    [ObservableProperty] private bool _useRetroDirect;
    
    
    [RelayCommand]
    private void Help()
    {
        Log.Information("ProgCalViewModel.Help(): send HelpMessage");
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }

    [RelayCommand]
    private void Close()
    {
        Log.Information("ProgCalViewModel.Close(): send CloseMessage");
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }

    public ProgCalViewModel()
    {
        _model.DefineProgCal();
        Populate();
        
    }

    private void Populate()
    {
        ProgCalItems = _model.allItems;
        ProgCalPeriods = _model.allPeriods;
        
    }
    
    public void UpdateAspects(bool useIt)
    {
        UseAspects = useIt;
        Populate();
    }
    
    public void UpdateParallels(bool useIt)
    {
        UseParallels = useIt;
        Populate();
    }
    
    
    public void UpdateRetroDirect(bool useIt)
    {
        UseRetroDirect = useIt;
        Populate();
    }
    
}
