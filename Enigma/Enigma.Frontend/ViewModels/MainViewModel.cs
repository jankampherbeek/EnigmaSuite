// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Frontend.Ui.Charts;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for the startscreen</summary>
/// <remarks>There is no model for this ViewModel</remarks>
public partial class MainViewModel: ObservableObject
{

    [RelayCommand]
    private static void ChartsModule()
    {
        ChartsMainWindow chartsWindow = new();
        chartsWindow.ShowDialog();
    }

    [RelayCommand]
    private static void ResearchModule()
    {
        ResearchMainWindow researchWindow = new();
        researchWindow.ShowDialog();
    }
    
    
    [RelayCommand]
    private static void Help()
    {
        DataVault.Instance.CurrentViewBase = "Main";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }
    
    
}