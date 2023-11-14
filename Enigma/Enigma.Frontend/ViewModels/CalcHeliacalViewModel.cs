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
using Enigma.Domain.Constants;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for heliacal calculations.</summary>
public partial class CalcHeliacalViewModel:ObservableObject
{

    private readonly CalcHeliacalModel _model = App.ServiceProvider.GetRequiredService<CalcHeliacalModel>();
    private bool _calculateClicked;
    [NotifyPropertyChangedFor(nameof(DateValid))]   
    [ObservableProperty] private string _date = "";
    [NotifyPropertyChangedFor(nameof(GeoLatValid))]    
    [ObservableProperty] private string _geoLat = "";
    [NotifyPropertyChangedFor(nameof(GeoLongValid))]        
    [ObservableProperty] private string _geoLong = "";
    [NotifyPropertyChangedFor(nameof(AltitudeValid))]        
    [ObservableProperty] private string _altitude = "";
    [NotifyPropertyChangedFor(nameof(CombinationCelBodyMethodValid))]        
    [ObservableProperty] private int _eventIndex;
    [NotifyPropertyChangedFor(nameof(CombinationCelBodyMethodValid))]        
    [ObservableProperty] private int _objectIndex;
    [ObservableProperty] private int _calendarIndex;
    [ObservableProperty] private int _dirLongIndex;
    [ObservableProperty] private int _dirLatIndex;
    [ObservableProperty] private ObservableCollection<string> _allHeliacalObjects;
    [ObservableProperty] private ObservableCollection<string> _allEventTypes;
    [ObservableProperty] private ObservableCollection<string> _allCalendars;
    [ObservableProperty] private ObservableCollection<string> _allDirectionsForLongitude;
    [ObservableProperty] private ObservableCollection<string> _allDirectionsForLatitude;
    public SolidColorBrush GeoLatValid => IsGeoLatValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush GeoLongValid => IsGeoLongValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush AltitudeValid => IsAltitudeValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush DateValid => IsDateValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush CombinationCelBodyMethodValid => IsCombinationCelBodyMethodValid() ? Brushes.Gray : Brushes.Red;

    public CalcHeliacalViewModel()
    {
        AllCalendars = new ObservableCollection<string>(_model.AllCalendars);
        AllDirectionsForLatitude = new ObservableCollection<string>(_model.AllDirectionsForLatitude);
        AllDirectionsForLongitude = new ObservableCollection<string>(_model.AllDirectionsForLongitude);
        AllEventTypes = new ObservableCollection<string>(_model.AllEventTypes);
        AllHeliacalObjects = new ObservableCollection<string>(_model.AllHeliacalObjects);

    }
    
    
    private bool IsDateValid()
    {
        if (string.IsNullOrEmpty(Date) && !_calculateClicked) return true;
        Calendars cal = CalendarIndex == 0 ? Calendars.Gregorian : Calendars.Julian;
        return _model.IsDateValid(Date, cal, YearCounts.Astronomical);
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
        return GeoLong != string.Empty && _model.IsGeoLongValid(GeoLong, dir);
    }
    
    private bool IsAltitudeValid()
    {
        if (string.IsNullOrEmpty(Altitude) && !_calculateClicked) return true;
        return _model.IsAltitudeValid(Altitude);
    }

    private bool IsCombinationCelBodyMethodValid()
    {
        return (EventIndex <= 2) || (ObjectIndex <= 2);
    }
    
    [RelayCommand]
    private void Calculate()
    {
        _calculateClicked = true;
        string errors = FindErrors();
        if (string.IsNullOrEmpty(errors))
        {
            _model.CalcHeliacalEvent(HeliacalEventTypesExtensions.EventTypeForIndex(EventIndex), 
                HeliacalObjectsExtensions.HeliacalObjectForIndex(ObjectIndex));       
        }
        else
            MessageBox.Show(errors,  StandardTexts.TITLE_ERROR);
    }
    
    private string FindErrors()
    {
        StringBuilder errorsText = new();
        if (!IsDateValid())
            errorsText.Append(StandardTexts.ERROR_DATE + EnigmaConstants.NEW_LINE);
        if (!IsGeoLatValid())
            errorsText.Append(StandardTexts.ERROR_GEOGRAPHIC_LATITUDE + EnigmaConstants.NEW_LINE);
        if (!IsGeoLongValid())
            errorsText.Append(StandardTexts.ERROR_GEOGRAPHIC_LONGITUDE + EnigmaConstants.NEW_LINE);
        if (!IsAltitudeValid())
            errorsText.Append(StandardTexts.ERROR_OBSERVER_ALTITUDE + EnigmaConstants.NEW_LINE);
        if (!IsCombinationCelBodyMethodValid())
            errorsText.Append(StandardTexts.ERROR_HELIACAL_COMBI_CELBODY_METHOD + EnigmaConstants.NEW_LINE);
        return errorsText.ToString();
    }
    
    [RelayCommand]
    private static void Help()
    {
        DataVaultGeneral.Instance.CurrentViewBase = "CalcHeliacal";
        new HelpWindow().ShowDialog();
    }
    
    
}