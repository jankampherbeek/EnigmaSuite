// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for point selection</summary>
public partial class ResearchPointSelectionViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ResearchWindowsFlow.RESEARCH_POINT_SELECTION;
    
    [ObservableProperty] private bool _methodSupportsCusps;
    [ObservableProperty] private bool _includeCusps;
    [ObservableProperty] private ObservableCollection<SelectableChartPointDetails> _allChartPointDetails;

    public ResearchPointSelectionViewModel()
    {
        var model = App.ServiceProvider.GetRequiredService<ResearchPointSelectionModel>();
        _allChartPointDetails =  new ObservableCollection<SelectableChartPointDetails>(model.GetAllCelPointDetails());
    }
  
    [RelayCommand]
    private void Continue()
    {
        List<ChartPoints> selectedPoints = (from item in AllChartPointDetails 
            where item.Selected select item.ChartPoint).ToList();
        ResearchPointSelection selection = new(selectedPoints, IncludeCusps);
        WeakReferenceMessenger.Default.Send(new ResearchPointSelectionMessage(selection));
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }

    [RelayCommand]
    private static void Cancel()
    {
        WeakReferenceMessenger.Default.Send(new CancelMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand]
    private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
}