// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
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
    [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
    [NotifyCanExecuteChangedFor(nameof(PrimDirCommand))]
    [NotifyCanExecuteChangedFor(nameof(SecDirCommand))]
    [NotifyCanExecuteChangedFor(nameof(SymbDirCommand))]
    [NotifyCanExecuteChangedFor(nameof(TransitsCommand))]
    [NotifyCanExecuteChangedFor(nameof(SolarCommand))]
    [ObservableProperty] private int _eventPeriodIndex = -1;
    [ObservableProperty] private int _periodIndex = -1;
    [ObservableProperty] private string _currentEventName = "No event defined";
    [ObservableProperty] private string _currentPeriodName = "No period defined";
    [ObservableProperty] private string _currentChartName = string.Empty;
    [ObservableProperty] private ObservableCollection<PresentableProgresData> _presentableEventsPeriods;
    [NotifyPropertyChangedFor(nameof(EventPeriodIndex))]
    [ObservableProperty] private PresentableProgresData? _selectedProgDate;
    
    private readonly ProgressiveMainModel _model;
    
    public ProgressiveMainViewModel()
    {
        _model = App.ServiceProvider.GetRequiredService<ProgressiveMainModel>();
        _presentableEventsPeriods = new ObservableCollection<PresentableProgresData>(_model.PresentableEventsPeriods);
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
    private void DatesItemChanged()
    {
        SelectedProgDate = PresentableEventsPeriods[EventPeriodIndex];
        _dataVault.CurrentProgEvent = (ProgEvent?)_model.AvailableEventsPeriods[EventPeriodIndex];
        PopulateData();
    }

    
    [RelayCommand]
    private static void Configuration()
    {
        ConfigurationWindow configWindow = new();
        configWindow.ShowDialog();
    }
    
    [RelayCommand]
    private static void ConfigProg()
    {
        new ConfigProgWindow().ShowDialog();
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
    
    [RelayCommand(CanExecute = nameof(IsProgDateSelected))]
    private static void Delete()
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
    

    [RelayCommand(CanExecute = nameof(IsProgDateSelected))]
    private static void PrimDir()
    {
        MessageBox.Show("Not implemented yet");
    }
    
    [RelayCommand(CanExecute = nameof(IsProgDateSelected))]
    private static void SecDir()
    {
        MessageBox.Show("Not implemented yet");
    }
    
    [RelayCommand(CanExecute = nameof(IsProgDateSelected))]
    private void Transits()
    {
        _dataVault.CurrentProgresMethod = ProgresMethods.Transits;
        new ProgEventResultsWindow().ShowDialog();
    }
    
    [RelayCommand(CanExecute = nameof(IsProgDateSelected))]
    private static void SymbDir()
    {
        MessageBox.Show("Not implemented yet");
    }
    
    [RelayCommand(CanExecute = nameof(IsProgDateSelected))]
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
    
    private bool IsProgDateSelected()
    {
        return EventPeriodIndex >= 0;
    }
    

}