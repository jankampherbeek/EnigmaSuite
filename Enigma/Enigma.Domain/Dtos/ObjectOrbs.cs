// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Interfaces;
using Enigma.Domain.References;

namespace Enigma.Domain.Dtos;

public class OrbDefinitions : IOrbDefinitions
{
    public KeyValuePair<ChartPoints, double> DefineChartPointOrb(ChartPoints chartPoint, Dictionary<ChartPoints, ChartPointConfigSpecs?> chartPointConfigSpecs)
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
