// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for Venus Star Point window</summary>
public partial class VspViewModel : ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.VSP;
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;
    private readonly VspModel _model;

    [ObservableProperty] private string _chartName;
    [ObservableProperty] private bool _showSignBackgroundColors = true;
    [ObservableProperty] private List<PresentableVspPosition> _vspPositions;
    [ObservableProperty] private string _explanationText;

    [RelayCommand]
    private void Help()
    {
        Log.Information("VspViewModel.Help(): send HelpMessage");
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }

    [RelayCommand]
    private void Close()
    {
        Log.Information("VspViewModel.Close(): send CloseMessage");
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }

    public VspViewModel()
    {
        _model = App.ServiceProvider.GetRequiredService<VspModel>();
        
        // Initialize data
        ExplanationText = "The current representation of the Venus Star Point (VSP) was established in close consultation with Arielle Guttman, who discovered and described this principle.\nPlease see the user manual for more information.";
        ChartName = _model.GetCurrentChartName();
        System.Diagnostics.Debug.WriteLine($"VspViewModel: ChartName = {ChartName}");
        VspPositions = _model.GetVspPositions();
        System.Diagnostics.Debug.WriteLine($"VspViewModel: VspPositions count = {VspPositions?.Count ?? 0}");
    }
    
   
}
