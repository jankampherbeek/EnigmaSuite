// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Charts;
using Enigma.Domain.Progressive;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ProgressiveMainViewModel: ObservableObject
{
    private readonly List<Window> _openWindows = new();
    private readonly DataVault _dataVault = DataVault.Instance;
    private CalculatedChart? _currentChart;
    [ObservableProperty] private int _eventIndex = -1;
    [ObservableProperty] private int _periodIndex = -1;
    [ObservableProperty] private string _currentEventName = "No event defined";
    [ObservableProperty] private string _currentPeriodName = "No period defined";
    [ObservableProperty] private string _currentChartName = string.Empty;
    [ObservableProperty] private ObservableCollection<PresentableProgresData> _presentableEvents;
    [ObservableProperty] private ObservableCollection<PresentableProgresData> _presentablePeriods;
    [NotifyPropertyChangedFor(nameof(EventIndex))]
    [ObservableProperty] private PresentableProgresData? _selectedEvent;
    [NotifyPropertyChangedFor(nameof(PeriodIndex))]
    [ObservableProperty] private PresentableProgresData? _selectedPeriod;
    
    private readonly ProgressiveMainModel _model;
    
    public ProgressiveMainViewModel()
    {
        _model = App.ServiceProvider.GetRequiredService<ProgressiveMainModel>();
        _presentableEvents = new ObservableCollection<PresentableProgresData>(_model.PresentableEvents);
        _presentablePeriods = new ObservableCollection<PresentableProgresData>(_model.PresentablePeriods);
        PopulateData();
    }

    private void OpenWindow(Window window)
    {
        if (!_openWindows.Contains(window))
        {
            _openWindows.Add(window);
        }
        window.Show();
    }
    
    private void PopulateData()
    {
        _currentChart = _dataVault.GetCurrentChart();
        if (_currentChart != null) CurrentChartName = _currentChart.InputtedChartData.MetaData.Name;
    }
    
    [RelayCommand]
    private void EventItemChanged()
    {
        SelectedEvent = PresentableEvents[EventIndex];
      //  _dataVault.SetCurrentEvent(EventIndex);
        PopulateData();
    }
    
    [RelayCommand]
    private void PeriodItemChanged()
    {
        SelectedPeriod = PresentablePeriods[PeriodIndex];
        //  _dataVault.SetCurrentPeriod(PeriodIndex);
        PopulateData();
    }
    
    [RelayCommand]
    private static void Configuration()
    {
        ConfigurationWindow configWindow = new();
        configWindow.ShowDialog();
    }
    
    [RelayCommand]
    private void NewEvent()
    {
        new ProgEventWindow().ShowDialog();
        if (_dataVault.CurrentProgEvent != null)
        {
            _model.SaveCurrentEvent();
        }
        // show event
    }
    
    [RelayCommand]
    private static void SearchEvent()
    {
        MessageBox.Show("Not implemented yet");
    }
    
    [RelayCommand]
    private static void DeleteEvent()
    {
        MessageBox.Show("Not implemented yet");
    }
    
    [RelayCommand]
    private void NewPeriod()
    {
        new ProgPeriodWindow().ShowDialog();
        if (_dataVault.CurrentProgPeriod != null)
        {
            _model.SaveCurrentPeriod();
        }
        // show event        
        
    }
    
    [RelayCommand]
    private static void SearchPeriod()
    {
        MessageBox.Show("Not implemented yet");
    }
    
    [RelayCommand]
    private static void DeletePeriod()
    {
        MessageBox.Show("Not implemented yet");
    }
    
    [RelayCommand]
    private static void PrimDir()
    {
        MessageBox.Show("Not implemented yet");
    }
    
    [RelayCommand]
    private static void SecDir()
    {
        MessageBox.Show("Not implemented yet");
    }
    
    [RelayCommand]
    private static void Transits()
    {
        MessageBox.Show("Not implemented yet");
    }
    
    [RelayCommand]
    private static void SymbDir()
    {
        MessageBox.Show("Not implemented yet");
    }
    
    [RelayCommand]
    private static void Solar()
    {
        MessageBox.Show("Not implemented yet");
    }
    
    [RelayCommand]
    private void Help()
    {
        _dataVault.CurrentViewBase = "ProgressiveMain";    // TODO create helppage progressive main
        new HelpWindow().ShowDialog();
    }


    [RelayCommand]
    private static void UserManual()
    {
        DataVault.Instance.CurrentViewBase = "UserManual";
        new HelpWindow().ShowDialog();
    }
    
    /// <summary>Closes all child windows of main progressive window. Clears all events and periods in DataVault.</summary>
    [RelayCommand]
    private void HandleClose()
    {
        //_dataVault.ClearExistingEvents();
        foreach (Window window in _openWindows)
        {
            window.Close();
        }
    }
}