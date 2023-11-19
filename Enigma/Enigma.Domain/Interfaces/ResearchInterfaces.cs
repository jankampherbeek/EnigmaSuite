// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Research;

namespace Enigma.Domain.Interfaces;

/// <summary>Manages points that need to be excluded from research.</summary>
public interface IPointsExclusionManager
{
    /// <summary>Define points to exclude for a specific research method.</summary>
    /// <param name="researchMethod"/>
    /// <returns>Specification of points that need to be excluded using the given research method.</returns>
    public PointsToExclude DefineExclusions(ResearchMethods researchMethod);

    /// <summary>Define points to exclude for heliocentric calculations</summary>
    /// <returns>Specification of points that need to be excluded</returns>
    public PointsToExclude DefineHelioExclusions();

    /// <summary>Define points to exclude for the calculation of cycles</summary>
    /// <returns>Specification of points that need to be excluded</returns>
    public PointsToExclude DefineCycleExclusions();
}