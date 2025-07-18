// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class SolarResultsViewModel : ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.SOLAR_RESULTS;
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;

    [ObservableProperty] private string _chartName;
    [ObservableProperty] private bool _showSignBackgroundColors = true;
    [ObservableProperty] private List<PresentableProgPosition> _solarPositions;
    [ObservableProperty] private List<PresentableProgAspect> _solarAspects;
    
    
    public SolarResultsViewModel()
    {
        var model = App.ServiceProvider.GetRequiredService<SolarResultsModel>();
        ChartName = model.GetChartName();
        SolarPositions = model.GetSolarPositions();
        SolarAspects = model.GetSolarAspects();
    }

    /// <summary>
    /// Initialize with calculated solar data
    /// </summary>
    /// <param name="model">The solar results model with calculated data</param>
    public void InitializeWithSolarData(SolarResultsModel model)
    {
        
        ChartName = model.GetChartName();
        SolarPositions = model.GetSolarPositions();
        SolarAspects = model.GetSolarAspects();
    }

    partial void OnShowSignBackgroundColorsChanged(bool value)
    {
        // This will be handled by the window code-behind through the controller
        // The property change will trigger the UI update
    }

    [RelayCommand]
    private void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseNonDlgMessage(VM_IDENTIFICATION, _windowId));
    }

    [RelayCommand]
    private static void Help()
    {
        Log.Information("SolarResultsViewModel.Help(): send HelpMessage");
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
} 