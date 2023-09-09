// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Points;

namespace Enigma.Core.Analysis.Helpers;


/// <inheritdoc/>
public sealed class BaseMidpointsCreator : IBaseMidpointsCreator
{
    private const double HALF_CIRCLE = 180.0;
    private const double FULL_CIRCLE = 360.0;


    /// <inheritdoc/>
    public List<BaseMidpoint> CreateBaseMidpoints(List<PositionedPoint> positionedPoints)
    {
        List<BaseMidpoint> midpoints = new();
        int nrOfPoints = positionedPoints.Count;
        for (int i = 0; i < nrOfPoints; i++)
        {
            for (int j = i + 1; j < nrOfPoints; j++)
            {
                midpoints.Add(ConstructEffectiveMidpointInDial(positionedPoints[i], positionedPoints[j]));
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
            convertedMidpoints.Add(midpoint with { Position = pos });
        }
        return convertedMidpoints;
    }


    private static BaseMidpoint ConstructEffectiveMidpointInDial(PositionedPoint point1, PositionedPoint point2)
    {
        double pos1 = point1.Position;
        double pos2 = point2.Position;
        double smallPos = (pos1 < pos2) ? pos1 : pos2;
        double largePos = (pos1 < pos2) ? pos2 : pos1;
        double diff = largePos - smallPos;
        double firstPosShortestArc = (diff < HALF_CIRCLE) ? smallPos : largePos;
        double lastPosShortestArc = (diff < HALF_CIRCLE) ? largePos : smallPos;
        diff = lastPosShortestArc - firstPosShortestArc;
        if (diff < 0.0) diff += FULL_CIRCLE;
        double mPos = (diff / 2) + firstPosShortestArc;
        if (mPos >= FULL_CIRCLE) mPos -= FULL_CIRCLE;
        return new BaseMidpoint(point1, point2, mPos);
    }


}