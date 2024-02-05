// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using System.Text;
using Enigma.Core.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Configuration;

/// <inheritdoc/>
public class ConfigurationDelta: IConfigurationDelta
{
    private readonly IDeltaTexts _deltaTexts;
    private const double DELTA = 0.000001;

    public ConfigurationDelta(IDeltaTexts deltaTexts)
    {
        _deltaTexts = deltaTexts;
    }
    
    /// <inheritdoc/>
    public Dictionary<string, string> RetrieveTextsForDeltas(AstroConfig defaultConfig, AstroConfig updatedConfig)
    {
        return CompareAndCreateDeltaTexts(defaultConfig, updatedConfig);
    }

    public Dictionary<string, string> RetrieveTextsForProgDeltas(ConfigProg defaultProgConfig,
        ConfigProg updatedProgConfig)
    {
        return CompareAndCreateProgDeltaTexts(defaultProgConfig, updatedProgConfig);
    }
    
    private Dictionary<string, string> CompareAndCreateDeltaTexts(AstroConfig defConf, AstroConfig newConf)
    {
        Dictionary<string, string> allDeltas = new();
        if (defConf.HouseSystem != newConf.HouseSystem) 
            allDeltas.Add(StandardTexts.CFG_HOUSE_SYSTEM, ((int)newConf.HouseSystem).ToString());
        if (defConf.Ayanamsha != newConf.Ayanamsha) 
            allDeltas.Add(StandardTexts.CFG_AYANAMSHA,((int)newConf.Ayanamsha).ToString());
        if (defConf.ObserverPosition != newConf.ObserverPosition) 
            allDeltas.Add(StandardTexts.CFG_OBSERVER_POSITION,((int)newConf.ObserverPosition).ToString());
        if (defConf.ZodiacType != newConf.ZodiacType) 
            allDeltas.Add(StandardTexts.CFG_ZODIAC_TYPE,((int)newConf.ZodiacType).ToString());
        if (defConf.ProjectionType != newConf.ProjectionType) 
            allDeltas.Add(StandardTexts.CFG_PROJECTION_TYPE,((int)newConf.ProjectionType).ToString());
        if (defConf.OrbMethod != newConf.OrbMethod) 
            allDeltas.Add(StandardTexts.CFG_ORB_METHOD,((int)newConf.OrbMethod).ToString());
        if (Math.Abs(defConf.BaseOrbAspects - newConf.BaseOrbAspects) > DELTA) 
            allDeltas.Add(StandardTexts.CFG_BASE_ORB_ASPECTS,((int)newConf.BaseOrbAspects).ToString());
        if (Math.Abs(defConf.BaseOrbMidpoints - newConf.BaseOrbMidpoints) > DELTA) 
            allDeltas.Add(StandardTexts.CFG_BASE_ORB_MIDPOINTS,((int)newConf.BaseOrbMidpoints).ToString());
        if (defConf.UseCuspsForAspects != newConf.UseCuspsForAspects) 
            allDeltas.Add(StandardTexts.CFG_USE_CUSPS_FOR_ASPECTS,(newConf.UseCuspsForAspects).ToString());
        foreach ((ChartPoints pointKey, ChartPointConfigSpecs? value) in defConf.ChartPoints)
        {
            bool found = newConf.ChartPoints.TryGetValue(pointKey, out ChartPointConfigSpecs? newPointValue);
            if (!found || newPointValue is null || newPointValue.Equals(value)) continue;
            Tuple<string, string> deltaForPoint = _deltaTexts.CreateDeltaForPoint(pointKey, newPointValue);
            allDeltas.Add(deltaForPoint.Item1, deltaForPoint.Item2);
        }
        foreach ((AspectTypes aspectKey, AspectConfigSpecs? value) in defConf.Aspects)
        {
            bool found = newConf.Aspects.TryGetValue(aspectKey, out AspectConfigSpecs? newAspectValue);
            if (!found || newAspectValue is null || newAspectValue.Equals(value)) continue;
            Tuple<string, string> deltaForAspect = _deltaTexts.CreateDeltaForAspect(aspectKey, newAspectValue);
            allDeltas.Add(deltaForAspect.Item1, deltaForAspect.Item2);
        }
        return allDeltas;
    }

