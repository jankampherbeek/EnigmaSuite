// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class CyclesMainViewModel: ObservableObject
{
    private readonly CyclesMainModel _model = App.ServiceProvider.GetRequiredService<CyclesMainModel>();
    private readonly DataVaultGeneral _dataVaultGeneral = DataVaultGeneral.Instance;


    [RelayCommand]
    private void Waves()
    {
        new CyclesDoolaardWindow().ShowDialog();
    }
    
    [RelayCommand]
    private void PlotPositions()
    {
        new CyclesPositionsGraphWindow().ShowDialog();
    }
    
    
    [RelayCommand]
    private void Help()
    {
        // TODO write helptext for CyclesMain
        _dataVaultGeneral.CurrentViewBase = "CyclesMain";
        new HelpWindow().ShowDialog();
    }
    
    
    
}