// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Enums;
using System.Collections.Immutable;

namespace Enigma.Domain.AstronCalculations;

/// <summary>
/// User preferences for the calculation.
/// </summary>
public record CalculationPreferences
{
    readonly public ImmutableArray<CelPoints> ActualCelPoints;
    readonly public ZodiacTypes ActualZodiacType;
    readonly public Ayanamshas ActualAyanamsha;
    readonly public ObserverPositions ActualObserverPosition;
    readonly public ProjectionTypes ActualProjectionType;
    readonly public HouseSystems ActualHouseSystem;


    public CalculationPreferences(ImmutableArray<CelPoints> celPoints,
                                  ZodiacTypes zodiacType,
                                  Ayanamshas ayanamsha,
                                  ObserverPositions observerPosition,
                                  ProjectionTypes projectionType,
                                  HouseSystems houseSystem)
    {
        ActualCelPoints = celPoints;
        ActualZodiacType = zodiacType;
        ActualAyanamsha = ayanamsha;
        ActualObserverPosition = observerPosition;
        ActualProjectionType = projectionType;
        ActualHouseSystem = houseSystem;
    }
}