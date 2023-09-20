// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ProgEventResultsViewModel: ObservableObject
{
    [ObservableProperty] private string _methodName;
    [ObservableProperty] private string _details;
    [ObservableProperty] private string _eventDescription;
    [ObservableProperty] private string _eventDateTime;
    [ObservableProperty] private List<PresentableProgPosition> _presProgPositions;
    public ProgEventResultsViewModel()
    {
        var model = App.ServiceProvider.GetRequiredService<ProgEventResultsModel>();
        MethodName = model.MethodName;
        Details = model.Details;
        EventDescription = model.EventDescription;
        EventDateTime = model.EventDateTime;
        PresProgPositions = model.presProgPositions;
        // define current event
        // read config
        // construct request
        // fire request,    check ChartCalculation in Support, change to CalcHelper and add progressive calculations.
        // set results in DataVault
        // define results as latest added
        // open resultswindow
    }
}