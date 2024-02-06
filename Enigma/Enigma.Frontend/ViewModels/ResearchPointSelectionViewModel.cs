// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
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
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for point selection. Collects user input about the celestial points to use and for the optional
/// inclusion of cusps.
/// Sends messages: CloseMessage, CancelMessage and HelpMessage.</summary>
public partial class ResearchPointSelectionViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ResearchWindowsFlow.RESEARCH_POINT_SELECTION;
    
  //  [ObservableProperty] private bool _methodSupportsCusps;
    private readonly bool _includeCusps = false;
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
        ResearchPointSelection selection = new(selectedPoints, _includeCusps);
        DataVaultResearch.Instance.CurrentPointsSelection = selection;
        Log.Information("ResearchPointSelectionViewModel.Continue(): send CloseMessage");         
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }

    [RelayCommand]
    private static void Cancel()
    {
        Log.Information("ResearchPointSelectionViewModel.Cancel(): send CancelMessage"); 
        WeakReferenceMessenger.Default.Send(new CancelMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand]
    private static void Help()
    {
        Log.Information("ResearchPointSelectionViewModel.Help(): send HelpMessage"); 
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
}