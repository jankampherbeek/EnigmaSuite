// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;

namespace Enigma.Core.Handlers.Analysis.Helpers;

/// <inheritdoc/>
public sealed class PointsForMidpoints : IPointsForMidpoints            // TODO define new solution for PointsForMidpoints
{

    /// <inheritdoc/>
    public List<PositionedPoint> CreateAnalysisPoints(CalculatedChart chart, double dialSize)
    {
        // TODO 1.0.0 make pointgroups, coordinatesystems and maincoord for midpoints configurable.
        //var pointGroups = new List<PointCats> { PointCats.Classic, PointCats.Modern, PointCats.Hypothetical, PointCats.MathPoint, PointCats.Minor, PointCats.Mundane, PointCats.Zodiac };
       // CoordinateSystems coordSystem = CoordinateSystems.Ecliptical;
        //      bool mainCoord = true;
        //      List<PositionedPoint> pointsIn360Dial = _analysisPointsMapping.ChartToSingleAnalysisPoints(pointGroups, coordSystem, mainCoord, chart);
        List<PositionedPoint> pointsInActualDial = new();
        //      foreach (var point in pointsIn360Dial)
        //      {
        //          double pos = point.Position;
        //          while (pos >= dialSize) pos -= dialSize;
        //          pointsInActualDial.Add(new PositionedPoint(point.Point, pos));
        //      }
        return pointsInActualDial;
    }

}