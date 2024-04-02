// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for Longitude Equivalents.</summary>
public partial class RadixLongitudeEquivalentsViewModel:ObservableObject
{
    
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.RADIX_LONGITUDE_EQUIVALENTS;
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;
    [ObservableProperty] private ObservableCollection<PresentableLongitudeEquivalent> _actualLongitudeEquivalents;
    [ObservableProperty] private string _chartId;
    [ObservableProperty] private string _description;
    
    public RadixLongitudeEquivalentsViewModel()
    {
        var model = App.ServiceProvider.GetRequiredService<RadixLongitudeEquivalentsModel>();
        _actualLongitudeEquivalents =  new ObservableCollection<PresentableLongitudeEquivalent>(model.GetPresentableLongitudeEquivalents());
        _chartId = model.GetChartIdName();
        _description = model.DescriptiveText();
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