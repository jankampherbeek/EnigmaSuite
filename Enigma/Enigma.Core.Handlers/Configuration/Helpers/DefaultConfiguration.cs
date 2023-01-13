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
        List<ChartPointConfigSpecs> chartPointsSpecs = CreateChartPoints();
        List<AspectConfigSpecs> aspectSpecs = CreateAspects();

        double baseOrbAspects = 10.0;
        double baseOrbMidpoints = 1.6;
        bool useCuspsForAspects = false;
        return new AstroConfig(houseSystem, ayanamsha, observerPosition, zodiacType, projectionType, orbMethod,
            chartPointsSpecs, aspectSpecs, baseOrbAspects, baseOrbMidpoints, useCuspsForAspects);
    }

    private static List<ChartPointConfigSpecs> CreateChartPoints()
    {
        List<ChartPointConfigSpecs> chartPointConfigSpecs = new()
        {
            new ChartPointConfigSpecs(ChartPoints.Sun, true, 'a', 100),
            new ChartPointConfigSpecs(ChartPoints.Moon, true, 'b', 100),
            new ChartPointConfigSpecs(ChartPoints.Mercury,  true, 'c',80),
            new ChartPointConfigSpecs(ChartPoints.Venus,  true, 'd',80),
            new ChartPointConfigSpecs(ChartPoints.Earth,  false, 'e',100),
            new ChartPointConfigSpecs(ChartPoints.Mars,  true, 'f',80),
            new ChartPointConfigSpecs(ChartPoints.Jupiter,  true, 'g',65),
            new ChartPointConfigSpecs(ChartPoints.Saturn,  true, 'h',65),
            new ChartPointConfigSpecs(ChartPoints.Uranus,  true, 'i',50),
            new ChartPointConfigSpecs(ChartPoints.Neptune,  true, 'j',50),
            new ChartPointConfigSpecs(ChartPoints.Pluto,  true, 'k',50),
            new ChartPointConfigSpecs(ChartPoints.MeanNode,  true, '{',65),
            new ChartPointConfigSpecs(ChartPoints.TrueNode,  true, '}',65),
            new ChartPointConfigSpecs(ChartPoints.Chiron,  true, 'w',65),
            new ChartPointConfigSpecs(ChartPoints.PersephoneRam, false, '/', 40),
            new ChartPointConfigSpecs(ChartPoints.HermesRam, false, '<', 40),
            new ChartPointConfigSpecs(ChartPoints.DemeterRam, false, '>', 40),
            new ChartPointConfigSpecs(ChartPoints.CupidoUra, false, 'y', 40),
            new ChartPointConfigSpecs(ChartPoints.HadesUra, false, 'z', 40),
            new ChartPointConfigSpecs(ChartPoints.ZeusUra, false, '!', 40),
            new ChartPointConfigSpecs(ChartPoints.KronosUra, false, '@', 40),
            new ChartPointConfigSpecs(ChartPoints.ApollonUra, false, '#', 40),
            new ChartPointConfigSpecs(ChartPoints.AdmetosUra, false, '$', 40),
            new ChartPointConfigSpecs(ChartPoints.VulcanusUra, false, '%', 40),
            new ChartPointConfigSpecs(ChartPoints.PoseidonUra, false, '&', 40),
            new ChartPointConfigSpecs(ChartPoints.Eris, false, '*', 40),
            new ChartPointConfigSpecs(ChartPoints.Pholus, false, ')', 40),
            new ChartPointConfigSpecs(ChartPoints.Ceres, false, '_', 40),
            new ChartPointConfigSpecs(ChartPoints.Pallas, false, 'û', 40),
            new ChartPointConfigSpecs(ChartPoints.Juno, false, 'ü', 40),
            new ChartPointConfigSpecs(ChartPoints.Vesta, false, 'À', 40),
            new ChartPointConfigSpecs(ChartPoints.Isis, false, 'â', 40),
            new ChartPointConfigSpecs(ChartPoints.Nessus, false, '(', 40),
            new ChartPointConfigSpecs(ChartPoints.Huya, false, 'ï', 40),
            new ChartPointConfigSpecs(ChartPoints.Varuna, false, 'ò', 40),
            new ChartPointConfigSpecs(ChartPoints.Ixion, false, 'ó', 40),
            new ChartPointConfigSpecs(ChartPoints.Quaoar, false, 'ô', 40),
            new ChartPointConfigSpecs(ChartPoints.Haumea, false, 'í', 40),
            new ChartPointConfigSpecs(ChartPoints.Orcus, false, 'ù', 40),
            new ChartPointConfigSpecs(ChartPoints.Makemake, false, 'î', 40),
            new ChartPointConfigSpecs(ChartPoints.Sedna, false, 'ö', 40),
            new ChartPointConfigSpecs(ChartPoints.Hygieia, false, 'Á', 40),
            new ChartPointConfigSpecs(ChartPoints.Astraea, false, 'Ã', 40),
            new ChartPointConfigSpecs(ChartPoints.ApogeeMean, false, ',', 40),
            new ChartPointConfigSpecs(ChartPoints.ApogeeCorrected, false, '.', 65),
            new ChartPointConfigSpecs(ChartPoints.ApogeeInterpolated, false, '.', 65),
            new ChartPointConfigSpecs(ChartPoints.ApogeeDuval, false, '.', 65),
            new ChartPointConfigSpecs(ChartPoints.PersephoneCarteret, false, 'à', 40),
            new ChartPointConfigSpecs(ChartPoints.VulcanusCarteret, false, 'Ï', 40),
            new ChartPointConfigSpecs(ChartPoints.ZeroAries, false, '1', 0),
            new ChartPointConfigSpecs(ChartPoints.ZeroCancer, false, '4', 0),
            new ChartPointConfigSpecs(ChartPoints.FortunaNoSect, false, 'e', 40),
            new ChartPointConfigSpecs(ChartPoints.FortunaSect, false, 'e', 40),
            new ChartPointConfigSpecs(ChartPoints.Ascendant, true, 'A', 100),
            new ChartPointConfigSpecs(ChartPoints.Mc, true, 'M', 100),
            new ChartPointConfigSpecs(ChartPoints.EastPoint, false, ' ', 20),
            new ChartPointConfigSpecs(ChartPoints.Vertex, false, ' ', 0)
        };

        return chartPointConfigSpecs;
    }

    private static List<AspectConfigSpecs> CreateAspects()
    {
        List<AspectConfigSpecs> aspectConfigSpecs = new()
        {
            new AspectConfigSpecs(AspectTypes.Conjunction, true, 'B', 100),
            new AspectConfigSpecs(AspectTypes.Opposition, true, 'C', 100),
            new AspectConfigSpecs(AspectTypes.Triangle, true, 'D', 85),
            new AspectConfigSpecs(AspectTypes.Square, true, 'E', 85),
            new AspectConfigSpecs(AspectTypes.Septile, false, 'N', 30),
            new AspectConfigSpecs(AspectTypes.Sextile, true, 'F', 70),
            new AspectConfigSpecs(AspectTypes.Quintile, false, 'Q', 30),
            new AspectConfigSpecs(AspectTypes.SemiSextile, false, 'G', 30),
            new AspectConfigSpecs(AspectTypes.SemiSquare, false, 'I', 30),
            new AspectConfigSpecs(AspectTypes.SemiQuintile, false, 'Ô', 30),
            new AspectConfigSpecs(AspectTypes.BiQuintile, false, 'L', 30),
            new AspectConfigSpecs(AspectTypes.Inconjunct, false, 'H', 30),
            new AspectConfigSpecs(AspectTypes.SesquiQuadrate, false, 'J', 30),
            new AspectConfigSpecs(AspectTypes.TriDecile, false, 'Õ', 15),
            new AspectConfigSpecs(AspectTypes.BiSeptile, false, 'Ú', 15),
            new AspectConfigSpecs(AspectTypes.TriSeptile, false, 'Û', 15),
            new AspectConfigSpecs(AspectTypes.Novile, false, 'Ü', 15),
            new AspectConfigSpecs(AspectTypes.BiNovile, false, 'Ñ', 15),
            new AspectConfigSpecs(AspectTypes.QuadraNovile, false, '|', 15),
            new AspectConfigSpecs(AspectTypes.Undecile, false, 'ç', 15),
            new AspectConfigSpecs(AspectTypes.Centile, false, 'Ç', 15),
            new AspectConfigSpecs(AspectTypes.Vigintile, false, 'Ï', 15)
        };
        return aspectConfigSpecs;
    }


}