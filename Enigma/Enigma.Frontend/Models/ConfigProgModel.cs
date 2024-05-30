// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Api;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for progressive configuration.</summary>
public class ConfigProgModel
{
    private Rosetta _rosetta = Rosetta.Instance;
    private readonly IConfigurationApi _configApi;
    public double TransitOrb { get; }
    public double SecDirOrb { get; }
    public double SymDirOrb { get; }
    public int SymDirTimeKeyIndex { get; }
    public int PdMethodIndex { get; }
    public int PdApproachIndex { get; }
    public int PdTimeKeyIndex { get; }
    public int PdConverseIndex { get; }
    public int PdLatAspectsIndex { get; }

    private ConfigProg _configProg;
    public ConfigProgModel(IConfigurationApi configApi)
    {
        _configApi = configApi;
        _configProg = CurrentConfig.Instance.GetConfigProg();
        TransitOrb = _configProg.ConfigTransits.Orb;
        SecDirOrb = _configProg.ConfigSecDir.Orb;
        SymDirOrb = _configProg.ConfigSymDir.Orb;
        SymDirTimeKeyIndex = (int)_configProg.ConfigSymDir.TimeKey;
        PdMethodIndex = (int)_configProg.ConfigPrimDir.Method;
        PdApproachIndex = (int)_configProg.ConfigPrimDir.Approach;
        PdTimeKeyIndex = (int)_configProg.ConfigPrimDir.TimeKey;
        PdConverseIndex = (int)_configProg.ConfigPrimDir.ConverseOption;
        PdLatAspectsIndex = (int)_configProg.ConfigPrimDir.LatAspOptions;
    }
    
    public void UpdateConfig(ConfigProg configProg)
    {
        _configApi.WriteDeltasForConfig(configProg);
        CurrentConfig.Instance.ChangeConfigProg(configProg);
        DataVaultCharts.Instance.ClearExistingCharts();
    }
    
    public List<string> AllSymDirKeys()
    {
        return SymbolicKeyExtensions.AllDetails().Select(key => _rosetta.GetText(key.RbKey)).ToList();
    }

    public List<string> AllPdMethods()
    {
        return PrimDirMethodsExtensions.AllDetails().Select(item => _rosetta.GetText(item.RbKey)).ToList();
    }
    
    public List<string> AllPdTimeKeys()
    {
        return PrimDirTimeKeysExtensions.AllDetails().Select(item => _rosetta.GetText(item.RbKey)).ToList();
    }
    
    public List<string> AllPdLatAspects()
    {
        return PrimDirLatAspOptionsExtensions.AllDetails().Select(item => _rosetta.GetText(item.RbKey)).ToList();
    }
    
    public List<string> AllPdConverseOptions()
    {
        return PrimDirConverseOptionsExtensions.AllDetails().Select(item => _rosetta.GetText(item.RbKey)).ToList();
    }

    public List<string> AllPdApproaches()
    {
        return PrimDirApproachesExtensions.AllDetails().Select(item => _rosetta.GetText(item.RbKey)).ToList();
    }
    
    public List<ProgPoint> AllTransitPoints()
    {
        return (from point in PointsExtensions.AllDetails() 
            from configPoint in CurrentConfig.Instance.GetConfigProg().ConfigTransits.ProgPoints 
            where configPoint.Key == point.Point 
            select new ProgPoint(point.Point, configPoint.Value.IsUsed, configPoint.Value.Glyph, point.Text)).ToList();
    }
    
    
    public List<ProgPoint> AllSecDirPoints() 
    {
        return (from point in PointsExtensions.AllDetails() 
            from configPoint in CurrentConfig.Instance.GetConfigProg().ConfigSecDir.ProgPoints 
            where configPoint.Key == point.Point 
            select new ProgPoint(point.Point, configPoint.Value.IsUsed, configPoint.Value.Glyph, point.Text)).ToList();
    }
    
    public List<ProgPoint> AllSymDirPoints() 
    {
        return (from point in PointsExtensions.AllDetails() 
            from configPoint in CurrentConfig.Instance.GetConfigProg().ConfigSymDir.ProgPoints 
            where configPoint.Key == point.Point 
            select new ProgPoint(point.Point, configPoint.Value.IsUsed, configPoint.Value.Glyph, point.Text)).ToList();
    }
    
    public List<ProgPoint> AllSignificators()
    {
        return (from point in PointsExtensions.AllDetails() 
            from configPoint in _configProg.ConfigPrimDir.Significators 
            where configPoint.Key == point.Point 
            select new ProgPoint(point.Point, configPoint.Value.IsUsed, configPoint.Value.Glyph, point.Text)).ToList();
    }
    
    public List<ProgPoint> AllPromissors()
    {
        return (from point in PointsExtensions.AllDetails() 
            from configPoint in _configProg.ConfigPrimDir.Promissors 
            where configPoint.Key == point.Point 
            select new ProgPoint(point.Point, configPoint.Value.IsUsed, configPoint.Value.Glyph, point.Text)).ToList();
    }

    public List<ProgAspect> AllAspects()
    {
        return (from aspect in AspectTypesExtensions.AllDetails()
                from configAspect in _configProg.ConfigPrimDir.Aspects
                where configAspect.Key == aspect.Aspect
                select new ProgAspect(aspect.Aspect, configAspect.Value.IsUsed, configAspect.Value.Glyph, 
                    _rosetta.GetText(aspect.RbKey))).ToList();   
    }
    
}

    
public class ProgPoint
{
    public ChartPoints ChartPoint { get; }
    public bool IsUsed { get; set; }
    public char Glyph { get; set; }
    public string PointName { get; set; }


    public ProgPoint(ChartPoints chartPoint, bool isUsed, char glyph, string pointName)
    {
        ChartPoint = chartPoint;
        IsUsed = isUsed;
        Glyph = glyph;
        PointName = pointName;
    }

}

public class ProgAspect
{
    public AspectTypes Aspect { get; }
    public bool IsUsed { get; set; }
    public char Glyph { get; set; }
    public string AspectName { get; set; }

    public ProgAspect(AspectTypes aspect, bool isUsed, char glyph, string aspectName)
    {
        Aspect = aspect;
        IsUsed = isUsed;
        Glyph = glyph;
        AspectName = aspectName;
    }
    
    
}