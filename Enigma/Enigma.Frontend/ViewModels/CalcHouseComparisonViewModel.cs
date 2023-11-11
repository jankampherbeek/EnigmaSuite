// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
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

public partial class CalcHouseComparisonViewModel: ObservableObject
{
    private const string ERROR_HOUSE_SYSTEM_SELECTION = "Select two different housesystems to compare.";
    private const string ERROR_GEOGRAPHIC_LATITUDE = "Enter a correct value for the geographic latitude.";
    private const string ERROR_DATE = "Enter a correct value for the date.";
    private const string TITLE_ERROR = "Something is wrong.";

    [ObservableProperty] private string _date = "";
    [ObservableProperty] private string _geoLat = "";
    [ObservableProperty] private ObservableCollection<string> _allQuadrantHouses1;
    [ObservableProperty] private ObservableCollection<string> _allQuadrantHouses2;
    [ObservableProperty] private int _houseIndex1 = 0;
    [ObservableProperty] private int _houseIndex2 = 3;
    
    private readonly CalcHouseComparisonModel _model = App.ServiceProvider.GetRequiredService<CalcHouseComparisonModel>();
    
    public CalcHouseComparisonViewModel()
    {
        AllQuadrantHouses1 = new ObservableCollection<string>(_model.AllQuadrantHouses);
        AllQuadrantHouses2 = new ObservableCollection<string>(_model.AllQuadrantHouses);
    }
    

    [RelayCommand]
    private void Compare()
    {
        string errors = FindErrors();
        if (string.IsNullOrEmpty(errors)) 
            _model.CompareSystems(HouseSystemsExtensions.HouseSystemForIndex(HouseIndex1), 
                HouseSystemsExtensions.HouseSystemForIndex(HouseIndex2));            
        else MessageBox.Show(errors, TITLE_ERROR);
    }

    private string FindErrors()
    {
        StringBuilder errorsText = new();
        if (!IsHouseSystemSelectionValid()) 
            errorsText.Append(ERROR_HOUSE_SYSTEM_SELECTION + EnigmaConstants.NEW_LINE);
        if (!IsGeoLatValid()) 
            errorsText.Append(ERROR_GEOGRAPHIC_LATITUDE + EnigmaConstants.NEW_LINE);
        if (!IsDateValid()) 
            errorsText.Append(ERROR_DATE + EnigmaConstants.NEW_LINE);
        return errorsText.ToString();
    }
    
    private bool IsGeoLatValid()
    {
        return GeoLat != string.Empty && _model.IsGeoLatValid(GeoLat);
    }
    
    private bool IsDateValid()
    {
        return Date != string.Empty && _model.IsDateValid(Date);
    }

    private bool IsHouseSystemSelectionValid()
    {
        return HouseIndex1 != HouseIndex2;
    }

    
    [RelayCommand]
    private static void Help()
    {
        // todo create helppage for CalcHouseComparison
        DataVaultGeneral.Instance.CurrentViewBase = "CalcHouseComparison";
        new HelpWindow().ShowDialog();
    }
    
}