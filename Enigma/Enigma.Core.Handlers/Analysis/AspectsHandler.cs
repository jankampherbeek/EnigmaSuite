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
public sealed class AspectsHandler : IAspectsHandler                // TODO 0.1 Add aspects to cusps.
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
        List<FullChartPointPos> chartPointPositions = request.CalcChart.ChartPointPositions;
        FullHousesPositions fullHousesPositions = request.CalcChart.FullHousePositions;
        List<ChartPointConfigSpecs> chartPointConfigSpecs = request.Config.ChartPoints;
        List<FullChartPointPos> relevantChartPointPositions = _aspectPointSelector.SelectPoints(chartPointPositions, fullHousesPositions, chartPointConfigSpecs);
        List<PositionedPoint> posPoints = _pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions, CoordinateSystems.Ecliptical, true);
        List<DistanceBetween2Points> distances = _distanceCalculator.FindShortestDistances(posPoints);
        List<AspectConfigSpecs> allAspects = request.Config.Aspects;
        List<AspectConfigSpecs> relevantAspects = new();
        foreach (AspectConfigSpecs acSpec in allAspects)
        {
            if (acSpec.IsUsed) relevantAspects.Add(acSpec);
        }
        List<DefinedAspect> definedAspects = new();
        foreach (DistanceBetween2Points distance in distances)
        {
            foreach (AspectConfigSpecs aspect in allAspects)
            {
                double maxOrb = _aspectOrbConstructor.DefineOrb(distance.Point1.Point, distance.Point2.Point, aspect.PercentageOrb/100.0, request.Config.BaseOrbAspects);
                AspectTypes aspectType = aspect.AspectType;
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
