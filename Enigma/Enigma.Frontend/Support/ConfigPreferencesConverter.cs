using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Support;

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
            config.ObserverPosition, config.ProjectionType, config.HouseSystem);
    }
}