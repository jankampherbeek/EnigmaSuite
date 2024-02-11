// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.References;
using Enigma.Domain.Requests;

namespace Enigma.Core.Handlers;


/// <summary>Handler for aspects.</summary>
public interface IAspectsHandler
{

    /// <summary>Find aspects between chart points.</summary>
    /// <param name="request">Request with positions.</param>
    /// <returns>Aspects found.</returns>
    public IEnumerable<DefinedAspect> AspectsForChartPoints(AspectRequest request);

    /// <summary>Find aspects between chart points.</summary>
    /// <param name="posPoints">Celestial points with positions.</param>
    /// <param name="cuspPoints">Cusps with positions.</param>
    /// <param name="relevantAspects">Supported aspects as defined in configuration.</param>
    /// <param name="chartPointConfigSpecs">Configuration for chartpoints.</param>
    /// <param name="baseOrb">Base orb for aspects.</param>
    /// <returns>List with aspects between celestial points and between celestial points and cusps. Aspects between cusps are omitted.</returns>
    public List<DefinedAspect> AspectsForPosPoints(List<PositionedPoint> posPoints, List<PositionedPoint> cuspPoints, 
        Dictionary<AspectTypes, AspectConfigSpecs> relevantAspects, 
        Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointConfigSpecs, double baseOrb);
}



/// <inheritdoc/>
public sealed class AspectsHandler : IAspectsHandler
{
    private readonly IPointsMapping _pointsMapping;
    private readonly ICalculatedDistance _calculatedDistance;
    private readonly IAspectPointSelector _aspectPointSelector;
    private readonly IAspectOrbConstructor _aspectOrbConstructor;

    public AspectsHandler(IPointsMapping pointsMapping, ICalculatedDistance calculatedDistance, IAspectPointSelector aspectPointSelector, IAspectOrbConstructor aspectOrbConstructor)
    {
        _pointsMapping = pointsMapping;
        _calculatedDistance = calculatedDistance;
        _aspectPointSelector = aspectPointSelector;
        _aspectOrbConstructor = aspectOrbConstructor;
    }

    /// <inheritdoc/>
    public IEnumerable<DefinedAspect> AspectsForChartPoints(AspectRequest request)
    {
        Dictionary<ChartPoints, FullPointPos> chartPointPositions =
            (from posPoint in request.CalcChart.Positions
             where posPoint.Key.GetDetails().PointCat == PointCats.Common || posPoint.Key.GetDetails().PointCat == PointCats.Angle
             select posPoint)
            .ToDictionary(x => x.Key, x => x.Value);

        Dictionary<ChartPoints, ChartPointConfigSpecs?> chartPointConfigSpecs = request.Config.ChartPoints;

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
        Dictionary<AspectTypes, AspectConfigSpecs?> allAspects = request.Config.Aspects;
        Dictionary<AspectTypes, AspectConfigSpecs> relevantAspects = allAspects.Where(acSpec 
            => acSpec.Value.IsUsed).ToDictionary(acSpec => acSpec.Key, 
            acSpec => acSpec.Value);
        return AspectsForPosPoints(posPoints, cuspPoints, relevantAspects, request.Config.ChartPoints, request.Config.BaseOrbAspects);
    }

    /// <inheritdoc/>
    public List<DefinedAspect> AspectsForPosPoints(List<PositionedPoint> posPoints, List<PositionedPoint> cuspPoints, Dictionary<AspectTypes, AspectConfigSpecs> relevantAspects, Dictionary<ChartPoints, ChartPointConfigSpecs?> chartPointConfigSpecs, double baseOrb)
    {

        List<DistanceBetween2Points> pointDistances = _calculatedDistance.ShortestDistances(posPoints);
        List<DistanceBetween2Points> cuspDistances = new();
        if (cuspPoints.Count > 0)
        {
            cuspDistances = _calculatedDistance.ShortestDistanceBetweenPointsAndCusps(posPoints, cuspPoints);
        }
        List<DistanceBetween2Points> allDistances = new(pointDistances.Count + cuspDistances.Count);
        allDistances.AddRange(pointDistances);
        allDistances.AddRange(cuspDistances);
        return (from distance in allDistances 
            from aspectConfigSpec in relevantAspects 
            let maxOrb = _aspectOrbConstructor.DefineOrb(distance.Point1.Point, distance.Point2.Point, 
                aspectConfigSpec.Value.PercentageOrb / 100.0, baseOrb, chartPointConfigSpecs) 
            let aspectType = aspectConfigSpec.Key 
            let aspectDistance = aspectType.GetDetails().Angle 
            let actualOrb = Math.Abs(distance.Distance - aspectDistance) 
            where actualOrb <= maxOrb 
            select new DefinedAspect(distance.Point1.Point, distance.Point2.Point, aspectType.GetDetails(), 
                maxOrb, actualOrb)).ToList();
    }


}
