// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Core.Research.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.Interfaces;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Core.Research;

/// <inheritdoc/>
public sealed class AspectsCounting : IAspectsCounting
{
    private readonly IAspectsHandler _aspectsHandler;
    private readonly IAspectPointSelector _aspectPointSelector;
    private readonly IPointsMapping _pointsMapping;
    private readonly IResearchMethodUtils _researchMethodUtils;

    public AspectsCounting(IAspectsHandler aspectsHandler, IAspectPointSelector aspectPointSelector, IPointsMapping pointsMapping, IResearchMethodUtils researchMethodUtils)
    {
        _aspectsHandler = aspectsHandler;
        _aspectPointSelector = aspectPointSelector;
        _pointsMapping = pointsMapping;
        _researchMethodUtils = researchMethodUtils;
    }


    /// <inheritdoc/>
    public CountOfAspectsResponse CountAspects(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        return PerformCount(charts, request);
    }


    private CountOfAspectsResponse PerformCount(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        AstroConfig config = request.Config;
        Dictionary<AspectTypes, AspectConfigSpecs> configSelectedAspects = _researchMethodUtils.DefineConfigSelectedAspects(config);

        Dictionary<ChartPoints, ChartPointConfigSpecs?> chartPointConfigSpecs = config.ChartPoints;
        int celPointSize = chartPointConfigSpecs.Count;
        int selectedCelPointSize = 0;
        int cuspSize = charts[0].Positions.Count(item => item.Key.GetDetails().PointCat == PointCats.Cusp);
        int aspectSize = configSelectedAspects.Count;
        int[,,] allCounts = new int[celPointSize, celPointSize + cuspSize, aspectSize];
        List<PositionedPoint> allPoints = new();

        foreach (CalculatedResearchChart calcResearchChart in charts)
        {
            Dictionary<ChartPoints, FullPointPos> relevantChartPointPositions = _researchMethodUtils.DefineSelectedPointPositions(calcResearchChart, request.PointSelection);

            List<PositionedPoint> posPoints = _pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions, CoordinateSystems.Ecliptical, true);
            List<PositionedPoint> cuspPoints = new();
            if (request.PointSelection.IncludeCusps)
            {
                var relevantCusps = (from posPoint in calcResearchChart.Positions where (posPoint.Key.GetDetails().PointCat == PointCats.Cusp) select posPoint).ToDictionary(x => x.Key, x => x.Value);
                cuspPoints = _pointsMapping.MapFullPointPos2PositionedPoint(relevantCusps, CoordinateSystems.Ecliptical, true);
            }
            selectedCelPointSize = relevantChartPointPositions.Count;
            allPoints = new List<PositionedPoint>(posPoints.Count + cuspPoints.Count);
            allPoints.AddRange(posPoints);
            allPoints.AddRange(cuspPoints);

            List<DefinedAspect> definedAspects = _aspectsHandler.AspectsForPosPoints(posPoints, cuspPoints, configSelectedAspects, chartPointConfigSpecs, config.BaseOrbAspects);
            foreach (DefinedAspect defAspect in definedAspects)
            {
                int index1 = _researchMethodUtils.FindIndexForPoint(defAspect.Point1, allPoints);
                int index2 = _researchMethodUtils.FindIndexForPoint(defAspect.Point2, allPoints);
                int index3 = _researchMethodUtils.FindIndexForAspectType(defAspect.Aspect.Aspect, configSelectedAspects);
                allCounts[index1, index2, index3] += 1;
            }
        }
        return CreateResponse(request, selectedCelPointSize, allCounts, allPoints, configSelectedAspects);
    }



    private static CountOfAspectsResponse CreateResponse(GeneralResearchRequest request, int selectedCelPointSize,
        int[,,] allCounts, IReadOnlyCollection<PositionedPoint> posPoints, Dictionary<AspectTypes, AspectConfigSpecs> aspects)
    {
        List<AspectTypes> aspectTypes = aspects.Select(acSpec => acSpec.Key).ToList();
        List<ChartPoints> chartPoints = posPoints.Select(posPoint => posPoint.Point).ToList();
        int[,] totalsPerPointCombi = new int[posPoints.Count, posPoints.Count];
        int[] totalsPerAspect = new int[aspects.Count];

        for (int i = 0; i < selectedCelPointSize; i++)
        {
            for (int j = 0; j < posPoints.Count; j++)
            {
                int total = 0;
                for (int k = 0; k < aspects.Count; k++)         // will still work in case of overlapping orbs.
                {
                    total += allCounts[i, j, k];
                    totalsPerAspect[k] += allCounts[i, j, k];
                }
                totalsPerPointCombi[i, j] += total;
            }
        }
        return new CountOfAspectsResponse(request, allCounts, totalsPerPointCombi, totalsPerAspect, chartPoints, aspectTypes);
    }

}