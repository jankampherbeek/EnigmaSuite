// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ConfigProgViewModel:ObservableObject
{
    [NotifyPropertyChangedFor(nameof(OrbSecDirValid))]
    [ObservableProperty] private string _orbSecDirText;
    [NotifyPropertyChangedFor(nameof(OrbSymDirValid))]
    [ObservableProperty] private string _orbSymDirText;
    [NotifyPropertyChangedFor(nameof(OrbTransitValid))]
    [ObservableProperty] private string _orbTransitText;
    private bool _includeConverseDirections;
    [ObservableProperty] private bool _applyRelocation;
    [ObservableProperty] private ObservableCollection<string> _allPrimDirMethods;
    [ObservableProperty] private ObservableCollection<string> _allSolarMethods;
    [ObservableProperty] private ObservableCollection<string> _allPrimDirKeys;
    [ObservableProperty] private ObservableCollection<string> _allSymDirKeys;
    [ObservableProperty] private ObservableCollection<ProgPoint> _allSignificators;
    [ObservableProperty] private ObservableCollection<ProgPoint> _allPromissors;    
    [ObservableProperty] private ObservableCollection<ProgPoint> _allTransitPoints;
    [ObservableProperty] private ObservableCollection<ProgPoint> _allSecDirPoints;
    [ObservableProperty] private ObservableCollection<ProgPoint> _allSymDirPoints;
    [ObservableProperty] private int _primDirMethodIndex;
    [ObservableProperty] private int _solarMethodIndex;
    [ObservableProperty] private int _primDirKeyIndex;
    [ObservableProperty] private int _symDirKeyIndex;
    
    private double _orbSecDirValue;
    private double _orbSymDirValue;
    private double _orbTransitValue;
    
    private readonly ConfigProgModel _model = App.ServiceProvider.GetRequiredService<ConfigProgModel>();
    
    public SolidColorBrush OrbSecDirValid => CheckOrbSecDir() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush OrbSymDirValid => CheckOrbSymDir() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush OrbTransitValid => CheckOrbTransit() ? Brushes.White : Brushes.Yellow;
    
    public ConfigProgViewModel()
    {
        AllPrimDirMethods = new ObservableCollection<string>(_model.AllPrimDirMethods());
        AllSolarMethods = new ObservableCollection<string>(_model.AllSolarMethods());
        AllPrimDirKeys = new ObservableCollection<string>(_model.AllPrimDirKeys());
        AllSymDirKeys =  new ObservableCollection<string>(_model.AllSymDirKeys());
        AllSignificators = new ObservableCollection<ProgPoint>(_model.AllSignificators());
        AllPromissors = new ObservableCollection<ProgPoint>(_model.AllPromissors());
        AllTransitPoints = new ObservableCollection<ProgPoint>(_model.AllTransitPoints());
        AllSecDirPoints = new ObservableCollection<ProgPoint>(_model.AllSecDirPoints());
        AllSymDirPoints = new ObservableCollection<ProgPoint>(_model.AllSymDirPoints());
        PrimDirKeyIndex = _model.PrimDirTimeKeyIndex;
        SymDirKeyIndex = _model.SymDirTimeKeyIndex;
        SolarMethodIndex = _model.SolarMethodIndex;
        PrimDirMethodIndex = _model.PrimDirMethodIndex;
        _orbSecDirValue = _model.SecDirOrb;
        _orbSymDirValue = _model.SymDirOrb;
        _orbTransitValue = _model.TransitOrb;
        OrbSecDirText = _orbSecDirValue.ToString((CultureInfo.InvariantCulture));
        OrbSymDirText = _orbSymDirValue.ToString((CultureInfo.InvariantCulture));
        OrbTransitText = _orbTransitValue.ToString((CultureInfo.InvariantCulture));
        _includeConverseDirections = false;
        ApplyRelocation = _model.UseRelocation;
    }
    
    private bool CheckOrbSecDir()
    {
        return double.TryParse(OrbSecDirText.Replace(',', '.'), NumberStyles.Any, 
            CultureInfo.InvariantCulture, out _orbSecDirValue);
    }
    
    private bool CheckOrbSymDir()
    {
        return double.TryParse(OrbSymDirText.Replace(',', '.'), NumberStyles.Any, 
            CultureInfo.InvariantCulture, out _orbSymDirValue);
    }
    
    private bool CheckOrbTransit()
    {
        return double.TryParse(OrbTransitText.Replace(',', '.'), NumberStyles.Any, 
            CultureInfo.InvariantCulture, out _orbTransitValue);
    }

    
    [RelayCommand]
    private void SaveConfig()
    {
        ConfigProgTransits configTransits = new(_orbTransitValue, AllTransitPoints.ToDictionary(point => point.ChartPoint, 
            point => new ProgPointConfigSpecs(point.IsUsed, point.Glyph)));
        ConfigProgSecDir configSecDir = new (_orbSecDirValue, AllSecDirPoints.ToDictionary(point => point.ChartPoint, 
            point => new ProgPointConfigSpecs(point.IsUsed, point.Glyph)));
        ConfigProgPrimDir configPrimDir = new(PrimaryKeyExtensions.PrimaryKeysForIndex(PrimDirKeyIndex), 
            PrimaryDirMethodsExtensions.MethodForIndex(PrimDirMethodIndex),
            _includeConverseDirections,
            AllPromissors.ToDictionary(point => point.ChartPoint,
                point => new ProgPointConfigSpecs(point.IsUsed, point.Glyph)),
            AllSignificators.ToDictionary(point => point.ChartPoint,
                point => new ProgPointConfigSpecs(point.IsUsed, point.Glyph)));
        ConfigProgSymDir configSymDir = new(_orbSymDirValue,
            SymbolicKeyExtensions.SymbolicKeysForIndex(SymDirKeyIndex),
            AllSymDirPoints.ToDictionary(point => point.ChartPoint, 
                point => new ProgPointConfigSpecs(point.IsUsed, point.Glyph)));
        ConfigProgSolar configSolar = new(SolarMethodsExtensions.MethodForIndex(SolarMethodIndex), ApplyRelocation); 
        ConfigProg configProg = new(configTransits, configSecDir, configPrimDir, configSymDir, configSolar); 
        _model.UpdateConfig(configProg);
    }
    
    
    [RelayCommand]
    private static void Help()
    {
        DataVaultGeneral.Instance.CurrentViewBase = "ConfigurationsProg";   // TODO create help file for prog configuration
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }
    
}