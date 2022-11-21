// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Work.Analysis.Midpoints.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;

namespace Enigma.Core.Work.Analysis.Midpoints;

public class MidpointsHandler: IMidpointsHandler
{
    private IAnalysisPointsForMidpoints _analysisPointsForMidpoints;
    private readonly IBaseMidpointsCreator _baseMidpointsCreator;
    private readonly IOccupiedMidpoints _occupiedMidpoints;


    public MidpointsHandler(IAnalysisPointsForMidpoints analysisPointsForMidpoints, 
        IBaseMidpointsCreator baseMidpointsCreator,
        IOccupiedMidpoints occupiedMidpoints)
    {
        _analysisPointsForMidpoints = analysisPointsForMidpoints;
        _baseMidpointsCreator = baseMidpointsCreator;
        _occupiedMidpoints = occupiedMidpoints;
    }

    public List<BaseMidpoint> RetrieveBaseMidpoints(CalculatedChart chart)
    {
        double dialSize = 360.0;
        List<AnalysisPoint> analysisPoints = _analysisPointsForMidpoints.CreateAnalysisPoints(chart, dialSize);
        return _baseMidpointsCreator.CreateBaseMidpoints(analysisPoints);
    }

    public List<OccupiedMidpoint> RetrieveOccupiedMidpoints(CalculatedChart chart, double dialSize)
    {
        return _occupiedMidpoints.CalculateOccupiedMidpoints(chart, dialSize);
    }
}