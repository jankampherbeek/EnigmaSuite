// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Domain.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Points;
using Serilog;

namespace Enigma.Core.Domain.Points;

/// <inheritdoc/>
public sealed class PointsMapping: IPointsMapping
{
    /// <inheritdoc/>
    public PositionedPoint MapFullPointPos2PositionedPoint(KeyValuePair<ChartPoints, FullPointPos> position, CoordinateSystems coordinateSystem, bool useMainCoordinate)
    {
        ChartPoints chartPoint = position.Key;
        double positionValue = FindPositionForCoordinate(position, coordinateSystem, useMainCoordinate);
        return new PositionedPoint(chartPoint, positionValue);
    }

    /// <inheritdoc/>
    public List<PositionedPoint> MapFullPointPos2PositionedPoint(Dictionary<ChartPoints, FullPointPos> positions, CoordinateSystems coordinateSystem, bool useMainCoordinate)
    {
        List<PositionedPoint> positionedPoints = new();
        foreach (KeyValuePair<ChartPoints, FullPointPos> position in positions)
        {
            positionedPoints.Add(MapFullPointPos2PositionedPoint(position, coordinateSystem, useMainCoordinate));
        }
        return positionedPoints;
    }

    private static double FindPositionForCoordinate(KeyValuePair<ChartPoints, FullPointPos> position, CoordinateSystems coordinateSystem, bool useMainCoordinate)
    {
        switch (coordinateSystem)
        {
            case CoordinateSystems.Ecliptical:
                if (useMainCoordinate) { return position.Value.Ecliptical.MainPosSpeed.Position; } else { return position.Value.Ecliptical.DeviationPosSpeed.Position; }
            case CoordinateSystems.Equatorial:
                if (useMainCoordinate) { return position.Value.Equatorial.MainPosSpeed.Position; } else { return position.Value.Equatorial.DeviationPosSpeed.Position; }
            case CoordinateSystems.Horizontal:
                if (useMainCoordinate) { return position.Value.Horizontal.MainPosSpeed.Position; } else { return position.Value.Horizontal.DeviationPosSpeed.Position; }
            default:
                string errorText = "PointsMapping.FindPositionForCoordinate(): unknow coordinate system : " + coordinateSystem.ToString();
                Log.Error(errorText);
                throw new ArgumentException(errorText);
        }
    }

}