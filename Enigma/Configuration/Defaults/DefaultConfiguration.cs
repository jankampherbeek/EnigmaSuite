// Jan Kampherbeek, (c) 2022.
// Enigma Research is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Configuration.Domain;
using Enigma.Domain.Analysis;
using Enigma.Domain.CalcVars;

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
            new CelPointSpecs(SolarSystemPoints.Sun, 1.0, true),
            new CelPointSpecs(SolarSystemPoints.Moon, 1.0, true),
            new CelPointSpecs(SolarSystemPoints.Mercury, 0.8, true),
            new CelPointSpecs(SolarSystemPoints.Venus, 0.8, true),
            new CelPointSpecs(SolarSystemPoints.Earth, 1.0, false),
            new CelPointSpecs(SolarSystemPoints.Mars, 0.8, true),
            new CelPointSpecs(SolarSystemPoints.Jupiter, 0.65, true),
            new CelPointSpecs(SolarSystemPoints.Saturn, 0.65, true),
            new CelPointSpecs(SolarSystemPoints.Uranus, 0.5, true),
            new CelPointSpecs(SolarSystemPoints.Neptune, 0.5, true),
            new CelPointSpecs(SolarSystemPoints.Pluto, 0.5, true),
            new CelPointSpecs(SolarSystemPoints.MeanNode, 0.65, false),
            new CelPointSpecs(SolarSystemPoints.TrueNode, 0.65, true),
            new CelPointSpecs(SolarSystemPoints.Chiron, 0.65, true),
            new CelPointSpecs(SolarSystemPoints.PersephoneRam, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.HermesRam, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.DemeterRam, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.CupidoUra, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.HadesUra, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.ZeusUra, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.KronosUra, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.ApollonUra, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.AdmetosUra, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.VulcanusUra, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.PoseidonUra, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Eris, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Pholus, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Ceres, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Pallas, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Juno, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Vesta, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Isis, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Nessus, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Huya, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Varuna, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Ixion, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Quaoar, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Haumea, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Orcus, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Makemake, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Sedna, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Hygieia, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.Astraea, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.ApogeeMean, 0.65, true),
            new CelPointSpecs(SolarSystemPoints.ApogeeCorrected, 0.65, false),
            new CelPointSpecs(SolarSystemPoints.ApogeeInterpolated, 0.65, false),
            new CelPointSpecs(SolarSystemPoints.ApogeeDuval, 0.65, false),
            new CelPointSpecs(SolarSystemPoints.ZeroAries, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.ParsFortunaNoSect, 0.65, false),
            new CelPointSpecs(SolarSystemPoints.ParsFortunaSect, 0.65, true),
            new CelPointSpecs(SolarSystemPoints.PersephoneCarteret, 0.4, false),
            new CelPointSpecs(SolarSystemPoints.VulcanusCarteret, 0.4, false)
        };
        return celPointSpecs;
    }

    private List<AspectSpecs> CreateAspects()
    {
        List<AspectSpecs> aspectSpecs = new()
        {
            new AspectSpecs(AspectTypes.Conjunction, 1.0, true),
            new AspectSpecs(AspectTypes.Opposition, 1.0, true),
            new AspectSpecs(AspectTypes.Triangle, 0.85, true),
            new AspectSpecs(AspectTypes.Square, 0.85, true),
            new AspectSpecs(AspectTypes.Septile, 0.3, false),
            new AspectSpecs(AspectTypes.Sextile, 0.7, true),
            new AspectSpecs(AspectTypes.Quintile, 0.3, false),
            new AspectSpecs(AspectTypes.SemiSextile, 0.3, false),
            new AspectSpecs(AspectTypes.SemiSquare, 0.3, false),
            new AspectSpecs(AspectTypes.SemiQuintile, 0.3, false),
            new AspectSpecs(AspectTypes.BiQuintile, 0.3, false),
            new AspectSpecs(AspectTypes.Inconjunct, 0.3, true),
            new AspectSpecs(AspectTypes.SesquiQuadrate, 0.3, false),
            new AspectSpecs(AspectTypes.TriDecile, 0.15, false),
            new AspectSpecs(AspectTypes.BiSeptile, 0.15, false),
            new AspectSpecs(AspectTypes.TriSeptile, 0.15, false),
            new AspectSpecs(AspectTypes.Novile, 0.15, false),
            new AspectSpecs(AspectTypes.BiNovile, 0.15, false),
            new AspectSpecs(AspectTypes.QuadraNovile, 0.15, false),
            new AspectSpecs(AspectTypes.Undecile, 0.15, false),
            new AspectSpecs(AspectTypes.Centile, 0.15, false),
            new AspectSpecs(AspectTypes.Vigintile, 0.15, false)
        };
        return aspectSpecs;
    }

    private List<MundanePointSpecs> CreateMundanePoints()
    {
        List<MundanePointSpecs> mundanePointSpecs = new()
        {
            new MundanePointSpecs(MundanePoints.Ascendant, 1.0, true),
            new MundanePointSpecs(MundanePoints.Mc, 1.0, true),
            new MundanePointSpecs(MundanePoints.EastPoint, 0.2, false),
            new MundanePointSpecs(MundanePoints.Vertex, 0.2, false)
        };
        return mundanePointSpecs;
    }

    private List<ArabicPointSpecs> CreateArabicPoints()
    {
        List<ArabicPointSpecs> arabicPointSpecs = new()
        {
            new ArabicPointSpecs(ArabicPoints.FortunaSect, 0.4, true),
            new ArabicPointSpecs(ArabicPoints.FortunaNoSect, 0.4, false)
        };
        return arabicPointSpecs;
    }



}