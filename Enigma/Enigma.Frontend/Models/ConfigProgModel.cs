// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Documents;
using Enigma.Api.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.ViewModels;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for progressive configuration.</summary>
public class ConfigProgModel
{
    private readonly IConfigurationApi _configApi;
    public double TransitOrb { get; }
    public double SecDirOrb { get; }
    public double PrimDirOrb { get; }
    public double SymDirOrb { get; }
    public int PrimDirTimeKeyIndex { get; }
    public int PrimDirMethodIndex { get; }
    public int SymDirTimeKeyIndex { get; }
    public int SolarMethodIndex { get; }
    public bool UseConversePrimDir { get; }
    public bool UseRelocation { get; }
    private ConfigProg _configProg;
    public ConfigProgModel(IConfigurationApi configApi)
    {
        _configApi = configApi;
        _configProg = CurrentConfig.Instance.GetConfigProg();
        TransitOrb = _configProg.ConfigTransits.Orb;
        SecDirOrb = _configProg.ConfigSecDir.Orb;
        PrimDirOrb = _configProg.ConfigPrimDir.Orb;
        SymDirOrb = _configProg.ConfigSymDir.Orb;
        PrimDirTimeKeyIndex = (int)_configProg.ConfigPrimDir.TimeKey;
        SymDirTimeKeyIndex = (int)_configProg.ConfigSymDir.TimeKey;
        PrimDirMethodIndex = (int)_configProg.ConfigPrimDir.DirMethod;
        SolarMethodIndex = (int)_configProg.ConfigSolar.SolarMethod;
        UseConversePrimDir = _configProg.ConfigPrimDir.IncludeConverse;
        UseRelocation = _configProg.ConfigSolar.Relocate;
    }
    
    public void UpdateConfig(ConfigProg configProg)
    {
        _configApi.WriteConfig(configProg);
        CurrentConfig.Instance.ChangeConfigProg(configProg);
        // TODO: clear exixting progressions
    }
    
    public List<string> AllPrimDirMethods()
    {
        return PrimaryDirMethodsExtensions.AllDetails().Select(method => method.MethodName).ToList();
    }
    
    public List<string> AllPrimDirKeys()
    {
        return PrimaryKeyExtensions.AllDetails().Select(key => key.Text).ToList();
    }

    public List<string> AllSymDirKeys()
    {
        return SymbolicKeyExtensions.AllDetails().Select(key => key.Text).ToList();
    }

    public List<string> AllSolarMethods()
    {
        return SolarMethodsExtensions.AllDetails().Select(key => key.MethodName).ToList();
    }

  //  public Dictionary<ChartPoints, ProgPointConfigSpecs> AllTransitPoints()
  //  {
  //      return _configProg.ConfigTransits.ProgPoints;
  //  }
    
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
    
    public List<ProgPoint> AllSignificators()
    {
        return (from point in PointsExtensions.AllDetails() 
            from configPoint in CurrentConfig.Instance.GetConfigProg().ConfigPrimDir.Significators 
            where configPoint.Key == point.Point 
            select new ProgPoint(point.Point, configPoint.Value.IsUsed, configPoint.Value.Glyph, point.Text)).ToList();
    }
    
    public List<ProgPoint> AllPromissors()
    {
        return (from point in PointsExtensions.AllDetails() 
            from configPoint in CurrentConfig.Instance.GetConfigProg().ConfigPrimDir.Promissors 
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