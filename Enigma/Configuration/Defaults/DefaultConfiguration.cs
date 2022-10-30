// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;
using Enigma.Domain.Analysis;
using Enigma.Domain.Enums;
using Enigma.Domain.AstronCalculations;

namespace Enigma.Configuration.Parsers;
public interface IDefaultConfiguration
{
    public AstroConfig CreateDefaultConfig();
}

public class DefaultConfiguration : IDefaultConfiguration
{
    public AstroConfig CreateDefaultConfig()
    {
        return CombineDefaultDetails();
    }

    private AstroConfig CombineDefaultDetails()
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

    private List <CelPointSpecs> CreateCelPoints()
    {
        List<CelPointSpecs> celPointSpecs = new()
        {
            new CelPointSpecs(SolarSystemPoints.Sun, 100, true),
            new CelPointSpecs(SolarSystemPoints.Moon, 100, true),
            new CelPointSpecs(SolarSystemPoints.Mercury, 80, true),
            new CelPointSpecs(SolarSystemPoints.Venus, 80, true),
            new CelPointSpecs(SolarSystemPoints.Earth, 100, false),
            new CelPointSpecs(SolarSystemPoints.Mars, 80, true),
            new CelPointSpecs(SolarSystemPoints.Jupiter, 65, true),
            new CelPointSpecs(SolarSystemPoints.Saturn, 65, true),
            new CelPointSpecs(SolarSystemPoints.Uranus, 50, true),
            new CelPointSpecs(SolarSystemPoints.Neptune, 50, true),
            new CelPointSpecs(SolarSystemPoints.Pluto, 50, true),
            new CelPointSpecs(SolarSystemPoints.MeanNode, 65, false),
            new CelPointSpecs(SolarSystemPoints.TrueNode, 65, true),
            new CelPointSpecs(SolarSystemPoints.Chiron, 65, true),
            new CelPointSpecs(SolarSystemPoints.PersephoneRam, 40, false),
            new CelPointSpecs(SolarSystemPoints.HermesRam, 40, false),
            new CelPointSpecs(SolarSystemPoints.DemeterRam, 40, false),
            new CelPointSpecs(SolarSystemPoints.CupidoUra, 40, false),
            new CelPointSpecs(SolarSystemPoints.HadesUra, 40, false),
            new CelPointSpecs(SolarSystemPoints.ZeusUra, 40, false),
            new CelPointSpecs(SolarSystemPoints.KronosUra, 40, false),
            new CelPointSpecs(SolarSystemPoints.ApollonUra, 40, false),
            new CelPointSpecs(SolarSystemPoints.AdmetosUra, 40, false),
            new CelPointSpecs(SolarSystemPoints.VulcanusUra, 40, false),
            new CelPointSpecs(SolarSystemPoints.PoseidonUra, 40, false),
            new CelPointSpecs(SolarSystemPoints.Eris, 40, false),
            new CelPointSpecs(SolarSystemPoints.Pholus, 40, false),
            new CelPointSpecs(SolarSystemPoints.Ceres, 40, false),
            new CelPointSpecs(SolarSystemPoints.Pallas, 40, false),
            new CelPointSpecs(SolarSystemPoints.Juno, 40, false),
            new CelPointSpecs(SolarSystemPoints.Vesta, 40, false),
            new CelPointSpecs(SolarSystemPoints.Isis, 40, false),
            new CelPointSpecs(SolarSystemPoints.Nessus, 40, false),
            new CelPointSpecs(SolarSystemPoints.Huya, 40, false),
            new CelPointSpecs(SolarSystemPoints.Varuna, 40, false),
            new CelPointSpecs(SolarSystemPoints.Ixion, 40, false),
            new CelPointSpecs(SolarSystemPoints.Quaoar, 40, false),
            new CelPointSpecs(SolarSystemPoints.Haumea, 40, false),
            new CelPointSpecs(SolarSystemPoints.Orcus, 40, false),
            new CelPointSpecs(SolarSystemPoints.Makemake, 40, false),
            new CelPointSpecs(SolarSystemPoints.Sedna, 40, false),
            new CelPointSpecs(SolarSystemPoints.Hygieia, 40, false),
            new CelPointSpecs(SolarSystemPoints.Astraea, 40, false),
            new CelPointSpecs(SolarSystemPoints.ApogeeMean, 65, true),
            new CelPointSpecs(SolarSystemPoints.ApogeeCorrected, 65, false),
            new CelPointSpecs(SolarSystemPoints.ApogeeInterpolated, 65, false),
            new CelPointSpecs(SolarSystemPoints.ApogeeDuval, 65, false),
            new CelPointSpecs(SolarSystemPoints.ZeroAries, 40, false),
            new CelPointSpecs(SolarSystemPoints.ParsFortunaNoSect, 65, false),
            new CelPointSpecs(SolarSystemPoints.ParsFortunaSect, 65, true),
            new CelPointSpecs(SolarSystemPoints.PersephoneCarteret, 40, false),
            new CelPointSpecs(SolarSystemPoints.VulcanusCarteret, 40, false)
        };
        return celPointSpecs;
    }

    private List<AspectSpecs> CreateAspects()
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

    private List<MundanePointSpecs> CreateMundanePoints()
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

    private List<ArabicPointSpecs> CreateArabicPoints()
    {
        List<ArabicPointSpecs> arabicPointSpecs = new()
        {
            new ArabicPointSpecs(ArabicPoints.FortunaSect, 40, true),
            new ArabicPointSpecs(ArabicPoints.FortunaNoSect, 40, false)
        };
        return arabicPointSpecs;
    }



}