// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Analysis.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Core.Work.Analysis.Midpoints;

/// <inheritdoc/>
public class AnalysisPointsForMidpoints : IAnalysisPointsForMidpoints
{
    private readonly IAnalysisPointsMapping _analysisPointsMapping;

    public AnalysisPointsForMidpoints(IAnalysisPointsMapping analysisPointsMapping)
    {
        _analysisPointsMapping = analysisPointsMapping;
    }

    /// <inheritdoc/>
    public List<AnalysisPoint> CreateAnalysisPoints(CalculatedChart chart, double dialSize)
    {
        // TODO 1.0.0 make pointgroups, coordinatesystems and maincoord for midpoints configurable.
        var pointGroups = new List<PointGroups> { PointGroups.SolarSystemPoints, PointGroups.MundanePoints, PointGroups.ZodiacalPoints };
        CoordinateSystems coordSystem = CoordinateSystems.Ecliptical;
        bool mainCoord = true;
        List<AnalysisPoint> pointsIn360Dial = _analysisPointsMapping.ChartToSingleAnalysisPoints(pointGroups, coordSystem, mainCoord, chart);
        List<AnalysisPoint> pointsInActualDial = new();
        foreach (var point in pointsIn360Dial)
        {
            double pos = point.Position;
            while (pos >= dialSize) pos -= dialSize;
            pointsInActualDial.Add(new AnalysisPoint(point.PointGroup, point.ItemId, pos, point.Glyph));
        }
        return pointsInActualDial;
    }
}