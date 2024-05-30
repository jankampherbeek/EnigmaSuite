// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using System.Text;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Configuration;

/// <summary>Helper for creating texts that describe the delta in a configuration.</summary>
public interface IDeltaTexts
{
    /// <summary>Handle the delta for a chart point.</summary>
    /// <param name="point">The chart point.</param>
    /// <param name="pointSpecs">Specifications for the updated chart point.</param>
    /// <returns>A key and value for the delta of a chart point.</returns>
    public Tuple<string, string> CreateDeltaForPoint(ChartPoints point, ChartPointConfigSpecs? pointSpecs);
    
    /// <summary>Handle the delta for an aspect.</summary>
    /// <param name="aspect">The aspect.</param>
    /// <param name="aspectSpecs">Specifications for the updated aspect.</param>
    /// <returns>A key and value for the delta of an aspect.</returns>
    public Tuple<string, string> CreateDeltaForAspect(AspectTypes aspect, AspectConfigSpecs? aspectSpecs);

    /// <summary>Handle the eelta for an aspect line color.</summary>
    /// <param name="aspect">The aspect.</param>
    /// <param name="color">Name of te color.</param>
    /// <returns>A key and value for the delta of an aspect line color.</returns>
    public Tuple<string, string> CreateDeltaForaspectcolor(AspectTypes aspect, string color);
    
    /// <summary>Handle the delta for a chartpoint for a specific progression method.</summary>
    /// <param name="progresMethod">The progression method.</param>
    /// <param name="point">The chart point.</param>
    /// <param name="pointSpecs">Specifications for the chart point.</param>
    /// <returns>A key and value for the delta of a chart point.</returns>
    public Tuple<string, string> CreateDeltaForProgChartPoint(ProgresMethods progresMethod, ChartPoints point,
        ProgPointConfigSpecs? pointSpecs);

    /// <summary>Handle the delta for a progressive orb.</summary>
    /// <param name="progresMethod">The progression method.</param>
    /// <param name="orb">Value for the orb.</param>
    /// <returns>A key and value for the orb.</returns>
    public Tuple<string, string> CreateDeltaForProgOrb(ProgresMethods progresMethod, double orb);

    /// <summary>Handle the delta for a timekey for symbolic directions.</summary>
    /// <param name="key">The key</param>
    /// <returns>Key and value for the timekey.</returns>
    public Tuple<string, string> CreateDeltaForProgSymKey(SymbolicKeys timeKey);

}



/// <summary>Handles the delta's between the default configuration and an updated configuration.</summary>
public interface IConfigurationDelta
{
    /// <summary>Create a dictionary with delta's that can be saved as the configuration delta file.</summary>
    /// <param name="defaultConfig">The default config.</param>
    /// <param name="updatedConfig">The config with the updates.</param>
    /// <returns>Dictionary with key values for configuration delta's.</returns>
    public Dictionary<string, string> RetrieveTextsForDeltas(AstroConfig defaultConfig, AstroConfig updatedConfig);

