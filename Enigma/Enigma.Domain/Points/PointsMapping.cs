// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Serilog;

namespace Enigma.Domain.Points;

/// <summary>Mapping between different types of points.</summary>
public interface IPointsMapping
{
    /// <summary>Map a KeyValuePair with a ChartPoint and a FullPointPos to an instance of PositionedPoint.</summary>
    /// <param name="position">KeyValuePair with point and positions.</param>
    /// <param name="coordinateSystem">Instance of the enum CoordinateSystems.</param>
    /// <param name="useMainCoordinate">True for the main coordinate (either longitude, right ascension or azimuth), otherwise false (either latitude, declination of altitude).</param>
    /// <returns>Instance of PositionedPoint.</returns>
    public PositionedPoint MapFullPointPos2PositionedPoint(KeyValuePair<ChartPoints, FullPointPos> position, CoordinateSystems coordinateSystem, bool useMainCoordinate);

    /// <summary>Maps multiple KeyValuePairs with a ChartPoint and a FullPointPos to instances of PositionedPoint.</summary>
    /// <param name="positions">Multiple instances of FullChartPointPos.</param>
    /// <param name="coordinateSystem">Instance of the enum CoordinateSystems.</param>
    /// <param name="useMainCoordinate">True for the main coordinate (either longitude, right ascension or azimuth), otherwise false (either latitude, declination of altitude).</param>
    /// <returns>Multiple instances of PositionedPoint, in the same sequence as the original instances of FullChartPointPos.</returns>
    public List<PositionedPoint> MapFullPointPos2PositionedPoint(Dictionary<ChartPoints, FullPointPos> positions, CoordinateSystems coordinateSystem, bool useMainCoordinate);

}


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