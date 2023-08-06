// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Charts;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for midpoints in radix</summary>
public partial class RadixMidpointsViewModel: ObservableObject
{
    [ObservableProperty] private ObservableCollection<PresentableMidpoint> _actualMidpoints;
    [ObservableProperty] private ObservableCollection<PresentableOccupiedMidpoint> _actualOccupiedMidpoints;
    [ObservableProperty] private string _chartId;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _orbSize;
    private int _dialSize;
    private readonly RadixMidpointsModel _model;
    
    public RadixMidpointsViewModel()
    {
        _dialSize = 360;
        _model = App.ServiceProvider.GetRequiredService<RadixMidpointsModel>();
        _chartId = _model.RetrieveChartName();
        _description = _model.DescriptiveText();
        const double actualOrb = 1.6;                             // TODO 0.2.0 retrieve Orb from settings
        OrbSize = _model.DegreesToDms(actualOrb);
        Tuple<List<PresentableMidpoint>, List<PresentableOccupiedMidpoint>> midpoints = _model.RetrieveAndFormatMidpoints(_dialSize);
        ActualMidpoints = new ObservableCollection<PresentableMidpoint>(midpoints.Item1);
        ActualOccupiedMidpoints =  new ObservableCollection<PresentableOccupiedMidpoint>(midpoints.Item2);        

    }

    private void DefineMidpoints()
    {
        Tuple<List<PresentableMidpoint>, List<PresentableOccupiedMidpoint>> midpoints = _model.RetrieveAndFormatMidpoints(_dialSize);
        ActualMidpoints = new ObservableCollection<PresentableMidpoint>(midpoints.Item1);
        ActualOccupiedMidpoints =  new ObservableCollection<PresentableOccupiedMidpoint>(midpoints.Item2);
    }

    
    [RelayCommand]
    private void ChangeDial360()
    {
        _dialSize = 360;
        DefineMidpoints();
    }
    
    [RelayCommand]
    private void ChangeDial90()
    {
        _dialSize = 90;
        DefineMidpoints();
    }
    
    [RelayCommand]
    private void ChangeDial45()
    {
        _dialSize = 45;
        DefineMidpoints();
    }
    
    [RelayCommand]
    private static void Help()
    {
        ShowHelp();
    }

    private static void ShowHelp()
    {
        DataVault.Instance.CurrentViewBase = "RadixMidpoints";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }

    
}