// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for configuration</summary>
public partial class ConfigurationViewModel: ObservableObject
{
    [ObservableProperty] private int _houseIndex;
    [NotifyPropertyChangedFor(nameof(AyanamshaEnabled))]
    [ObservableProperty] private int _zodiacTypeIndex;
    [ObservableProperty] private int _ayanamshaIndex;
    [ObservableProperty] private int _observerPositionIndex;
    [ObservableProperty] private int _projectionTypeIndex;
    [ObservableProperty] private int _orbMethodIndex;
    [ObservableProperty] private int _primDirMethodIndex;
    [ObservableProperty] private int _primDirKeyIndex;
    [NotifyPropertyChangedFor(nameof(BaseOrbAspectsValid))]
    [ObservableProperty] private string _baseOrbAspectsText;
    [NotifyPropertyChangedFor(nameof(BaseOrbMidpointsValid))]
    [ObservableProperty] private string _baseOrbMidpointsText;
    [NotifyPropertyChangedFor(nameof(OrbPrimDirValid))]
    [ObservableProperty] private string _orbPrimDirText;
    [ObservableProperty] private bool _applyAspectsToCusps;
    [ObservableProperty] private bool _includeConverseDirections;
    [ObservableProperty] private ObservableCollection<string> _allHouses;
    [ObservableProperty] private ObservableCollection<string> _allZodiacTypes;
    [ObservableProperty] private ObservableCollection<string> _allAyanamshas;
    [ObservableProperty] private ObservableCollection<string> _allObserverPositions;
    [ObservableProperty] private ObservableCollection<string> _allProjectionTypes;
    [NotifyPropertyChangedFor(nameof(UpdateAvailableProgPoints))]
    [ObservableProperty] private ObservableCollection<GeneralPoint> _allGeneralPoints;
    [ObservableProperty] private ObservableCollection<GeneralAspect> _allAspects;
    [ObservableProperty] private ObservableCollection<string> _allOrbMethods;
    [ObservableProperty] private ObservableCollection<string> _allPrimDirMethods;
    [ObservableProperty] private ObservableCollection<string> _allPrimDirKeys;
    [ObservableProperty] private ObservableCollection<SelectableChartPointDetails> _allSignificators;
    
    private double _baseOrbAspectsValue;
    private double _baseOrbMidpointsValue;
    private double _orbPrimDirValue;
    
    
    public SolidColorBrush BaseOrbAspectsValid => CheckBaseOrbAspects() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush BaseOrbMidpointsValid => CheckBaseOrbMidpoints() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush OrbPrimDirValid => CheckOrbPrimDir() ? Brushes.White : Brushes.Yellow;
    public bool UpdateAvailableProgPoints => PerformUpdateProgPoints();

    private bool PerformUpdateProgPoints()
    {
        AllSignificators = new ObservableCollection<SelectableChartPointDetails>(ConfigurationModel.AllSignificators(AllGeneralPoints));
        return true;
    }
    
    private readonly ConfigurationModel _model = App.ServiceProvider.GetRequiredService<ConfigurationModel>();

    public bool AyanamshaEnabled => ZodiacTypeIndex == (int)ZodiacTypes.Sidereal;

