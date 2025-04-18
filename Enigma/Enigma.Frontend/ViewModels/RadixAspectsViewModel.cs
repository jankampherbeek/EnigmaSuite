// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
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

/// <summary>ViewModel for showing radix aspects</summary>
public partial class RadixAspectsViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.RADIX_ASPECTS;
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;
    [ObservableProperty] private ObservableCollection<NotifyingPresentableAspects> _actualAspects;
    [ObservableProperty] private string _chartId;
    [ObservableProperty] private string _description;
    
    public RadixAspectsViewModel()
    {
        var model = App.ServiceProvider.GetRequiredService<RadixAspectsModel>();
  
        _actualAspects = new ObservableCollection<NotifyingPresentableAspects>
            (model.GetPresentableAspectsForChartPoints()
            .Select(aspects => new NotifyingPresentableAspects(aspects)));

        _chartId = model.GetChartIdName();
        _description = model.DescriptiveText();
    }
    
    [RelayCommand]
    private static void Help()
    {
        Log.Information("RadixAspectsViewModel.Help(): send HelpMessage");
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand]
    private void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseNonDlgMessage(VM_IDENTIFICATION, _windowId ));
        Log.Information("RadixAspectsViewModel.Close(): send CloseNonDlgMessage");
    }

    
}