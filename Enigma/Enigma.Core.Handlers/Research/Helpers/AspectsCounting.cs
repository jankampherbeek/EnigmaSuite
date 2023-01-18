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
using Enigma.Domain.Exceptions;
using Enigma.Domain.Points;
using Enigma.Domain.Research;
using Enigma.Research.Domain;
using Serilog;


namespace Enigma.Core.Handlers.Research.Helpers;

/// <inheritdoc/>
public sealed class AspectsCounting: IAspectsCounting
{
    private readonly IAspectsHandler _aspectsHandler;
    private readonly IAspectPointSelector _aspectPointSelector;
    private readonly IPointsMapping _pointsMapping;

    public AspectsCounting(IAspectsHandler aspectsHandler, IAspectPointSelector aspectPointSelector, IPointsMapping pointsMapping)
    {
        _aspectsHandler = aspectsHandler;
        _aspectPointSelector = aspectPointSelector;
        _pointsMapping = pointsMapping;
    }




    /// <inheritdoc/>
    public CountOfAspectsResponse CountAspects(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        return PerformCount(charts, request);
    }


    private CountOfAspectsResponse PerformCount(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        AstroConfig config = request.Config;
        List<AspectConfigSpecs> allAspects = config.Aspects;
        List<AspectConfigSpecs> relevantAspects = new();
        foreach (AspectConfigSpecs acSpec in allAspects)
        {
            if (acSpec.IsUsed) relevantAspects.Add(acSpec);
        }


        List<ChartPointConfigSpecs> chartPointConfigSpecs = config.ChartPoints;
        int celPointSize = chartPointConfigSpecs.Count;
        int selectedCelPointSize = 0;
        int cuspSize = config.UseCuspsForAspects ? charts[0].FullHousePositions.Cusps.Count : 0;
        int aspectSize = relevantAspects.Count;
        int[,,] allCounts = new int[celPointSize, celPointSize + cuspSize, aspectSize];
        List<PositionedPoint> allPoints = new();

        foreach (CalculatedResearchChart calcResearchChart in charts)
        {
            List<FullChartPointPos> chartPointPositions = calcResearchChart.CelPointPositions;
            FullHousesPositions fullHousesPositions = calcResearchChart.FullHousePositions;
            List<FullChartPointPos> relevantChartPointPositions = _aspectPointSelector.SelectPoints(chartPointPositions, fullHousesPositions, chartPointConfigSpecs);


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

            List<DefinedAspect> definedAspects = _aspectsHandler.AspectsForPosPoints(posPoints, cuspPoints, relevantAspects, config.BaseOrbAspects);
            foreach (DefinedAspect defAspect in definedAspects)
            {
                int index1 = FindIndexForPoint(defAspect.Point1, allPoints);
                int index2 = FindIndexForPoint(defAspect.Point2, allPoints);
                int index3 = FindIndexForAspectType(defAspect.Aspect.Aspect, relevantAspects);
                allCounts[index1, index2, index3] += 1;    
            }
        }
        return CreateResponse(request, selectedCelPointSize, allCounts, allPoints, relevantAspects);
    }

    private int FindIndexForPoint(ChartPoints point, List<PositionedPoint> allPoints)
    {
        for (int i = 0; i < allPoints.Count; i++)
        {
            if (allPoints[i].Point == point) return i;
        }
        string errorText = "AspectsCounting.FindIndexForPoint(). Could not find index for ChartPoint : " + point;
        Log.Error(errorText);
        throw new EnigmaException(errorText);
    }

    private int FindIndexForAspectType(AspectTypes aspectType, List<AspectConfigSpecs> allAspects)
    {
        for (int i = 0; i < allAspects.Count; i++)
        {
            if (allAspects[i].AspectType == aspectType) return i;
        }
        string errorText = "AspectsCounting.FindIndexForAspectType(). Could not find index for AspectType : " + aspectType;
        Log.Error(errorText);
        throw new EnigmaException(errorText);
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