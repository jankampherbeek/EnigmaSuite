// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;

namespace Enigma.Domain.Analysis;

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