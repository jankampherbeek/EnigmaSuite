// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;

namespace Enigma.Domain.Analysis;

/// <summary>
/// Details for an effective midpoint.
/// </summary>
public record EffectiveMidpoint
{
    public readonly string MundanePoint;
    public readonly SolarSystemPoints? SolSysPoint1;
    public readonly SolarSystemPoints SolSysPoint2;
    public readonly AspectDetails EffAspectDetails;
    public readonly double Orb;
    public readonly double ActualOrb;
    public bool IsMundane;
/*
    /// <summary>
    /// Construct a mundane aspect (between cusp and celestial point).
    /// </summary>
    /// <param name="mundanePoint">String that indicates the mundane point.</param>
    /// <param name="solSysPoint">The solar system point.</param>
    /// <param name="effAspectDetails">Details for the aspect.</param>
    /// <param name="orb">The actual orb.</param>
    public EffectiveAspect(string mundanePoint, SolarSystemPoints solSysPoint, AspectDetails effAspectDetails, double orb, double actualOrb)
    {
        IsMundane = true;
        MundanePoint = mundanePoint;
        SolSysPoint1 = null;
        SolSysPoint2 = solSysPoint;
        EffAspectDetails = effAspectDetails;
        Orb = orb;
        ActualOrb = actualOrb;
    }

    /// <summary>
    /// Construct an aspect between two celestial points.
    /// </summary>
    /// <param name="solSysPoint1">The first solar system point.</param>
    /// <param name="solSysPoint2">The second solar system point.</param>
    /// <param name="effAspectDetails">Details for the aspect.</param>
    /// <param name="orb">The actual orb.</param>
    public EffectiveAspect(SolarSystemPoints solSysPoint1, SolarSystemPoints solSysPoint2, AspectDetails effAspectDetails, double orb, double actualOrb)
    {
        IsMundane = false;
        SolSysPoint1 = solSysPoint1;
        SolSysPoint2 = solSysPoint2;
        EffAspectDetails = effAspectDetails;
        Orb = orb;
        ActualOrb = actualOrb;
    }
*/
}