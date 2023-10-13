// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ProgEventResultsViewModel: ObservableObject
{
    [ObservableProperty] private string _methodName;
    [ObservableProperty] private string _details;
    [ObservableProperty] private string _eventDescription;
    [ObservableProperty] private string _eventDateTime;
    [ObservableProperty] private List<PresentableProgPosition> _presProgPositions;
    [ObservableProperty] private List<PresentableProgAspect> _presProgAspects;
    
    public ProgEventResultsViewModel()
    {
        var model = App.ServiceProvider.GetRequiredService<ProgEventResultsModel>();
        MethodName = model.MethodName;
        Details = model.Details;
        EventDescription = model.EventDescription;
        EventDateTime = model.EventDateTime;
        model.HandleTransits();
        PresProgPositions = model.PresProgPositions;
        PresProgAspects = model.PresProgAspects;
    }
    
    
    [RelayCommand]
    private static void Help()
    {
        DataVault.Instance.CurrentViewBase = "ProgEventResults";    // TODO create helppage ProgEventResults
        new HelpWindow().ShowDialog();
    }
    
}