// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messages;
using Enigma.Frontend.Ui.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class CyclesSinglePositionsViewModel: ObservableObject
{
    private readonly CyclesSinglePositionsModel _model = App.ServiceProvider.GetRequiredService<CyclesSinglePositionsModel>();
    private bool _continueClicked;
    
    [NotifyPropertyChangedFor(nameof(StartDateValid))]   
    [ObservableProperty] private string _startDate = "";
    [NotifyPropertyChangedFor(nameof(EndDateValid))]   
    [ObservableProperty] private string _endDate = "";
    [ObservableProperty] private int _calendarIndex;
    [ObservableProperty] private int _coordinateIndex;
    [ObservableProperty] private ObservableCollection<string> _allCalendars;
    [ObservableProperty] private ObservableCollection<string> _allCoordinates;
    [ObservableProperty] private ObservableCollection<string> _allAyanamshas;
    [ObservableProperty] private ObservableCollection<string> _allObserverPositions;
    
    public SolidColorBrush StartDateValid => IsStartDateValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush EndDateValid => IsEndDateValid() ? Brushes.Gray : Brushes.Red;


    public CyclesSinglePositionsViewModel()
    {
        AllCalendars = new ObservableCollection<string>(_model.AllCalendars);
        AllCoordinates = new ObservableCollection<string>(_model.AllCoordinates);
    }
    
    private bool IsStartDateValid()
    {
        if (string.IsNullOrEmpty(StartDate) && !_continueClicked) return true;
        Calendars cal = CalendarIndex == 0 ? Calendars.Gregorian : Calendars.Julian;
        return _model.IsStartDateValid(StartDate, cal);
    }
    
    private bool IsEndDateValid()
    {
        if (string.IsNullOrEmpty(EndDate) && !_continueClicked) return true;
        Calendars cal = CalendarIndex == 0 ? Calendars.Gregorian : Calendars.Julian;
        return _model.IsEndDateValid(EndDate, cal);
    }

    [RelayCommand]
    private void Cancel()
    {
        WeakReferenceMessenger.Default.Send(new CancelMessage("CyclesSinglePositions"));
    }
    
}