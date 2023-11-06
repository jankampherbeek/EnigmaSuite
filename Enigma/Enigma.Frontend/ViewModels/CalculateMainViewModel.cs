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

/// <summary>View model for calculations and tools.</summary>
public partial class CalculateMainViewModel: ObservableObject
{
    private readonly CalculateMainModel _model = App.ServiceProvider.GetRequiredService<CalculateMainModel>();
    private readonly DataVaultGeneral _dataVaultGeneral = DataVaultGeneral.Instance;


    [RelayCommand]
    private void CompareHouses()
    {
        // TODO open view for compare houses
    }

    [RelayCommand]
    private void Helical()
    {
        // TODO open view for heliacal
    }
    
    [RelayCommand]
    private void Help()
    {
        // TODO write helptext for CalculateMain
        _dataVaultGeneral.CurrentViewBase = "CalculateMain";
        new HelpWindow().ShowDialog();
    }
    
    
}