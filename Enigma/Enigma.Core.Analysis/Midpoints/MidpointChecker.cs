// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis.Dto;
using Enigma.Domain.Analysis;

namespace Enigma.Core.Analysis.Midpoints;


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


/// <inheritdoc/>
public class MidpointChecker : IMidpointChecker
{
    private readonly double _halfCircle = 180.0;
    private readonly double _fullCircle = 360.0;

    private readonly IMidpointOrbConstructor _orbConstructor;
    private readonly IMidpointSpecifications _midpointSpecifications;

    public MidpointChecker(IMidpointOrbConstructor orbConstructor, IMidpointSpecifications midpointSpecifications)
    {
        _orbConstructor = orbConstructor;
        _midpointSpecifications = midpointSpecifications;
    }

    /// <inheritdoc/>
    public List<EffectiveMidpoint> FindMidpoints(List<AnalysisPoint> analysisPoints)
    {
        List<EffectiveMidpoint> midpoints = new();
        int nrOfPoints = analysisPoints.Count;
        for (int i = 0; i < nrOfPoints; i++)
        {
            for (int j = i + 1; j < nrOfPoints; j++)
            {
                midpoints.Add(ConstructEffectiveMidpoint(analysisPoints[i], analysisPoints[j]));
            }
        }
        return midpoints;
    }

    public List<EffOccupiedMidpoint> FindOccupiedMidpoints(MidpointTypes midpointType, List<AnalysisPoint> analysisPoints)
    {
        throw new NotImplementedException();
    }

 
    private EffectiveMidpoint ConstructEffectiveMidpoint(AnalysisPoint point1, AnalysisPoint point2)
    {
        double pos1 = point1.Position;
        double pos2 = point2.Position;

        double smallPos = (pos1 < pos2) ? pos1 : pos2;
        double largePos = (pos1 < pos2) ? pos2 : pos1;

        double diff = largePos - smallPos;

        double firstPosShortestArc = (diff < _halfCircle) ? smallPos : largePos;
        double lastPosShortestArc = (diff < _halfCircle) ? largePos : smallPos;

        diff = lastPosShortestArc - firstPosShortestArc;
        if (diff < 0.0) diff += _fullCircle;

        double mPos = (diff / 2) + firstPosShortestArc;
        if (mPos >= _fullCircle) mPos -= _fullCircle;

        return new EffectiveMidpoint(point1, point2, mPos);

    }

}
