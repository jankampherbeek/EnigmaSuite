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

public partial class CalcHouseComparisonViewModel: ObservableObject
{
    [ObservableProperty] private int _houseIndex1;
    [ObservableProperty] private int _houseIndex2;
    [NotifyPropertyChangedFor(nameof(GeoLatValid))]
    [NotifyCanExecuteChangedFor(nameof(CompareCommand))]
    [NotifyPropertyChangedFor(nameof(DateValid))]
    [ObservableProperty] private string _date = "";
    [NotifyCanExecuteChangedFor(nameof(CompareCommand))]
    [ObservableProperty] private string _geoLat = "";
    [ObservableProperty] private ObservableCollection<string> _allQuadrantHouses1;
    [ObservableProperty] private ObservableCollection<string> _allQuadrantHouses2;
    private readonly CalcHouseComparisonModel _model = App.ServiceProvider.GetRequiredService<CalcHouseComparisonModel>();
 
    public SolidColorBrush GeoLatValid => IsGeoLatValid() ? Brushes.White : Brushes.Yellow; 
    public SolidColorBrush DateValid => IsDateValid() ? Brushes.White : Brushes.Yellow;

    public CalcHouseComparisonViewModel()
    {
        AllQuadrantHouses1 = new ObservableCollection<string>(_model.AllQuadrantHouses);
        AllQuadrantHouses2 = new ObservableCollection<string>(_model.AllQuadrantHouses);
    }
    
    [RelayCommand(CanExecute = nameof(IsInputOk))]
    private void Compare()
    {
        _model.CompareSystems(HouseSystemsExtensions.HouseSystemForIndex(HouseIndex1), 
            HouseSystemsExtensions.HouseSystemForIndex(HouseIndex2));
    }
    
    private bool IsInputOk()
    {
        if (GeoLat == string.Empty || Date == string.Empty) return false;
        return IsGeoLatValid() && IsDateValid();
    }
    
    
    private bool IsGeoLatValid()
    {
        if (GeoLat == string.Empty) return true;
        return _model.IsGeoLatValid(GeoLat);
    }
    
    private bool IsDateValid()
    {
        if (Date == string.Empty) return true;
        return _model.IsDateValid(Date);
    }
    

    
    [RelayCommand]
    private static void Help()
    {
        // todo create helppage for CalcHouseComparison
        DataVaultGeneral.Instance.CurrentViewBase = "CalcHouseComparison";
        new HelpWindow().ShowDialog();
    }
    
}