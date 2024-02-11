// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Domain.Dtos;

/// <summary>Definitions for an orb for a ChartPoint.</summary>
public interface IOrbDefinitions
{
    /// <summary>Define the orb for a chart point.</summary>
    /// <param name="chartPoint">The ChartPoint.</param>
    /// <param name="chartPointConfigSpecs">Orbs per chartpoint.</param>
    /// <returns>The defined orb.</returns>
    public KeyValuePair<ChartPoints, double> DefineChartPointOrb(ChartPoints chartPoint, Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointConfigSpecs);

}

public class OrbDefinitions : IOrbDefinitions
{
    public KeyValuePair<ChartPoints, double> DefineChartPointOrb(ChartPoints chartPoint, Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointConfigSpecs)
    {
        double orbForChartPoint = 0.0;
        foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs> spec 
                 in chartPointConfigSpecs.Where(spec => spec.Key == chartPoint))
        {
            orbForChartPoint = spec.Value.PercentageOrb / 100.0;
        }
        return new KeyValuePair<ChartPoints, double>(chartPoint, orbForChartPoint);
    }
}
