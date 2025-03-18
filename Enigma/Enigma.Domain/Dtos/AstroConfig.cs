// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024, 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Drawing;
using Enigma.Domain.References;

namespace Enigma.Domain.Dtos;

public sealed class AstroConfig(
    HouseSystems houseSystem,
    Ayanamshas ayanamsha,
    ObserverPositions observerPosition,
    ZodiacTypes zodiacType,
    ProjectionTypes projectionType,
    OrbMethods orbMethod,
    Dictionary<ChartPoints, ChartPointConfigSpecs> chartPoints,
    Dictionary<AspectTypes, AspectConfigSpecs> aspects,
    Dictionary<AspectTypes, string> aspectColors,
    double baseOrbAspects,
    double baseOrbMidpoints,
    double orbParallels,
    double orbMidpointsDecl,
    bool useCuspsForAspects,
    ApogeeTypes apogeeType,
    bool oscillateNodes)
{
    public HouseSystems HouseSystem { get; } = houseSystem;
    public Ayanamshas Ayanamsha { get; } = ayanamsha;
    public ObserverPositions ObserverPosition { get; } = observerPosition;
    public ZodiacTypes ZodiacType { get; } = zodiacType;
    public ProjectionTypes ProjectionType { get; } = projectionType;
    public OrbMethods OrbMethod { get; } = orbMethod;
    public bool UseCuspsForAspects { get; } = useCuspsForAspects;
    public Dictionary<ChartPoints, ChartPointConfigSpecs> ChartPoints = chartPoints;
    public Dictionary<AspectTypes, AspectConfigSpecs> Aspects = aspects;
    public Dictionary<AspectTypes, string> AspectColors = aspectColors;
    public double BaseOrbAspects { get; } = baseOrbAspects;
    public double BaseOrbMidpoints { get; } = baseOrbMidpoints;
    public double OrbMidpointsDecl { get; } = orbMidpointsDecl;
    public double OrbParallels { get; } = orbParallels;
    public ApogeeTypes ApogeeType { get; } = apogeeType;
    public bool OscillateNodes { get; } = oscillateNodes;
}

/// <summary>Configuration details for a chart point.</summary>
/// <param name="IsUsed">True if selected, otherwise false.</param>
/// <param name="Glyph">Character for the glyph, space if no glyph is available.</param>
/// <param name="PercentageOrb">Factor to calculate the orb.</param>
/// <param name="ShowInChart">True if chart point should be visible in chart, otherwise false.</param>
public record ChartPointConfigSpecs(bool IsUsed, char Glyph, int PercentageOrb, bool ShowInChart);



/// <summary>Details for an aspect to be used in a configuration.</summary>
/// <param name="IsUsed">True if selected, otherwise false.</param>
/// <param name="Glyph">Character for the glyph, space if no glyph is available.</param>
/// <param name="PercentageOrb">Factor to calculate the orb.</param>
/// <param name="ShowInChart">True if aspect should be visible in chart, otherwise false.</param>
public record AspectConfigSpecs(bool IsUsed, char Glyph, int PercentageOrb, bool ShowInChart);

