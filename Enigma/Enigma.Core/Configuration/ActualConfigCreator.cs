// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Drawing;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Serilog;

namespace Enigma.Core.Configuration;


/// <summary>Create actual configuration.</summary>
public interface IActualConfigCreator
{
    /// <summary>Combine default configuration and deltas into actual configuration.</summary>
    /// <param name="defaultConfig">The default configuration.</param>
    /// <param name="deltas">Dictionary with the deltas.</param>
    /// <returns>Actual config.</returns>
    public AstroConfig CreateActualConfig(AstroConfig defaultConfig, Dictionary<string, string> deltas);

    /// <summary>Combine default configuration for progressions and deltas into actual configuration for progressions.</summary>
    /// <param name="defaultConfig">The default configuration.</param>
    /// <param name="deltas">Dictionary with the deltas.</param>
    /// <returns>Actual config for progressions.</returns>
    public ConfigProg CreateActualProgConfig(ConfigProg defaultConfig, Dictionary<string, string> deltas);
}

/// <inheritdoc/>
public class ActualConfigCreator: IActualConfigCreator
{
    private const string PREFIX_TRANSITS = "TR_";
    private const string PREFIX_SECONDARY = "SC_";
    private const string PREFIX_SYMBOLIC = "SM_";
    private const string PREFIX_PRIMARY = "PD_";
    private const string PREFIX_CHARTPOINTS = "CP_";
    private const string PREFIX_ASPECTS = "AT_";
    
    
    /// <inheritdoc/>
    public AstroConfig CreateActualConfig(AstroConfig defaultConfig, Dictionary<string, string> deltas)
    {
        HouseSystems houseSystem = deltas.TryGetValue(StandardTexts.CFG_HOUSE_SYSTEM, out string? houseIdTxt) &&    
                                   int.TryParse(houseIdTxt, out int houseId) ? 
                                   HouseSystemsExtensions.HouseSystemForIndex(houseId) : defaultConfig.HouseSystem;
        Ayanamshas ayanamsha = deltas.TryGetValue(StandardTexts.CFG_AYANAMSHA, out string? ayIdTxt) && 
                               int.TryParse(ayIdTxt, out int ayId) ? 
                               AyanamshaExtensions.AyanamshaForIndex(ayId) : defaultConfig.Ayanamsha;
        ZodiacTypes zodiacType = deltas.TryGetValue(StandardTexts.CFG_ZODIAC_TYPE, out string? zodIdTxt) && 
                                 int.TryParse(zodIdTxt, out int zodId) ? 
                                 ZodiacTypeExtensions.ZodiacTypeForIndex(zodId) : defaultConfig.ZodiacType;
        ProjectionTypes projectionType = deltas.TryGetValue(StandardTexts.CFG_PROJECTION_TYPE, out string? projIdTxt) && 
                                         int.TryParse(projIdTxt, out int projId) ? 
                                         ProjectionTypesExtensions.ProjectionTypeForIndex(projId) : 
                                         defaultConfig.ProjectionType;
        ObserverPositions observerPosition = deltas.TryGetValue(StandardTexts.CFG_OBSERVER_POSITION, out string? obsIdTxt) && 
                                             int.TryParse(obsIdTxt, out int obsId) ? 
                                             ObserverPositionsExtensions.ObserverPositionForIndex(obsId) : 
                                             defaultConfig.ObserverPosition;
        OrbMethods orbMethod = deltas.TryGetValue(StandardTexts.CFG_ORB_METHOD, out string? orbIdTxt) && 
                               int.TryParse(orbIdTxt, out int orbId) ? 
                               OrbMethodsExtensions.OrbMethodForIndex(orbId) : defaultConfig.OrbMethod;
        double baseOrbAspects = deltas.TryGetValue(StandardTexts.CFG_BASE_ORB_ASPECTS, out string? aspOrbTxt) && 
                                double.TryParse(aspOrbTxt, out double aspOrb) ? aspOrb : defaultConfig.BaseOrbAspects;
        double baseOrbMidpoints = deltas.TryGetValue(StandardTexts.CFG_BASE_ORB_MIDPOINTS, out string? mpOrbTxt) && 
                                  double.TryParse(mpOrbTxt, out double mpOrb) ? mpOrb : defaultConfig.BaseOrbMidpoints;
        double orbParallels =  deltas.TryGetValue(StandardTexts.CFG_ORB_PARALLELS, out string? parOrbTxt) && 
                               double.TryParse(parOrbTxt, out double parOrb) ? parOrb : defaultConfig.OrbParallels;
        double orbMidpointsDecl =  deltas.TryGetValue(StandardTexts.CFG_ORB_MIDPOINTS_DECL, out string? mpdOrbTxt) && 
                               double.TryParse(mpdOrbTxt, out double mpdOrb) ? mpdOrb : defaultConfig.OrbMidpointsDecl;
        
        bool useCuspsForAspects = deltas.TryGetValue(StandardTexts.CFG_USE_CUSPS_FOR_ASPECTS, out string? useCTxt) && 
                                  bool.TryParse(useCTxt, out bool useC) ? useC : defaultConfig.UseCuspsForAspects;
        
        Dictionary<ChartPoints, ChartPointConfigSpecs> chartPoints = CreateChartPoints(defaultConfig.ChartPoints, deltas);
        Dictionary<AspectTypes, AspectConfigSpecs> aspectTypes = CreateAspects(defaultConfig.Aspects, deltas);
        Dictionary<AspectTypes, string> aspectColors = CreateAspectColors(defaultConfig.AspectColors, deltas);
        
        return new AstroConfig(houseSystem, ayanamsha, observerPosition, zodiacType, projectionType, orbMethod,
            chartPoints, aspectTypes, aspectColors, baseOrbAspects, baseOrbMidpoints, orbParallels, orbMidpointsDecl, 
            useCuspsForAspects);
    }

