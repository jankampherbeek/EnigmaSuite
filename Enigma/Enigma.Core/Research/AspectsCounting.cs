// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Core.Persistency;
using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Core.Research;

/// <summary>Counting for aspects.</summary>
public interface IAspectsCounting
{
    /// <summary>Perform a count of aspects.</summary>
    /// <param name="charts">The calculated charts to check.</param>
    /// <param name="request">The original request.</param>
    /// <returns>The calculated counts.</returns>
    public CountOfAspectsResponse CountAspects(List<CalculatedResearchChart> charts, GeneralResearchRequest request);
}

/// <inheritdoc/>
public sealed class AspectsCounting(
    IAspectsHandler aspectsHandler,
    IPointsMapping pointsMapping,
    IResearchMethodUtils researchMethodUtils,
    IProjectDao projectDao)
    : IAspectsCounting
{
    
    
    
    /// <inheritdoc/>
    public CountOfAspectsResponse CountAspects(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        return PerformCount(charts, request);
    }


    private CountOfAspectsResponse PerformCount(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        var ctrlGroupFactor = projectDao.ReadProject(request.ProjectName)!.MultiFactor;
        var config = request.Config;
        var configSelectedAspects = researchMethodUtils.DefineConfigSelectedAspects(config);
        var chartPointConfigSpecs = config.ChartPoints;
        var celPointSize = chartPointConfigSpecs.Count;
        var selectedCelPointSize = 0;
        var cuspSize = charts[0].Positions.Count(item => item.Key.GetDetails().PointCat == PointCats.Cusp);
        var aspectSize = configSelectedAspects.Count;
        var allCounts = new int[celPointSize, celPointSize + cuspSize, aspectSize];
        List<PositionedPoint> allPoints = new();

        foreach (var calcResearchChart in charts)
        {
            var relevantChartPointPositions = researchMethodUtils.DefineSelectedPointPositions(calcResearchChart, request.PointSelection);

            var posPoints = pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions, CoordinateSystems.Ecliptical, true);
            List<PositionedPoint> cuspPoints = [];
            if (request.PointSelection.IncludeCusps)
            {
                var relevantCusps = (from posPoint in calcResearchChart.Positions where (posPoint.Key.GetDetails().PointCat == PointCats.Cusp) select posPoint).ToDictionary(x => x.Key, x => x.Value);
                cuspPoints = pointsMapping.MapFullPointPos2PositionedPoint(relevantCusps, CoordinateSystems.Ecliptical, true);
            }
            selectedCelPointSize = relevantChartPointPositions.Count;
            allPoints = new List<PositionedPoint>(posPoints.Count + cuspPoints.Count);
            allPoints.AddRange(posPoints);
            allPoints.AddRange(cuspPoints);

            var definedAspects = aspectsHandler.AspectsForPosPoints(posPoints, cuspPoints, configSelectedAspects, chartPointConfigSpecs, config.BaseOrbAspects);
            foreach (var defAspect in definedAspects)
            {
                var index1 = researchMethodUtils.FindIndexForPoint(defAspect.Point1, allPoints);
                var index2 = researchMethodUtils.FindIndexForPoint(defAspect.Point2, allPoints);
                var index3 = researchMethodUtils.FindIndexForAspectType(defAspect.Aspect.Aspect, configSelectedAspects);
                allCounts[index1, index2, index3] += 1;
            }
        }
        return CreateResponse(request, selectedCelPointSize, allCounts, allPoints, configSelectedAspects, ctrlGroupFactor);
    }



    private static CountOfAspectsResponse CreateResponse(GeneralResearchRequest request, int selectedCelPointSize,
        int[,,] allCounts, IReadOnlyCollection<PositionedPoint> posPoints, Dictionary<AspectTypes, 
            AspectConfigSpecs> aspects, int ctrlgroupFactor)
    {
        var aspectTypes = aspects.Select(acSpec => acSpec.Key).ToList();
        var chartPoints = posPoints.Select(posPoint => posPoint.Point).ToList();
        var totalsPerPointCombi = new int[posPoints.Count, posPoints.Count];
        var totalsPerAspect = new int[aspects.Count];

        for (var i = 0; i < selectedCelPointSize; i++)
        {
            for (var j = 0; j < posPoints.Count; j++)
            {
                var total = 0;
                for (var k = 0; k < aspects.Count; k++)         // will still work in case of overlapping orbs.
                {
                    total += allCounts[i, j, k];
                    totalsPerAspect[k] += allCounts[i, j, k];
                }
                totalsPerPointCombi[i, j] += total;
            }
        }
        return new CountOfAspectsResponse(request, ctrlgroupFactor, allCounts, totalsPerPointCombi, totalsPerAspect, chartPoints, aspectTypes);
    }

}