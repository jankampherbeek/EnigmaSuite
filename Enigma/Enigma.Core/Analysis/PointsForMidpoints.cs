// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.References;

namespace Enigma.Core.Analysis;

/// <summary>Handle points to be used for the calculation of midpoints.</summary>
public interface IPointsForMidpoints
{
    /// <summary>Create analysispoints for a specific dial.</summary>
    /// <param name="chart">Calculated chart.</param>
    /// <param name="dialSize">Dial size in degrees.</param>
    /// <returns>Constructed AnalysisPoints.</returns>
    public List<PositionedPoint> CreateAnalysisPoints(CalculatedChart chart, double dialSize);
}



/// <inheritdoc/>
public sealed class PointsForMidpoints : IPointsForMidpoints
{
    private readonly IPointsMapping _pointsMapping;

    public PointsForMidpoints(IPointsMapping pointsMapping)
    {
        _pointsMapping = pointsMapping;
    }


    /// <inheritdoc/>
    public List<PositionedPoint> CreateAnalysisPoints(CalculatedChart chart, double dialSize)
    {
        Dictionary<ChartPoints, FullPointPos> positions = (
            from posPoint in chart.Positions    // TODO 0.6 remove restrictions for EstPoint and Vertex as glyphs for these points have been implemented.
            where posPoint.Key.GetDetails().PointCat == PointCats.Common || (posPoint.Key.GetDetails().PointCat == PointCats.Angle && posPoint.Key != ChartPoints.Vertex && posPoint.Key != ChartPoints.EastPoint) ||
        (posPoint.Key.GetDetails().PointCat == PointCats.Zodiac && posPoint.Key == ChartPoints.ZeroAries)
            select posPoint).ToDictionary(x => x.Key, x => x.Value);

        const CoordinateSystems coordSystem = CoordinateSystems.Ecliptical;
        const bool mainCoord = true;
        List<PositionedPoint> pointsIn360Dial = _pointsMapping.MapFullPointPos2PositionedPoint(positions, coordSystem, mainCoord);
        List<PositionedPoint> pointsInActualDial = new();
        foreach (var point in pointsIn360Dial)
        {
            double pos = point.Position;
            while (pos >= dialSize) pos -= dialSize;
            pointsInActualDial.Add(point with { Position = pos });
        }
        return pointsInActualDial;
    }

}