// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Helpers.Interfaces;
using Enigma.Domain.Analysis;

namespace Enigma.Core.Helpers.Analysis.Midpoints;

/// <inheritdoc/>
public class MidpointsHelper : IMidpointsHelper
{
    private readonly double _halfCircle = 180.0;
    private readonly double _fullCircle = 360.0;

    /// <inheritdoc/>
    public EffectiveMidpoint ConstructEffectiveMidpointInDial(AnalysisPoint point1, AnalysisPoint point2, double divisionForDial)
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
        double dialSize = divisionForDial * 360.0;
        double posInDial = mPos;
        while(posInDial >= dialSize) posInDial-= dialSize;
        return new EffectiveMidpoint(point1, point2, mPos, posInDial);
    }

    /// <inheritdoc/>
    public double MeasureMidpointDeviation(double division, double midpointPos, double posCelPoint)
    {
        double dialSize = division * 360.0;
        double correctedMidpointPos = midpointPos;
        double candidatePos = posCelPoint;
        
  //      while(correctedMidpointPos > (dialSize)) correctedMidpointPos -= dialSize;
  //      while(candidatePos > (dialSize)) candidatePos -= dialSize;
        double smallPos = (candidatePos < correctedMidpointPos) ? candidatePos : correctedMidpointPos;
        double largePos = (candidatePos < correctedMidpointPos) ? correctedMidpointPos: candidatePos;
        double deviation = largePos - smallPos;   
        if (deviation > (dialSize / 2.0)) deviation = dialSize - deviation;
        return deviation;
    }

    /// <inheritdoc/>
    public List<EffectiveMidpoint> CreateMidpoints4Dial(double division, List<EffectiveMidpoint> midPoints360Degrees)
    {
        double dialSize = division * 360.0;
        List<EffectiveMidpoint> dialMidpoints = new();
        foreach (var midpoint in midPoints360Degrees)
        {
            double longInDial = midpoint.Position;
            while (longInDial > dialSize)
            {
                longInDial -= dialSize;
            }
            dialMidpoints.Add(new EffectiveMidpoint(midpoint.Point1, midpoint.Point2, longInDial, division));
        }
        return dialMidpoints;
    }

}
