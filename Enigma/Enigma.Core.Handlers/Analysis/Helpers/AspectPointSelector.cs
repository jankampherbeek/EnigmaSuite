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
    public List<FullChartPointPos> SelectPoints(List<FullChartPointPos> chartPointPositions, FullHousesPositions fullHousesPositions, List<ChartPointConfigSpecs> chartPointConfigSpecs)
    {
        List<FullChartPointPos> relevantChartPointPositions = new();
        foreach (FullChartPointPos fcpPos in chartPointPositions)
        {
            PointCats actualPointCat = fcpPos.ChartPoint.GetDetails().PointCat;
            if (actualPointCat == PointCats.Classic || actualPointCat == PointCats.Modern || actualPointCat == PointCats.MathPoint || actualPointCat == PointCats.Minor || actualPointCat == PointCats.Hypothetical)
            {
                relevantChartPointPositions.Add(fcpPos);
            }
        }
        relevantChartPointPositions.Add(fullHousesPositions.Mc);
        relevantChartPointPositions.Add(fullHousesPositions.Ascendant);
        foreach (ChartPointConfigSpecs spec in chartPointConfigSpecs)
        {
            if (spec.IsUsed && spec.Point == ChartPoints.Vertex)
            {
                relevantChartPointPositions.Add(fullHousesPositions.Vertex);
            }
            if (spec.IsUsed && spec.Point == ChartPoints.EastPoint)
            {
                relevantChartPointPositions.Add(fullHousesPositions.EastPoint);
            }
        }        
        return relevantChartPointPositions;
    }


}