    private Dictionary<string, string> CompareAndCreateProgDeltaTexts(ConfigProg defConf, ConfigProg newConf)
    {
        Dictionary<string, string> allDeltas = new();
        if ((Math.Abs(defConf.ConfigTransits.Orb - newConf.ConfigTransits.Orb)) > DELTA ) 
            allDeltas.Add("TR_ORB", newConf.ConfigTransits.Orb.ToString(CultureInfo.InvariantCulture));
        if ((Math.Abs(defConf.ConfigSecDir.Orb - newConf.ConfigSecDir.Orb)) > DELTA ) 
            allDeltas.Add("SC_ORB", newConf.ConfigSecDir.Orb.ToString(CultureInfo.InvariantCulture));
        if ((Math.Abs(defConf.ConfigSymDir.Orb - newConf.ConfigSymDir.Orb)) > DELTA ) 
            allDeltas.Add("SM_ORB", newConf.ConfigSymDir.Orb.ToString(CultureInfo.InvariantCulture));
        if (defConf.ConfigSymDir.TimeKey != newConf.ConfigSymDir.TimeKey) 
            allDeltas.Add("SM_KEY", ((int)newConf.ConfigSymDir.TimeKey).ToString());
        foreach ((ChartPoints pointKey, ProgPointConfigSpecs? value) in defConf.ConfigTransits.ProgPoints)
        {
            bool found = newConf.ConfigTransits.ProgPoints.TryGetValue(pointKey, out ProgPointConfigSpecs? newPointValue);
            if (!found || newPointValue is null || newPointValue.Equals(value)) continue;
            Tuple<string, string> deltaForPoint = _deltaTexts.CreateDeltaForProgChartPoint(
                ProgresMethods.Transits, pointKey, newPointValue);
            allDeltas.Add(deltaForPoint.Item1, deltaForPoint.Item2);
        }
        foreach ((ChartPoints pointKey, ProgPointConfigSpecs? value) in defConf.ConfigSecDir.ProgPoints)
        {
            bool found = newConf.ConfigSecDir.ProgPoints.TryGetValue(pointKey, out ProgPointConfigSpecs? newPointValue);
            if (!found || newPointValue is null || newPointValue.Equals(value)) continue;
            Tuple<string, string> deltaForPoint = _deltaTexts.CreateDeltaForProgChartPoint(
                ProgresMethods.Secondary, pointKey, newPointValue);
            allDeltas.Add(deltaForPoint.Item1, deltaForPoint.Item2);
        }
        foreach ((ChartPoints pointKey, ProgPointConfigSpecs? value) in defConf.ConfigSymDir.ProgPoints)
        {
            bool found = newConf.ConfigSymDir.ProgPoints.TryGetValue(pointKey, out ProgPointConfigSpecs? newPointValue);
            if (!found || newPointValue is null || newPointValue.Equals(value)) continue;
            Tuple<string, string> deltaForPoint = _deltaTexts.CreateDeltaForProgChartPoint(
                ProgresMethods.Symbolic, pointKey, newPointValue);
            allDeltas.Add(deltaForPoint.Item1, deltaForPoint.Item2);
        }
        return allDeltas;
    }
    
}

/// <inheritdoc/>
public class DeltaTexts: IDeltaTexts
{
    private const string SEPARATOR = "||";
    
    /// <inheritdoc/>
    public Tuple<string, string> CreateDeltaForPoint(ChartPoints point, ChartPointConfigSpecs? pointSpecs)
    {
        const string prefix = "CP_";
        StringBuilder keyTxt = new();
        StringBuilder valueTxt = new();
        if (pointSpecs is null) return new Tuple<string, string>(keyTxt.ToString(), valueTxt.ToString());
        keyTxt.Append(prefix);
        keyTxt.Append((int)point);
        valueTxt.Append(pointSpecs.IsUsed ? "y" : "n");
        valueTxt.Append(SEPARATOR);
        valueTxt.Append((pointSpecs.Glyph));
        valueTxt.Append(SEPARATOR);
        valueTxt.Append(pointSpecs.PercentageOrb);
        valueTxt.Append(SEPARATOR);
        valueTxt.Append(pointSpecs.ShowInChart ? "y" : "n");
        return new Tuple<string, string>(keyTxt.ToString(), valueTxt.ToString());
    }

    /// <inheritdoc/>
    public Tuple<string, string> CreateDeltaForAspect(AspectTypes aspect, AspectConfigSpecs? aspectSpecs)
    {
        const string prefix = "AT_";
        StringBuilder keyTxt = new("");
        StringBuilder valueTxt = new("");
        if (aspectSpecs is null) return new Tuple<string, string>(keyTxt.ToString(), valueTxt.ToString());
        keyTxt.Append(prefix);
        keyTxt.Append((int)aspect);
        valueTxt.Append(aspectSpecs.IsUsed ? "y" : "n");
        valueTxt.Append(SEPARATOR);
        valueTxt.Append((aspectSpecs.Glyph));        
        valueTxt.Append(SEPARATOR);
        valueTxt.Append(aspectSpecs.PercentageOrb);
        valueTxt.Append(SEPARATOR);
        valueTxt.Append(aspectSpecs.ShowInChart ? "y" : "n");
        return new Tuple<string, string>(keyTxt.ToString(), valueTxt.ToString());
    }

    public Tuple<string, string> CreateDeltaForProgChartPoint(ProgresMethods progresMethod, ChartPoints point,
        ProgPointConfigSpecs? pointSpecs)
    {
        string prefix = progresMethod switch
        {
            ProgresMethods.Transits => "TR_CP_",
            ProgresMethods.Secondary => "SC_CP_",
            ProgresMethods.Symbolic => "SM_CP_",
            _ => ""
        };
        StringBuilder keyTxt = new();
        StringBuilder valueTxt = new();
        if (pointSpecs is null) return new Tuple<string, string>(keyTxt.ToString(), valueTxt.ToString());
        keyTxt.Append(prefix);
        keyTxt.Append((int)point);
        valueTxt.Append(pointSpecs.IsUsed ? "y" : "n");
        valueTxt.Append(SEPARATOR);
        valueTxt.Append((pointSpecs.Glyph));
        return new Tuple<string, string>(keyTxt.ToString(), valueTxt.ToString());
    }

    public Tuple<string, string> CreateDeltaForProgOrb(ProgresMethods progresMethod, double orb)
    {
        string keyTxt = progresMethod switch
        {
            ProgresMethods.Transits => "TR_ORB",
            ProgresMethods.Secondary => "SC_ORB",
            ProgresMethods.Symbolic => "SM_ORB",
            _ => ""
        };
        string valueTxt = orb.ToString(CultureInfo.InvariantCulture);
        return new Tuple<string, string>(keyTxt, valueTxt);
    }

    public Tuple<string, string> CreateDeltaForProgSymKey(SymbolicKeys timeKey)
    {
        string keyTxt = "SM_KEY";
        string valueTxt = ((int)timeKey).ToString();
        return new Tuple<string, string>(keyTxt, valueTxt);        
    }
    
}