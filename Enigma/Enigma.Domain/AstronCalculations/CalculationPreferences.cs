// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Enums;

namespace Enigma.Domain.AstronCalculations;

/// <summary>
/// User preferences for the calculation.
/// </summary>
public record CalculationPreferences(List<CelPoints> ActualCelPoints,
    ZodiacTypes ActualZodiacType,
    Ayanamshas ActualAyanamsha,
    CoordinateSystems CoordinateSystem,
    ObserverPositions ActualObserverPosition,
    ProjectionTypes ActualProjectionType,
    HouseSystems ActualHouseSystem);
