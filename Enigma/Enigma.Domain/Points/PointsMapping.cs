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
    public PositionedPoint MapFullPointPos2PositionedPoint(FullChartPointPos fullCelPointPos, CoordinateSystems coordinateSystem, bool useMainCoordinate)
    {
        ChartPoints chartPoint = fullCelPointPos.ChartPoint;
        FullPointPos fullPointPos = fullCelPointPos.PointPos;
        double position = FindPositionForCoordinate(fullPointPos, coordinateSystem, useMainCoordinate);
        return new PositionedPoint(chartPoint, position);
    }

    /// <inheritdoc/>
    public List<PositionedPoint> MapFullPointPos2PositionedPoint(List<FullChartPointPos> fullCelPointPositions, CoordinateSystems coordinateSystem, bool useMainCoordinate)
    {
        List<PositionedPoint> positionedPoints = new();
        foreach (FullChartPointPos fullCelPointPos in fullCelPointPositions)
        {
            positionedPoints.Add(MapFullPointPos2PositionedPoint(fullCelPointPos, coordinateSystem, useMainCoordinate));
        }
        return positionedPoints;
    }

    private static double FindPositionForCoordinate(FullPointPos generalPointPos, CoordinateSystems coordinateSystem, bool useMainCoordinate)
    {
        switch (coordinateSystem)
        {
            case CoordinateSystems.Ecliptical:
                if (useMainCoordinate) { return generalPointPos.Longitude.Position; } else { return generalPointPos.Latitude.Position; }
            case CoordinateSystems.Equatorial:
                if (useMainCoordinate) { return generalPointPos.RightAscension.Position; } else { return generalPointPos.Declination.Position; }
            case CoordinateSystems.Horizontal:
                if (useMainCoordinate) { return generalPointPos.AzimuthAltitude.Azimuth; } else { return generalPointPos.AzimuthAltitude.Altitude; }
            default:
                string errorText = "PointsMapping.FindPositionForCoordinate(): unknow coordinate system : " + coordinateSystem.ToString();
                Log.Error(errorText);
                throw new ArgumentException(errorText);
        }
    }

}