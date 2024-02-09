// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.Interfaces;
using Enigma.Domain.References;
using Serilog;

namespace Enigma.Domain.Points;

/// <inheritdoc/>
public sealed class PointsMapping : IPointsMapping
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
        return positions.Select(position => MapFullPointPos2PositionedPoint(position, coordinateSystem, useMainCoordinate)).ToList();
    }

    private static double FindPositionForCoordinate(KeyValuePair<ChartPoints, FullPointPos> position, CoordinateSystems coordinateSystem, bool useMainCoordinate)
    {
        switch (coordinateSystem)
        {
            case CoordinateSystems.Ecliptical:
                return useMainCoordinate ? position.Value.Ecliptical.MainPosSpeed.Position : position.Value.Ecliptical.DeviationPosSpeed.Position;
            case CoordinateSystems.Equatorial:
                return useMainCoordinate ? position.Value.Equatorial.MainPosSpeed.Position : position.Value.Equatorial.DeviationPosSpeed.Position;
            case CoordinateSystems.Horizontal:
                return useMainCoordinate ? position.Value.Horizontal.MainPosSpeed.Position : position.Value.Horizontal.DeviationPosSpeed.Position;
            default:
                Log.Error("PointsMapping.FindPositionForCoordinate(): unknow coordinate system : {Cs}", coordinateSystem);
                throw new ArgumentException("Unknown coordinate system");
        }
    }

}