// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Constants;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
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
    [NotifyPropertyChangedFor(nameof(CombinationObsPosCoordinateValid))]     
    [ObservableProperty] private int _calendarIndex;
    [ObservableProperty] private int _coordinateIndex;
    [ObservableProperty] private int _ayanamshaIndex;
    [ObservableProperty] private int _observerPositionIndex;

    [ObservableProperty] private ObservableCollection<string> _allCalendars;
    [ObservableProperty] private ObservableCollection<string> _allCoordinates;
    [ObservableProperty] private ObservableCollection<string> _allAyanamshas;
    [ObservableProperty] private ObservableCollection<string> _allObserverPositions;
    
    public SolidColorBrush StartDateValid => IsStartDateValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush EndDateValid => IsEndDateValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush CombinationObsPosCoordinateValid =>
        IsCombinationObsPosCoordinateValid() ? Brushes.Gray : Brushes.Red;

    public CyclesSinglePositionsViewModel()
    {
        AllCalendars = new ObservableCollection<string>(_model.AllCalendars);
        AllCoordinates = new ObservableCollection<string>(_model.AllCoordinates);
        AllAyanamshas = new ObservableCollection<string>(_model.AllAyanamshas);
        AllObserverPositions = new ObservableCollection<string>(_model.AllObserverPositions);
    }
    
    [RelayCommand]
    private void Continue()
    {
        _continueClicked = true;
        string errors = FindErrors();
        if (string.IsNullOrEmpty(errors))
        {
            // todo save data
            // send msg
        }
        else
            MessageBox.Show(errors,  StandardTexts.TITLE_ERROR);
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

    private bool IsCombinationObsPosCoordinateValid()     // ObserverPosition 1 = helioCentric, (topocentric not included)
                                                          // Coordinate 2 and 3 are ra and decl.
    {
        return ((ObserverPositionIndex != 1) || (CoordinateIndex != 2 && CoordinateIndex !=3));
    }
    
    private string FindErrors()
    {
        StringBuilder errorsText = new();
        if (!IsStartDateValid())
            errorsText.Append(StandardTexts.ERROR_DATE_START + EnigmaConstants.NEW_LINE);
        if (!IsEndDateValid())
            errorsText.Append(StandardTexts.ERROR_DATE_END + EnigmaConstants.NEW_LINE);
        if (!IsCombinationObsPosCoordinateValid())
            errorsText.Append(StandardTexts.ERROR_CYCLES_COMBI_OBSPOS_COORDINATE + EnigmaConstants.NEW_LINE);
        return errorsText.ToString();
    }
    
    
    [RelayCommand]
    private void Cancel()
    {
        WeakReferenceMessenger.Default.Send(new CancelMessage("CyclesSinglePositions"));
    }
    
}