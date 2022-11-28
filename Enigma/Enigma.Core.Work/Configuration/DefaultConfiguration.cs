// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Configuration.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Configuration;
using Enigma.Domain.Enums;

namespace Enigma.Configuration.Parsers;


public class DefaultConfiguration : IDefaultConfiguration
{
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
        ProjectionTypes projectionType = ProjectionTypes.twoDimensional;
        OrbMethods orbMethod = OrbMethods.Weighted;
        List<CelPointSpecs> celPointsSpecs = CreateCelPoints();
        List<AspectSpecs> aspectSpecs = CreateAspects();
        List<MundanePointSpecs> mundanePointSpecs = CreateMundanePoints();
        List<ArabicPointSpecs> arabicPointSpecs = CreateArabicPoints();
        double baseOrbAspects = 10.0;
        double baseOrbMidpoints = 1.6;
        return new AstroConfig(houseSystem, ayanamsha, observerPosition, zodiacType, projectionType, orbMethod,
            celPointsSpecs, aspectSpecs, mundanePointSpecs, arabicPointSpecs, baseOrbAspects, baseOrbMidpoints);
    }

    private static List<CelPointSpecs> CreateCelPoints()
    {
        List<CelPointSpecs> celPointSpecs = new()
        {
            new CelPointSpecs(CelPoints.Sun, 100, true),
            new CelPointSpecs(CelPoints.Moon, 100, true),
            new CelPointSpecs(CelPoints.Mercury, 80, true),
            new CelPointSpecs(CelPoints.Venus, 80, true),
            new CelPointSpecs(CelPoints.Earth, 100, false),
            new CelPointSpecs(CelPoints.Mars, 80, true),
            new CelPointSpecs(CelPoints.Jupiter, 65, true),
            new CelPointSpecs(CelPoints.Saturn, 65, true),
            new CelPointSpecs(CelPoints.Uranus, 50, true),
            new CelPointSpecs(CelPoints.Neptune, 50, true),
            new CelPointSpecs(CelPoints.Pluto, 50, true),
            new CelPointSpecs(CelPoints.MeanNode, 65, false),
            new CelPointSpecs(CelPoints.TrueNode, 65, true),
            new CelPointSpecs(CelPoints.Chiron, 65, true),
            new CelPointSpecs(CelPoints.PersephoneRam, 40, false),
            new CelPointSpecs(CelPoints.HermesRam, 40, false),
            new CelPointSpecs(CelPoints.DemeterRam, 40, false),
            new CelPointSpecs(CelPoints.CupidoUra, 40, false),
            new CelPointSpecs(CelPoints.HadesUra, 40, false),
            new CelPointSpecs(CelPoints.ZeusUra, 40, false),
            new CelPointSpecs(CelPoints.KronosUra, 40, false),
            new CelPointSpecs(CelPoints.ApollonUra, 40, false),
            new CelPointSpecs(CelPoints.AdmetosUra, 40, false),
            new CelPointSpecs(CelPoints.VulcanusUra, 40, false),
            new CelPointSpecs(CelPoints.PoseidonUra, 40, false),
            new CelPointSpecs(CelPoints.Eris, 40, false),
            new CelPointSpecs(CelPoints.Pholus, 40, false),
            new CelPointSpecs(CelPoints.Ceres, 40, false),
            new CelPointSpecs(CelPoints.Pallas, 40, false),
            new CelPointSpecs(CelPoints.Juno, 40, false),
            new CelPointSpecs(CelPoints.Vesta, 40, false),
            new CelPointSpecs(CelPoints.Isis, 40, false),
            new CelPointSpecs(CelPoints.Nessus, 40, false),
            new CelPointSpecs(CelPoints.Huya, 40, false),
            new CelPointSpecs(CelPoints.Varuna, 40, false),
            new CelPointSpecs(CelPoints.Ixion, 40, false),
            new CelPointSpecs(CelPoints.Quaoar, 40, false),
            new CelPointSpecs(CelPoints.Haumea, 40, false),
            new CelPointSpecs(CelPoints.Orcus, 40, false),
            new CelPointSpecs(CelPoints.Makemake, 40, false),
            new CelPointSpecs(CelPoints.Sedna, 40, false),
            new CelPointSpecs(CelPoints.Hygieia, 40, false),
            new CelPointSpecs(CelPoints.Astraea, 40, false),
            new CelPointSpecs(CelPoints.ApogeeMean, 65, true),
            new CelPointSpecs(CelPoints.ApogeeCorrected, 65, false),
            new CelPointSpecs(CelPoints.ApogeeInterpolated, 65, false),
            new CelPointSpecs(CelPoints.ApogeeDuval, 65, false),
            new CelPointSpecs(CelPoints.ZeroAries, 40, false),
            new CelPointSpecs(CelPoints.ParsFortunaNoSect, 65, false),
            new CelPointSpecs(CelPoints.ParsFortunaSect, 65, true),
            new CelPointSpecs(CelPoints.PersephoneCarteret, 40, false),
            new CelPointSpecs(CelPoints.VulcanusCarteret, 40, false)
        };
        return celPointSpecs;
    }

    private static List<AspectSpecs> CreateAspects()
    {
        List<AspectSpecs> aspectSpecs = new()
        {
            new AspectSpecs(AspectTypes.Conjunction, 100, true),
            new AspectSpecs(AspectTypes.Opposition, 100, true),
            new AspectSpecs(AspectTypes.Triangle, 85, true),
            new AspectSpecs(AspectTypes.Square, 85, true),
            new AspectSpecs(AspectTypes.Septile, 30, false),
            new AspectSpecs(AspectTypes.Sextile, 70, true),
            new AspectSpecs(AspectTypes.Quintile, 30, false),
            new AspectSpecs(AspectTypes.SemiSextile, 30, false),
            new AspectSpecs(AspectTypes.SemiSquare, 30, false),
            new AspectSpecs(AspectTypes.SemiQuintile, 30, false),
            new AspectSpecs(AspectTypes.BiQuintile, 30, false),
            new AspectSpecs(AspectTypes.Inconjunct, 30, true),
            new AspectSpecs(AspectTypes.SesquiQuadrate, 30, false),
            new AspectSpecs(AspectTypes.TriDecile, 15, false),
            new AspectSpecs(AspectTypes.BiSeptile, 15, false),
            new AspectSpecs(AspectTypes.TriSeptile, 15, false),
            new AspectSpecs(AspectTypes.Novile, 15, false),
            new AspectSpecs(AspectTypes.BiNovile, 15, false),
            new AspectSpecs(AspectTypes.QuadraNovile, 15, false),
            new AspectSpecs(AspectTypes.Undecile, 15, false),
            new AspectSpecs(AspectTypes.Centile, 15, false),
            new AspectSpecs(AspectTypes.Vigintile, 15, false)
        };
        return aspectSpecs;
    }

    private static List<MundanePointSpecs> CreateMundanePoints()
    {
        List<MundanePointSpecs> mundanePointSpecs = new()
        {
            new MundanePointSpecs(MundanePoints.Ascendant, 100, true),
            new MundanePointSpecs(MundanePoints.Mc, 100, true),
            new MundanePointSpecs(MundanePoints.EastPoint, 20, false),
            new MundanePointSpecs(MundanePoints.Vertex, 20, false)
        };
        return mundanePointSpecs;
    }

    private static List<ArabicPointSpecs> CreateArabicPoints()
    {
        List<ArabicPointSpecs> arabicPointSpecs = new()
        {
            new ArabicPointSpecs(ArabicPoints.FortunaSect, 40, true),
            new ArabicPointSpecs(ArabicPoints.FortunaNoSect, 40, false)
        };
        return arabicPointSpecs;
    }



}