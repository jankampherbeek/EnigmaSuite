// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Work.Analysis.Midpoints.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Core.Work.Analysis.Midpoints;


public class MidpointsHandler: IMidpointsHandler
{
    private readonly IAnalysisPointsMapping _analysisPointsMapping;
    private readonly IBaseMidpointsCreator _baseMidpointsCreator;
    private readonly double maxOrb = 1.6;         // TODO 0.2.0 retrieve orb from configuration.

    public MidpointsHandler(IAnalysisPointsMapping analysisPointsMapping, IBaseMidpointsCreator baseMidpointsCreator)
    {
        _analysisPointsMapping = analysisPointsMapping;
        _baseMidpointsCreator = baseMidpointsCreator;
    }

    public List<BaseMidpoint> RetrieveBaseMidpoints(CalculatedChart chart)
    {
        double dialSize = 360.0;
        List<AnalysisPoint> analysisPoints = CreateAnalysisPoints(chart, dialSize);
        return _baseMidpointsCreator.CreateBaseMidpoints(analysisPoints);
    }

    public List<OccupiedMidpoint> RetrieveOccupiedMidpoints(CalculatedChart chart, double dialSize) 
    {

        List<AnalysisPoint> analysisPointsInActualDial = CreateAnalysisPoints(chart, dialSize);
        List<BaseMidpoint> baseMidpointsIn360Dial = RetrieveBaseMidpoints(chart);
        List<BaseMidpoint> baseMidpoints = _baseMidpointsCreator.ConvertBaseMidpointsToDial(baseMidpointsIn360Dial, dialSize);
        List<OccupiedMidpoint> occupiedMidpoints = new();

        foreach (var baseMidpoint in baseMidpoints)
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


    private double MeasureMidpointDeviation(double midpointPos, double posCelPoint, double dialSize)
    {

       double candidatePos = posCelPoint;
        double smallPos = (candidatePos < midpointPos) ? candidatePos : midpointPos;
        double largePos = (candidatePos < midpointPos) ? midpointPos : candidatePos;
        double deviation = largePos - smallPos;
        if (deviation >= (dialSize / 2.0)) deviation = dialSize - deviation;
        return deviation;
    }


    private List<AnalysisPoint> CreateAnalysisPoints(CalculatedChart chart, double dialSize) {
        // TODO 1.0.0 make pointgroups, coordinatesystems and maincoord for midpoints configurable.
        List<PointGroups> pointGroups = new List<PointGroups> { PointGroups.SolarSystemPoints, PointGroups.MundanePoints, PointGroups.ZodiacalPoints };
        CoordinateSystems coordSystem = CoordinateSystems.Ecliptical;
        bool mainCoord = true;
        List<AnalysisPoint> pointsIn360Dial = _analysisPointsMapping.ChartToSingLeAnalysisPoints(pointGroups, coordSystem, mainCoord, chart);
        List<AnalysisPoint> pointsInActualDial = new();
        foreach (var point in pointsIn360Dial) 
        {
            double pos = point.Position;
            while(pos >= dialSize) pos-= dialSize;
            pointsInActualDial.Add(new AnalysisPoint(point.PointGroup, point.ItemId, pos, point.Glyph));
        }
        return pointsInActualDial;
    }


}