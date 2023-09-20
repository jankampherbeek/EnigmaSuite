// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ProgEventViewModel: ObservableObject
{
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

    
    public bool LmtEnabled => TimeZoneIndex == _enumIndexForLmt;
    public SolidColorBrush GeoLatValid => IsGeoLatValid() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush GeoLongValid => IsGeoLongValid() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush LmtGeoLongValid => IsLmtGeoLongValid() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush DateValid => IsDateValid() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush TimeValid => IsTimeValid() ? Brushes.White : Brushes.Yellow;
    
    public ProgEventViewModel()
    {
        AllDirectionsForLatitude = new ObservableCollection<string>(_model.AllDirectionsForLatitude);
        AllDirectionsForLongitude = new ObservableCollection<string>(_model.AllDirectionsForLongitude);
        AllLmtDirectionsForLongitude  = new ObservableCollection<string>(_model.AllDirectionsForLongitude);
        AllTimeZones = new ObservableCollection<string>(_model.AllTimeZones);
        _enumIndexForLmt = (int)TimeZones.Lmt;        
    }
    
    [RelayCommand(CanExecute = nameof(IsInputOk))]
    private void FinalizeEvent()
    {
        _model.CreateEventData(Description, LocationName);
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
        return _model.IsDateValid(Date, Calendars.Gregorian, YearCounts.CE);
    }
    
    private bool IsTimeValid()
    {
        TimeZones timeZone = TimeZonesExtensions.TimeZoneForIndex(TimeZoneIndex);
        
        return Time == string.Empty || _model.IsTimeValid(Time, timeZone, ApplyDst);
    }
    
    
    [RelayCommand]
    private static void Help()
    {
        DataVault.Instance.CurrentViewBase = "RadixEventInput";    // TODO create helppage
        new HelpWindow().ShowDialog();
    }
    
}