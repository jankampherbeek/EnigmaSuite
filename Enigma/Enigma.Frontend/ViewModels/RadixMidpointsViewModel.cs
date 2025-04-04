// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for midpoints in radix</summary>
public partial class RadixMidpointsViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.RADIX_MIDPOINTS;
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;
    [ObservableProperty] private ObservableCollection<PresentableMidpoint> _actualMidpoints;
    [ObservableProperty] private ObservableCollection<NotifyingPresentableMidpoints> _actualOccupiedMidpoints;
    [ObservableProperty] private string _description;
    private int _dialSize;
    private readonly RadixMidpointsModel _model;
    
    public RadixMidpointsViewModel()
    {
        _dialSize = 360;
        _model = App.ServiceProvider.GetRequiredService<RadixMidpointsModel>();
        _description = _model.DescriptiveText();
        Tuple<List<PresentableMidpoint>, List<PresentableOccupiedMidpoint>> midpoints = _model.RetrieveAndFormatMidpoints(_dialSize);
        ActualMidpoints = new ObservableCollection<PresentableMidpoint>(midpoints.Item1);
        ActualOccupiedMidpoints =  new ObservableCollection<NotifyingPresentableMidpoints>(midpoints.Item2.Select(midpoints => new NotifyingPresentableMidpoints(midpoints)));        
    }

    private void DefineMidpoints()
    {
        Tuple<List<PresentableMidpoint>, List<PresentableOccupiedMidpoint>> midpoints = _model.RetrieveAndFormatMidpoints(_dialSize);
        ActualMidpoints = new ObservableCollection<PresentableMidpoint>(midpoints.Item1);
        ActualOccupiedMidpoints =  new ObservableCollection<NotifyingPresentableMidpoints>(midpoints.Item2.Select(midpoints => new NotifyingPresentableMidpoints(midpoints)));   
    }

    
    [RelayCommand]
    private void ChangeDial360()
    {
        _dialSize = 360;
        DefineMidpoints();
    }
    
    [RelayCommand]
    private void ChangeDial90()
    {
        _dialSize = 90;
        DefineMidpoints();
    }
    
    [RelayCommand]
    private void ChangeDial45()
    {
        _dialSize = 45;
        DefineMidpoints();
    }
    
    [RelayCommand]
    private static void Help()
    {
        Log.Information("RadixMidpointsViewModel.Help(): send HelpMessage"); 
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand]
    private void Close()
    {
        Log.Information("RadixMidpointsViewModel.Close(): send CloseNonDlgMessage"); 
        WeakReferenceMessenger.Default.Send(new CloseNonDlgMessage(VM_IDENTIFICATION, _windowId ));
    }

    
}