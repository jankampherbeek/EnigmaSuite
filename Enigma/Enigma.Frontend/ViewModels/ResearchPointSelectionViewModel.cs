using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Configuration;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;
using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ResearchPointSelectionViewModel: ObservableObject
{
    
    [ObservableProperty] private bool _methodSupportsCusps;
    [ObservableProperty] private bool _includeCusps;
    [ObservableProperty] private ObservableCollection<SelectableChartPointDetails> _allChartPointDetails;
    
    private readonly DataVault _dataVault = DataVault.Instance;
    private ResearchPointSelectionModel _model;
    
    public ResearchPointSelectionViewModel()
    {
        _model = App.ServiceProvider.GetRequiredService<ResearchPointSelectionModel>();
        _allChartPointDetails =  new ObservableCollection<SelectableChartPointDetails>(_model.GetAllCelPointDetails());
    }

  
    [RelayCommand]
    private void CompleteSelection()
    {
        List<ChartPoints> selectedPoints = (from item in AllChartPointDetails 
            where item.Selected select item.ChartPoint).ToList();
        _dataVault.CurrentPointsSelection = new ResearchPointsSelection(selectedPoints, IncludeCusps);
    }
    
    private bool IsSelectionComplete()
    {
        int minNr = _model.minimalNrOfPoints;
        int selectedNr = AllChartPointDetails.Count(item => item.Selected);
        return minNr > 0 && selectedNr >= minNr;
    }
    
    
    [RelayCommand]
    private static void Help()
    {
        DataVault.Instance.CurrentViewBase = "ResearchPointsSelection";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }
}