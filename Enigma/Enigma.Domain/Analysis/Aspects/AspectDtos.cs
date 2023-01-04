// Jan Kampherbeek, (c) 2022, 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Points;

namespace Enigma.Domain.Analysis.Aspects;


/// <summary>Details for an actual aspect and its forming partners.</summary>
/// <param name="Point1">The first point.</param>
/// <param name="Point2">The second point.</param>
/// <param name="Aspect">Details about the aspect.</param>
/// <param name="maxOrb">Max allowed orb.</param>
/// <param name="actualOrb">Actual orb.</param>
public record DefinedAspect(GeneralPoint Point1, GeneralPoint Point2, AspectDetails Aspect, double maxOrb, double actualOrb);


/// <summary>
/// Details for an effective aspect and its forming partners.
/// </summary>
[Obsolete]
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
public record AspectsPerChart(string ChartId, List<EffectiveAspect> EffectiveAspects);


/// <summary>Totals of aspect counts.</summary>
/// <param name="CelPoints">All celestial points that have been used.</param>
/// <param name="MundanePoints">All mundane points that have been used, if any.</param>
/// <param name="Cusps">All cusps that have been used, if any.</param>
/// <param name="AspectTypes">All aspects that have been used.</param>
/// <param name="Totals">A 2d array with the counts.</param>
/// <remarks>The first index relates to the CelPoints, MundanePoints and Cusps (in that sequence). The second index relates to the AspectTypes.</remarks>
/// <example>Note that all indexes start at zero. If the record contains 5 celestial points, no mundane points and 12 cusps, and 4 aspects, the position [7,2] refers to the third cusp and the third aspect.</example>
public record AspectTotals(List<CelPoints> CelPoints, List<MundanePoints> MundanePoints, List<int> Cusps, List<AspectTypes> AspectTypes, int[,] Totals);