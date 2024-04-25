// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

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

/// <summary>ViewModel for parallels in radix</summary>
public partial class RadixParallelsViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.RADIX_PARALLELS;
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;
    [ObservableProperty] private ObservableCollection<NotifyingPresentableParallels> _actualParallels;
    [ObservableProperty] private string _chartId;
    [ObservableProperty] private string _description;
    
    public RadixParallelsViewModel()
    {
        var model = App.ServiceProvider.GetRequiredService<RadixParallelsModel>();
        _actualParallels =  new ObservableCollection<NotifyingPresentableParallels>(model.GetPresentableParallelsForChartPoints().
            Select((parallels => new NotifyingPresentableParallels(parallels))));
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