    public ConfigProg CreateActualProgConfig(ConfigProg defaultConfig, Dictionary<string, string> deltas)
    {
        SymbolicKeys symKey = deltas.TryGetValue("SM_KEY", out string? symKeyIdTxt) &&    
                                   int.TryParse(symKeyIdTxt, out int symKeyId) ? 
            SymbolicKeyExtensions.SymbolicKeysForIndex(symKeyId) : defaultConfig.ConfigSymDir.TimeKey;
        double orbTransits = deltas.TryGetValue("TR_ORB", out string? trOrbValue) && 
                                double.TryParse(trOrbValue, out double trOrb) ? trOrb : defaultConfig.ConfigTransits.Orb;
        double orbSecDir = deltas.TryGetValue("SC_ORB", out string? scOrbValue) && 
                             double.TryParse(scOrbValue, out double scOrb) ? scOrb : defaultConfig.ConfigSecDir.Orb;
        double orbSymDir = deltas.TryGetValue("SM_ORB", out string? smOrbValue) && 
                             double.TryParse(smOrbValue, out double smOrb) ? smOrb : defaultConfig.ConfigSymDir.Orb;
        /*Dictionary<ChartPoints, ProgPointConfigSpecs> transitPoints = 
            CreateProgPoints(ProgresMethods.Transits, defaultConfig.ConfigTransits.ProgPoints, deltas);
        Dictionary<ChartPoints, ProgPointConfigSpecs> secDirPoints = 
            CreateProgPoints(ProgresMethods.Secondary, defaultConfig.ConfigSecDir.ProgPoints, deltas);
        Dictionary<ChartPoints, ProgPointConfigSpecs> symDirPoints = 
            CreateProgPoints(ProgresMethods.Symbolic, defaultConfig.ConfigSymDir.ProgPoints, deltas);*/
        Dictionary<ChartPoints, ProgPointConfigSpecs> transitPoints = 
            CreateProgPoints(PREFIX_TRANSITS, defaultConfig.ConfigTransits.ProgPoints, deltas);
        Dictionary<ChartPoints, ProgPointConfigSpecs> secDirPoints = 
            CreateProgPoints(PREFIX_SECONDARY, defaultConfig.ConfigSecDir.ProgPoints, deltas);
        Dictionary<ChartPoints, ProgPointConfigSpecs> symDirPoints = 
            CreateProgPoints(PREFIX_SYMBOLIC, defaultConfig.ConfigSymDir.ProgPoints, deltas);
        ConfigProgTransits cfgTransits = new(orbTransits, transitPoints);
        ConfigProgSecDir cfgSecDir = new(orbSecDir, secDirPoints);
        ConfigProgSymDir cfgSymDir = new( orbSymDir, symKey, symDirPoints);

        PrimDirMethods pdMethod =
            deltas.TryGetValue(StandardTexts.CFG_PD_METHOD, out string? pdMethodIdTxt) &&
            int.TryParse(pdMethodIdTxt, out int pdMethodId)
                ? PrimDirMethodsExtensions.PrimDirMethodForIndex(pdMethodId)
                : defaultConfig.ConfigPrimDir.Method;
        PrimDirApproaches pdApproach =
            deltas.TryGetValue(StandardTexts.CFG_PD_APPROACH, out string? pdApproachIdTxt) &&
            int.TryParse(pdApproachIdTxt, out int pdApproachId)
                ? PrimDirApproachesExtensions.PrimDirApproachForIndex(pdApproachId)
                : defaultConfig.ConfigPrimDir.Approach;
        PrimDirTimeKeys pdTimeKey =
            deltas.TryGetValue(StandardTexts.CFG_PD_TIMEKEY, out string? pdTimeKeyIdTxt) &&
            int.TryParse(pdTimeKeyIdTxt, out int pdTimeKeyId)
                ? PrimDirTimeKeysExtensions.PrimDirTimeKeyForIndex(pdTimeKeyId)
                : defaultConfig.ConfigPrimDir.TimeKey;
        PrimDirConverseOptions pdConverse =
            deltas.TryGetValue(StandardTexts.CFG_PD_CONVERSE, out string? pdConverseIdTxt) &&
            int.TryParse(pdConverseIdTxt, out int pdConverseId)
                ? PrimDirConverseOptionsExtensions.PrimDirConverseOptionForIndex(pdConverseId)
                : defaultConfig.ConfigPrimDir.ConverseOption;
        PrimDirLatAspOptions pdLatAsp =
            deltas.TryGetValue(StandardTexts.CFG_PD_LATASP, out string? pdLatAspeIdTxt) &&
            int.TryParse(pdLatAspeIdTxt, out int pdLatAspId)
                ? PrimDirLatAspOptionsExtensions.PrimDirLatAspOptionForIndex(pdLatAspId)
                : defaultConfig.ConfigPrimDir.LatAspOptions;
        Dictionary<ChartPoints, ProgPointConfigSpecs> pdSignificators = 
            CreateProgPoints(StandardTexts.PCF_SIGNIFICATORS, defaultConfig.ConfigPrimDir.Significators, deltas);
        Dictionary<ChartPoints, ProgPointConfigSpecs> pdPromissors = 
            CreateProgPoints(StandardTexts.PCF_PROMISSORS, defaultConfig.ConfigPrimDir.Promissors, deltas);
        Dictionary<AspectTypes, AspectConfigSpecs> pdAspects =
            CreateAspects(defaultConfig.ConfigPrimDir.Aspects, deltas);
        ConfigProgPrimDir cfgPrimDir = new(pdMethod, pdApproach, pdTimeKey, pdConverse, pdLatAsp, pdSignificators, pdPromissors, pdAspects ) ;
        return new ConfigProg(cfgTransits, cfgSecDir, cfgSymDir, cfgPrimDir);
    }

