// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
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

public partial class PreNatalFactorSelectionViewModel: ObservableObject
{
    
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.PRENATAL_FACTOR_SELECTION;
    
    [ObservableProperty] private ObservableCollection<SelectableChartPointDetails> _allChartPointDetails;

    public PreNatalFactorSelectionViewModel()
    {
        var model = App.ServiceProvider.GetRequiredService<PreNatalFactorSelectionModel>();
        _allChartPointDetails =  new ObservableCollection<SelectableChartPointDetails>(model.GetAllCelPointDetails());
    }
  
    [RelayCommand]
    private void Continue()
    {
        List<ChartPoints> selectedPoints = (from item in AllChartPointDetails 
            where item.Selected select item.ChartPoint).ToList();
        DataVaultCharts.Instance.CurrentPointsSelection = selectedPoints;
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }


    
    [RelayCommand]
    private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
}