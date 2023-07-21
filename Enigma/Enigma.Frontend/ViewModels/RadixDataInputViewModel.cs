// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Charts;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for data input for a chart</summary>
public partial class RadixDataInputViewModel: ObservableObject
{
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

    public bool LmtEnabled => TimeZoneIndex == _enumIndexForLmt;
    public SolidColorBrush GeoLatValid => IsGeoLatValid() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush GeoLongValid => IsGeoLongValid() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush LmtGeoLongValid => IsLmtGeoLongValid() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush DateValid => IsDateValid() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush TimeValid => IsTimeValid() ? Brushes.White : Brushes.Yellow;

    
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
    private static void Help()
    {
        DataVault.Instance.CurrentViewBase = "RadixDataInput";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }
    
    [RelayCommand(CanExecute = nameof(IsInputOk))]
    private void Calculate()
    {
        _model.CreateChartData(NameId, Description, Source, LocationName, 
            ChartCategories.Unknown.ChartCategoryForIndex(CategoryIndex), RoddenRatings.Unknown.RoddenRatingForIndex(RatingIndex));
    }

    private bool IsInputOk()
    {
        if (GeoLat == string.Empty || GeoLong == string.Empty || Date == string.Empty ||
            Time == string.Empty) return false;
        if (_enumIndexForLmt == TimeZoneIndex && LmtGeoLong == string.Empty) return false;
        return IsGeoLatValid() && IsGeoLongValid() && IsLmtGeoLongValid() && IsDateValid() && IsTimeValid();
    }
    
    
    private bool IsGeoLatValid()
    {
        if (GeoLat == string.Empty) return true;
        Directions4GeoLat dir = DirLatIndex == 0 ? Directions4GeoLat.North : Directions4GeoLat.South; 
        return _model.IsGeoLatValid(GeoLat, dir);
    }
    
    private bool IsGeoLongValid()
    {
        if (GeoLong == string.Empty) return true;
        Directions4GeoLong dir = DirLongIndex == 0 ? Directions4GeoLong.East : Directions4GeoLong.West; 
        return _model.IsGeoLongValid(GeoLong, dir);
    }

    private bool IsLmtGeoLongValid()
    {
        if (LmtGeoLong == string.Empty) return true;
        Directions4GeoLong dir = LmtDirLongIndex == 0 ? Directions4GeoLong.East : Directions4GeoLong.West; 
        return _model.IsLmtGeoLongValid(LmtGeoLong, dir);
    }
    
    private bool IsDateValid()
    {
        if (Date == string.Empty) return true;
        Calendars cal = CalendarIndex == 0 ? Calendars.Gregorian : Calendars.Julian;
        YearCounts yCount = YearCounts.Astronomical.YearCountForIndex(YearCountIndex);
        return _model.IsDateValid(Date, cal, yCount);
    }
    
    private bool IsTimeValid()
    {
        TimeZones timeZone = TimeZones.Ut.TimeZoneForIndex(TimeZoneIndex);
        
        return Time == string.Empty || _model.IsTimeValid(Time, timeZone, ApplyDst);
    }
}