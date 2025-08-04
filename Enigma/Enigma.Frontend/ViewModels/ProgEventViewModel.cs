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
using Enigma.Domain.Dtos;
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
    private double _offset;
    private bool _dst;
    [ObservableProperty] private int _dirLatIndex;
    [ObservableProperty] private int _dirLongIndex;
    [ObservableProperty] private int _calendarIndex;
    [ObservableProperty] private int _yearCountIndex;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private string _locationName = "No location";
    [NotifyPropertyChangedFor(nameof(GeoLatValid))]
    [NotifyCanExecuteChangedFor(nameof(FinalizeEventCommand))]
    [ObservableProperty] private string _geoLat = "00:00:00";
    [NotifyPropertyChangedFor(nameof(GeoLongValid))]
    [ObservableProperty] private string _geoLong = "000:00:00";
    [NotifyCanExecuteChangedFor(nameof(FinalizeEventCommand))]
    [NotifyPropertyChangedFor(nameof(DateValid))]
    [ObservableProperty] private string _date = "";
    [NotifyCanExecuteChangedFor(nameof(FinalizeEventCommand))]
    [NotifyPropertyChangedFor(nameof(TimeValid))]
    [ObservableProperty] private string _time = "12:00:00";
    [NotifyPropertyChangedFor(nameof(TimeZoneValid))]
    [ObservableProperty] private string _timeZone = "00:00:00";
    [NotifyPropertyChangedFor(nameof(TimeZone))]
    [ObservableProperty] private bool _applyDst;
    [NotifyPropertyChangedFor(nameof(ApplyDst))]
    [ObservableProperty] private ObservableCollection<string> _allDirectionsForLatitude;
    [ObservableProperty] private ObservableCollection<string> _allDirectionsForLongitude;
    [ObservableProperty] private ObservableCollection<string> _allLmtDirectionsForLongitude;
    [ObservableProperty] private ObservableCollection<string> _allCalendars;
    [ObservableProperty] private ObservableCollection<string> _allYearCounts;
    [ObservableProperty] private ObservableCollection<Country> _allCountries;
    [ObservableProperty] private ObservableCollection<City> _citiesForCountry;
    ZoneInfo _zoneInfo;
    private bool _isManualCoordinateEdit;
    private bool _isManualTimeZoneEdit;
    private bool _isUpdatingCoordinatesProgrammatically;

    partial void OnDateChanged(string value)
    {
        UpdateDateTime();
    }
    partial void OnTimeChanged(string value)
    {
        UpdateDateTime();
    }

    partial void OnTimeZoneChanged(string value)
    {
        UpdateDateTime();
    }

    private readonly ProgEventModel _model = App.ServiceProvider.GetRequiredService<ProgEventModel>();
    private readonly ITimeZoneApi _timeZoneApi = App.ServiceProvider.GetRequiredService<ITimeZoneApi>();
    private bool _saveClicked;
    private IDataInputConverter _dataInputConverter;
    public SolidColorBrush GeoLatValid => IsGeoLatValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush GeoLongValid => IsGeoLongValid() ? Brushes.Gray : Brushes.Red;
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
            UpdateDateTime();
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
    }

    
    private void UpdateDateTime()
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
            _zoneInfo = _timeZoneApi.GetTimeZoneDst(dateTime, SelectedCity.IndicationTz);
            TimeZone = FormatTimeZone(_zoneInfo.Offset - (_zoneInfo.Dst ? 1.0 : 0.0));
            ApplyDst = _zoneInfo.Dst;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error updating timezone");
            TimeZone = "";
            ApplyDst = false;
        }
    }

    private double GetEffectiveTimeZoneOffset()
    {
        // If timezone was manually edited, parse it to get the offset
        if (_isManualTimeZoneEdit && !string.IsNullOrEmpty(TimeZone))
        {
            try
            {
                var parts = TimeZone.Split(':');
                if (parts.Length >= 2)
                {
                    var hours = int.Parse(parts[0]);
                    var minutes = int.Parse(parts[1]);
                    var seconds = parts.Length > 2 ? int.Parse(parts[2]) : 0;
                    return hours + (minutes / 60.0) + (seconds / 3600.0);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error parsing manual timezone");
            }
        }
        
        // Use the calculated offset with DST if applicable
        return _offset - (ApplyDst ? 1.0 : 0.0);
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

    
    private bool IsDateValid()
    {
        if (string.IsNullOrEmpty(Date) && !_saveClicked) return true; 
        Calendars cal = CalendarIndex == 0 ? Calendars.Gregorian : Calendars.Julian;
        YearCounts yCount = YearCountsExtensions.YearCountForIndex(YearCountIndex);
        return _model.IsDateValid(Date, cal, yCount);
    }
 
    
    private bool IsTimeValid()
    {
        var effectiveOffset = GetEffectiveTimeZoneOffset();
        return _model.IsTimeValid(Time, effectiveOffset, ApplyDst);
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
        WeakReferenceMessenger.Default.Send(new CloseProgEventViewMessage(VM_IDENTIFICATION));
    }
    
}