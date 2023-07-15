// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Configuration;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;
using Enigma.Domain.Research;


namespace Enigma.Core.Handlers.Research.Helpers;

/// <inheritdoc/>
public sealed class OccupiedMidpointsCounting : IOccupiedMidpointsCounting
{
    private readonly IMidpointsHandler _midpointsHandler;
    private readonly IPointsMapping _pointsMapping;
    private readonly IResearchMethodUtils _researchMethodUtils;

    public OccupiedMidpointsCounting(IMidpointsHandler midpointsHandler, IPointsMapping pointsMapping, IResearchMethodUtils researchMethodUtils)
    {
        _midpointsHandler = midpointsHandler;
        _pointsMapping = pointsMapping;
        _researchMethodUtils = researchMethodUtils;
    }


    /// <inheritdoc/>
    public CountOfOccupiedMidpointsResponse CountMidpoints(List<CalculatedResearchChart> charts, CountOccupiedMidpointsRequest request)
    {
        return PerformCount(charts, request);
    }


    private CountOfOccupiedMidpointsResponse PerformCount(List<CalculatedResearchChart> charts, CountOccupiedMidpointsRequest request)
    {
        AstroConfig config = request.Config;
        List<ChartPoints> selectedPoints = request.PointsSelection.SelectedPoints;
        Dictionary<OccupiedMidpointStructure, int> allCounts = InitializeAllCounts(selectedPoints);

        double dialSize = 360.0 / request.DivisionForDial;
        double orb = 1.6;           // todo 0.2 use orb from config

        foreach (CalculatedResearchChart calcResearchChart in charts)
        {
            Dictionary<ChartPoints, FullPointPos> commonPositions = (
                from posPoint in calcResearchChart.Positions
                where (posPoint.Key.GetDetails().PointCat == PointCats.Common || posPoint.Key.GetDetails().PointCat == PointCats.Angle)
                select posPoint).ToDictionary(x => x.Key, x => x.Value);
            Dictionary<ChartPoints, FullPointPos> relevantChartPointPositions = _researchMethodUtils.DefineSelectedPointPositions(calcResearchChart, request.PointsSelection);
            List<PositionedPoint> posPoints = _pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions, CoordinateSystems.Ecliptical, true);
            List<OccupiedMidpoint> occupiedMidpoints = _midpointsHandler.RetrieveOccupiedMidpoints(posPoints, dialSize, orb);

            foreach (OccupiedMidpoint occupiedMidpoint in occupiedMidpoints)
            {
                OccupiedMidpointStructure mpStructure = new(occupiedMidpoint.Midpoint.Point1.Point, occupiedMidpoint.Midpoint.Point2.Point, occupiedMidpoint.OccupyingPoint.Point);
                allCounts[mpStructure]++;
            }
        }
        return new CountOfOccupiedMidpointsResponse(request, allCounts);
    }


    private static Dictionary<OccupiedMidpointStructure, int> InitializeAllCounts(List<ChartPoints> selectedPoints)
    {
        int countValue = 0;
        Dictionary<OccupiedMidpointStructure, int> allCounts = new();
        foreach (ChartPoints firstPoint in selectedPoints)
        {
            foreach (ChartPoints secondPoint in selectedPoints)
            {
                foreach (ChartPoints occupyingPoint in selectedPoints)
                {
                    allCounts.Add(new OccupiedMidpointStructure(firstPoint, secondPoint, occupyingPoint), countValue);
                }
            }
        }
        return allCounts;
    }




}