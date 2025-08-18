// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Globalization;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Models;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for Venus Star Point window</summary>
public partial class VspViewModel : ObservableObject, IRecipient<CloseMessage>
{
    private const string VM_IDENTIFICATION = "VspWindow";
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;
    private readonly VspModel _model;

    [ObservableProperty] private string _chartName;
    [ObservableProperty] private bool _showSignBackgroundColors = true;
    [ObservableProperty] private List<PresentableVspPosition> _vspPositions;
    [ObservableProperty] private bool _isPrenatal = false;

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
        
        // Register for messaging
        WeakReferenceMessenger.Default.Register<CloseMessage>(this);
        
        // Initialize data
        ChartName = _model.GetCurrentChartName();
        System.Diagnostics.Debug.WriteLine($"VspViewModel: ChartName = {ChartName}");
        VspPositions = _model.GetVspPositions(IsPrenatal);
        System.Diagnostics.Debug.WriteLine($"VspViewModel: VspPositions count = {VspPositions?.Count ?? 0}");
    }

    /// <summary>Update the prenatal setting and recalculate VSP positions</summary>
    /// <param name="isPrenatal">Whether to include prenatal positions</param>
    public void UpdatePrenatal(bool isPrenatal)
    {
        IsPrenatal = isPrenatal;
        VspPositions = _model.GetVspPositions(IsPrenatal);
    }

    public void Receive(CloseMessage message)
    {
        if (message.Value == VM_IDENTIFICATION)
        {
            WeakReferenceMessenger.Default.Unregister<CloseMessage>(this);
        }
    }
}
