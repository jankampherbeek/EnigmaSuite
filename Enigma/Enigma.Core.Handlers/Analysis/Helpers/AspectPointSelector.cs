// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;

namespace Enigma.Core.Handlers.Analysis.Helpers;

/// <inheritdoc/>
public class AspectPointSelector : IAspectPointSelector
{

    /// <inheritdoc/>
    public Dictionary<ChartPoints, FullPointPos> SelectPoints(Dictionary<ChartPoints, FullPointPos> chartPointPositions, Dictionary<ChartPoints, FullPointPos> anglePositions, List<ChartPointConfigSpecs> chartPointConfigSpecs)
    {
        Dictionary<ChartPoints, FullPointPos> relevantChartPointPositions = new();
        
        // two foreach loops to enforce that the sequence between commong points (first) and angles (second) is maintained.
        foreach (ChartPointConfigSpecs spec in chartPointConfigSpecs)
        {
            if (spec.IsUsed && spec.Point.GetDetails().PointCat == PointCats.Common)
            {
                relevantChartPointPositions.Add(spec.Point, chartPointPositions[spec.Point]);
            }

        }
        foreach (ChartPointConfigSpecs spec in chartPointConfigSpecs)
        {
            if (spec.IsUsed && spec.Point.GetDetails().PointCat == PointCats.Angle)
            {
                relevantChartPointPositions.Add(spec.Point, anglePositions[spec.Point]);
            }
        }
        return relevantChartPointPositions;
    }


}