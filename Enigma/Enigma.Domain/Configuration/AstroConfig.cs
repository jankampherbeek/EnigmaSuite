// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;

namespace Enigma.Domain.Configuration;

public class AstroConfig
{
    public HouseSystems HouseSystem { get; }
    public Ayanamshas Ayanamsha { get; }
    public ObserverPositions ObserverPosition { get; }
    public ZodiacTypes ZodiacType { get; }
    public ProjectionTypes ProjectionType { get; }
    public OrbMethods OrbMethod { get; }
    public List<CelPointSpecs> CelPoints { get; }
    public List<AspectSpecs> Aspects { get; }
    public List<MundanePointSpecs> MundanePoints { get; }
    public List<ArabicPointSpecs> ArabicPoints { get; }
    public double BaseOrbAspects { get; }
    public double BaseOrbMidpoints { get; }

    public AstroConfig(HouseSystems houseSystem, Ayanamshas ayanamsha, ObserverPositions observerPosition, ZodiacTypes zodiacType, ProjectionTypes projectionType, OrbMethods orbMethod,
        List<CelPointSpecs> celPoints, List<AspectSpecs> aspects, List<MundanePointSpecs> mundanePoints, List<ArabicPointSpecs> arabicPoints, double baseOrbAspects, double baseOrbMidpoints)
    {
        HouseSystem = houseSystem;
        Ayanamsha = ayanamsha;
        ObserverPosition = observerPosition;
        ZodiacType = zodiacType;
        ProjectionType = projectionType;
        OrbMethod = orbMethod;
        CelPoints = celPoints;
        MundanePoints = mundanePoints;
        ArabicPoints = arabicPoints;
        Aspects = aspects;
        BaseOrbAspects = baseOrbAspects;
        BaseOrbMidpoints = baseOrbMidpoints;    
    }
}


// TODO rename CelPointSpecs, AspectSpecs, MundanePOintSpecs and ArabicPointSpecs to *FrontendSpecs

public record CelPointSpecs
{
    public SolarSystemPoints SolarSystemPoint { get; }
    public int PercentageAspectOrb { get; }
    public bool IsUsed { get; }

    public CelPointSpecs(SolarSystemPoints solarSystemPoint, int percentageAspectOrb, bool isUsed)
    {
        SolarSystemPoint = solarSystemPoint;
        PercentageAspectOrb = percentageAspectOrb;
        IsUsed = isUsed;
    }
}

public record AspectSpecs
{
    public AspectTypes AspectType { get; }
    public int PercentageAspectOrb { get; }
    public bool IsUsed { get; }

    public AspectSpecs(AspectTypes aspectType, int percentageaspectOrb, bool isUsed)
    {
        AspectType = aspectType;
        PercentageAspectOrb = percentageaspectOrb;
        IsUsed = isUsed;
    }
}

public record MundanePointSpecs
{
    public MundanePoints MundanePoint { get; }
    public int PercentageOrb { get; }
    public bool IsUsed { get; }

    public MundanePointSpecs(MundanePoints mundanePoint, int percentageOrb, bool isUsed)
    {
        MundanePoint = mundanePoint;
        PercentageOrb = percentageOrb;
        IsUsed = isUsed;
    }
}


public record ArabicPointSpecs
{
    public ArabicPoints ArabicPoint { get; }
    public int PercentageOrb { get; }
    public bool IsUsed { get; }

    public ArabicPointSpecs(ArabicPoints arabicPoint, int percentageOrb, bool isUsed)
    {
        ArabicPoint = arabicPoint;
        PercentageOrb = percentageOrb;
        IsUsed = isUsed;
    }
}