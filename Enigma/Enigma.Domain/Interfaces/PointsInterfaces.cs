// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Points;

namespace Enigma.Core.Domain.Interfaces;

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