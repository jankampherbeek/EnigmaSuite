﻿// Enigma Astrology Research.
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


public sealed class UnaspectedCounting: IUnaspectedCounting
{
    private readonly IAspectsHandler _aspectsHandler;
    private readonly IPointsMapping _pointsMapping;
    private readonly IResearchMethodUtils _researchMethodUtils;

    public UnaspectedCounting(IAspectsHandler aspectsHandler, IPointsMapping pointsMapping, IResearchMethodUtils researchMethodUtils)
    {
        _aspectsHandler = aspectsHandler;
        _pointsMapping = pointsMapping;
        _researchMethodUtils = researchMethodUtils;

    }
    public CountOfUnaspectedResponse CountUnaspected(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        return PerformCount(charts, request);
    }

    private CountOfUnaspectedResponse PerformCount(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        AstroConfig config = request.Config;

        List<AspectConfigSpecs> configSelectedAspects = _researchMethodUtils.DefineConfigSelectedAspects(config);
        List<ChartPoints> configSelectedChartPoints = _researchMethodUtils.DefineConfigSelectedChartPoints(config);
        int configCelPointSize = configSelectedChartPoints.Count;
        int[,] allCounts = new int[configCelPointSize, charts.Count];


        

        int chartIndex = 0;
        foreach (CalculatedResearchChart calcResearchChart in charts)
        {
            List<FullChartPointPos> relevantChartPointPositions = _researchMethodUtils.DefineSelectedPointPositions(config, calcResearchChart, configSelectedChartPoints, request.PointsSelection);
            List<PositionedPoint> posPoints = _pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions, CoordinateSystems.Ecliptical, true);
            List<PositionedPoint> cuspPoints = new();       // use empty list
            List<DefinedAspect> definedAspects = _aspectsHandler.AspectsForPosPoints(posPoints, cuspPoints, configSelectedAspects, config.BaseOrbAspects);     
            
            foreach (PositionedPoint posPoint in posPoints)
            {
                ChartPoints point = posPoint.Point;
                int aspectCount = 0;
                foreach (DefinedAspect defAspect in definedAspects)
                {
                    if (defAspect.Point1 == point || defAspect.Point2 == point)
                    {
                        aspectCount++;
                    }
                }
                if (aspectCount == 0) 
                {
                    for (int i = 0; i < configCelPointSize; i++)
                    {
                        if (relevantChartPointPositions[i].ChartPoint == point)
                        {
                            allCounts[chartIndex, i]++;
                        }
                    }
                }
            }
            chartIndex++;
        }
        List<SimpleCount> resultingCounts = new();
        for (int i = 0; i < configCelPointSize; i++)
        {
            ChartPoints point = config.ChartPoints[i].Point;
            int unaspectedCount = 0;
            for (int j = 0; j < charts.Count; j++)
            {
                unaspectedCount+= allCounts[i, j];
            }
            resultingCounts.Add(new SimpleCount(point, unaspectedCount));
        }
        return new CountOfUnaspectedResponse(request, resultingCounts);
    }



}