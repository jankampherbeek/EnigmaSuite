// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

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
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ProgressiveMainViewModel: ObservableObject, IRecipient<EventCompletedMessage>
{
    private const string VM_IDENTIFICATION = "ProgressiveMain";  // todo read value from ChartsWindowsFlow
    private readonly DataVaultProg _dataVaultProg = DataVaultProg.Instance;
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;
    private CalculatedChart? _currentChart;
    [NotifyCanExecuteChangedFor(nameof(DeleteEventCommand))]
    [NotifyCanExecuteChangedFor(nameof(SecDirCommand))]
    [NotifyCanExecuteChangedFor(nameof(SymbDirCommand))]
    [NotifyCanExecuteChangedFor(nameof(TransitsCommand))]
    [ObservableProperty] private int _eventIndex = -1;
    [ObservableProperty] private string _currentEventName = "No event defined";
    [ObservableProperty] private string _currentChartName = string.Empty;
    [ObservableProperty] private ObservableCollection<PresentableProgresData> _presentableEventsPeriods = new();
    [NotifyPropertyChangedFor(nameof(EventIndex))]
    [ObservableProperty] private PresentableProgresData? _selectedProgDate;
    
    private readonly ProgressiveMainModel _model;
    
    public ProgressiveMainViewModel()
    {
        WeakReferenceMessenger.Default.Register<EventCompletedMessage>(this);
        _model = App.ServiceProvider.GetRequiredService<ProgressiveMainModel>();
        PopulateData();
        PopulateEvents();
    }
    
   
    private void PopulateData()
    {
        _currentChart = _dataVaultCharts.GetCurrentChart();
        if (_currentChart != null) CurrentChartName = _currentChart.InputtedChartData.MetaData.Name;
    }

    private void PopulateEvents()
    {
        PresentableEventsPeriods = new ObservableCollection<PresentableProgresData>(_model.GetPresentableEventsPeriods());
    }
    
    [RelayCommand]
    private void DatesItemChanged()
    {
        if (EventIndex >= 0)    // Event not deleted
        {
            SelectedProgDate = PresentableEventsPeriods[EventIndex];
            _dataVaultProg.CurrentProgEvent = (ProgEvent?)_model.AvailableEventsPeriods[EventIndex];
        }

        PopulateData();
        PopulateEvents();
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
    private void DeleteEvent()
    {
        var currentEvent = _dataVaultProg.CurrentProgEvent;
        string descr = currentEvent != null ? currentEvent.Description : "";
        if (MessageBox.Show("Do you want to delete the event:  "+ descr + " from the database?",
                "Delete event",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            MessageBox.Show(
                _model.DeleteCurrentEvent(currentEvent.Id)
                    ? "The event: "+ descr + " was succesfully deleted."
                    : "The event: "+ descr + " was not found and could not be deleted.",
                "Result of delete"); 
        PopulateEvents();
    }
    
  [RelayCommand(CanExecute = nameof(IsProgDateSelected))]
    private void SecDir()
    {
        _dataVaultProg.CurrentProgresMethod = ProgresMethods.Secondary;
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

   
    [RelayCommand]
    private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }


    [RelayCommand]
    private static void UserManual()
    {
        UserManual userManual = new();
        userManual.ShowUserManual();
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
        PopulateEvents();
    }
 
    
}