// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Configuration.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;

namespace Enigma.Core.Handlers.Configuration.Helpers;

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
        HouseSystems houseSystem = HouseSystems.Placidus;
        Ayanamshas ayanamsha = Ayanamshas.None;
        ObserverPositions observerPosition = ObserverPositions.GeoCentric;
        ZodiacTypes zodiacType = ZodiacTypes.Tropical;
        ProjectionTypes projectionType = ProjectionTypes.TwoDimensional;
        OrbMethods orbMethod = OrbMethods.Weighted;
        Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointsSpecs = CreateChartPoints();
        Dictionary<AspectTypes, AspectConfigSpecs> aspectSpecs = CreateAspects();

        double baseOrbAspects = 10.0;
        double baseOrbMidpoints = 1.6;
        bool useCuspsForAspects = false;
        return new AstroConfig(houseSystem, ayanamsha, observerPosition, zodiacType, projectionType, orbMethod,
            chartPointsSpecs, aspectSpecs, baseOrbAspects, baseOrbMidpoints, useCuspsForAspects);
    }

    private static Dictionary<ChartPoints, ChartPointConfigSpecs> CreateChartPoints()
    {
        Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointConfigSpecs = new()
        {
            { ChartPoints.Sun,  new ChartPointConfigSpecs(true, 'a', 100) },
            { ChartPoints.Moon, new ChartPointConfigSpecs(true, 'b', 100) },
            { ChartPoints.Mercury,  new ChartPointConfigSpecs(true, 'c',80) },
            { ChartPoints.Venus,  new ChartPointConfigSpecs(true, 'd',80) },
            { ChartPoints.Earth,  new ChartPointConfigSpecs(false, 'e',100) },
            { ChartPoints.Mars,  new ChartPointConfigSpecs(true, 'f',80) },
            { ChartPoints.Jupiter,  new ChartPointConfigSpecs(true, 'g',65) },
            { ChartPoints.Saturn,  new ChartPointConfigSpecs(true, 'h',65) },
            { ChartPoints.Uranus,  new ChartPointConfigSpecs(true, 'i',50) },
            { ChartPoints.Neptune,  new ChartPointConfigSpecs(true, 'j',50) },
            { ChartPoints.Pluto,  new ChartPointConfigSpecs(true, 'k',50) },
            { ChartPoints.MeanNode,  new ChartPointConfigSpecs(false, '{',65) },
            { ChartPoints.TrueNode,  new ChartPointConfigSpecs(true, '{',65) },
            { ChartPoints.Chiron,  new ChartPointConfigSpecs(true, 'w',65) },
            { ChartPoints.PersephoneRam, new ChartPointConfigSpecs(false, '/', 40) },
            { ChartPoints.HermesRam, new ChartPointConfigSpecs(false, '<', 40) },
            { ChartPoints.DemeterRam, new ChartPointConfigSpecs(false, '>', 40) },
            { ChartPoints.CupidoUra, new ChartPointConfigSpecs(false, 'y', 40) },
            { ChartPoints.HadesUra, new ChartPointConfigSpecs(false, 'z', 40) },
            { ChartPoints.ZeusUra, new ChartPointConfigSpecs(false, '!', 40) },
            { ChartPoints.KronosUra, new ChartPointConfigSpecs(false, '@', 40) },
            { ChartPoints.ApollonUra, new ChartPointConfigSpecs(false, '#', 40) },
            { ChartPoints.AdmetosUra, new ChartPointConfigSpecs(false, '$', 40) },
            { ChartPoints.VulcanusUra, new ChartPointConfigSpecs(false, '%', 40) },
            { ChartPoints.PoseidonUra, new ChartPointConfigSpecs(false, '&', 40) },
            { ChartPoints.Eris, new ChartPointConfigSpecs(false, '*', 40) },
            { ChartPoints.Pholus, new ChartPointConfigSpecs(false, ')', 40) },
            { ChartPoints.Ceres, new ChartPointConfigSpecs(false, '_', 40) },
            { ChartPoints.Pallas, new ChartPointConfigSpecs(false, 'û', 40) },
            { ChartPoints.Juno, new ChartPointConfigSpecs(false, 'ü', 40) },
            { ChartPoints.Vesta, new ChartPointConfigSpecs(false, 'À', 40) },
            { ChartPoints.Isis, new ChartPointConfigSpecs(false, 'â', 40) },
            { ChartPoints.Nessus, new ChartPointConfigSpecs(false, '(', 40) },
            { ChartPoints.Huya, new ChartPointConfigSpecs(false, 'ï', 40) },
            { ChartPoints.Varuna, new ChartPointConfigSpecs(false, 'ò', 40) },
            { ChartPoints.Ixion, new ChartPointConfigSpecs(false, 'ó', 40) },
            { ChartPoints.Quaoar, new ChartPointConfigSpecs(false, 'ô', 40) },
            { ChartPoints.Haumea, new ChartPointConfigSpecs(false, 'í', 40) },
            { ChartPoints.Orcus, new ChartPointConfigSpecs(false, 'ù', 40) },
            { ChartPoints.Makemake, new ChartPointConfigSpecs(false, 'î', 40) },
            { ChartPoints.Sedna, new ChartPointConfigSpecs(false, 'ö', 40) },
            { ChartPoints.Hygieia, new ChartPointConfigSpecs(false, 'Á', 40) },
            { ChartPoints.Astraea, new ChartPointConfigSpecs(false, 'Ã', 40) },
            { ChartPoints.ApogeeMean, new ChartPointConfigSpecs(false, ',', 65) },
            { ChartPoints.ApogeeCorrected, new ChartPointConfigSpecs(false, '.', 65) },
            { ChartPoints.ApogeeInterpolated, new ChartPointConfigSpecs(false, '.', 65) },
            { ChartPoints.ApogeeDuval, new ChartPointConfigSpecs(false, '.', 65) },
            { ChartPoints.PersephoneCarteret, new ChartPointConfigSpecs(false, 'à', 40) },
            { ChartPoints.VulcanusCarteret, new ChartPointConfigSpecs(false, 'Ï', 40) },
            { ChartPoints.ZeroAries, new ChartPointConfigSpecs(false, '1', 0) },
            { ChartPoints.FortunaNoSect, new ChartPointConfigSpecs(false, 'e', 40) },
            { ChartPoints.FortunaSect, new ChartPointConfigSpecs(false, 'e', 40) },
            { ChartPoints.Ascendant, new ChartPointConfigSpecs(true, 'A', 100) },
            { ChartPoints.Mc, new ChartPointConfigSpecs(true, 'M', 100) },
            { ChartPoints.EastPoint, new ChartPointConfigSpecs(false, ' ', 20) },
            { ChartPoints.Vertex, new ChartPointConfigSpecs(false, ' ', 0) }
        };

        return chartPointConfigSpecs;
    }

    private static Dictionary<AspectTypes, AspectConfigSpecs> CreateAspects()
    {
        Dictionary<AspectTypes, AspectConfigSpecs> aspectConfigSpecs = new()
        {
            { AspectTypes.Conjunction, new AspectConfigSpecs(true, 'B', 100) },
            { AspectTypes.Opposition, new AspectConfigSpecs(true, 'C', 100) },
            { AspectTypes.Triangle, new AspectConfigSpecs(true, 'D', 85) },
            { AspectTypes.Square, new AspectConfigSpecs(true, 'E', 85) },
            { AspectTypes.Septile, new AspectConfigSpecs(false, 'N', 30) },
            { AspectTypes.Sextile, new AspectConfigSpecs(true, 'F', 70) },
            { AspectTypes.Quintile, new AspectConfigSpecs(false, 'Q', 30) },
            { AspectTypes.SemiSextile, new AspectConfigSpecs(false, 'G', 30) },
            { AspectTypes.SemiSquare, new AspectConfigSpecs(false, 'I', 30) },
            { AspectTypes.SemiQuintile, new AspectConfigSpecs(false, 'Ô', 30) },
            { AspectTypes.BiQuintile, new AspectConfigSpecs(false, 'L', 30) },
            { AspectTypes.Inconjunct, new AspectConfigSpecs(false, 'H', 30) },
            { AspectTypes.SesquiQuadrate, new AspectConfigSpecs(false, 'J', 30) },
            { AspectTypes.TriDecile, new AspectConfigSpecs(false, 'Õ', 15) },
            { AspectTypes.BiSeptile, new AspectConfigSpecs(false, 'Ú', 15) },
            { AspectTypes.TriSeptile, new AspectConfigSpecs(false, 'Û', 15) },
            { AspectTypes.Novile, new AspectConfigSpecs(false, 'Ü', 15) },
            { AspectTypes.BiNovile, new AspectConfigSpecs(false, 'Ñ', 15) },
            { AspectTypes.QuadraNovile, new AspectConfigSpecs(false, '|', 15) },
            { AspectTypes.Undecile, new AspectConfigSpecs(false, 'ç', 15) },
            { AspectTypes.Centile, new AspectConfigSpecs(false, 'Ç', 15) },
            { AspectTypes.Vigintile, new AspectConfigSpecs(false, 'Ï', 15) }
        };
        return aspectConfigSpecs;
    }


}