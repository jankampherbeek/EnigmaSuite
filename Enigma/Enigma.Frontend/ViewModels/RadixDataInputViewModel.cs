// Enigma Astrology Research.
// Copyright (c) 2023 Jan Kampherbeek.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
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

/// <summary>ViewModel for data input for a chart</summary>
public partial class RadixDataInputViewModel : ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.RADIX_DATA_INPUT;
    
    private Country _selectedCountry;
    private City _selectedCity;
    private double _offset;
    private bool _dst;
    private bool _isManualCoordinateEdit;
    private bool _isManualTimeZoneEdit;
    private bool _isUpdatingCoordinatesProgrammatically;
    private static readonly Dictionary<string, List<City>> _cityCache = new();
    
    [ObservableProperty] private string _nameId = "";
    [ObservableProperty] private string _description = "";
    [ObservableProperty] private string _source = "";
    [ObservableProperty] private string _locationName = "";
    [NotifyPropertyChangedFor(nameof(GeoLatValid))]
    [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    [NotifyPropertyChangedFor(nameof(GeoLongValid))]
    [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    [NotifyPropertyChangedFor(nameof(DateValid))]
    [NotifyPropertyChangedFor(nameof(TimeZone))]
    [NotifyPropertyChangedFor(nameof(ApplyDst))]
    [ObservableProperty] private string _date = "";

    partial void OnDateChanged(string value)
    {
        if (!_isManualTimeZoneEdit)
        {
            UpdateTimeZone();
        }
    }
    //
    // [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    // [NotifyPropertyChangedFor(nameof(TimeValid))]
    // [NotifyPropertyChangedFor(nameof(TimeZone))]
    // [NotifyPropertyChangedFor(nameof(ApplyDst))]
    [ObservableProperty] private string _time = "";

    partial void OnTimeChanged(string value)
    {
        if (!_isManualTimeZoneEdit)
        {
            UpdateTimeZone();
        }
    }

    [ObservableProperty] private bool _applyDst;
    [ObservableProperty] private int _categoryIndex;
    [ObservableProperty] private int _ratingIndex;
    [ObservableProperty] private int _dirLatIndex;
    [ObservableProperty] private int _dirLongIndex;
    [ObservableProperty] private int _lmtDirLongIndex;
    [ObservableProperty] private int _calendarIndex;
    [ObservableProperty] private int _yearCountIndex;
    [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    [ObservableProperty] private ObservableCollection<string> _allRatings;
    [ObservableProperty] private ObservableCollection<string> _allCategories;
    [ObservableProperty] private ObservableCollection<string> _allDirectionsForLatitude;
    [ObservableProperty] private ObservableCollection<string> _allDirectionsForLongitude;
    [ObservableProperty] private ObservableCollection<string> _allLmtDirectionsForLongitude;
    [ObservableProperty] private ObservableCollection<string> _allCalendars;
    [ObservableProperty] private ObservableCollection<string> _allYearCounts;
    [ObservableProperty] private ObservableCollection<Country> _allCountries;
    [ObservableProperty] private ObservableCollection<City> _citiesForCountry;
    [ObservableProperty] private bool _isLoadingCities;
    
    [NotifyPropertyChangedFor(nameof(GeoLongValid))]
    [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    [ObservableProperty]
    private string _geoLong = "";

    partial void OnGeoLongChanged(string value)
    {
        // Mark as manual edit when user changes coordinates (but not during programmatic updates)
        if (!_isUpdatingCoordinatesProgrammatically)
        {
            _isManualCoordinateEdit = true;
        }
    }

    [NotifyPropertyChangedFor(nameof(GeoLatValid))]
    [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    [ObservableProperty]
    private string _geoLat = "";

    partial void OnGeoLatChanged(string value)
    {
        // Mark as manual edit when user changes coordinates (but not during programmatic updates)
        if (!_isUpdatingCoordinatesProgrammatically)
        {
            _isManualCoordinateEdit = true;
        }
    }

    [NotifyPropertyChangedFor(nameof(TimeZoneValid))]
    [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    [ObservableProperty] private string _timeZone = "";

    partial void OnTimeZoneChanged(string value)
    {
        // Mark as manual edit when user changes timezone
        _isManualTimeZoneEdit = true;
    }
    
    [ObservableProperty]
    private RadixDataInputModel _model = App.ServiceProvider.GetRequiredService<RadixDataInputModel>();
    private readonly ITimeZoneApi _timeZoneApi = App.ServiceProvider.GetRequiredService<ITimeZoneApi>();
    
    private bool _calculateClicked;
  
    public SolidColorBrush GeoLatValid => IsGeoLatValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush GeoLongValid => IsGeoLongValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush DateValid => IsDateValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush TimeValid => IsLocalTimeValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush TimeZoneValid => IsTimeZoneValid() ? Brushes.Gray : Brushes.Red;

    private IDataInputConverter _dataInputConverter;
    
    public RadixDataInputViewModel()
    {
        _dataInputConverter = new DataInputConverter();
        AllRatings = new ObservableCollection<string>(_model.AllRatings);
        AllCategories = new ObservableCollection<string>(_model.AllCategories);
        AllDirectionsForLatitude = new ObservableCollection<string>(_model.AllDirectionsForLatitude);
        AllDirectionsForLongitude = new ObservableCollection<string>(_model.AllDirectionsForLongitude);
        AllLmtDirectionsForLongitude = new ObservableCollection<string>(_model.AllDirectionsForLongitude);
        AllCalendars = new ObservableCollection<string>(_model.AllCalendars);
        AllYearCounts = new ObservableCollection<string>(_model.AllYearCounts);
        AllCountries = new ObservableCollection<Country>(_model.AllCountries);
        CitiesForCountry = new ObservableCollection<City>();
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
            
            // Update location name when city is selected
            if (_selectedCity != null)
            {
                LocationName = _selectedCity.Name;
            }
            
            // Only update coordinates automatically if user hasn't manually edited them
            if (!_isManualCoordinateEdit)
            {
                UpdateCoordinates();
            }
            // Always update timezone when city changes (unless manually edited)
            if (!_isManualTimeZoneEdit)
            {
                UpdateTimeZone();
            }
        }
    }

    private async Task UpdateCitiesAsync()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        IsLoadingCities = true;
        
        // Clear current cities immediately to prevent showing wrong ones
        Application.Current.Dispatcher.Invoke(() =>
        {
            CitiesForCountry.Clear();
        });
        
        try
        {
            var countryCode = SelectedCountry.Code;
            List<City> cities;
            var loadStart = System.Diagnostics.Stopwatch.StartNew();
            
            // Check cache first
            if (_cityCache.TryGetValue(countryCode, out cities))
            {
                // Create a new list to avoid reference issues
                cities = new List<City>(cities);
            }
            else
            {
                // Load from data source and cache
                cities = await Task.Run(() => _model.CitiesForCountry(countryCode));
                _cityCache[countryCode] = new List<City>(cities); // Store a copy in cache
            }
            
            loadStart.Stop();
            
            var uiStart = System.Diagnostics.Stopwatch.StartNew();
            Application.Current.Dispatcher.Invoke(() =>
            {
                CitiesForCountry = new ObservableCollection<City>(cities);
             });
            uiStart.Stop();
        }
        catch (Exception ex)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CitiesForCountry.Clear();
            });
        }
        finally
        {
            IsLoadingCities = false;
            stopwatch.Stop();
        }
    }
    
    private void UpdateCoordinates()
    {
        if (SelectedCity == null) return;
        
        // Set flag to prevent property change handlers from marking as manual edit
        _isUpdatingCoordinatesProgrammatically = true;
        
        // Reset manual edit flag when updating from city selection
        _isManualCoordinateEdit = false;
        
        GeoLong = _dataInputConverter.ValueTxtToFormattedCoordinate(SelectedCity.GeoLong);
        GeoLat = _dataInputConverter.ValueTxtToFormattedCoordinate(SelectedCity.GeoLat);
        DirLongIndex = SelectedCity.GeoLong.StartsWith('-') ? 1 : 0;
        DirLatIndex = SelectedCity.GeoLat.StartsWith('-') ? 1 : 0;
        
        // Reset the programmatic update flag
        _isUpdatingCoordinatesProgrammatically = false;
    }

    private async void UpdateTimeZone()
    {
        if (SelectedCity == null || string.IsNullOrEmpty(Date) || string.IsNullOrEmpty(Time)) return;

        // Ensure city data is fully loaded before proceeding
        var timeout = 0;
        while ((CitiesForCountry == null || !CitiesForCountry.Any()) && timeout < 100) // Max 5 second timeout
        {
            await Task.Delay(50);
            timeout++;
        }
        
        // Additional check: ensure SelectedCity belongs to the current country
        if (SelectedCountry != null && SelectedCity.Country != SelectedCountry.Code)
        {
            Log.Warning($"SelectedCity country mismatch: {SelectedCity.Country} != {SelectedCountry.Code}");
            return;
        }

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
            // Parse longitude for LMT calculation
            var longitude = ParseLongitudeFromCity(SelectedCity);
            var zoneInfo = _timeZoneApi.GetTimeZoneDst(dateTime, SelectedCity.IndicationTz, longitude);
            
            // The following three lines are repeated later. But they prevent retrieving wrong values when caching
            // large amounts of data, e.g. for the USA.
            _offset = zoneInfo.Offset;
            _dst = zoneInfo.Dst;
            TimeZone = FormatTimeZone(_offset);
            
            
            // Use only the base timezone offset without DST
            _offset = zoneInfo.Offset;
            _dst = zoneInfo.Dst;
            
            // Reset manual edit flag when updating automatically
            _isManualTimeZoneEdit = false;
            
            TimeZone = FormatTimeZone(_offset);
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
    
    private double ParseLongitudeFromCity(City city)
    {
        if (city == null || string.IsNullOrEmpty(city.GeoLong))
            return 0.0;
            
        try
        {
            // Parse the longitude string which is in decimal format
            // Negative values represent Western longitude
            if (double.TryParse(city.GeoLong, NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude))
            {
                return longitude;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"Error parsing longitude from city: {city.GeoLong}");
        }
        
        return 0.0;
    }

    private bool IsTimeZoneValid()
    {
        if (string.IsNullOrEmpty(TimeZone) && !_calculateClicked) return true;
        return _model.IsTimeZoneValid(TimeZone);
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
                    bool positive = hours > 0;
                    hours = Math.Abs(hours);
                    var minutes = int.Parse(parts[1]);
                    var seconds = parts.Length > 2 ? int.Parse(parts[2]) : 0;
                    var result = hours + (minutes / 60.0) + (seconds / 3600.0);
                    if (!positive) result = -result;
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error parsing manual timezone");
            }
        }
        
        // Use the calculated offset with DST if applicable
        return _offset;
    }

    [RelayCommand]
    private void Calculate()
    {
        _calculateClicked = true;
        string errors = FindErrors();
        if (string.IsNullOrEmpty(errors))
        {
            Log.Information("RadixDataInputViewModel.Calculate(): starting calculation of chart");
            _model.CreateChartData(NameId, Description, Source, LocationName, CategoryIndex, RatingIndex);
            Log.Information("RadixDataInputViewModel.Calculate(): send NewChartMessage and CloseMessage");  
            WeakReferenceMessenger.Default.Send(new CloseRadixDataInputViewMessage(VM_IDENTIFICATION));
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
        if (!IsDateValid())
            errorsText.Append(StandardTexts.ERROR_DATE + EnigmaConstants.NEW_LINE);
        if (!IsTimeZoneValid())
            errorsText.Append(StandardTexts.ERROR_TIMEZONE + EnigmaConstants.NEW_LINE);
        if (!IsTimeValid() || !IsLocalTimeValid())   
            errorsText.Append(StandardTexts.ERROR_TIME + EnigmaConstants.NEW_LINE);
        return errorsText.ToString();
    }
    
    private bool IsGeoLatValid()
    {
        if (string.IsNullOrEmpty(GeoLat) && !_calculateClicked) return true; 
        Directions4GeoLat dir = DirLatIndex == 0 ? Directions4GeoLat.North : Directions4GeoLat.South; 
        return _model.IsGeoLatValid(GeoLat, dir);
    }
    
    private bool IsGeoLongValid()
    {
        if (string.IsNullOrEmpty(GeoLong) && !_calculateClicked) return true; 
        Directions4GeoLong dir = DirLongIndex == 0 ? Directions4GeoLong.East : Directions4GeoLong.West; 
        return _model.IsGeoLongValid(GeoLong, dir);
    }
    
    private bool IsDateValid()
    {
        if (string.IsNullOrEmpty(Date) && !_calculateClicked) return true; 
        Calendars cal = CalendarIndex == 0 ? Calendars.Gregorian : Calendars.Julian;
        YearCounts yCount = YearCountsExtensions.YearCountForIndex(YearCountIndex);
        return _model.IsDateValid(Date, cal, yCount);
    }
    
    private bool IsLocalTimeValid()
    {
        if (string.IsNullOrEmpty(Time) && !_calculateClicked) return true; 
        return _model.IsLocalTimeValid(Time);
    }

    private bool IsTimeValid()
    {
        var effectiveOffset = GetEffectiveTimeZoneOffset();
        return _model.IsTimeValid(Time, effectiveOffset, ApplyDst);
    }
    
    [RelayCommand]
    private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand]
    private static void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseRadixDataInputViewMessage(VM_IDENTIFICATION));
    }
}