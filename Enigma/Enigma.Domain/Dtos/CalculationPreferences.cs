// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024, 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.References;

namespace Enigma.Domain.Dtos;

/// <summary>User preferences for the calculation.</summary>
/// <param name="ActualChartPoints">The chartpoints to use.</param>
/// <param name="ActualZodiacType">Type of zodiac.</param>
/// <param name="ActualAyanamsha">Preferred Ayanamsha.</param>
/// <param name="CoordinateSystem">Coordinate system.</param>
/// <param name="ActualObserverPosition">Position of observer.</param>
/// <param name="ActualProjectionType">Type of projection.</param>
/// <param name="ActualHouseSystem">House system.</param>
/// <param name="ApogeeType">Type of calculation for the apogee.</param>
/// <param name="Oscillate">Use oscillated version for lunar nodes.</param>
public record CalculationPreferences(List<ChartPoints> ActualChartPoints,
    ZodiacTypes ActualZodiacType,
    Ayanamshas ActualAyanamsha,
    CoordinateSystems CoordinateSystem,
    ObserverPositions ActualObserverPosition,
    ProjectionTypes ActualProjectionType,
    HouseSystems ActualHouseSystem,
    ApogeeTypes ApogeeType,
    bool Oscillate);


/// <summary>Creates calculation preferences from a configuration.</summary>
public interface ICalculationPreferencesCreator
{
    /// <summary>Create calculation preferences.</summary>
    /// <param name="config">Configuration.</param>
    /// <param name="coordSys">Coordinate system.</param>
    /// <returns>The created calculation preferences.</returns>
    public CalculationPreferences CreatePrefs(AstroConfig? config, CoordinateSystems coordSys);

    /// <summary>Create calculation preferences for a single chart point.</summary>
    /// <param name="point">The chart point.</param>
    /// <param name="config">Configuration.</param>
    /// <param name="coordSys">Coordinate system.</param>
    /// <returns>The created calculation preferences, including only one chart point.</returns>
    public CalculationPreferences CreatePrefsForSinglePoint(ChartPoints point, AstroConfig? config, CoordinateSystems coordSys);
    
}


// =====================  Implementation ==================================================================

/// <inheritdoc/>
public class CalculationPreferencesCreator : ICalculationPreferencesCreator
{
    
    /// <inheritdoc/>
    public CalculationPreferences CreatePrefs(AstroConfig? config, CoordinateSystems coordSys)
    {
        List<ChartPoints> actualChartPoints = (
            from point in config.ChartPoints 
            where point.Value.IsUsed 
            select point.Key).ToList();
        return new CalculationPreferences(actualChartPoints, config.ZodiacType, config.Ayanamsha,
            coordSys, config.ObserverPosition, config.ProjectionType, config.HouseSystem, config.ApogeeType, config.OscillateNodes);
    }

    /// <inheritdoc/>
    public CalculationPreferences CreatePrefsForSinglePoint(ChartPoints point, AstroConfig? config, CoordinateSystems coordSys)
    {
        List<ChartPoints> actualChartPoints = new() { point };
        return new CalculationPreferences(actualChartPoints, config.ZodiacType, config.Ayanamsha,
            coordSys, config.ObserverPosition, config.ProjectionType, config.HouseSystem, config.ApogeeType, config.OscillateNodes);
    }
}



