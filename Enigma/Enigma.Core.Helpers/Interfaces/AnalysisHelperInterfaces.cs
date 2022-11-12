// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;

namespace Enigma.Core.Helpers.Interfaces;


/// <summary>
/// Checks for aspects.
/// </summary>
public interface IAspectChecker
{
    /// <summary>
    /// Find aspects between celestial points.
    /// </summary>
    /// <param name="calculatedChart">Chart with positions.</param>
    /// <returns>List with effective aspects.</returns>
    List<EffectiveAspect> FindAspectsForSolSysPoints(CalculatedChart calculatedChart);
    /// <summary>
    /// Find aspects between a mundane point and a celestial point.
    /// </summary>
    /// <param name="calculatedChart">Chart with positions.</param>
    /// <returns>List with effective aspects.</returns>
    List<EffectiveAspect> FindAspectsForMundanePoints(CalculatedChart calculatedChart);
}


/// <summary>
/// Define actual orb for an aspect.
/// </summary>
public interface IAspectOrbConstructor
{
    /// <summary>Define orb between two celestial points.</summary>
    public double DefineOrb(SolarSystemPoints point1, SolarSystemPoints point2, AspectDetails aspectDetails);
    /// <summary>Define orb between mundane point and celestial point.
    public double DefineOrb(string mundanePoint, SolarSystemPoints solSysPoint, AspectDetails aspectDetails);
}

/// <summary>
/// Checks for midpoints.
/// </summary>
public interface IMidpointChecker
{
    /// <summary>
    /// Find all midpoints using the shortest arc between two points, longitudes based on a 360-degree dial.
    /// </summary>
    /// <param name="analysisPoints">Points to analyse.</param>
    /// <returns>List with all midpoints.</returns>
    List<EffectiveMidpoint> FindMidpoints(List<AnalysisPoint> analysisPoints);

    /// <summary>
    /// Find occupied midpoints.
    /// </summary>
    /// <param name="midPointType">Indicates the dial to use.</param>
    /// <param name="analysisPoints">Points to analyse.</param>
    /// <returns>List with all occupied midpoints.</returns>
    List<EffOccupiedMidpoint> FindOccupiedMidpoints(MidpointTypes midpointType, List<AnalysisPoint> analysisPoints);
}