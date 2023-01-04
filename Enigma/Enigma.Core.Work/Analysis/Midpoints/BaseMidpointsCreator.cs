// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Analysis.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Points;

namespace Enigma.Core.Work.Analysis.Midpoints;


/// <inheritdoc/>
public class BaseMidpointsCreator : IBaseMidpointsCreator
{
    private readonly double _halfCircle = 180.0;
    private readonly double _fullCircle = 360.0;

    /// <inheritdoc/>
    public List<BaseMidpoint> CreateBaseMidpoints(List<AnalysisPoint> analysisPoints)
    {
        List<BaseMidpoint> midpoints = new();
        int nrOfPoints = analysisPoints.Count;
        for (int i = 0; i < nrOfPoints; i++)
        {
            for (int j = i + 1; j < nrOfPoints; j++)
            {
                midpoints.Add(ConstructEffectiveMidpointInDial(analysisPoints[i], analysisPoints[j]));
            }
        }
        return midpoints;
    }


    /// <inheritdoc/>
    public List<BaseMidpoint> ConvertBaseMidpointsToDial(List<BaseMidpoint> baseMidpoints, double dialSize)
    {
        List<BaseMidpoint> convertedMidpoints = new();
        foreach (var midpoint in baseMidpoints)
        {
            double pos = midpoint.Position;
            while (pos >= dialSize) pos -= dialSize;
            convertedMidpoints.Add(new BaseMidpoint(midpoint.Point1, midpoint.Point2, pos));
        }
        return convertedMidpoints;
    }


    private BaseMidpoint ConstructEffectiveMidpointInDial(AnalysisPoint point1, AnalysisPoint point2)
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
        return new BaseMidpoint(point1, point2, mPos);
    }


}