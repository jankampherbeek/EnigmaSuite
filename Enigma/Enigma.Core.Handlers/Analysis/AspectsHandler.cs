// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Domain.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Charts;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Analysis;

/// <inheritdoc/>
public sealed class AspectsHandler : IAspectsHandler       
{
    private readonly IPointsMapping _pointsMapping;
    private readonly IDistanceCalculator _distanceCalculator;
    private readonly IAspectPointSelector _aspectPointSelector;
    private readonly IAspectOrbConstructor _aspectOrbConstructor;

    public AspectsHandler(IPointsMapping pointsMapping, IDistanceCalculator distanceCalculator, IAspectPointSelector aspectPointSelector, IAspectOrbConstructor aspectOrbConstructor)
    {
        _pointsMapping = pointsMapping;
        _distanceCalculator = distanceCalculator;
        _aspectPointSelector = aspectPointSelector;
        _aspectOrbConstructor = aspectOrbConstructor;
    }

    /// <inheritdoc/>
    public List<DefinedAspect> AspectsForChartPoints(AspectRequest request)
    {
        Dictionary<ChartPoints, FullPointPos> chartPointPositions =  
            (from posPoint in request.CalcChart.Positions 
            where posPoint.Key.GetDetails().PointCat == PointCats.Common || posPoint.Key.GetDetails().PointCat == PointCats.Angle
            select posPoint)
            .ToDictionary(x => x.Key, x => x.Value);

        Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointConfigSpecs = request.Config.ChartPoints;

        Dictionary<ChartPoints, FullPointPos> relevantChartPointPositions = _aspectPointSelector.SelectPoints(chartPointPositions, chartPointConfigSpecs);
        List<PositionedPoint> posPoints = _pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions, CoordinateSystems.Ecliptical, true);
        List<PositionedPoint> cuspPoints = new();
        if (request.Config.UseCuspsForAspects)
        {
            Dictionary<ChartPoints, FullPointPos> relevantCusps =
                (from posPoint in request.CalcChart.Positions
                where posPoint.Key.GetDetails().PointCat == PointCats.Cusp
                select posPoint)
                .ToDictionary(x => x.Key, x => x.Value);
            cuspPoints = _pointsMapping.MapFullPointPos2PositionedPoint(relevantCusps, CoordinateSystems.Ecliptical, true);
        }
        Dictionary<AspectTypes, AspectConfigSpecs> allAspects = request.Config.Aspects;
        Dictionary<AspectTypes, AspectConfigSpecs> relevantAspects = new();
        foreach (KeyValuePair<AspectTypes, AspectConfigSpecs> acSpec in allAspects)
        {
            if (acSpec.Value.IsUsed) relevantAspects.Add(acSpec.Key, acSpec.Value);
        }
        return AspectsForPosPoints(posPoints, cuspPoints, relevantAspects, request.Config.ChartPoints, request.Config.BaseOrbAspects);
    }

    /// <inheritdoc/>
    public List<DefinedAspect> AspectsForPosPoints(List<PositionedPoint> posPoints, List<PositionedPoint> cuspPoints, Dictionary<AspectTypes, AspectConfigSpecs> relevantAspects, Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointConfigSpecs, double baseOrb)
    {

        List<DistanceBetween2Points> pointDistances = _distanceCalculator.FindShortestDistances(posPoints);
        List<DistanceBetween2Points> cuspDistances = new();
        if (cuspPoints.Count > 0)
        {
            cuspDistances = _distanceCalculator.FindShortestDistanceBetweenPointsAndCusps(posPoints, cuspPoints);  
        }
        List<DistanceBetween2Points> allDistances = new(pointDistances.Count + cuspDistances.Count);
        allDistances.AddRange(pointDistances);
        allDistances.AddRange(cuspDistances);
        List<DefinedAspect> definedAspects = new();
        foreach (DistanceBetween2Points distance in allDistances)
        {
            foreach (KeyValuePair<AspectTypes, AspectConfigSpecs> aspectConfigSpec in relevantAspects)
            {
                double maxOrb = _aspectOrbConstructor.DefineOrb(distance.Point1.Point, distance.Point2.Point, aspectConfigSpec.Value.PercentageOrb / 100.0, baseOrb, chartPointConfigSpecs);
                AspectTypes aspectType = aspectConfigSpec.Key;
                double aspectDistance = aspectType.GetDetails().Angle;
                double actualOrb = Math.Abs(distance.Distance - aspectDistance);
                if (actualOrb <= maxOrb)
                {
                    definedAspects.Add(new DefinedAspect(distance.Point1.Point, distance.Point2.Point, aspectType.GetDetails(), maxOrb, actualOrb));
                }
            }
        }
        return definedAspects;
    }


}
