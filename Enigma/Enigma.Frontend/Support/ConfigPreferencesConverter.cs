// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024, 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Support;

/// <summary>Conversions between Configuration and CalculationPreferences.</summary>
public interface IConfigPreferencesConverter
{
    /// <summary>Create CalculationPreferences from current configuration.</summary>
    /// <returns>The constructed calculation preferences.</returns>
    public CalculationPreferences RetrieveCalculationPreferences();
}

public class ConfigPreferencesConverter: IConfigPreferencesConverter
{
    public CalculationPreferences RetrieveCalculationPreferences()
    {
        AstroConfig config = CurrentConfig.Instance.GetConfig();
        List<ChartPoints> celPoints = (
            from spec in config.ChartPoints 
            where spec.Value.IsUsed 
            select spec.Key).ToList();
        return new CalculationPreferences(celPoints, config.ZodiacType, config.Ayanamsha, CoordinateSystems.Ecliptical, 
            config.ObserverPosition, config.ProjectionType, config.HouseSystem, config.ApogeeType, config.OscillateNodes);
    }
}