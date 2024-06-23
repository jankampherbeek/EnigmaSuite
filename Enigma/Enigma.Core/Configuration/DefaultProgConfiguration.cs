// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts.Prog.PrimDir;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Configuration;

/// <summary>Create default configuration for progressions.</summary>
public interface IDefaultProgConfiguration
{
    /// <returns>Default configuration for progressions.</returns>
    public ConfigProg CreateDefaultConfig();
}


/// <inheritdoc/>
public sealed class DefaultProgConfiguration: IDefaultProgConfiguration
{
    /// <inheritdoc/>
    public ConfigProg CreateDefaultConfig()
    {
        return CombineDefaultDetails();
    }

    private static ConfigProg CombineDefaultDetails()
    {
        return new ConfigProg(CreateConfigTransits(), CreateConfigSecDir(), CreateConfigSymDir(), CreateConfigPrimDir());
    }

    private static ConfigProgTransits CreateConfigTransits()
    {
        const double orb = 1.0;
        Dictionary<ChartPoints, ProgPointConfigSpecs> progPoints = CreateProgPoints();
        progPoints[ChartPoints.Sun] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Sun].Glyph);
        progPoints[ChartPoints.Moon] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Moon].Glyph);
        progPoints[ChartPoints.Mercury] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Mercury].Glyph);
        progPoints[ChartPoints.Venus] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Venus].Glyph);
        progPoints[ChartPoints.Mars] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Mars].Glyph);
        progPoints[ChartPoints.Jupiter] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Jupiter].Glyph);
        progPoints[ChartPoints.Saturn] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Saturn].Glyph);
        progPoints[ChartPoints.Uranus] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Uranus].Glyph);
        progPoints[ChartPoints.Neptune] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Neptune].Glyph);
        progPoints[ChartPoints.Pluto] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Pluto].Glyph);
        progPoints[ChartPoints.Chiron] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Chiron].Glyph);
        progPoints[ChartPoints.TrueNode] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.TrueNode].Glyph);
        return new ConfigProgTransits(orb, progPoints);
    }
    
    private static ConfigProgSecDir CreateConfigSecDir()
    {
        const double orb = 1.0;
        Dictionary<ChartPoints, ProgPointConfigSpecs> progPoints = CreateProgPoints();
        progPoints[ChartPoints.Sun] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Sun].Glyph);
        progPoints[ChartPoints.Moon] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Moon].Glyph);
        progPoints[ChartPoints.Mercury] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Mercury].Glyph);
        progPoints[ChartPoints.Venus] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Venus].Glyph);
        progPoints[ChartPoints.Mars] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Mars].Glyph);
        progPoints[ChartPoints.Jupiter] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Jupiter].Glyph);
        progPoints[ChartPoints.Saturn] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Saturn].Glyph);
        progPoints[ChartPoints.TrueNode] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.TrueNode].Glyph);
        return new ConfigProgSecDir(orb, progPoints);
    }
    
    
    private static ConfigProgSymDir CreateConfigSymDir()
    {
        const double orb = 1.0;
        const SymbolicKeys timeKey = SymbolicKeys.OneDegree;
        Dictionary<ChartPoints, ProgPointConfigSpecs> progPoints = CreateProgPoints();
        progPoints[ChartPoints.Sun] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Sun].Glyph);
        progPoints[ChartPoints.Moon] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Moon].Glyph);
        progPoints[ChartPoints.Mercury] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Mercury].Glyph);
        progPoints[ChartPoints.Venus] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Venus].Glyph);
        progPoints[ChartPoints.Mars] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Mars].Glyph);
        progPoints[ChartPoints.Jupiter] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Jupiter].Glyph);
        progPoints[ChartPoints.Saturn] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Saturn].Glyph);
        progPoints[ChartPoints.Uranus] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Uranus].Glyph);
        progPoints[ChartPoints.Neptune] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Neptune].Glyph);
        progPoints[ChartPoints.Pluto] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Pluto].Glyph);
        progPoints[ChartPoints.Chiron] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Chiron].Glyph);
        progPoints[ChartPoints.TrueNode] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.TrueNode].Glyph);
        progPoints[ChartPoints.Mc] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Mc].Glyph);
        progPoints[ChartPoints.Ascendant] = new ProgPointConfigSpecs(true, progPoints[ChartPoints.Ascendant].Glyph);
        return new ConfigProgSymDir(orb, timeKey, progPoints);
    }

    private static ConfigProgPrimDir CreateConfigPrimDir()
    {
        const PrimDirMethods method = PrimDirMethods.Placidus;
        const PrimDirApproaches approach = PrimDirApproaches.Mundane;
        const PrimDirTimeKeys timeKey = PrimDirTimeKeys.Naibod;
        const PrimDirConverseOptions converseOption = PrimDirConverseOptions.None;
        const PrimDirLatAspOptions latAspOptions = PrimDirLatAspOptions.Bianchini;
        Dictionary<ChartPoints, ProgPointConfigSpecs> significators = CreateProgPoints();
        Dictionary<ChartPoints, ProgPointConfigSpecs> promissors = CreateProgPoints();
        Dictionary<AspectTypes, AspectConfigSpecs> aspects = CreateAspects();
        significators[ChartPoints.Sun] = new ProgPointConfigSpecs(true, significators[ChartPoints.Sun].Glyph);
        significators[ChartPoints.Moon] = new ProgPointConfigSpecs(true, significators[ChartPoints.Moon].Glyph);
        significators[ChartPoints.Mercury] = new ProgPointConfigSpecs(true, significators[ChartPoints.Mercury].Glyph);
        significators[ChartPoints.Venus] = new ProgPointConfigSpecs(true, significators[ChartPoints.Venus].Glyph);
        significators[ChartPoints.Mars] = new ProgPointConfigSpecs(true, significators[ChartPoints.Mars].Glyph);
        significators[ChartPoints.Jupiter] = new ProgPointConfigSpecs(true, significators[ChartPoints.Jupiter].Glyph);
        significators[ChartPoints.Saturn] = new ProgPointConfigSpecs(true, significators[ChartPoints.Saturn].Glyph);
        significators[ChartPoints.Mc] = new ProgPointConfigSpecs(true, significators[ChartPoints.Mc].Glyph);
        significators[ChartPoints.Ascendant] = new ProgPointConfigSpecs(true, significators[ChartPoints.Ascendant].Glyph);
        promissors[ChartPoints.Sun] = new ProgPointConfigSpecs(true, promissors[ChartPoints.Sun].Glyph);
        promissors[ChartPoints.Moon] = new ProgPointConfigSpecs(true, promissors[ChartPoints.Moon].Glyph);
        promissors[ChartPoints.Mercury] = new ProgPointConfigSpecs(true, promissors[ChartPoints.Mercury].Glyph);
        promissors[ChartPoints.Venus] = new ProgPointConfigSpecs(true, promissors[ChartPoints.Venus].Glyph);
        promissors[ChartPoints.Mars] = new ProgPointConfigSpecs(true, promissors[ChartPoints.Mars].Glyph);
        promissors[ChartPoints.Jupiter] = new ProgPointConfigSpecs(true, promissors[ChartPoints.Jupiter].Glyph);
        promissors[ChartPoints.Saturn] = new ProgPointConfigSpecs(true, promissors[ChartPoints.Saturn].Glyph);
        aspects[AspectTypes.Conjunction] = new AspectConfigSpecs(true, aspects[AspectTypes.Conjunction].Glyph, 0, true);
        aspects[AspectTypes.Opposition] = new AspectConfigSpecs(true, aspects[AspectTypes.Opposition].Glyph, 0, true);
        return new ConfigProgPrimDir(method, approach, timeKey, converseOption, latAspOptions, significators, promissors,
            aspects);
    }
    
    

    private static Dictionary<ChartPoints, ProgPointConfigSpecs> CreateProgPoints()
    {
        Dictionary<ChartPoints, ProgPointConfigSpecs> progPointConfigSpecs = new()
        {
            { ChartPoints.Sun, new ProgPointConfigSpecs(false, 'a') },
            { ChartPoints.Moon, new ProgPointConfigSpecs(false, 'b') },
            { ChartPoints.Mercury, new ProgPointConfigSpecs(false, 'c') },
            { ChartPoints.Venus, new ProgPointConfigSpecs(false, 'd') },
            { ChartPoints.Mars, new ProgPointConfigSpecs(false, 'f') },
            { ChartPoints.Jupiter, new ProgPointConfigSpecs(false, 'g') },
            { ChartPoints.Saturn, new ProgPointConfigSpecs(false, 'h') },
            { ChartPoints.Uranus, new ProgPointConfigSpecs(false, 'i') },
            { ChartPoints.Neptune, new ProgPointConfigSpecs(false, 'j') },
            { ChartPoints.Pluto, new ProgPointConfigSpecs(false, 'k') },
            { ChartPoints.MeanNode, new ProgPointConfigSpecs(false, '{') },
            { ChartPoints.TrueNode, new ProgPointConfigSpecs(false, '{') },
            { ChartPoints.Chiron, new ProgPointConfigSpecs(false, 'w') },
            { ChartPoints.PersephoneRam, new ProgPointConfigSpecs(false, '/') },
            { ChartPoints.HermesRam, new ProgPointConfigSpecs(false, '<') },
            { ChartPoints.DemeterRam, new ProgPointConfigSpecs(false, '>') },
            { ChartPoints.CupidoUra, new ProgPointConfigSpecs(false, 'y') },
            { ChartPoints.HadesUra, new ProgPointConfigSpecs(false, 'z') },
            { ChartPoints.ZeusUra, new ProgPointConfigSpecs(false, '!') },
            { ChartPoints.KronosUra, new ProgPointConfigSpecs(false, '@') },
            { ChartPoints.ApollonUra, new ProgPointConfigSpecs(false, '#') },
            { ChartPoints.AdmetosUra, new ProgPointConfigSpecs(false, '$') },
            { ChartPoints.VulcanusUra, new ProgPointConfigSpecs(false, '%') },
            { ChartPoints.PoseidonUra, new ProgPointConfigSpecs(false, '&') },
            { ChartPoints.Eris, new ProgPointConfigSpecs(false, '*') },
            { ChartPoints.Pholus, new ProgPointConfigSpecs(false, ')') },
            { ChartPoints.Ceres, new ProgPointConfigSpecs(false, '_') },
            { ChartPoints.Pallas, new ProgPointConfigSpecs(false, 'û') },
            { ChartPoints.Juno, new ProgPointConfigSpecs(false, 'ü') },
            { ChartPoints.Vesta, new ProgPointConfigSpecs(false, 'À') },
            { ChartPoints.Isis, new ProgPointConfigSpecs(false, 'â') },
            { ChartPoints.Nessus, new ProgPointConfigSpecs(false, '(') },
            { ChartPoints.Huya, new ProgPointConfigSpecs(false, 'ï') },
            { ChartPoints.Varuna, new ProgPointConfigSpecs(false, 'ò') },
            { ChartPoints.Ixion, new ProgPointConfigSpecs(false, 'ó') },
            { ChartPoints.Quaoar, new ProgPointConfigSpecs(false, 'ô') },
            { ChartPoints.Haumea, new ProgPointConfigSpecs(false, 'í') },
            { ChartPoints.Orcus, new ProgPointConfigSpecs(false, 'ù') },
            { ChartPoints.Makemake, new ProgPointConfigSpecs(false, 'î') },
            { ChartPoints.Sedna, new ProgPointConfigSpecs(false, 'ö') },
            { ChartPoints.Hygieia, new ProgPointConfigSpecs(false, 'Á') },
            { ChartPoints.Astraea, new ProgPointConfigSpecs(false, 'Ã') },
            { ChartPoints.ApogeeMean, new ProgPointConfigSpecs(false, ',') },
            { ChartPoints.ApogeeCorrected, new ProgPointConfigSpecs(false, '.') },
            { ChartPoints.ApogeeInterpolated, new ProgPointConfigSpecs(false, '.') },
            { ChartPoints.ApogeeDuval, new ProgPointConfigSpecs(false, '.') },
            { ChartPoints.PersephoneCarteret, new ProgPointConfigSpecs(false, 'à') },
            { ChartPoints.VulcanusCarteret, new ProgPointConfigSpecs(false, 'Ï') },
            { ChartPoints.FortunaNoSect, new ProgPointConfigSpecs(false, 'e') },
            { ChartPoints.FortunaSect, new ProgPointConfigSpecs(false, 'e') },
            { ChartPoints.Ascendant, new ProgPointConfigSpecs(false, 'A') },
            { ChartPoints.Mc, new ProgPointConfigSpecs(false, 'M') },
            { ChartPoints.EastPoint, new ProgPointConfigSpecs(false, ' ') },
            { ChartPoints.Vertex, new ProgPointConfigSpecs(false, ' ') }
        };
        return progPointConfigSpecs;
    }

    private static Dictionary<AspectTypes, AspectConfigSpecs> CreateAspects()
    {
        Dictionary<AspectTypes, AspectConfigSpecs> progPointAspectSpecs = new()
        {
            { AspectTypes.Conjunction, new AspectConfigSpecs(false, 'B', 0, false) },
            { AspectTypes.Opposition, new AspectConfigSpecs(false, 'C', 0, false) },
            { AspectTypes.Triangle, new AspectConfigSpecs(false, 'D', 0, false) },
            { AspectTypes.Square, new AspectConfigSpecs(false, 'E', 0, false) },
            { AspectTypes.Septile, new AspectConfigSpecs(false, 'N', 0, false) },
            { AspectTypes.Sextile, new AspectConfigSpecs(false, 'F', 0, false) },
            { AspectTypes.Quintile, new AspectConfigSpecs(false, 'K', 0, false) },
            { AspectTypes.SemiSextile, new AspectConfigSpecs(false, 'G', 0, false) },
            { AspectTypes.SemiSquare, new AspectConfigSpecs(false, 'I', 0, false) },
            { AspectTypes.BiQuintile, new AspectConfigSpecs(false, 'L', 0, false) },
            { AspectTypes.SemiQuintile, new AspectConfigSpecs(false, 'Ö', 0, false) },
            { AspectTypes.Inconjunct, new AspectConfigSpecs(false, 'H', 0, false) },
            { AspectTypes.SesquiQuadrate, new AspectConfigSpecs(false, 'J', 0, false) },
            { AspectTypes.TriDecile, new AspectConfigSpecs(false, 'Õ', 0, false) },
            { AspectTypes.BiSeptile, new AspectConfigSpecs(false, 'Ú', 0, false) },
            { AspectTypes.TriSeptile, new AspectConfigSpecs(false, 'Û', 0, false) },
            { AspectTypes.Novile, new AspectConfigSpecs(false, 'Ü', 0, false) },
            { AspectTypes.BiNovile, new AspectConfigSpecs(false, 'Ñ', 0, false) },
            { AspectTypes.QuadraNovile, new AspectConfigSpecs(false, '|', 0, false) },
            { AspectTypes.Undecile, new AspectConfigSpecs(false, 'ç', 0, false) },
            { AspectTypes.Centile, new AspectConfigSpecs(false, 'Ç', 0, false) },
            { AspectTypes.Vigintile, new AspectConfigSpecs(false, 'Ï', 0, false) },
        };
        return progPointAspectSpecs;
    }
    
}