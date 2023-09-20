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
using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.References;
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

    [ObservableProperty] private bool _applyAspectsToCusps;

    [ObservableProperty] private ObservableCollection<string> _allHouses;
    [ObservableProperty] private ObservableCollection<string> _allZodiacTypes;
    [ObservableProperty] private ObservableCollection<string> _allAyanamshas;
    [ObservableProperty] private ObservableCollection<string> _allObserverPositions;
    [ObservableProperty] private ObservableCollection<string> _allProjectionTypes;
    [ObservableProperty] private ObservableCollection<GeneralPoint> _allGeneralPoints;
    [ObservableProperty] private ObservableCollection<GeneralAspect> _allAspects;
    [ObservableProperty] private ObservableCollection<string> _allOrbMethods;

    
    private double _baseOrbAspectsValue;
    private double _baseOrbMidpointsValue;

    
    
    public SolidColorBrush BaseOrbAspectsValid => CheckBaseOrbAspects() ? Brushes.White : Brushes.Yellow;
    public SolidColorBrush BaseOrbMidpointsValid => CheckBaseOrbMidpoints() ? Brushes.White : Brushes.Yellow;


    
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

        
        HouseIndex = _model.HouseIndex;
        ZodiacTypeIndex = _model.ZodiacTypeIndex;
        AyanamshaIndex = _model.AyanamshaIndex;
        ObserverPositionIndex = _model.ObserverPositionIndex;
        _baseOrbAspectsValue = _model.AspectBaseOrb;
        BaseOrbAspectsText = _baseOrbAspectsValue.ToString(CultureInfo.InvariantCulture);
        _baseOrbMidpointsValue = _model.MidpointBaseOrb;
        BaseOrbMidpointsText = _baseOrbMidpointsValue.ToString(CultureInfo.InvariantCulture);

    }
    
    private bool CheckBaseOrbAspects() {
        return double.TryParse(BaseOrbAspectsText.Replace(',', '.'), out _baseOrbAspectsValue);
    }
    
    private bool CheckBaseOrbMidpoints()
    {
        return double.TryParse(BaseOrbMidpointsText.Replace(',', '.'), out _baseOrbMidpointsValue);
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