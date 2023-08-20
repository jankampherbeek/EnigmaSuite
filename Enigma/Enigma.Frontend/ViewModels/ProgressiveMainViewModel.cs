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
    private DataVault _dataVault = DataVault.Instance;
    private CalculatedChart? _currentChart;
    [ObservableProperty] private int _eventIndex = -1;
    [ObservableProperty] private int _periodIndex = -1;
    [ObservableProperty] private string _currentEventName = "No event defined";
    [ObservableProperty] private string _currentPeriodName = "No period defined";
    [ObservableProperty] private string _currentChartName = string.Empty;
    [ObservableProperty] private ObservableCollection<PresentableProgresData> _availableEvents;
    [ObservableProperty] private ObservableCollection<PresentableProgresData> _availablePeriods;
    [NotifyPropertyChangedFor(nameof(EventIndex))]
    [ObservableProperty] private PresentableProgresData? _selectedEvent;
    [NotifyPropertyChangedFor(nameof(PeriodIndex))]
    [ObservableProperty] private PresentableProgresData? _selectedPeriod;
    
    private readonly ProgressiveMainModel _model;
    
    
    public ProgressiveMainViewModel()
    {
        _model = App.ServiceProvider.GetRequiredService<ProgressiveMainModel>();
        _availableEvents = new ObservableCollection<PresentableProgresData>(_model.AvailableEvents);
        _availablePeriods = new ObservableCollection<PresentableProgresData>(_model.AvailablePeriods);
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
        SelectedEvent = AvailableEvents[EventIndex];
      //  _dataVault.SetCurrentEvent(EventIndex);
        PopulateData();
    }
    
    [RelayCommand]
    private void PeriodItemChanged()
    {
        SelectedPeriod = AvailablePeriods[PeriodIndex];
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
    private static void NewEvent()
    {
        new ProgEventWindow().ShowDialog();
        // todo check for cancel
        // save chart, new index is returned
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
    private static void NewPeriod()
    {
        MessageBox.Show("Not implemented yet");
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