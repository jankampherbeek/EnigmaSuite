// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;

namespace Enigma.Domain.Analysis.Aspects;

/// <summary>
/// Details for an effective aspect and its forming partners.
/// </summary>
public record EffectiveAspect
{
    public readonly string MundanePoint;
    public readonly CelPoints? CelPoint1;
    public readonly CelPoints CelPoint2;
    public readonly AspectDetails EffAspectDetails;
    public readonly double Orb;
    public readonly double ActualOrb;
    public readonly bool IsMundane;

    /// <summary>
    /// Construct a mundane aspect (between cusp and celestial point).
    /// </summary>
    /// <param name="mundanePoint">String that indicates the mundane point.</param>
    /// <param name="celPoint">The celestial point.</param>
    /// <param name="effAspectDetails">Details for the aspect.</param>
    /// <param name="orb">The actual orb.</param>
    public EffectiveAspect(string mundanePoint, CelPoints celPoint, AspectDetails effAspectDetails, double orb, double actualOrb)
    {
        IsMundane = true;
        MundanePoint = mundanePoint;
        CelPoint1 = null;
        CelPoint2 = celPoint;
        EffAspectDetails = effAspectDetails;
        Orb = orb;
        ActualOrb = actualOrb;
    }

    /// <summary>
    /// Construct an aspect between two celestial points.
    /// </summary>
    /// <param name="celPoint1">The first celestial point.</param>
    /// <param name="celPoint2">The second celestial point.</param>
    /// <param name="effAspectDetails">Details for the aspect.</param>
    /// <param name="orb">The actual orb.</param>
    public EffectiveAspect(CelPoints celPoint1, CelPoints celPoint2, AspectDetails effAspectDetails, double orb, double actualOrb)
    {
        IsMundane = false;
        MundanePoint = "";
        CelPoint1 = celPoint1;
        CelPoint2 = celPoint2;
        EffAspectDetails = effAspectDetails;
        Orb = orb;
        ActualOrb = actualOrb;
    }

}


/// <summary>List of aspects for a specific chart.</summary>
/// <remarks>Main usage is for research projects that involve the counting of aspects.</remarks>
/// <param name="ChartId">Unique id for the chart (unique within the existing dataset).</param>
/// <param name="EffectiveAspects">The aspects that are effective for this chart.</param>
public record AspectsPerChart(int ChartId, List<EffectiveAspect> EffectiveAspects);