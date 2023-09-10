// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Points;
using Enigma.Domain.References;

namespace Enigma.Domain.Configuration;

public sealed class AstroConfig
{
    public HouseSystems HouseSystem { get; }
    public Ayanamshas Ayanamsha { get; }
    public ObserverPositions ObserverPosition { get; }
    public ZodiacTypes ZodiacType { get; }
    public ProjectionTypes ProjectionType { get; }
    public OrbMethods OrbMethod { get; }
    public bool UseCuspsForAspects { get; }
    public Dictionary<ChartPoints, ChartPointConfigSpecs> ChartPoints;
    public Dictionary<AspectTypes, AspectConfigSpecs> Aspects;
    public double BaseOrbAspects { get; }
    public double BaseOrbMidpoints { get; }


    public AstroConfig(HouseSystems houseSystem, Ayanamshas ayanamsha, ObserverPositions observerPosition, ZodiacTypes zodiacType, ProjectionTypes projectionType, OrbMethods orbMethod,
        Dictionary<ChartPoints, ChartPointConfigSpecs> chartPoints, Dictionary<AspectTypes, AspectConfigSpecs> aspects, double baseOrbAspects, double baseOrbMidpoints, bool useCuspsForAspects)
    {
        HouseSystem = houseSystem;
        Ayanamsha = ayanamsha;
        ObserverPosition = observerPosition;
        ZodiacType = zodiacType;
        ProjectionType = projectionType;
        OrbMethod = orbMethod;
        ChartPoints = chartPoints;
        Aspects = aspects;
        BaseOrbAspects = baseOrbAspects;
        BaseOrbMidpoints = baseOrbMidpoints;
        UseCuspsForAspects = useCuspsForAspects;
    }
}

/// <summary>Configuration details for a chart point.</summary>
/// <param name="IsUsed">True if selected, otherwise false.</param>
/// <param name="Glyph">Character for the glyph, space if no glyph is available.</param>
/// <param name="PercentageOrb">Factor to calculate the orb.</param>
public record ChartPointConfigSpecs(bool IsUsed, char Glyph, int PercentageOrb);


/// <summary>Details for an aspect to be used in a configuration.</summary>
/// <param name="IsUsed">True if selected, otherwise false.</param>
/// <param name="Glyph">Character for the glyph, space if no glyph is available.</param>
/// <param name="PercentageOrb">Factor to calculate the orb.</param>

public record AspectConfigSpecs(bool IsUsed, char Glyph, int PercentageOrb);



