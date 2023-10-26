// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.References;
using Enigma.Domain.Research;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for point selection</summary>
public partial class ResearchPointSelectionViewModel: ObservableObject
{
    
    [ObservableProperty] private bool _methodSupportsCusps;
    [ObservableProperty] private bool _includeCusps;
    [ObservableProperty] private ObservableCollection<SelectableChartPointDetails> _allChartPointDetails;
    
    private readonly DataVaultResearch _dataVaultResearch = DataVaultResearch.Instance;

    public ResearchPointSelectionViewModel()
    {
        _dataVaultResearch.ResearchCanceled = true;
        var model = App.ServiceProvider.GetRequiredService<ResearchPointSelectionModel>();
        _allChartPointDetails =  new ObservableCollection<SelectableChartPointDetails>(model.GetAllCelPointDetails());
    }
  
    [RelayCommand]
    private void CompleteSelection()
    {
        _dataVaultResearch.ResearchCanceled = false;
        List<ChartPoints> selectedPoints = (from item in AllChartPointDetails 
            where item.Selected select item.ChartPoint).ToList();
        _dataVaultResearch.CurrentPointsSelection = new ResearchPointsSelection(selectedPoints, IncludeCusps);
    }

    [RelayCommand]
    private void Cancel()
    {
        _dataVaultResearch.ResearchCanceled = true;
    }
    
    [RelayCommand]
    private static void Help()
    {
        DataVaultGeneral.Instance.CurrentViewBase = "ResearchPointsSelection";
        new HelpWindow().ShowDialog();
    }
}