    private Dictionary<ChartPoints, ChartPointConfigSpecs> CreateChartPoints(
        Dictionary<ChartPoints, ChartPointConfigSpecs> defChartPoints, 
        IReadOnlyDictionary<string, string> deltas)
    {
        Dictionary<ChartPoints, ChartPointConfigSpecs> actualChartPoints = new();
        foreach (var defPoint in defChartPoints)
        {
            string cpKey = PREFIX_CHARTPOINTS + (int)defPoint.Key;
            if (deltas.TryGetValue(cpKey, out string? newConfigTxt))
            {
                try
                {
                    Tuple<bool, char, int, bool> cfgParts = DefineElementsForConfig(newConfigTxt);
                    ChartPointConfigSpecs newConfigSpecs = new(cfgParts.Item1, cfgParts.Item2, cfgParts.Item3, cfgParts.Item4);
                    actualChartPoints.Add(defPoint.Key, newConfigSpecs);
                }
                catch (Exception e)
                {
                    Log.Error(
                        "ActualConfigCreator: Error when constructing chartspoints for new configuration. Msg {Msg}",
                        e.Message);
                    throw new ArgumentException("Creating configuration failed.");
                }
            }
            else actualChartPoints.Add(defPoint.Key, defPoint.Value);
        }
        return actualChartPoints;
    }

