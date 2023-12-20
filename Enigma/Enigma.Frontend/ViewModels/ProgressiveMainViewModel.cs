// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ProgressiveMainViewModel: ObservableObject, IRecipient<EventCompletedMessage>
{
    private const string VM_IDENTIFICATION = "ProgressiveMain";  // todo read value from ChartsWindowsFlow
    private const string USER_MANUAL = "UserManual";
    private readonly List<Window> _openWindows = new();
    private readonly DataVaultProg _dataVaultProg = DataVaultProg.Instance;
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;
    private readonly DataVaultGeneral _dataVaultGeneral = DataVaultGeneral.Instance;
    private CalculatedChart? _currentChart;
    [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
 //   [NotifyCanExecuteChangedFor(nameof(PrimDirCommand))]
    [NotifyCanExecuteChangedFor(nameof(SecDirCommand))]
    [NotifyCanExecuteChangedFor(nameof(SymbDirCommand))]
    [NotifyCanExecuteChangedFor(nameof(TransitsCommand))]
 //   [NotifyCanExecuteChangedFor(nameof(SolarCommand))]
    [ObservableProperty] private int _eventIndex = -1;
 //   [ObservableProperty] private int _periodIndex = -1;
    [ObservableProperty] private string _currentEventName = "No event defined";
 //   [ObservableProperty] private string _currentPeriodName = "No period defined";
    [ObservableProperty] private string _currentChartName = string.Empty;
    [ObservableProperty] private ObservableCollection<PresentableProgresData> _presentableEventsPeriods;
    [NotifyPropertyChangedFor(nameof(EventIndex))]
    [ObservableProperty] private PresentableProgresData? _selectedProgDate;
    
    private readonly ProgressiveMainModel _model;
    
    public ProgressiveMainViewModel()
    {
        WeakReferenceMessenger.Default.Register<EventCompletedMessage>(this);
        _model = App.ServiceProvider.GetRequiredService<ProgressiveMainModel>();
        _presentableEventsPeriods = new ObservableCollection<PresentableProgresData>(_model.PresentableEventsPeriods);
        PopulateData();
    }
    
   
    private void PopulateData()
    {
        _currentChart = _dataVaultCharts.GetCurrentChart();
        if (_currentChart != null) CurrentChartName = _currentChart.InputtedChartData.MetaData.Name;
    }
    
    [RelayCommand]
    private void DatesItemChanged()
    {
        SelectedProgDate = PresentableEventsPeriods[EventIndex];
        _dataVaultProg.CurrentProgEvent = (ProgEvent?)_model.AvailableEventsPeriods[EventIndex];
        PopulateData();
    }
    
    

    
    [RelayCommand]
    private static void Configuration()
    {
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, GeneralWindowsFlow.CONFIGURATION));
    }
    
    [RelayCommand]
    private static void ConfigProg()
    {
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ChartsWindowsFlow.CONFIG_PROG));
    }
    
    [RelayCommand]
    private static void NewEvent()
    {
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ChartsWindowsFlow.PROG_EVENT));
    }

    [RelayCommand(CanExecute = nameof(IsProgDateSelected))]
    private static void Delete()
    {
        MessageBox.Show("Not implemented yet");
    }
    
    /*[RelayCommand]
    private void NewPeriod()
    {
        new ProgPeriodWindow().ShowDialog();
        if (_dataVaultProg.CurrentProgPeriod != null)
        {
            _model.SaveCurrentPeriod();
        }
        // show event        
        
    }*/
    
    /*
    [RelayCommand]
    private static void SearchPeriod()
    {
        MessageBox.Show("Not implemented yet");
    }
    */
    

    /*[RelayCommand(CanExecute = nameof(IsProgDateSelected))]
    private static void PrimDir()
    {
        MessageBox.Show("Not implemented yet");
    }*/
    
    [RelayCommand(CanExecute = nameof(IsProgDateSelected))]
    private void SecDir()
    {
        _dataVaultProg.CurrentProgresMethod = ProgresMethods.Secundary;
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ChartsWindowsFlow.PROG_EVENT_RESULTS));
    }
    
    [RelayCommand(CanExecute = nameof(IsProgDateSelected))]
    private void Transits()
    {
        _dataVaultProg.CurrentProgresMethod = ProgresMethods.Transits;
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ChartsWindowsFlow.PROG_EVENT_RESULTS));
    }
    
    [RelayCommand(CanExecute = nameof(IsProgDateSelected))]
    private void SymbDir()
    {
        _dataVaultProg.CurrentProgresMethod = ProgresMethods.Symbolic;
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ChartsWindowsFlow.PROG_EVENT_RESULTS));
    }

    
    /*[RelayCommand(CanExecute = nameof(IsProgDateSelected))]
    private static void Solar()
    {
        MessageBox.Show("Not implemented yet");
    }*/
    
    [RelayCommand]
    private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }


    [RelayCommand]
    private static void UserManual()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(USER_MANUAL));
    }
    
    [RelayCommand]
    private static void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseChildWindowsMessage(VM_IDENTIFICATION));
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }
    
    private bool IsProgDateSelected()
    {
        return EventIndex >= 0;
    }


    public void Receive(EventCompletedMessage message)
    {
        if (_dataVaultProg.CurrentProgEvent != null)
        {
            _model.SaveCurrentEvent();
        }
        // show event
    }
}