// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ProgEventResultsViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.PROG_EVENT_RESULTS;
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;
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
        var method = DataVaultProg.Instance.CurrentProgresMethod;
        switch (method)
        {
            case ProgresMethods.Transits:
                model.HandleTransits();
                break;
            case ProgresMethods.Secondary:
                model.HandleSecDir();
                break;
            case ProgresMethods.Symbolic:
                model.HandleSymDir();
                break;
        }

        PresProgPositions = model.PresProgPositions;
        PresProgAspects = model.PresProgAspects;
    }
    
    [RelayCommand]
    private static void Help()  // TODO create helppage ProgEventResults
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand]
    private void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseNonDlgMessage(VM_IDENTIFICATION, _windowId ));
    }

    
}