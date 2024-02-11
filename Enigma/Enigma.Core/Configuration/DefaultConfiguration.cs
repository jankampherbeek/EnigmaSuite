// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Configuration;

/// <summary>Create default configuration for radix.</summary>
public interface IDefaultConfiguration
{
    /// <returns>Default configuration for radix.</returns>
    public AstroConfig CreateDefaultConfig();
}

/// <inheritdoc/>
public sealed class DefaultConfiguration : IDefaultConfiguration
{
    /// <inheritdoc/>
    public AstroConfig CreateDefaultConfig()
    {
        return CombineDefaultDetails();
    }

    private static AstroConfig CombineDefaultDetails()
    {
        const HouseSystems houseSystem = HouseSystems.Regiomontanus;
        const Ayanamshas ayanamsha = Ayanamshas.None;
        const ObserverPositions observerPosition = ObserverPositions.TopoCentric;
        const ZodiacTypes zodiacType = ZodiacTypes.Tropical;
        const ProjectionTypes projectionType = ProjectionTypes.TwoDimensional;
        const OrbMethods orbMethod = OrbMethods.Weighted;
        Dictionary<ChartPoints, ChartPointConfigSpecs?> chartPointsSpecs = CreateChartPoints();
        Dictionary<AspectTypes, AspectConfigSpecs?> aspectSpecs = CreateAspects();

        const double baseOrbAspects = 10.0;
        const double baseOrbMidpoints = 1.6;
        const bool useCuspsForAspects = false;
        return new AstroConfig(houseSystem, ayanamsha, observerPosition, zodiacType, projectionType, orbMethod,
            chartPointsSpecs, aspectSpecs, baseOrbAspects, baseOrbMidpoints, useCuspsForAspects);
    }

    private static Dictionary<ChartPoints, ChartPointConfigSpecs?> CreateChartPoints()
    {
        Dictionary<ChartPoints, ChartPointConfigSpecs?> chartPointConfigSpecs = new()
        {
            { ChartPoints.Sun,  new ChartPointConfigSpecs(true, 'a', 100, true) },
            { ChartPoints.Moon, new ChartPointConfigSpecs(true, 'b', 100, true) },
            { ChartPoints.Mercury,  new ChartPointConfigSpecs(true, 'c',80, true) },
            { ChartPoints.Venus,  new ChartPointConfigSpecs(true, 'd',80, true) },
            { ChartPoints.Earth,  new ChartPointConfigSpecs(false, 'e',100, true) },
            { ChartPoints.Mars,  new ChartPointConfigSpecs(true, 'f',80, true)},
            { ChartPoints.Jupiter,  new ChartPointConfigSpecs(true, 'g',65, true)},
            { ChartPoints.Saturn,  new ChartPointConfigSpecs(true, 'h',65, true)},
            { ChartPoints.Uranus,  new ChartPointConfigSpecs(true, 'i',50, true) },
            { ChartPoints.Neptune,  new ChartPointConfigSpecs(true, 'j',50, true) },
            { ChartPoints.Pluto,  new ChartPointConfigSpecs(true, 'k',50, true) },
            { ChartPoints.MeanNode,  new ChartPointConfigSpecs(false, '{',65, true) },
            { ChartPoints.TrueNode,  new ChartPointConfigSpecs(true, '{',65, true) },
            { ChartPoints.Chiron,  new ChartPointConfigSpecs(true, 'w',65, true) },
            { ChartPoints.PersephoneRam, new ChartPointConfigSpecs(false, '/', 40, true) },
            { ChartPoints.HermesRam, new ChartPointConfigSpecs(false, '<', 40, true) },
            { ChartPoints.DemeterRam, new ChartPointConfigSpecs(false, '>', 40, true) },
            { ChartPoints.CupidoUra, new ChartPointConfigSpecs(false, 'y', 40, true) },
            { ChartPoints.HadesUra, new ChartPointConfigSpecs(false, 'z', 40, true) },
            { ChartPoints.ZeusUra, new ChartPointConfigSpecs(false, '!', 40, true) },
            { ChartPoints.KronosUra, new ChartPointConfigSpecs(false, '@', 40, true) },
            { ChartPoints.ApollonUra, new ChartPointConfigSpecs(false, '#', 40, true) },
            { ChartPoints.AdmetosUra, new ChartPointConfigSpecs(false, '$', 40, true) },
            { ChartPoints.VulcanusUra, new ChartPointConfigSpecs(false, '%', 40, true) },
            { ChartPoints.PoseidonUra, new ChartPointConfigSpecs(false, '&', 40, true) },
            { ChartPoints.Eris, new ChartPointConfigSpecs(false, '*', 40, true) },
            { ChartPoints.Pholus, new ChartPointConfigSpecs(false, ')', 40, true) },
            { ChartPoints.Ceres, new ChartPointConfigSpecs(false, '_', 40, true) },
            { ChartPoints.Pallas, new ChartPointConfigSpecs(false, 'û', 40, true) },
            { ChartPoints.Juno, new ChartPointConfigSpecs(false, 'ü', 40, true) },
            { ChartPoints.Vesta, new ChartPointConfigSpecs(false, 'À', 40, true) },
            { ChartPoints.Isis, new ChartPointConfigSpecs(false, 'â', 40, true) },
            { ChartPoints.Nessus, new ChartPointConfigSpecs(false, '(', 40, true) },
            { ChartPoints.Huya, new ChartPointConfigSpecs(false, 'ï', 40, true) },
            { ChartPoints.Varuna, new ChartPointConfigSpecs(false, 'ò', 40, true) },
            { ChartPoints.Ixion, new ChartPointConfigSpecs(false, 'ó', 40, true) },
            { ChartPoints.Quaoar, new ChartPointConfigSpecs(false, 'ô', 40, true) },
            { ChartPoints.Haumea, new ChartPointConfigSpecs(false, 'í', 40, true) },
            { ChartPoints.Orcus, new ChartPointConfigSpecs(false, 'ù', 40, true) },
            { ChartPoints.Makemake, new ChartPointConfigSpecs(false, 'î', 40, true) },
            { ChartPoints.Sedna, new ChartPointConfigSpecs(false, 'ö', 40, true) },
            { ChartPoints.Hygieia, new ChartPointConfigSpecs(false, 'Á', 40, true) },
            { ChartPoints.Astraea, new ChartPointConfigSpecs(false, 'Ã', 40, true) },
            { ChartPoints.ApogeeMean, new ChartPointConfigSpecs(false, ',', 65, true) },
            { ChartPoints.ApogeeCorrected, new ChartPointConfigSpecs(false, '.', 65, true) },
            { ChartPoints.ApogeeInterpolated, new ChartPointConfigSpecs(false, '.', 65, true) },
            { ChartPoints.ApogeeDuval, new ChartPointConfigSpecs(false, '.', 65, true) },
            { ChartPoints.PersephoneCarteret, new ChartPointConfigSpecs(false, 'à', 40, true) },
            { ChartPoints.VulcanusCarteret, new ChartPointConfigSpecs(false, 'Ï', 40, true) },
            { ChartPoints.ZeroAries, new ChartPointConfigSpecs(false, '1', 0, false) },
            { ChartPoints.FortunaNoSect, new ChartPointConfigSpecs(false, 'e', 40, true) },
            { ChartPoints.FortunaSect, new ChartPointConfigSpecs(false, 'e', 40, true) },
            { ChartPoints.Ascendant, new ChartPointConfigSpecs(true, 'A', 100, true) },
            { ChartPoints.Mc, new ChartPointConfigSpecs(true, 'M', 100, true) },
            { ChartPoints.EastPoint, new ChartPointConfigSpecs(false, ' ', 20, true) },
            { ChartPoints.Vertex, new ChartPointConfigSpecs(false, ' ', 0, true) }
        };

        return chartPointConfigSpecs;
    }

