// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Api.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for configuration</summary>
public class ConfigurationModel
{
    private readonly IConfigurationApi _configApi;

    public int HouseIndex { get; }
    public int ZodiacTypeIndex { get; }
    public int AyanamshaIndex { get; }
    public int ObserverPositionIndex { get; }
    public int ProjectionTypeIndex { get; set; }
    public double AspectBaseOrb { get; }
    public double MidpointBaseOrb { get; }
    
    public ConfigurationModel(IConfigurationApi configApi)
    {
        _configApi = configApi;
        AstroConfig currentConfig = CurrentConfig.Instance.GetConfig();
        HouseIndex =  (int)currentConfig.HouseSystem;
        ZodiacTypeIndex = (int)currentConfig.ZodiacType;
        AyanamshaIndex = (int)currentConfig.Ayanamsha;
        ObserverPositionIndex = (int)currentConfig.ObserverPosition;
        ProjectionTypeIndex = (int)currentConfig.ProjectionType;
        AspectBaseOrb = currentConfig.BaseOrbAspects;
        MidpointBaseOrb = currentConfig.BaseOrbMidpoints;
    }

    public void UpdateConfig(AstroConfig astroConfig)
    {
        _configApi.WriteConfig(astroConfig);
        CurrentConfig.Instance.ChangeConfig(astroConfig);
        DataVault.Instance.ClearExistingCharts();
    }

    public static List<string> AllHouses()
    {
        return HouseSystems.NoHouses.AllDetails().Select(detail => detail.Text).ToList();
    }

    public static List<string> AllZodiacTypes()
    {
        return ZodiacTypes.Tropical.AllDetails().Select(  detail => detail.Text).ToList();
    }

    public static List<string> AllAyanamshas()
    {
        return Ayanamshas.None.AllDetails().Select(detail => detail.Text).ToList();
    }

    public static List<string> AllObserverPositions()
    {
        return ObserverPositions.GeoCentric.AllDetails().Select(detail => detail.Text).ToList();
    }

    public static List<string> AllProjectionTypes()
    {
        return ProjectionTypes.TwoDimensional.AllDetails().Select(detail => detail.Text).ToList();
    }

    public static List<string> AllOrbMethods()
    {
        return OrbMethods.Weighted.AllDetails().Select(detail => detail.Text).ToList();
    }

    public static List<GeneralPoint> AllGeneralPoints()
    {
        return (from point in ChartPoints.None.AllDetails() 
                from configPoint in CurrentConfig.Instance.GetConfig().ChartPoints 
                where configPoint.Key == point.Point 
                select new GeneralPoint(point.Point, configPoint.Value.IsUsed, configPoint.Value.Glyph, point.Text, configPoint.Value.PercentageOrb)).ToList();
     }
    
    public static List<GeneralAspect> AllAspects()
    {
        return (from aspect in AspectTypes.Conjunction.AllDetails()
                from configAspect in CurrentConfig.Instance.GetConfig().Aspects
                where configAspect.Key == aspect.Aspect
                select new GeneralAspect(aspect.Aspect, configAspect.Value.IsUsed, configAspect.Value.Glyph, aspect.Text, configAspect.Value.PercentageOrb)).ToList();
    }

}

public class GeneralPoint
{
    public ChartPoints ChartPoint { get; }
    public bool IsUsed { get; set; }
    public char Glyph { get; set; } 
    public string PointName { get; set; }
    public int OrbPercentage { get; set; }

    public GeneralPoint(ChartPoints chartPoint, bool isUsed, char glyph, string pointName, int orbPercentage)
    {
        ChartPoint = chartPoint;
        IsUsed = isUsed;
        Glyph = glyph;
        PointName = pointName;
        OrbPercentage = orbPercentage;
    }
}

public class GeneralAspect
{
    public AspectTypes AspectType {get; }
    public bool IsUsed { get; set; }
    public char Glyph { get; set; } 
    public string AspectName { get; set; }
    public int OrbPercentage { get; set; }

    public GeneralAspect(AspectTypes aspectType, bool isUsed, char glyph, string aspectName, int orbPercentage)
    {
        AspectType = aspectType;
        IsUsed = isUsed;
        Glyph = glyph;
        AspectName = aspectName;
        OrbPercentage = orbPercentage;
    }
}