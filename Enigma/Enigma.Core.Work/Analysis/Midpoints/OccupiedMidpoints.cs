// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Analysis.Midpoints.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;

namespace Enigma.Core.Work.Analysis.Midpoints;


public class OccupiedMidpoints: IOccupiedMidpoints
{

    private readonly IAnalysisPointsForMidpoints _analysisPointsForMidpoints;
    private readonly IBaseMidpointsCreator _baseMidpointsCreator;

    private readonly double maxOrb = 1.6;         // TODO 0.2.0 retrieve orb from configuration.

    public OccupiedMidpoints(IAnalysisPointsForMidpoints analysisPointsForMidpoints, IBaseMidpointsCreator baseMidpointsCreator)
    {
        _analysisPointsForMidpoints= analysisPointsForMidpoints;
        _baseMidpointsCreator= baseMidpointsCreator;
    }


    public List<OccupiedMidpoint> CalculateOccupiedMidpoints(CalculatedChart chart, double dialSize)
    {
        List<AnalysisPoint> analysisPointsInActualDial = _analysisPointsForMidpoints.CreateAnalysisPoints(chart, dialSize);
        List<BaseMidpoint> baseMidpointsIn360Dial = _baseMidpointsCreator.CreateBaseMidpoints(analysisPointsInActualDial);
        List<BaseMidpoint> baseMidpointsInActualDial = _baseMidpointsCreator.ConvertBaseMidpointsToDial(baseMidpointsIn360Dial, dialSize);
        List<OccupiedMidpoint> occupiedMidpoints = new();

        foreach (var baseMidpoint in baseMidpointsInActualDial)
        {
            double positionInDial = baseMidpoint.Position;
            foreach (var analysisPoint in analysisPointsInActualDial)
            {
                double orb = MeasureMidpointDeviation(positionInDial, analysisPoint.Position, dialSize);
                if (orb <= maxOrb)
                {
                    double exactness = 100.0 - ((orb / maxOrb) * 100.0);
                    occupiedMidpoints.Add(new OccupiedMidpoint(baseMidpoint, analysisPoint, orb, exactness));
                }
            }
        }
        return occupiedMidpoints;
    }


    private static double MeasureMidpointDeviation(double midpointPos, double posCelPoint, double dialSize)
    {
        double smallPos = (posCelPoint < midpointPos) ? posCelPoint : midpointPos;
        double largePos = (posCelPoint < midpointPos) ? midpointPos : posCelPoint;
        double deviation = largePos - smallPos;
        if (deviation >= (dialSize / 2.0)) deviation = dialSize - deviation;
        return deviation;
    }
}