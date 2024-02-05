// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
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
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for radix positions</summary>
public partial class RadixPositionsViewModel: ObservableObject
{
    
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.RADIX_POSITIONS;
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;
    [ObservableProperty] private ObservableCollection<PresentableHousePositions> _actualHousePositions;
    [ObservableProperty] private ObservableCollection<PresentableCommonPositions> _actualPointPositions;
    [ObservableProperty] private string _chartId;
    [ObservableProperty] private string _details;
    
    

    public RadixPositionsViewModel()
    {
        var model = App.ServiceProvider.GetRequiredService<RadixPositionsModel>();
        _chartId = model.GetIdName();
        _details = model.DescriptiveText();
        _actualHousePositions = new ObservableCollection<PresentableHousePositions>(model.GetHousePositionsCurrentChart());
        _actualPointPositions =
            new ObservableCollection<PresentableCommonPositions>(model.GetCelPointPositionsCurrentChart());
    }
    
    [RelayCommand]
    private void Close()
    {
        Log.Information("RadixPositionsViewModel.Close(): send CloseNonDlgMessage"); 
        WeakReferenceMessenger.Default.Send(new CloseNonDlgMessage(VM_IDENTIFICATION, _windowId ));
    }
    
    [RelayCommand]
    private static void Help()
    {
        Log.Information("RadixPositionsViewModel.Help(): send HelpMessage");        
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }


}