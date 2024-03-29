// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for configuration</summary>
public partial class ConfigurationViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = GeneralWindowsFlow.CONFIGURATION;
    private const string CONFIGURATION_SAVED = "The configuration was successfully saved.";    
    [ObservableProperty] private int _houseIndex;
    [NotifyPropertyChangedFor(nameof(AyanamshaEnabled))]
    [ObservableProperty] private int _zodiacTypeIndex;
    [ObservableProperty] private int _ayanamshaIndex;
    [ObservableProperty] private int _observerPositionIndex;
    [ObservableProperty] private int _projectionTypeIndex;
    [ObservableProperty] private int _orbMethodIndex;
    [NotifyPropertyChangedFor(nameof(BaseOrbAspectsValid))]
    [NotifyCanExecuteChangedFor(nameof(SaveConfigCommand))]
    [ObservableProperty] private string _baseOrbAspectsText;
    [NotifyPropertyChangedFor(nameof(BaseOrbMidpointsValid))]
    [NotifyCanExecuteChangedFor(nameof(SaveConfigCommand))]
    [ObservableProperty] private string _baseOrbMidpointsText;
    [ObservableProperty] private bool _applyAspectsToCusps;
    [ObservableProperty] private ObservableCollection<string> _allHouses;
    [ObservableProperty] private ObservableCollection<string> _allZodiacTypes;
    [ObservableProperty] private ObservableCollection<string> _allAyanamshas;
    [ObservableProperty] private ObservableCollection<string> _allObserverPositions;
    [ObservableProperty] private ObservableCollection<string> _allProjectionTypes;
    [ObservableProperty] private ObservableCollection<GeneralPoint> _allGeneralPoints;
    [ObservableProperty] private ObservableCollection<GeneralAspect> _allAspects;
    [ObservableProperty] private ObservableCollection<AspectColor> _allAspectColors;
    [ObservableProperty] private ObservableCollection<string> _allOrbMethods;

    
    
    private double _baseOrbAspectsValue;
    private double _baseOrbMidpointsValue;
    private bool _saveClicked;
    
    
    public SolidColorBrush BaseOrbAspectsValid => IsBaseOrbAspectsValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush BaseOrbMidpointsValid => IsBaseOrbMidpointsValid() ? Brushes.Gray : Brushes.Red;

    
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
        AllAspectColors = new ObservableCollection<AspectColor>(ConfigurationModel.AllAspectColors());
        AllAspects = new ObservableCollection<GeneralAspect>(ConfigurationModel.AllAspects());
        AllOrbMethods = new ObservableCollection<string>(ConfigurationModel.AllOrbMethods());
        
        HouseIndex = _model.HouseIndex;
        ZodiacTypeIndex = _model.ZodiacTypeIndex;
        AyanamshaIndex = _model.AyanamshaIndex;
        ObserverPositionIndex = _model.ObserverPositionIndex;
        _baseOrbAspectsValue = _model.AspectBaseOrb;
        ProjectionTypeIndex = _model.ProjectionTypeIndex;
        
        BaseOrbAspectsText = _baseOrbAspectsValue.ToString(CultureInfo.InvariantCulture);
        _baseOrbMidpointsValue = _model.MidpointBaseOrb;
        BaseOrbMidpointsText = _baseOrbMidpointsValue.ToString(CultureInfo.InvariantCulture);

    }
    


     private Dictionary<AspectTypes, AspectConfigSpecs> DefineAspectSpecs()
     {
         return AllAspects.ToDictionary(aspect => aspect.AspectType, 
             aspect => new AspectConfigSpecs(aspect.IsUsed, aspect.Glyph, aspect.OrbPercentage, aspect.ShowInChart));
     }

     private Dictionary<ChartPoints, ChartPointConfigSpecs> DefineChartPointSpecs()
     {
         return AllGeneralPoints.ToDictionary(point => point.ChartPoint, 
             point => new ChartPointConfigSpecs(point.IsUsed, point.Glyph, point.OrbPercentage, point.ShowInChart));
     }

     private Dictionary<AspectTypes, string> DefineAspectColorSpecs()
     {
         Dictionary<AspectTypes, string> allColors = new Dictionary<AspectTypes, string>();
         foreach (var aspectColor in AllAspectColors)
         {
             AspectTypes aspect = aspectColor.AspectType;
             string color = aspectColor.LineColor;
             allColors.Add(aspect, color);
         }
         return allColors;
     }
   
    
    [RelayCommand]
    private void SaveConfig()
    {
        _saveClicked = true;
        string errors = FindErrors();
        if (string.IsNullOrEmpty(errors))
        {
            HouseSystems houseSystem = HouseSystemsExtensions.HouseSystemForIndex(HouseIndex);
            Ayanamshas ayanamsha = AyanamshaExtensions.AyanamshaForIndex(AyanamshaIndex);
            ObserverPositions observerPosition = ObserverPositionsExtensions.ObserverPositionForIndex(ObserverPositionIndex);
            ZodiacTypes zodiacType = ZodiacTypeExtensions.ZodiacTypeForIndex(ZodiacTypeIndex);
            ProjectionTypes projectionType = ProjectionTypesExtensions.ProjectionTypeForIndex(ProjectionTypeIndex);
            const OrbMethods orbMethod = OrbMethods.Weighted;
            Dictionary<ChartPoints, ChartPointConfigSpecs> configChartPoints = DefineChartPointSpecs();
            Dictionary<AspectTypes, AspectConfigSpecs> configAspects = DefineAspectSpecs();
            Dictionary<AspectTypes, string> configAspectColors = DefineAspectColorSpecs();
            bool useCuspsForAspects = ApplyAspectsToCusps;
        
            AstroConfig config = new AstroConfig(houseSystem, ayanamsha, observerPosition, zodiacType, projectionType, orbMethod,
                configChartPoints, configAspects, configAspectColors, _baseOrbAspectsValue, _baseOrbMidpointsValue, useCuspsForAspects);
            _model.UpdateConfig(config);
            MessageBox.Show(CONFIGURATION_SAVED);
            Log.Information("ConfigurationViewModel.SaveConfig(): send ConfigUpdatedMessage and CloseMessage");
            WeakReferenceMessenger.Default.Send((new ConfigUpdatedMessage(VM_IDENTIFICATION)));
            WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
        }
        else
        {
            MessageBox.Show(errors, StandardTexts.TITLE_ERROR);
        }
    }

    private bool IsBaseOrbAspectsValid()
    {   
        if (string.IsNullOrEmpty(BaseOrbAspectsText) && !_saveClicked) return true; 
        return double.TryParse(BaseOrbAspectsText.Replace(',', '.'), NumberStyles.Any, 
            CultureInfo.InvariantCulture, out _baseOrbAspectsValue);
    }

    private bool IsBaseOrbMidpointsValid()
    {
        if (string.IsNullOrEmpty(BaseOrbMidpointsText) && !_saveClicked) return true;
        return double.TryParse(BaseOrbMidpointsText.Replace(',', '.'), NumberStyles.Any, 
            CultureInfo.InvariantCulture, out _baseOrbMidpointsValue);
    }

    private bool AreAspectLineColorsValid()
    {
        bool noErrors = true;
        List<string> supportedColors = new()
        {
            "YellowGreen",
            "Green",
            "SpringGreen",
            "Red",
            "Magenta",
            "Purple",
            "Blue",
            "DeepSkyBlue",
            "CornflowerBlue",
            "Gold",
            "Orange",
            "Gray",
            "Silver",
            "Black",
            "Goldenrod"
        };
        foreach (var aspectColor in AllAspectColors)
        {
            if (!supportedColors.Contains(aspectColor.LineColor))
            {
                noErrors = false;
            }
        }
        return noErrors;
    }
    
    private string FindErrors()
    {
        StringBuilder errorsText = new();
        if (!IsBaseOrbAspectsValid())
        {
            errorsText.Append(StandardTexts.ERROR_ORB_ASPECTS + EnigmaConstants.NEW_LINE);
        }
        if (!IsBaseOrbMidpointsValid())
        {
            errorsText.Append(StandardTexts.ERROR_ORB_MIDPOINTS + EnigmaConstants.NEW_LINE);
        }

        if (!AreAspectLineColorsValid())
        {
            errorsText.Append(StandardTexts.ERROR_ASPECT_COLORLINE + EnigmaConstants.NEW_LINE);
        }
        return errorsText.ToString();
    }
    
    [RelayCommand] private static void Close()
    {
        Log.Information("ConfigurationViewModel.Close(): send CloseMessage");
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand] private static void Help()
    {
        Log.Information("ConfigurationViewModel.Help(): send HelpMessage");
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
}