    public ConfigurationViewModel()
    {
        AllHouses = new ObservableCollection<string>(ConfigurationModel.AllHouses());
        AllZodiacTypes = new ObservableCollection<string>(ConfigurationModel.AllZodiacTypes());
        AllAyanamshas = new ObservableCollection<string>(ConfigurationModel.AllAyanamshas());
        AllObserverPositions = new ObservableCollection<string>(ConfigurationModel.AllObserverPositions());
        AllProjectionTypes = new ObservableCollection<string>(ConfigurationModel.AllProjectionTypes());
        AllGeneralPoints = new ObservableCollection<GeneralPoint>(ConfigurationModel.AllGeneralPoints());
        AllAspects = new ObservableCollection<GeneralAspect>(ConfigurationModel.AllAspects());
        AllOrbMethods = new ObservableCollection<string>(ConfigurationModel.AllOrbMethods());
        AllPrimDirMethods = new ObservableCollection<string>(ConfigurationModel.AllPrimDirMethods());
        AllPrimDirKeys = new ObservableCollection<string>(ConfigurationModel.AllPrimDirKeys());
        AllSignificators = new ObservableCollection<SelectableChartPointDetails>(ConfigurationModel.AllSignificators(AllGeneralPoints));
        
        HouseIndex = _model.HouseIndex;
        ZodiacTypeIndex = _model.ZodiacTypeIndex;
        AyanamshaIndex = _model.AyanamshaIndex;
        ObserverPositionIndex = _model.ObserverPositionIndex;
        _baseOrbAspectsValue = _model.AspectBaseOrb;
        BaseOrbAspectsText = _baseOrbAspectsValue.ToString(CultureInfo.InvariantCulture);
        _baseOrbMidpointsValue = _model.MidpointBaseOrb;
        BaseOrbMidpointsText = _baseOrbMidpointsValue.ToString(CultureInfo.InvariantCulture);
        _orbPrimDirText = "1.0";   // TODO read from new version of config
        _orbPrimDirValue = 1.0;    // TODO read from new version of config
    }
    
    private bool CheckBaseOrbAspects() {
        return double.TryParse(BaseOrbAspectsText.Replace(',', '.'), out _baseOrbAspectsValue);
    }
    
    private bool CheckBaseOrbMidpoints()
    {
        return double.TryParse(BaseOrbMidpointsText.Replace(',', '.'), out _baseOrbMidpointsValue);
    }

    private bool CheckOrbPrimDir()
    {
        return double.TryParse(OrbPrimDirText.Replace(',', '.'), out _orbPrimDirValue);
    }

     private Dictionary<AspectTypes, AspectConfigSpecs> DefineAspectSpecs()
     {
         return AllAspects.ToDictionary(aspect => aspect.AspectType, 
             aspect => new AspectConfigSpecs(aspect.IsUsed, aspect.Glyph, aspect.OrbPercentage));
     }

     private Dictionary<ChartPoints, ChartPointConfigSpecs> DefineChartPointSpecs()
     {
         return AllGeneralPoints.ToDictionary(point => point.ChartPoint, 
             point => new ChartPointConfigSpecs(point.IsUsed, point.Glyph, point.OrbPercentage));
     }
    
    
    [RelayCommand]
    private void SaveConfig()
    {
        HouseSystems houseSystem = HouseSystemsExtensions.HouseSystemForIndex(HouseIndex);
        Ayanamshas ayanamsha = AyanamshaExtensions.AyanamshaForIndex(AyanamshaIndex);
        ObserverPositions observerPosition = ObserverPositionsExtensions.ObserverPositionForIndex(ObserverPositionIndex);
        ZodiacTypes zodiacType = ZodiacTypeExtensions.ZodiacTypeForIndex(ZodiacTypeIndex);
        ProjectionTypes projectionType = ProjectionTypesExtensions.ProjectionTypeForIndex(ProjectionTypeIndex);
        const OrbMethods orbMethod = OrbMethods.Weighted;
        Dictionary<ChartPoints, ChartPointConfigSpecs> configChartPoints = DefineChartPointSpecs();
        Dictionary<AspectTypes, AspectConfigSpecs> configAspects = DefineAspectSpecs();
        bool useCuspsForAspects = ApplyAspectsToCusps;
        
        AstroConfig config = new AstroConfig(houseSystem, ayanamsha, observerPosition, zodiacType, projectionType, orbMethod,
            configChartPoints, configAspects, _baseOrbAspectsValue, _baseOrbMidpointsValue, useCuspsForAspects);
        _model.UpdateConfig(config);
    }
    
    
    [RelayCommand]
    private static void Help()
    {
        DataVault.Instance.CurrentViewBase = "Configurations";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }
}