// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Persistables;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class RadixSearchViewModel: ObservableObject
{
    
    private readonly RadixSearchModel _model = App.ServiceProvider.GetRequiredService<RadixSearchModel>();
    
    [ObservableProperty] private string _searchArgument = "";
    [NotifyCanExecuteChangedFor(nameof(SelectCommand))]
    [ObservableProperty] private int _chartIndex = -1;
    [ObservableProperty] private ObservableCollection<PersistableChartData>? _chartsFound;


    [RelayCommand]
    private void Search()
    {
        _model.PerformSearch(SearchArgument);
        if (_model.ChartsFound != null)
            ChartsFound = new ObservableCollection<PersistableChartData>(_model.ChartsFound);
    }
    
    [RelayCommand(CanExecute = nameof(IsChartSelected))]
    private void Select()
    {
        _model.AddFoundChartToDataVault(ChartIndex);
    }

    private bool IsChartSelected()
    {
        return ChartIndex >= 0;
    }
    
    
    [RelayCommand]
    private static void Help()
    {
        DataVaultGeneral.Instance.CurrentViewBase = "RadixSearch";
        new HelpWindow().ShowDialog();
    }
    

}