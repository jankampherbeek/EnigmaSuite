// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
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

public partial class OobCalViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.OOB_CAL;
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;
    [ObservableProperty] private string _chartId;
    [ObservableProperty] private string _description;
    [ObservableProperty] private ObservableCollection<PresentableOobEvents> _oobEvents;
    public OobCalViewModel()
    {
        var model = App.ServiceProvider.GetRequiredService<OobCalModel>();
        _description = model.DescriptiveText();
        _oobEvents = new ObservableCollection<PresentableOobEvents>(model.GetOobEvents());
    }
    
    [RelayCommand]
    private void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseNonDlgMessage(VM_IDENTIFICATION, _windowId ));
    }
    
    [RelayCommand]
    private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }

}