    private static Dictionary<AspectTypes, AspectConfigSpecs?> CreateAspects()
    {
        Dictionary<AspectTypes, AspectConfigSpecs?> aspectConfigSpecs = new()
        {
            { AspectTypes.Conjunction, new AspectConfigSpecs(true, 'B', 100, true) },
            { AspectTypes.Opposition, new AspectConfigSpecs(true, 'C', 100, true) },
            { AspectTypes.Triangle, new AspectConfigSpecs(true, 'D', 85, true) },
            { AspectTypes.Square, new AspectConfigSpecs(true, 'E', 85, true) },
            { AspectTypes.Septile, new AspectConfigSpecs(false, 'N', 30, true) },
            { AspectTypes.Sextile, new AspectConfigSpecs(true, 'F', 70, true) },
            { AspectTypes.Quintile, new AspectConfigSpecs(false, 'Q', 30, true) },
            { AspectTypes.SemiSextile, new AspectConfigSpecs(false, 'G', 30, true) },
            { AspectTypes.SemiSquare, new AspectConfigSpecs(false, 'I', 30, true) },
            { AspectTypes.SemiQuintile, new AspectConfigSpecs(false, 'Ô', 30, true) },
            { AspectTypes.BiQuintile, new AspectConfigSpecs(false, 'L', 30, true) },
            { AspectTypes.Inconjunct, new AspectConfigSpecs(false, 'H', 30, true) },
            { AspectTypes.SesquiQuadrate, new AspectConfigSpecs(false, 'J', 30, true) },
            { AspectTypes.TriDecile, new AspectConfigSpecs(false, 'Õ', 15, true) },
            { AspectTypes.BiSeptile, new AspectConfigSpecs(false, 'Ú', 15, true) },
            { AspectTypes.TriSeptile, new AspectConfigSpecs(false, 'Û', 15, true) },
            { AspectTypes.Novile, new AspectConfigSpecs(false, 'Ü', 15, true) },
            { AspectTypes.BiNovile, new AspectConfigSpecs(false, 'Ñ', 15, true) },
            { AspectTypes.QuadraNovile, new AspectConfigSpecs(false, '|', 15, true) },
            { AspectTypes.Undecile, new AspectConfigSpecs(false, 'ç', 15, true) },
            { AspectTypes.Centile, new AspectConfigSpecs(false, 'Ç', 15, true) },
            { AspectTypes.Vigintile, new AspectConfigSpecs(false, 'Ï', 15, true) }
        };
        return aspectConfigSpecs;
    }


}