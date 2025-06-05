// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Api.LocationAndTimeZones;
using Enigma.Core.LocationAndTimeZones;
using Enigma.Domain.Constants;
using Enigma.Domain.LocationsZones;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.Support.Conversions;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ProgEventViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.PROG_EVENT;
    private Country _selectedCountry;
    private City _selectedCity;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private string _locationName = "No location";
    [NotifyPropertyChangedFor(nameof(GeoLatValid))]
    [NotifyCanExecuteChangedFor(nameof(FinalizeEventCommand))]
    [ObservableProperty] private string _geoLat = "00:00:00";
    [NotifyPropertyChangedFor(nameof(GeoLongValid))]
    [ObservableProperty] private string _geoLong = "000:00:00";
    // [NotifyCanExecuteChangedFor(nameof(FinalizeEventCommand))]
    // [NotifyPropertyChangedFor(nameof(LmtGeoLongValid))]
    // [ObservableProperty] private string _lmtGeoLong = "";
    [NotifyCanExecuteChangedFor(nameof(FinalizeEventCommand))]
    [NotifyPropertyChangedFor(nameof(DateValid))]
    [ObservableProperty] private string _date = "";
    [NotifyCanExecuteChangedFor(nameof(FinalizeEventCommand))]
    [NotifyPropertyChangedFor(nameof(TimeValid))]
    [ObservableProperty] private string _time = "12:00:00";
    [ObservableProperty] private bool _applyDst;
    [ObservableProperty] private int _dirLatIndex;
    [ObservableProperty] private int _dirLongIndex;
    [ObservableProperty] private int _calendarIndex;
    [ObservableProperty] private int _yearCountIndex;
  //  [NotifyPropertyChangedFor(nameof(TimeZoneValid))]
  //  [NotifyCanExecuteChangedFor(nameof(FinalizeEvent))]
    // [ObservableProperty] private int _lmtDirLongIndex;
    // [NotifyCanExecuteChangedFor(nameof(FinalizeEventCommand))]
    // [NotifyPropertyChangedFor(nameof(LmtEnabled))]
   // [ObservableProperty] private int _timeZoneIndex;
    [ObservableProperty] private ObservableCollection<string> _allDirectionsForLatitude;
    [ObservableProperty] private ObservableCollection<string> _allDirectionsForLongitude;
    [ObservableProperty] private ObservableCollection<string> _allLmtDirectionsForLongitude;
    [ObservableProperty] private ObservableCollection<string> _allCalendars;
    [ObservableProperty] private ObservableCollection<string> _allYearCounts;
  //  [ObservableProperty] private ObservableCollection<string> _allTimeZones;
    [ObservableProperty] private ObservableCollection<Country> _allCountries;
    [ObservableProperty] private ObservableCollection<City> _citiesForCountry;
    [ObservableProperty] private string _timeZone = "";

    partial void OnDateChanged(string value)
    {
        UpdateTimeZone();
    }
    partial void OnTimeChanged(string value)
    {
        UpdateTimeZone();
    }

    
  //  private readonly int _enumIndexForLmt;
  //  [ObservableProperty]
    private readonly ProgEventModel _model = App.ServiceProvider.GetRequiredService<ProgEventModel>();
    private readonly ITimeZoneApi _timeZoneApi = App.ServiceProvider.GetRequiredService<ITimeZoneApi>();
    private bool _saveClicked;
    private IDataInputConverter _dataInputConverter;
    
  //  public bool LmtEnabled => TimeZoneIndex == _enumIndexForLmt;
    public SolidColorBrush GeoLatValid => IsGeoLatValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush GeoLongValid => IsGeoLongValid() ? Brushes.Gray : Brushes.Red;
 //   public SolidColorBrush LmtGeoLongValid => IsLmtGeoLongValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush DateValid => IsDateValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush TimeValid => IsTimeValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush TimeZoneValid => IsTimeZoneValid() ? Brushes.Gray : Brushes.Red;
    
    public ProgEventViewModel()
    {
        AllDirectionsForLatitude = new ObservableCollection<string>(_model.AllDirectionsForLatitude);
        AllDirectionsForLongitude = new ObservableCollection<string>(_model.AllDirectionsForLongitude);
        AllCountries = new ObservableCollection<Country>(_model.AllCountries);
        CitiesForCountry = new ObservableCollection<City>();
        AllCalendars = new ObservableCollection<string>(_model.AllCalendars);
        AllYearCounts = new ObservableCollection<string>(_model.AllYearCounts);
    //    AllLmtDirectionsForLongitude  = new ObservableCollection<string>(_model.AllDirectionsForLongitude);
    //    AllTimeZones = new ObservableCollection<string>(_model.AllTimeZones);
    //    _enumIndexForLmt = (int)TimeZones.Lmt;  
        _dataInputConverter = new DataInputConverter();
    }
    
    [RelayCommand]
    private void FinalizeEvent()
    {
        _saveClicked = true;
        string errors = FindErrors();
        if (string.IsNullOrEmpty(errors))
        {
            _model.CreateEventData(Description, LocationName);
            WeakReferenceMessenger.Default.Send(new CloseProgEventViewMessage(VM_IDENTIFICATION));
        }
        else
        {
            MessageBox.Show(errors, StandardTexts.TITLE_ERROR);
        }

    }
    
    public Country SelectedCountry
    {
        get => _selectedCountry;
        set
        {
            _selectedCountry = value;
            OnPropertyChanged();
            Task.Run(async () => 
            {
                await UpdateCitiesAsync();
            });            
    
        }
    }
    
    public City SelectedCity
    {
        get => _selectedCity;
        set
        {
            _selectedCity = value;
            OnPropertyChanged();
            UpdateCoordinates();
        }
    }
    
        private async Task UpdateCitiesAsync()
    {
        var countryCode = SelectedCountry.Code;
        var cities = await Task.Run(() => _model.CitiesForCountry(countryCode));
    
        Application.Current.Dispatcher.Invoke(() =>
        {
            CitiesForCountry = new ObservableCollection<City>(cities);
        });
    }
    
    private void UpdateCoordinates()
    {
        GeoLong = _dataInputConverter.ValueTxtToFormattedCoordinate(SelectedCity.GeoLong);
        GeoLat = _dataInputConverter.ValueTxtToFormattedCoordinate(SelectedCity.GeoLat);
        DirLongIndex = SelectedCity.GeoLong.StartsWith('-') ? 1 : 0;
        DirLatIndex = SelectedCity.GeoLat.StartsWith('-') ? 1 : 0;
        UpdateTimeZone();
    }

    private void UpdateTimeZone()
    {
        if (SelectedCity == null || string.IsNullOrEmpty(Date) || string.IsNullOrEmpty(Time)) return;

        try
        {
            var dateParts = Date.Split('/');
            var timeParts = Time.Split(':');
            var dateTime = new DateTimeHms(
                int.Parse(dateParts[0]), // Year
                int.Parse(dateParts[1]), // Month
                int.Parse(dateParts[2]), // Day
                int.Parse(timeParts[0]), // Hour
                int.Parse(timeParts[1]), // Minute
                timeParts.Length > 2 ? int.Parse(timeParts[2]) : 0 // Second (optional)
            );
            var zoneInfo = _timeZoneApi.GetTimeZoneDst(dateTime, SelectedCity.IndicationTz);
            // Use only the base timezone offset without DST
            TimeZone = FormatTimeZone(zoneInfo.Offset - (zoneInfo.Dst ? 1.0 : 0.0));
            ApplyDst = zoneInfo.Dst;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error updating timezone");
            TimeZone = "";
            ApplyDst = false;
        }
    }

    private string FormatTimeZone(double offset)
    {
        var sign = offset < 0 ? "-" : "";
        var absOffset = Math.Abs(offset);
        var hours = (int)absOffset;
        var minutes = (int)((absOffset - hours) * 60);
        var seconds = (int)(((absOffset - hours) * 60 - minutes) * 60);
        return $"{sign}{hours:D2}:{minutes:D2}:{seconds:D2}";
    }
    
    private string FindErrors()
    {
        StringBuilder errorsText = new();
        if (!IsGeoLatValid())
            errorsText.Append(StandardTexts.ERROR_GEOGRAPHIC_LATITUDE + EnigmaConstants.NEW_LINE);
        if (!IsGeoLongValid())
            errorsText.Append(StandardTexts.ERROR_GEOGRAPHIC_LONGITUDE + EnigmaConstants.NEW_LINE);
        // if (!IsLmtGeoLongValid())
        //     errorsText.Append(StandardTexts.ERROR_LMT_LONGITUDE + EnigmaConstants.NEW_LINE);
        if (!IsDateValid())
            errorsText.Append(StandardTexts.ERROR_DATE + EnigmaConstants.NEW_LINE);
        if (!IsTimeValid())
            errorsText.Append(StandardTexts.ERROR_TIME + EnigmaConstants.NEW_LINE);
        if (!IsTimeZoneValid())
            errorsText.Append(StandardTexts.ERROR_TIMEZONE + EnigmaConstants.NEW_LINE);        
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

    // private bool IsLmtGeoLongValid()
    // {
    //     if (string.IsNullOrEmpty(LmtGeoLong) && !_saveClicked) return true;
    //     if (_enumIndexForLmt != TimeZoneIndex) return true;
    //     if (LmtGeoLong == string.Empty) return false;
    //     Directions4GeoLong dir = LmtDirLongIndex == 0 ? Directions4GeoLong.East : Directions4GeoLong.West; 
    //     return _model.IsLmtGeoLongValid(LmtGeoLong, dir);
    // }
    
    // private bool IsDateValid()
    // {
    //     if (string.IsNullOrEmpty(Date) && !_saveClicked) return true; 
    //     return _model.IsDateValid(Date, Calendars.Gregorian, YearCounts.CE);
    // }
    
    private bool IsDateValid()
    {
        if (string.IsNullOrEmpty(Date) && !_saveClicked) return true; 
        Calendars cal = CalendarIndex == 0 ? Calendars.Gregorian : Calendars.Julian;
        YearCounts yCount = YearCountsExtensions.YearCountForIndex(YearCountIndex);
        return _model.IsDateValid(Date, cal, yCount);
    }
    
    
    // private bool IsTimeValid()
    // {
    //     if (string.IsNullOrEmpty(Time) && !_saveClicked) return true; 
    //     TimeZones timeZone = TimeZonesExtensions.TimeZoneForIndex(TimeZoneIndex);
    //     return Time == string.Empty || _model.IsTimeValid(Time, timeZone, ApplyDst);
    // }
    
    private bool IsTimeValid()
    {
        if (string.IsNullOrEmpty(Time) && !_saveClicked) return true; 
        return _model.IsLocalTimeValid(Time);
    }
    
    
    private bool IsTimeZoneValid()
    {
        if (string.IsNullOrEmpty(TimeZone) && !_saveClicked) return true;
        return _model.IsTimeZoneValid(TimeZone);
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