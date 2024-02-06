// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Api.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for progressive configuration.</summary>
public class ConfigProgModel
{
    private readonly IConfigurationApi _configApi;
    public double TransitOrb { get; }
    public double SecDirOrb { get; }
    public double SymDirOrb { get; }
    public int SymDirTimeKeyIndex { get; }

    private ConfigProg _configProg;
    public ConfigProgModel(IConfigurationApi configApi)
    {
        _configApi = configApi;
        _configProg = CurrentConfig.Instance.GetConfigProg();
        TransitOrb = _configProg.ConfigTransits.Orb;
        SecDirOrb = _configProg.ConfigSecDir.Orb;
        SymDirOrb = _configProg.ConfigSymDir.Orb;
        SymDirTimeKeyIndex = (int)_configProg.ConfigSymDir.TimeKey;
    }
    
    public void UpdateConfig(ConfigProg configProg)
    {
        _configApi.WriteDeltasForConfig(configProg);
        CurrentConfig.Instance.ChangeConfigProg(configProg);
        DataVaultCharts.Instance.ClearExistingCharts();
    }
    
    public static List<string> AllSymDirKeys()
    {
        return SymbolicKeyExtensions.AllDetails().Select(key => key.Text).ToList();
    }

    public static List<ProgPoint> AllTransitPoints()
    {
        return (from point in PointsExtensions.AllDetails() 
            from configPoint in CurrentConfig.Instance.GetConfigProg().ConfigTransits.ProgPoints 
            where configPoint.Key == point.Point 
            select new ProgPoint(point.Point, configPoint.Value.IsUsed, configPoint.Value.Glyph, point.Text)).ToList();
    }
    
    
    public static List<ProgPoint> AllSecDirPoints() 
    {
        return (from point in PointsExtensions.AllDetails() 
            from configPoint in CurrentConfig.Instance.GetConfigProg().ConfigSecDir.ProgPoints 
            where configPoint.Key == point.Point 
            select new ProgPoint(point.Point, configPoint.Value.IsUsed, configPoint.Value.Glyph, point.Text)).ToList();
    }
    
    public static List<ProgPoint> AllSymDirPoints() 
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