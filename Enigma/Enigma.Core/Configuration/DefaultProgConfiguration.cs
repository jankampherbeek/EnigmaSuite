// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

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
        return new ConfigProg(CreateConfigTransits(), CreateConfigSecDir(), CreateConfigSymDir());
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
}