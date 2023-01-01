// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Configuration.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Configuration;
using Enigma.Domain.Enums;
using Enigma.Domain.Points;

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
        ProjectionTypes projectionType = ProjectionTypes.TwoDimensional;
        OrbMethods orbMethod = OrbMethods.Weighted;
        List<CelPointConfigSpecs> celPointsSpecs = CreateCelPoints();
        List<AspectConfigSpecs> aspectSpecs = CreateAspects();
        List<MundanePointConfigSpecs> mundanePointSpecs = CreateMundanePoints();
        List<ArabicPointConfigSpecs> arabicPointSpecs = CreateArabicPoints();
        List<ZodiacPointConfigSpecs> zodiacPointSpecs = CreateZodiacPoints();
        double baseOrbAspects = 10.0;
        double baseOrbMidpoints = 1.6;
        return new AstroConfig(houseSystem, ayanamsha, observerPosition, zodiacType, projectionType, orbMethod,
            celPointsSpecs, aspectSpecs, mundanePointSpecs, arabicPointSpecs, zodiacPointSpecs, baseOrbAspects, baseOrbMidpoints);
    }

    private static List<CelPointConfigSpecs> CreateCelPoints()
    {
        List<CelPointConfigSpecs> celPointSpecs = new()
        {
            new CelPointConfigSpecs(CelPoints.Sun, 100, true),
            new CelPointConfigSpecs(CelPoints.Moon, 100, true),
            new CelPointConfigSpecs(CelPoints.Mercury, 80, true),
            new CelPointConfigSpecs(CelPoints.Venus, 80, true),
            new CelPointConfigSpecs(CelPoints.Earth, 100, false),
            new CelPointConfigSpecs(CelPoints.Mars, 80, true),
            new CelPointConfigSpecs(CelPoints.Jupiter, 65, true),
            new CelPointConfigSpecs(CelPoints.Saturn, 65, true),
            new CelPointConfigSpecs(CelPoints.Uranus, 50, true),
            new CelPointConfigSpecs(CelPoints.Neptune, 50, true),
            new CelPointConfigSpecs(CelPoints.Pluto, 50, true),
            new CelPointConfigSpecs(CelPoints.MeanNode, 65, false),
            new CelPointConfigSpecs(CelPoints.TrueNode, 65, true),
            new CelPointConfigSpecs(CelPoints.Chiron, 65, true),
            new CelPointConfigSpecs(CelPoints.PersephoneRam, 40, false),
            new CelPointConfigSpecs(CelPoints.HermesRam, 40, false),
            new CelPointConfigSpecs(CelPoints.DemeterRam, 40, false),
            new CelPointConfigSpecs(CelPoints.CupidoUra, 40, false),
            new CelPointConfigSpecs(CelPoints.HadesUra, 40, false),
            new CelPointConfigSpecs(CelPoints.ZeusUra, 40, false),
            new CelPointConfigSpecs(CelPoints.KronosUra, 40, false),
            new CelPointConfigSpecs(CelPoints.ApollonUra, 40, false),
            new CelPointConfigSpecs(CelPoints.AdmetosUra, 40, false),
            new CelPointConfigSpecs(CelPoints.VulcanusUra, 40, false),
            new CelPointConfigSpecs(CelPoints.PoseidonUra, 40, false),
            new CelPointConfigSpecs(CelPoints.Eris, 40, false),
            new CelPointConfigSpecs(CelPoints.Pholus, 40, false),
            new CelPointConfigSpecs(CelPoints.Ceres, 40, false),
            new CelPointConfigSpecs(CelPoints.Pallas, 40, false),
            new CelPointConfigSpecs(CelPoints.Juno, 40, false),
            new CelPointConfigSpecs(CelPoints.Vesta, 40, false),
            new CelPointConfigSpecs(CelPoints.Isis, 40, false),
            new CelPointConfigSpecs(CelPoints.Nessus, 40, false),
            new CelPointConfigSpecs(CelPoints.Huya, 40, false),
            new CelPointConfigSpecs(CelPoints.Varuna, 40, false),
            new CelPointConfigSpecs(CelPoints.Ixion, 40, false),
            new CelPointConfigSpecs(CelPoints.Quaoar, 40, false),
            new CelPointConfigSpecs(CelPoints.Haumea, 40, false),
            new CelPointConfigSpecs(CelPoints.Orcus, 40, false),
            new CelPointConfigSpecs(CelPoints.Makemake, 40, false),
            new CelPointConfigSpecs(CelPoints.Sedna, 40, false),
            new CelPointConfigSpecs(CelPoints.Hygieia, 40, false),
            new CelPointConfigSpecs(CelPoints.Astraea, 40, false),
            new CelPointConfigSpecs(CelPoints.ApogeeMean, 65, true),
            new CelPointConfigSpecs(CelPoints.ApogeeCorrected, 65, false),
            new CelPointConfigSpecs(CelPoints.ApogeeInterpolated, 65, false),
            new CelPointConfigSpecs(CelPoints.ApogeeDuval, 65, false),
  //          new CelPointConfigSpecs(CelPoints.ZeroAries, 40, false),
  //          new CelPointConfigSpecs(CelPoints.ParsFortunaNoSect, 65, false),
  //          new CelPointConfigSpecs(CelPoints.ParsFortunaSect, 65, true),
            new CelPointConfigSpecs(CelPoints.PersephoneCarteret, 40, false),
            new CelPointConfigSpecs(CelPoints.VulcanusCarteret, 40, false)
        };
        return celPointSpecs;
    }

    private static List<AspectConfigSpecs> CreateAspects()
    {
        List<AspectConfigSpecs> aspectSpecs = new()
        {
            new AspectConfigSpecs(AspectTypes.Conjunction, 100, true),
            new AspectConfigSpecs(AspectTypes.Opposition, 100, true),
            new AspectConfigSpecs(AspectTypes.Triangle, 85, true),
            new AspectConfigSpecs(AspectTypes.Square, 85, true),
            new AspectConfigSpecs(AspectTypes.Septile, 30, false),
            new AspectConfigSpecs(AspectTypes.Sextile, 70, true),
            new AspectConfigSpecs(AspectTypes.Quintile, 30, false),
            new AspectConfigSpecs(AspectTypes.SemiSextile, 30, false),
            new AspectConfigSpecs(AspectTypes.SemiSquare, 30, false),
            new AspectConfigSpecs(AspectTypes.SemiQuintile, 30, false),
            new AspectConfigSpecs(AspectTypes.BiQuintile, 30, false),
            new AspectConfigSpecs(AspectTypes.Inconjunct, 30, true),
            new AspectConfigSpecs(AspectTypes.SesquiQuadrate, 30, false),
            new AspectConfigSpecs(AspectTypes.TriDecile, 15, false),
            new AspectConfigSpecs(AspectTypes.BiSeptile, 15, false),
            new AspectConfigSpecs(AspectTypes.TriSeptile, 15, false),
            new AspectConfigSpecs(AspectTypes.Novile, 15, false),
            new AspectConfigSpecs(AspectTypes.BiNovile, 15, false),
            new AspectConfigSpecs(AspectTypes.QuadraNovile, 15, false),
            new AspectConfigSpecs(AspectTypes.Undecile, 15, false),
            new AspectConfigSpecs(AspectTypes.Centile, 15, false),
            new AspectConfigSpecs(AspectTypes.Vigintile, 15, false)
        };
        return aspectSpecs;
    }

    private static List<MundanePointConfigSpecs> CreateMundanePoints()
    {
        List<MundanePointConfigSpecs> mundanePointSpecs = new()
        {
            new MundanePointConfigSpecs(MundanePoints.Ascendant, 100, true),
            new MundanePointConfigSpecs(MundanePoints.Mc, 100, true),
            new MundanePointConfigSpecs(MundanePoints.EastPoint, 20, false),
            new MundanePointConfigSpecs(MundanePoints.Vertex, 20, false)
        };
        return mundanePointSpecs;
    }

    private static List<ArabicPointConfigSpecs> CreateArabicPoints()
    {
        List<ArabicPointConfigSpecs> arabicPointSpecs = new()
        {
            new ArabicPointConfigSpecs(ArabicPoints.FortunaSect, 40, true),
            new ArabicPointConfigSpecs(ArabicPoints.FortunaNoSect, 40, false)
        };
        return arabicPointSpecs;
    }

    private static List<ZodiacPointConfigSpecs> CreateZodiacPoints()
    {
        List<ZodiacPointConfigSpecs> zodiacPointSpecs = new()
        {
            new ZodiacPointConfigSpecs(ZodiacPoints.ZeroAries, 0, false)
        };
        return zodiacPointSpecs;
    }

}