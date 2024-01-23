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
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ProgEventViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.PROG_EVENT;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private string _locationName = "No location";
    [NotifyPropertyChangedFor(nameof(GeoLatValid))]
    [NotifyCanExecuteChangedFor(nameof(FinalizeEventCommand))]
    [ObservableProperty] private string _geoLat = "00:00:00";
    [NotifyCanExecuteChangedFor(nameof(FinalizeEventCommand))]
    [NotifyPropertyChangedFor(nameof(GeoLongValid))]
    [ObservableProperty] private string _geoLong = "000:00:00";
    [NotifyCanExecuteChangedFor(nameof(FinalizeEventCommand))]
    [NotifyPropertyChangedFor(nameof(LmtGeoLongValid))]
    [ObservableProperty] private string _lmtGeoLong = "";
    [NotifyCanExecuteChangedFor(nameof(FinalizeEventCommand))]
    [NotifyPropertyChangedFor(nameof(DateValid))]
    [ObservableProperty] private string _date = "";
    [NotifyCanExecuteChangedFor(nameof(FinalizeEventCommand))]
    [NotifyPropertyChangedFor(nameof(TimeValid))]
    [ObservableProperty] private string _time = "12:00:00";
    [ObservableProperty] private bool _applyDst;
    [ObservableProperty] private int _dirLatIndex;
    [ObservableProperty] private int _dirLongIndex;
    [ObservableProperty] private int _lmtDirLongIndex;
    [NotifyCanExecuteChangedFor(nameof(FinalizeEventCommand))]
    [NotifyPropertyChangedFor(nameof(LmtEnabled))]
    [ObservableProperty] private int _timeZoneIndex;
    [ObservableProperty] private ObservableCollection<string> _allDirectionsForLatitude;
    [ObservableProperty] private ObservableCollection<string> _allDirectionsForLongitude;
    [ObservableProperty] private ObservableCollection<string> _allLmtDirectionsForLongitude;
    [ObservableProperty] private ObservableCollection<string> _allCalendars;
    [ObservableProperty] private ObservableCollection<string> _allYearCounts;
    [ObservableProperty] private ObservableCollection<string> _allTimeZones;

    private readonly int _enumIndexForLmt;
    private readonly ProgEventModel _model = App.ServiceProvider.GetRequiredService<ProgEventModel>();
    private bool _saveClicked;
    
    public bool LmtEnabled => TimeZoneIndex == _enumIndexForLmt;
    public SolidColorBrush GeoLatValid => IsGeoLatValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush GeoLongValid => IsGeoLongValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush LmtGeoLongValid => IsLmtGeoLongValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush DateValid => IsDateValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush TimeValid => IsTimeValid() ? Brushes.Gray : Brushes.Red;
    
    public ProgEventViewModel()
    {
        AllDirectionsForLatitude = new ObservableCollection<string>(_model.AllDirectionsForLatitude);
        AllDirectionsForLongitude = new ObservableCollection<string>(_model.AllDirectionsForLongitude);
        AllLmtDirectionsForLongitude  = new ObservableCollection<string>(_model.AllDirectionsForLongitude);
        AllTimeZones = new ObservableCollection<string>(_model.AllTimeZones);
        _enumIndexForLmt = (int)TimeZones.Lmt;        
    }
    
    [RelayCommand]
    private void FinalizeEvent()
    {
        _saveClicked = true;
        string errors = FindErrors();
        if (string.IsNullOrEmpty(errors))
        {
            _model.CreateEventData(Description, LocationName);
            WeakReferenceMessenger.Default.Send(new EventCompletedMessage(VM_IDENTIFICATION));
            WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
        }
        else
        {
            MessageBox.Show(errors, StandardTexts.TITLE_ERROR);
        }
    }
    
    private string FindErrors()
    {
        StringBuilder errorsText = new();
        if (!IsGeoLatValid())
            errorsText.Append(StandardTexts.ERROR_GEOGRAPHIC_LATITUDE + EnigmaConstants.NEW_LINE);
        if (!IsGeoLongValid())
            errorsText.Append(StandardTexts.ERROR_GEOGRAPHIC_LONGITUDE + EnigmaConstants.NEW_LINE);
        if (!IsLmtGeoLongValid())
            errorsText.Append(StandardTexts.ERROR_LMT_LONGITUDE + EnigmaConstants.NEW_LINE);
        if (!IsDateValid())
            errorsText.Append(StandardTexts.ERROR_DATE + EnigmaConstants.NEW_LINE);
        if (!IsTimeValid())
            errorsText.Append(StandardTexts.ERROR_TIME + EnigmaConstants.NEW_LINE);
        return errorsText.ToString();
    }
    
    private bool IsGeoLatValid()
    {
        if (string.IsNullOrEmpty(GeoLat) && !_saveClicked) return true; 
        Directions4GeoLat dir = DirLatIndex == 0 ? Directions4GeoLat.North : Directions4GeoLat.South; 
        return _model.IsGeoLatValid(GeoLat, dir);
    }
    
    private bool IsGeoLongValid()
    {
        if (string.IsNullOrEmpty(GeoLong) && !_saveClicked) return true; 
        Directions4GeoLong dir = DirLongIndex == 0 ? Directions4GeoLong.East : Directions4GeoLong.West; 
        return _model.IsGeoLongValid(GeoLong, dir);
    }

    private bool IsLmtGeoLongValid()
    {
        if (string.IsNullOrEmpty(LmtGeoLong) && !_saveClicked) return true;
        if (_enumIndexForLmt != TimeZoneIndex) return true;
        if (LmtGeoLong == string.Empty) return false;
        Directions4GeoLong dir = LmtDirLongIndex == 0 ? Directions4GeoLong.East : Directions4GeoLong.West; 
        return _model.IsLmtGeoLongValid(LmtGeoLong, dir);
    }
    
    private bool IsDateValid()
    {
        if (string.IsNullOrEmpty(Date) && !_saveClicked) return true; 
        return _model.IsDateValid(Date, Calendars.Gregorian, YearCounts.CE);
    }
    
    private bool IsTimeValid()
    {
        if (string.IsNullOrEmpty(Time) && !_saveClicked) return true; 
        TimeZones timeZone = TimeZonesExtensions.TimeZoneForIndex(TimeZoneIndex);
        return Time == string.Empty || _model.IsTimeValid(Time, timeZone, ApplyDst);
    }
    
    
    [RelayCommand]
    private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand]
    private static void Cancel()
    {
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }
    
}