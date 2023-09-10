// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;
using Enigma.Domain.Research;

namespace Enigma.Domain.Interfaces;

/// <summary>Manages points that need to be excluded from research.</summary>
public interface IPointsExclusionManager
{
    /// <summary>Define point to exclude for a specific research method.</summary>
    /// <param name="researchMethod"/>
    /// <returns>Specification of points that need to be excluded using the given research method.</returns>
    public PointsToExclude DefineExclusions(ResearchMethods researchMethod);
}