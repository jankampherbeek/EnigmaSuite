// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;

namespace Enigma.Core.Analysis.Interfaces;

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