    /// <summary>Create a dictionary with delta's that can be saved as the configuration delta file for progressions.</summary>
    /// <param name="defaultProgConfig">The default config,</param>
    /// <param name="updatedProgConfig">The config with the updates.</param>
    /// <returns>Dictionary with key values for progressive configuration delta's.</returns>
    public Dictionary<string, string> RetrieveTextsForProgDeltas(ConfigProg defaultProgConfig,
        ConfigProg updatedProgConfig);
}


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

    /// <inheritdoc/>
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
            allDeltas.Add(StandardTexts.CFG_BASE_ORB_ASPECTS,((double)newConf.BaseOrbAspects).ToString(CultureInfo.InvariantCulture));
        if (Math.Abs(defConf.BaseOrbMidpoints - newConf.BaseOrbMidpoints) > DELTA) 
            allDeltas.Add(StandardTexts.CFG_BASE_ORB_MIDPOINTS,((double)newConf.BaseOrbMidpoints).ToString(CultureInfo.InvariantCulture));
        if (Math.Abs(defConf.OrbParallels - newConf.OrbParallels) > DELTA) 
            allDeltas.Add(StandardTexts.CFG_ORB_PARALLELS,((double)newConf.OrbParallels).ToString(CultureInfo.InvariantCulture));
        if (Math.Abs(defConf.OrbMidpointsDecl - newConf.OrbMidpointsDecl) > DELTA) 
            allDeltas.Add(StandardTexts.CFG_ORB_MIDPOINTS_DECL,((double)newConf.OrbMidpointsDecl).ToString(CultureInfo.InvariantCulture));
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
        foreach ((AspectTypes aspectKey, string color) in defConf.AspectColors)
        {
            bool found = newConf.AspectColors.TryGetValue(aspectKey, out string newColor);
            if (!found || string.IsNullOrEmpty(newColor) || newColor == color) continue;
            Tuple<string, string> deltaForAspectColor = _deltaTexts.CreateDeltaForaspectcolor(aspectKey, newColor);
            allDeltas.Add(deltaForAspectColor.Item1, deltaForAspectColor.Item2);
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
        if (defConf.ConfigPrimDir.Method != newConf.ConfigPrimDir.Method) 
            allDeltas.Add(StandardTexts.CFG_PD_METHOD,((int)newConf.ConfigPrimDir.Method).ToString());
        if (defConf.ConfigPrimDir.Method != newConf.ConfigPrimDir.Method) 
            allDeltas.Add(StandardTexts.CFG_PD_APPROACH,((int)newConf.ConfigPrimDir.Approach).ToString());
        if (defConf.ConfigPrimDir.Method != newConf.ConfigPrimDir.Method) 
            allDeltas.Add(StandardTexts.CFG_PD_TIMEKEY,((int)newConf.ConfigPrimDir.TimeKey).ToString());
        if (defConf.ConfigPrimDir.Method != newConf.ConfigPrimDir.Method) 
            allDeltas.Add(StandardTexts.CFG_PD_CONVERSE,((int)newConf.ConfigPrimDir.ConverseOption).ToString());
        if (defConf.ConfigPrimDir.Method != newConf.ConfigPrimDir.Method) 
            allDeltas.Add(StandardTexts.CFG_PD_LATASP,((int)newConf.ConfigPrimDir.LatAspOptions).ToString());
        
        
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
        
        foreach ((ChartPoints pointKey, ProgPointConfigSpecs? value) in defConf.ConfigPrimDir.Significators)
        {
            bool found = newConf.ConfigPrimDir.Significators.TryGetValue(pointKey, out ProgPointConfigSpecs? newPointValue);
            if (!found || newPointValue is null || newPointValue.Equals(value)) continue;
            Tuple<string, string> deltaForSignificator = _deltaTexts.CreateDeltaForProgChartPoint(
                ProgresMethods.Primary, pointKey, newPointValue);
            allDeltas.Add(deltaForSignificator.Item1.Replace("XX", StandardTexts.PCF_SIGNIFICATORS), deltaForSignificator.Item2);
        }
        foreach ((ChartPoints pointKey, ProgPointConfigSpecs? value) in defConf.ConfigPrimDir.Promissors)
        {
            bool found = newConf.ConfigPrimDir.Promissors.TryGetValue(pointKey, out ProgPointConfigSpecs? newPointValue);
            if (!found || newPointValue is null || newPointValue.Equals(value)) continue;
            Tuple<string, string> deltaForSignificator = _deltaTexts.CreateDeltaForProgChartPoint(
                ProgresMethods.Primary, pointKey, newPointValue);
            allDeltas.Add(deltaForSignificator.Item1.Replace("XX",StandardTexts.PCF_PROMISSORS), deltaForSignificator.Item2);
        }
        foreach ((AspectTypes aspectKey, AspectConfigSpecs? value) in defConf.ConfigPrimDir.Aspects)
        {
            bool found = newConf.ConfigPrimDir.Aspects.TryGetValue(aspectKey, out AspectConfigSpecs? newAspectValue);
            if (!found || newAspectValue is null || newAspectValue.Equals(value)) continue;
            Tuple<string, string> deltaForAspect = _deltaTexts.CreateDeltaForAspect(aspectKey, newAspectValue);
            allDeltas.Add(deltaForAspect.Item1, deltaForAspect.Item2.Replace("AT",StandardTexts.PCF_PDASPECTS));
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
        StringBuilder keyTxt = new();
        StringBuilder valueTxt = new();
        if (pointSpecs is null) return new Tuple<string, string>(keyTxt.ToString(), valueTxt.ToString());
        keyTxt.Append(StandardTexts.PCF_CHARTPOINTS);
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
        StringBuilder keyTxt = new("");
        StringBuilder valueTxt = new("");
        if (aspectSpecs is null) return new Tuple<string, string>(keyTxt.ToString(), valueTxt.ToString());
        keyTxt.Append(StandardTexts.PCF_ASPECTS);
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

    /// <inheritdoc/>
    public Tuple<string, string> CreateDeltaForaspectcolor(AspectTypes aspect, string color)
    {
        const string prefix = StandardTexts.PCF_ASPECTCOLOR;
        StringBuilder keyTxt = new("");
        StringBuilder valueTxt = new("");
        if (string.IsNullOrEmpty(color)) return new Tuple<string, string>(keyTxt.ToString(), valueTxt.ToString());
        keyTxt.Append(prefix);
        keyTxt.Append((int)aspect);
        valueTxt.Append(color);
        return new Tuple<string, string>(keyTxt.ToString(), valueTxt.ToString());
    }
    
    public Tuple<string, string> CreateDeltaForProgChartPoint(ProgresMethods progresMethod, ChartPoints point,
        ProgPointConfigSpecs? pointSpecs)
    {
        string prefix = progresMethod switch
        {
            ProgresMethods.Transits => StandardTexts.PCF_TRANSITPOINTS,
            ProgresMethods.Secondary => StandardTexts.PCF_SECONDARYPOINTS,
            ProgresMethods.Symbolic => StandardTexts.PCF_SYMBOLICPOINTS,
            ProgresMethods.Primary => StandardTexts.PCF_PDPLACEHOLDERPOINTS, // will be adapted for significator/promissor  
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
            ProgresMethods.Transits => StandardTexts.CFG_TRORB,
            ProgresMethods.Secondary => StandardTexts.CFG_SCORB,
            ProgresMethods.Symbolic => StandardTexts.CFG_SMORB,
            _ => ""
        };
        string valueTxt = orb.ToString(CultureInfo.InvariantCulture);
        return new Tuple<string, string>(keyTxt, valueTxt);
    }

    public Tuple<string, string> CreateDeltaForProgSymKey(SymbolicKeys timeKey)
    {
        string keyTxt = StandardTexts.CFG_SMKEY;
        string valueTxt = ((int)timeKey).ToString();
        return new Tuple<string, string>(keyTxt, valueTxt);        
    }
    
}