    private Dictionary<ChartPoints, ProgPointConfigSpecs> CreateProgPoints(
        //ProgresMethods progresMethod,
        string prefix,
        Dictionary<ChartPoints, ProgPointConfigSpecs> defChartPoints, 
        IReadOnlyDictionary<string, string> deltas)
    {
        Dictionary<ChartPoints, ProgPointConfigSpecs> actualProgPoints = new();
        foreach (var defPoint in defChartPoints)
        {
            string fullPrefix = prefix + PREFIX_CHARTPOINTS + (int)defPoint.Key;
            if ((prefix == StandardTexts.PCF_SIGNIFICATORS) || (prefix == StandardTexts.PCF_PROMISSORS)) fullPrefix = prefix;            
            if (deltas.TryGetValue(fullPrefix, out string? newConfigTxt))
            {
                try
                {
                    Tuple<bool, char> cfgParts = DefineElementsForProgConfig(newConfigTxt);
                    ProgPointConfigSpecs newConfigSpecs = new(cfgParts.Item1, cfgParts.Item2);
                    actualProgPoints.Add(defPoint.Key, newConfigSpecs);
                }
                catch (Exception e)
                {
                    Log.Error(
                        "ActualConfigCreator: Error when constructing progpoints for new configuration. Msg {Msg}",
                        e.Message);
                    throw new ArgumentException("Creating configuration for progressions failed.");
                }
            }
            else actualProgPoints.Add(defPoint.Key, defPoint.Value);
        }
        return actualProgPoints;
    }
    

    private Dictionary<AspectTypes, AspectConfigSpecs> CreateAspects(
        Dictionary<AspectTypes, AspectConfigSpecs> defAspectTypes, 
        IReadOnlyDictionary<string, string> deltas)
    {
        Dictionary<AspectTypes, AspectConfigSpecs> actualAspectTypes = new();
        foreach (var defAspect in defAspectTypes)
        {
            string atKey = PREFIX_ASPECTS + (int)defAspect.Key;
            if (deltas.TryGetValue(atKey, out string? newConfigTxt))
            {
                try
                {
                    Tuple<bool, char, int, bool> cfgParts = DefineElementsForAspectConfig(newConfigTxt);
                    AspectConfigSpecs newConfigSpecs = new(cfgParts.Item1, cfgParts.Item2, cfgParts.Item3, cfgParts.Item4);
                    actualAspectTypes.Add(defAspect.Key, newConfigSpecs);
                }
                catch (Exception e)
                {
                    Log.Error(
                        "ActualConfigCreator: Error when constructing aspect types for new configuration. Msg {Msg}",
                    e.Message);
                    throw new ArgumentException("Creating configuration failed.");
                }
            }
            else actualAspectTypes.Add(defAspect.Key, defAspect.Value);
        }
        return actualAspectTypes;
    }

    private Dictionary<AspectTypes, string> CreateAspectColors(Dictionary<AspectTypes, string> defAspectColors,
        IReadOnlyDictionary<string, string> deltas)
    {
        Dictionary<AspectTypes, string> actualAspectColors = new();
        foreach (var defAspectColor in defAspectColors)
        {
            string acKey = "AC_" + (int)defAspectColor.Key;
            actualAspectColors.Add(defAspectColor.Key,
                deltas.TryGetValue(acKey, out string? newConfigTxt) ? newConfigTxt : defAspectColor.Value);
        }
        return actualAspectColors;
    }
    
    
    private Tuple<bool, char, int, bool> DefineElementsForConfig(string configTxt)
    {
        string[] configElements = configTxt.Split("||");
        bool isUsed = configElements[0].Equals("y");
        char glyph = char.Parse(configElements[1]);
        int percentageOrb = int.Parse(configElements[2]);
        bool showInChart = configElements[3].Equals("y");
        return new Tuple<bool, char, int, bool>(isUsed, glyph, percentageOrb, showInChart); 
    }
    
    private Tuple<bool, char, int, bool> DefineElementsForAspectConfig(string configTxt)
    {
        string[] configElements = configTxt.Split("||");
        bool isUsed = configElements[0].Equals("y");
        char glyph = char.Parse(configElements[1]);
        int percentageOrb = int.Parse(configElements[2]);
        bool showInChart = configElements[3].Equals("y");
        return new Tuple<bool, char, int, bool>(isUsed, glyph, percentageOrb, showInChart); 
    }
    
    private Tuple<bool, char> DefineElementsForProgConfig(string configTxt)
    {
        string[] configElements = configTxt.Split("||");
        bool isUsed = configElements[0].Equals("y");
        char glyph = char.Parse(configElements[1]);
        return new Tuple<bool, char>(isUsed, glyph); 
    }
}