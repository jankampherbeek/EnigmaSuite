// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messages;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>View model for cycles.</summary>
/// <remarks>Simple frontend for Cycles, does not use a Model.</remarks>
public partial class CyclesMainViewModel: ObservableObject, IRecipient<CancelMessage>, IRecipient<ContinueMessage>
{

    private readonly DataVaultGeneral _dataVaultGeneral = DataVaultGeneral.Instance;

    private CyclesSinglePositionsWindow? _cyclesSinglePositionsWindow;


    public CyclesMainViewModel()
    {
       WeakReferenceMessenger.Default.Register<CancelMessage>(this);
    }
    
    
    [RelayCommand]
    private static void Waves()
    {
        new CyclesDoolaardWindow().ShowDialog();
    }
    
    [RelayCommand]
    private void PlotPositions()
    {
        _cyclesSinglePositionsWindow = new();
        _cyclesSinglePositionsWindow.ShowDialog();
    }

    [RelayCommand]
    private static void PlotPairs()
    {
        // todo new CyclesCombinationsGraphWindow
    }
    
    
    
    [RelayCommand]
    private void Help()
    {
        // TODO write helptext for CyclesMain
        _dataVaultGeneral.CurrentViewBase = "CyclesMain";
        new HelpWindow().ShowDialog();
    }


    public void Receive(CancelMessage message)
    {
        _cyclesSinglePositionsWindow.Close();
    }

    public void Receive(ContinueMessage message)
    {
        throw new System.NotImplementedException();
    }
}