// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
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
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for data input for a chart</summary>
public partial class RadixDataInputViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.RADIX_DATA_INPUT;
    [ObservableProperty] private string _nameId = "";
    [ObservableProperty] private string _description = "";
    [ObservableProperty] private string _source = "";
    [ObservableProperty] private string _locationName = "";
    [NotifyPropertyChangedFor(nameof(GeoLatValid))]
    [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    [ObservableProperty] private string _geoLat = "";
    [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    [NotifyPropertyChangedFor(nameof(GeoLongValid))]
    [ObservableProperty] private string _geoLong = "";
    [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    [NotifyPropertyChangedFor(nameof(LmtGeoLongValid))]
    [ObservableProperty] private string _lmtGeoLong = "";
    [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    [NotifyPropertyChangedFor(nameof(DateValid))]
    [ObservableProperty] private string _date = "";
    [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    [NotifyPropertyChangedFor(nameof(TimeValid))]
    [ObservableProperty] private string _time = "";
    [ObservableProperty] private bool _applyDst;
    [ObservableProperty] private int _categoryIndex;
    [ObservableProperty] private int _ratingIndex;
    [ObservableProperty] private int _dirLatIndex;
    [ObservableProperty] private int _dirLongIndex;
    [ObservableProperty] private int _lmtDirLongIndex;
    [ObservableProperty] private int _calendarIndex;
    [ObservableProperty] private int _yearCountIndex;
    [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    [NotifyPropertyChangedFor(nameof(LmtEnabled))]
    [ObservableProperty] private int _timeZoneIndex;
    
    [ObservableProperty] private ObservableCollection<string> _allRatings;
    [ObservableProperty] private ObservableCollection<string> _allCategories;
    [ObservableProperty] private ObservableCollection<string> _allDirectionsForLatitude;
    [ObservableProperty] private ObservableCollection<string> _allDirectionsForLongitude;
    [ObservableProperty] private ObservableCollection<string> _allLmtDirectionsForLongitude;
    [ObservableProperty] private ObservableCollection<string> _allCalendars;
    [ObservableProperty] private ObservableCollection<string> _allYearCounts;
    [ObservableProperty] private ObservableCollection<string> _allTimeZones;
    private readonly int _enumIndexForLmt;
    private readonly RadixDataInputModel _model = App.ServiceProvider.GetRequiredService<RadixDataInputModel>();
    private bool _calculateClicked;
    public bool LmtEnabled => TimeZoneIndex == _enumIndexForLmt;
    public SolidColorBrush GeoLatValid => IsGeoLatValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush GeoLongValid => IsGeoLongValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush LmtGeoLongValid => IsLmtGeoLongValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush DateValid => IsDateValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush TimeValid => IsTimeValid() ? Brushes.Gray : Brushes.Red;
    
    public RadixDataInputViewModel()
    {
        AllRatings = new ObservableCollection<string>(_model.AllRatings);
        AllCategories = new ObservableCollection<string>(_model.AllCategories);
        AllDirectionsForLatitude = new ObservableCollection<string>(_model.AllDirectionsForLatitude);
        AllDirectionsForLongitude = new ObservableCollection<string>(_model.AllDirectionsForLongitude);
        AllLmtDirectionsForLongitude  = new ObservableCollection<string>(_model.AllDirectionsForLongitude);
        AllCalendars = new ObservableCollection<string>(_model.AllCalendars);
        AllYearCounts = new ObservableCollection<string>(_model.AllYearCounts);
        AllTimeZones = new ObservableCollection<string>(_model.AllTimeZones);
        _enumIndexForLmt = (int)TimeZones.Lmt;
    }
    


    
    [RelayCommand]
    private void Calculate()
    {
        _calculateClicked = true;
        string errors = FindErrors();
        if (string.IsNullOrEmpty(errors))
        {
            Log.Information("RadixDataInputViewModel.Calculate(): starting alculation of chart");
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

    private bool IsLmtGeoLongValid()
    {
        if (string.IsNullOrEmpty(LmtGeoLong) && !_calculateClicked) return true;
        if (_enumIndexForLmt != TimeZoneIndex) return true;
        if (LmtGeoLong == string.Empty) return false;
        Directions4GeoLong dir = LmtDirLongIndex == 0 ? Directions4GeoLong.East : Directions4GeoLong.West; 
        return _model.IsLmtGeoLongValid(LmtGeoLong, dir);
    }
    
    private bool IsDateValid()
    {
        if (string.IsNullOrEmpty(Date) && !_calculateClicked) return true; 
        Calendars cal = CalendarIndex == 0 ? Calendars.Gregorian : Calendars.Julian;
        YearCounts yCount = YearCountsExtensions.YearCountForIndex(YearCountIndex);
        Log.Information("RadixDataInputViewModel.IsDateValid() calls RadixDataInputModel.IsDateValid()");
        return _model.IsDateValid(Date, cal, yCount);
    }
    
    private bool IsTimeValid()
    {
        if (string.IsNullOrEmpty(Time) && !_calculateClicked) return true; 
        TimeZones timeZone = TimeZonesExtensions.TimeZoneForIndex(TimeZoneIndex);
        return _model.IsTimeValid(Time, timeZone, ApplyDst);
    }
    
    [RelayCommand]
    private static void Help()
    {
        Log.Information("RadixDataInputViewModel.Help(): send HelpMessage");  
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand]
    private static void Close()
    {
   //     Log.Information("RadixDataInputViewModel.Close(): send CloseMessage");  
        WeakReferenceMessenger.Default.Send(new CloseRadixDataInputViewMessage(VM_IDENTIFICATION));
    }
}