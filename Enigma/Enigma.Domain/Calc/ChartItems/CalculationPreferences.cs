// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Points;
using Enigma.Domain.References;

namespace Enigma.Domain.Calc.ChartItems;

/// <summary>User preferences for the calculation.</summary>
/// <param name="ActualChartPoints">The chartpoints to use.</param>
/// <param name="ActualZodiacType">Type of zodiac.</param>
/// <param name="ActualAyanamsha">Preferred Ayanamsha.</param>
/// <param name="CoordinateSystem">Coordinate system.</param>
/// <param name="ActualObserverPosition">Position of observer.</param>
/// <param name="ActualProjectionType">Type of projection.</param>
/// <param name="ActualHouseSystem">House system.</param>
public record CalculationPreferences(List<ChartPoints> ActualChartPoints,
    ZodiacTypes ActualZodiacType,
    Ayanamshas ActualAyanamsha,
    CoordinateSystems CoordinateSystem,
    ObserverPositions ActualObserverPosition,
    ProjectionTypes ActualProjectionType,
    HouseSystems ActualHouseSystem);
