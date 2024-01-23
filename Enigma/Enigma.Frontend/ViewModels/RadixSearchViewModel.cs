// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Persistables;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for searchscreen for charts.</summary>
public partial class RadixSearchViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.RADIX_SEARCH;
    private readonly RadixSearchModel _model = App.ServiceProvider.GetRequiredService<RadixSearchModel>();
    
    [ObservableProperty] private string _searchArgument = "";
    [NotifyCanExecuteChangedFor(nameof(SelectCommand))]
    [ObservableProperty] private int _chartIndex = -1;
    [ObservableProperty] private ObservableCollection<PersistableChartIdentification>? _chartsFound;


    [RelayCommand]
    private void Search()
    {
        _model.PerformSearch(SearchArgument);
        if (_model.ChartsFound != null)
            ChartsFound = new ObservableCollection<PersistableChartIdentification>(_model.ChartsFound);
    }
    
    [RelayCommand(CanExecute = nameof(IsChartSelected))]
    private void Select()
    {
        _model.AddFoundChartToDataVault(ChartIndex);
        WeakReferenceMessenger.Default.Send(new FoundChartMessage(VM_IDENTIFICATION, _chartsFound[ChartIndex].Id));
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }

    private bool IsChartSelected()
    {
        return ChartIndex >= 0;
    }
    
    [RelayCommand]
    private static void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }
    
    
    [RelayCommand]
    private static void Help()
    {
        DataVaultGeneral.Instance.CurrentViewBase = "RadixSearch";
        new HelpWindow().ShowDialog();
    }
    

}