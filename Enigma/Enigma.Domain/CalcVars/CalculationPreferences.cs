// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using System.Collections.Immutable;

namespace Enigma.Domain.CalcVars;

/// <summary>
/// User preferences for the calculation.
/// </summary>
public record CalculationPreferences
{
    readonly public ImmutableArray<SolarSystemPoints> ActualSolarSystemPoints;
    readonly public ZodiacTypes ActualZodiacType;
    readonly public Ayanamshas ActualAyanamsha;
    readonly public ObserverPositions ActualObserverPosition;
    readonly public ProjectionTypes ActualProjectionType;
    readonly public HouseSystems ActualHouseSystem;


    public CalculationPreferences(ImmutableArray<SolarSystemPoints> solarSystemPoints, 
                                  ZodiacTypes zodiacType, 
                                  Ayanamshas ayanamsha, 
                                  ObserverPositions observerPosition, 
                                  ProjectionTypes projectionType, 
                                  HouseSystems houseSystem)
    {
        ActualSolarSystemPoints = solarSystemPoints;
        ActualZodiacType = zodiacType;
        ActualAyanamsha = ayanamsha;
        ActualObserverPosition = observerPosition;
        ActualProjectionType = projectionType;
        ActualHouseSystem = houseSystem;
    }
}