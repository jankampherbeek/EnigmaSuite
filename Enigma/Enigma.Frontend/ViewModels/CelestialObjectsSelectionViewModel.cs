// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class CelestialObjectsSelectionViewModel: ObservableObject
{
    [ObservableProperty] private ObservableCollection<SelectableChartPointDetails> _allChartPointDetails;

    private readonly CelestialObjectsSelectionModel _model;
    
    public CelestialObjectsSelectionViewModel()
    {
        _model = App.ServiceProvider.GetRequiredService<CelestialObjectsSelectionModel>();
        _allChartPointDetails = new ObservableCollection<SelectableChartPointDetails>(_model.SelCpDetails);
    }


    [RelayCommand]
    private void Continue()
    {
        // check for errors
        // save data
        // send msg
    }
    
    
    [RelayCommand]
    private static void Help()
    {
        DataVaultGeneral.Instance.CurrentViewBase = "CelestialObjectsSelection";   // todo create help for CelestialObjectsSelection
        new HelpWindow().ShowDialog();
    }
    
}




