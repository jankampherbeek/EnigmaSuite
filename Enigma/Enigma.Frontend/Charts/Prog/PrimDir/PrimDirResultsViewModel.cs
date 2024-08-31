// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.Charts.Prog.PrimDir;

public partial class PrimDirResultsViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.PRIM_DIR_RESULTS;   
    private readonly PrimDirResultsModel _model = App.ServiceProvider.GetRequiredService<PrimDirResultsModel>();
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;
    [ObservableProperty] private string _methodName;
    [ObservableProperty] private string _details;
    [ObservableProperty] private string _period;
    [ObservableProperty] private List<PresentablePrimDirs> _primDirs;
    
    public PrimDirResultsViewModel()
    {

        MethodName = _model.MethodName;
        Details = _model.Details;
       
        CalculatePrimDirs();
        Period = _model.Period;
        PrimDirs = _model.ActualPrimDirs;
    }

    private void CalculatePrimDirs()
    {
        _model.CalcActualPrimDirs();
        
    }

    [RelayCommand]
    private static void Help() 
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand]
    private void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseNonDlgMessage(VM_IDENTIFICATION, _windowId ));
    }
    
    
    
}