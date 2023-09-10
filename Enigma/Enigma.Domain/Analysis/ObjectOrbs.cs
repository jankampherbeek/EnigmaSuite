// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;
using Enigma.Domain.References;

namespace Enigma.Domain.Analysis;


/// <summary>Default orb factor for a chart point.</summary>
/// <param name="Point">The chart point.</param>
/// <param name="OrbFactor">Factor for the orb.</param>
public record ChartPointOrb(ChartPoints Point, double OrbFactor);


public class OrbDefinitions : IOrbDefinitions
{

    public ChartPointOrb DefineChartPointOrb(ChartPoints chartPoint, Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointConfigSpecs)
    {
        double orbForChartPoint = 0.0;
        foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs> spec 
                 in chartPointConfigSpecs.Where(spec => spec.Key == chartPoint))
        {
            orbForChartPoint = spec.Value.PercentageOrb / 100.0;
        }
        return new ChartPointOrb(chartPoint, orbForChartPoint);
    }
}


