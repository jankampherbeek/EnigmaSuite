// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Domain.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;

namespace Enigma.Core.Handlers.Analysis.Helpers;

/// <inheritdoc/>
public sealed class PointsForMidpoints : IPointsForMidpoints            // TODO define new solution for PointsForMidpoints
{

    private readonly IPointsMapping _pointsMapping; 

    public PointsForMidpoints(IPointsMapping pointsMapping)
    {
        _pointsMapping = pointsMapping;
    }





    //Dictionary<ChartPoints, FullPointPos> chartPointPositions, Dictionary<ChartPoints, FullPointPos> anglePositions, List<ChartPointConfigSpecs> chartPointConfigSpecs

    /// <inheritdoc/>
    public List<PositionedPoint> CreateAnalysisPoints(CalculatedChart chart, double dialSize)
    {
        // TODO 1.0.0 make pointgroups, coordinatesystems and maincoord for midpoints configurable.

        Dictionary<ChartPoints, FullPointPos> positions = new();
        foreach (KeyValuePair<ChartPoints, FullPointPos> pos in chart.Positions.CommonPoints)
        {
            positions.Add(pos.Key, pos.Value);
        }
        foreach (KeyValuePair<ChartPoints, FullPointPos> pos in chart.Positions.Angles)
        {
            if (pos.Key != ChartPoints.Vertex && pos.Key != ChartPoints.EastPoint)    // TODO 0.6  remove condition if glyphs for Vertex and Eastpoint are available.
            {
                positions.Add(pos.Key, pos.Value);
            }

        }
        foreach (KeyValuePair<ChartPoints, FullPointPos> pos in chart.Positions.ZodiacPoints)
        {
            positions.Add(pos.Key, pos.Value);
        }


        CoordinateSystems coordSystem = CoordinateSystems.Ecliptical;
        bool mainCoord = true;
        List<PositionedPoint> pointsIn360Dial = _pointsMapping.MapFullPointPos2PositionedPoint(positions, coordSystem, mainCoord);
        List<PositionedPoint> pointsInActualDial = new();
        foreach (var point in pointsIn360Dial)
        {
            double pos = point.Position;
            while (pos >= dialSize) pos -= dialSize;
                pointsInActualDial.Add(new PositionedPoint(point.Point, pos));
            }
        return pointsInActualDial;
    }

}