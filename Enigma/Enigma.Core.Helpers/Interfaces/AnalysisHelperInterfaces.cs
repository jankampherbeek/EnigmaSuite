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
public interface IMidpointsHelper
{

    /// <summary>Create the definition of an effective (occupied) midpoint.</summary>
    /// <param name="point1">The first participating point.</param>
    /// <param name="point2">The second participating point</param>
    /// <returns>Defined effective midpoint.</returns>
    public EffectiveMidpoint ConstructEffectiveMidpoint(AnalysisPoint point1, AnalysisPoint point2);

    /// <summary>Measure deviation from an exact midpoint.</summary>
    /// <param name="division">Fraction of 360 degrees, the midpoint-wheel.</param>
    /// <param name="midpointPos">Position of the midpoint.</param>
    /// <param name="posCelPoint">Position of the point to compare with the midpoint.</param>
    /// <returns>Deviation from the exact midpoint in degrees.</returns>
    public double MeasureMidpointDeviation(double division, double midpointPos, double posCelPoint);

    /// <summary>Convert a list of midpoints in a 360 degree dial to midpoints within a specific dial.</summary>
    /// <param name="division">Factor for the dial.</param>
    /// <param name="midPoints360Degrees">Original midpoints ina  360 degree dial.</param>
    /// <returns>Midpoints in a specific dial.</returns>
    public List<EffectiveMidpoint> CreateMidpoints4Dial(double division, List<EffectiveMidpoint> midPoints360Degrees);

}