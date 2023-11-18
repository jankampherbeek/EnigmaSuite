// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>View model for calculations and tools.</summary>
/// <remarks>Simple frontend for Calculations, does not use a Model.</remarks>
public partial class CalculateMainViewModel: ObservableObject
{
    
    private readonly DataVaultGeneral _dataVaultGeneral = DataVaultGeneral.Instance;
    
    [RelayCommand]
    private static void CompareHouses()
    {
        new CalcHouseComparisonWindow().ShowDialog();
    }

    [RelayCommand]
    private static void Heliacal()
    {
        new CalcHeliacalWindow().ShowDialog();
    }
    
    [RelayCommand]
    private void Help()
    {
        _dataVaultGeneral.CurrentViewBase = "CalculateMain";
        new HelpWindow().ShowDialog();
    }
    
    
}