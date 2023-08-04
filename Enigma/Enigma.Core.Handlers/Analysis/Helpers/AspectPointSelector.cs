// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;

namespace Enigma.Core.Handlers.Analysis.Helpers;

/// <inheritdoc/>
public class AspectPointSelector : IAspectPointSelector
{

    /// <inheritdoc/>
    public Dictionary<ChartPoints, FullPointPos> SelectPoints(Dictionary<ChartPoints, FullPointPos> positions, Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointConfigSpecs)
    {
        Dictionary<ChartPoints, FullPointPos> relevantChartPointPositions = new();

        // two foreach loops to enforce that the sequence between common points (first) and angles (second) is maintained.
        foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs> spec in chartPointConfigSpecs)
        {
            if (spec.Key.GetDetails().PointCat == PointCats.Common && spec.Value.IsUsed && positions.TryGetValue(spec.Key, out FullPointPos? position))
            {
                relevantChartPointPositions.Add(spec.Key, position);
            }

        }
        foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs> spec in chartPointConfigSpecs)
        {
            if (spec.Key.GetDetails().PointCat == PointCats.Angle && spec.Value.IsUsed)
            {
                relevantChartPointPositions.Add(spec.Key, positions[spec.Key]);
            }
        }
        return relevantChartPointPositions;
    }


}