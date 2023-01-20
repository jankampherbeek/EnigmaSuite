// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Domain.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;
using Enigma.Domain.Research;
using Enigma.Research.Domain;


namespace Enigma.Core.Handlers.Research.Helpers;

/// <inheritdoc/>
public sealed class AspectsCounting: IAspectsCounting
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
        List<AspectConfigSpecs> configSelectedAspects = _researchMethodUtils.DefineConfigSelectedAspects(config);

        List<ChartPointConfigSpecs> chartPointConfigSpecs = config.ChartPoints;
        int celPointSize = chartPointConfigSpecs.Count;
        int selectedCelPointSize = 0;
        int cuspSize = config.UseCuspsForAspects ? charts[0].FullHousePositions.Cusps.Count : 0;
        int aspectSize = configSelectedAspects.Count;
        int[,,] allCounts = new int[celPointSize, celPointSize + cuspSize, aspectSize];
        List<PositionedPoint> allPoints = new();

        foreach (CalculatedResearchChart calcResearchChart in charts)
        {
            List<FullChartPointPos> chartPointPositions = calcResearchChart.CelPointPositions;
            FullHousesPositions fullHousesPositions = calcResearchChart.FullHousePositions;
            List<FullChartPointPos> configChartPointPositions = _aspectPointSelector.SelectPoints(chartPointPositions, fullHousesPositions, chartPointConfigSpecs);
            List<ChartPoints> configSelectedChartPoints = _researchMethodUtils.DefineConfigSelectedChartPoints(config);

            List<FullChartPointPos> relevantChartPointPositions = _researchMethodUtils.DefineSelectedPointPositions(config, calcResearchChart, configSelectedChartPoints, request.PointsSelection);

            List<PositionedPoint> posPoints = _pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions, CoordinateSystems.Ecliptical, true);
            List<PositionedPoint> cuspPoints = new();
            if (request.PointsSelection.IncludeCusps)
            {
                List<FullChartPointPos> relevantCusps = fullHousesPositions.Cusps;
                cuspPoints = _pointsMapping.MapFullPointPos2PositionedPoint(relevantCusps, CoordinateSystems.Ecliptical, true);
            }
            selectedCelPointSize = relevantChartPointPositions.Count;
            allPoints = new List<PositionedPoint> (posPoints.Count + cuspPoints.Count);
            allPoints.AddRange(posPoints);
            allPoints.AddRange(cuspPoints);

            List<DefinedAspect> definedAspects = _aspectsHandler.AspectsForPosPoints(posPoints, cuspPoints, configSelectedAspects, config.BaseOrbAspects);
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



    private static CountOfAspectsResponse CreateResponse(GeneralResearchRequest request, int selectedCelPointSize, int[,,] allCounts, List<PositionedPoint> posPoints, List<AspectConfigSpecs> aspects)
    {
        List<AspectTypes> aspectTypes = new();
        foreach (AspectConfigSpecs acSpec in aspects)
        {
            aspectTypes.Add(acSpec.AspectType);
        }
        List<ChartPoints> chartPoints = new();
        foreach (PositionedPoint posPoint in posPoints)
        {
            chartPoints.Add(posPoint.Point);
        }
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