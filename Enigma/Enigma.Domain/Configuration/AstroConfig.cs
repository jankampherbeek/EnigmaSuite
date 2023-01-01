// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.Points;

namespace Enigma.Domain.Configuration;

public class AstroConfig
{
    public HouseSystems HouseSystem { get; }
    public Ayanamshas Ayanamsha { get; }
    public ObserverPositions ObserverPosition { get; }
    public ZodiacTypes ZodiacType { get; }
    public ProjectionTypes ProjectionType { get; }
    public OrbMethods OrbMethod { get; }
    public List<CelPointConfigSpecs> CelPoints { get; }
    public List<AspectConfigSpecs> Aspects { get; }
    public List<MundanePointConfigSpecs> MundanePoints { get; }
    public List<ArabicPointConfigSpecs> ArabicPoints { get; }
    public List<ZodiacPointConfigSpecs> ZodiacPoints { get; }
    public double BaseOrbAspects { get; }
    public double BaseOrbMidpoints { get; }

    public AstroConfig(HouseSystems houseSystem, Ayanamshas ayanamsha, ObserverPositions observerPosition, ZodiacTypes zodiacType, ProjectionTypes projectionType, OrbMethods orbMethod,
        List<CelPointConfigSpecs> celPoints, List<AspectConfigSpecs> aspects, List<MundanePointConfigSpecs> mundanePoints, List<ArabicPointConfigSpecs> arabicPoints, List<ZodiacPointConfigSpecs> zodiacPoints, 
        double baseOrbAspects, double baseOrbMidpoints)
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
        ZodiacPoints = zodiacPoints; 
        BaseOrbAspects = baseOrbAspects;
        BaseOrbMidpoints = baseOrbMidpoints;
    }
}

/// <summary>Details for a celestial point to be used in a configuration.</summary>
/// <param name="CelPoint">The celestial point.</param>
/// <param name="PercentageOrb">Factor to calculate the orb.</param>
/// <param name="IsUsed">True if selected, otherwise false.</param>
public record CelPointConfigSpecs(CelPoints CelPoint, int PercentageOrb, bool IsUsed);

/// <summary>Details for an aspect to be used in a configuration.</summary>
/// <param name="AspectType">The aspect.</param>
/// <param name="PercentageOrb">Factor to calculate the orb.</param>
/// <param name="IsUsed">True if selected, otherwise false.</param>
public record AspectConfigSpecs(AspectTypes AspectType, int PercentageOrb, bool IsUsed);

/// <summary>Details for a mundane point to be used in a configuration.</summary>
/// <param name="MundanePoint">The mundane point.</param>
/// <param name="PercentageOrb">Factor to calculate the orb.</param>
/// <param name="IsUsed">True if selected, otherwise false.</param>
public record MundanePointConfigSpecs(MundanePoints MundanePoint, int PercentageOrb, bool IsUsed);


/// <summary>Details for an Arabic Point to be used in a configuration.</summary>
/// <param name="ArabicPoint">The Arabic Point.</param>
/// <param name="PercentageOrb">Factor to calculate the orb.</param>
/// <param name="IsUsed">True if selected, otherwise false.</param>
public record ArabicPointConfigSpecs(ArabicPoints ArabicPoint, int PercentageOrb, bool IsUsed);


/// <summary>Details for a zodiacpoint to be used in a configuration.</summary>
/// <param name="ZodiacPoint">The ZodiacPoint.</param>
/// <param name="PercentageOrb">Factor to calculate the orb.</param>
/// <param name="IsUsed">True if selected, otherwise false.</param>
public record ZodiacPointConfigSpecs(ZodiacPoints ZodiacPoint, int PercentageOrb, bool IsUsed);

