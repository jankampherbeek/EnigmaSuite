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
    /// <summary>Map a single instance of FullChartPointPos to an instance of PositionedPoint.</summary>
    /// <param name="fullCelPointPos">The instance of FullChartPointPos.</param>
    /// <param name="coordinateSystem">Instance of the enum CoordinateSystems.</param>
    /// <param name="useMainCoordinate">True for the main coordinate (either longitude, right ascension or azimuth), otherwise false (either latitude, declination of altitude).</param>
    /// <returns>Instance of PositionedPoint.</returns>
    public PositionedPoint MapFullPointPos2PositionedPoint(FullChartPointPos fullCelPointPos, CoordinateSystems coordinateSystem, bool useMainCoordinate);

    /// <summary>Maps multiple instances of FullChartPointPos to instances of PositionedPoint.</summary>
    /// <param name="fullCelPointPositions">Multiple instances of FullChartPointPos.</param>
    /// <param name="coordinateSystem">Instance of the enum CoordinateSystems.</param>
    /// <param name="useMainCoordinate">True for the main coordinate (either longitude, right ascension or azimuth), otherwise false (either latitude, declination of altitude).</param>
    /// <returns>Multiple instances of PositionedPoint, in the same sequence as the original instances of FullChartPointPos.</returns>
    public List<PositionedPoint> MapFullPointPos2PositionedPoint(List<FullChartPointPos> fullCelPointPositions, CoordinateSystems coordinateSystem, bool useMainCoordinate);

}