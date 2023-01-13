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
    private readonly IAspectChecker _aspectChecker;
    private readonly IDistanceCalculator _distanceCalculator;

    public AspectsHandler(IPointsMapping pointsMapping,  IAspectChecker aspectChecker, IDistanceCalculator distanceCalculator)
    {
        _pointsMapping = pointsMapping;
        _aspectChecker = aspectChecker;
        _distanceCalculator = distanceCalculator;
    }

    /// <inheritdoc/>
    public List<DefinedAspect> AspectsForMundanePoints(List<AspectDetails> aspectDetails, CalculatedChart calculatedChart)
    {
        return _aspectChecker.FindAspectsForMundanePoints(aspectDetails, calculatedChart);
    }

    /// <inheritdoc/>
    public List<DefinedAspect> AspectsForMundanePoints(AspectRequest request)
    {
        return _aspectChecker.FindAspectsForMundanePoints(request.CalcChart, request.Config);
    }

    /// <inheritdoc/>
    public List<DefinedAspect> AspectsForCelPoints(List<AspectDetails> aspectDetails, List<FullChartPointPos> fullCelPointPositions)
    {
        return _aspectChecker.FindAspectsCelPoints(aspectDetails, fullCelPointPositions);
    }


    /// <inheritdoc/>
    public List<DefinedAspect> AspectsForCelPoints(AspectRequest request)
    {
        // define celpoints to use
        // rekening houden met mundane punten, cuspen. classic, mc, asc altijd geselecteerd.
        // 
        List<FullChartPointPos> chartPointPositions = request.CalcChart.ChartPointPositions;
        FullHousesPositions fullHousesPositions = request.CalcChart.FullHousePositions;

        List<FullChartPointPos> relevantCelPointPositions = new();
        foreach (FullChartPointPos fcpPos in chartPointPositions)
        {
            PointCats actualPointCat = fcpPos.ChartPoint.GetDetails().PointCat;
            if (actualPointCat == PointCats.Classic || actualPointCat == PointCats.Modern || actualPointCat == PointCats.MathPoint || actualPointCat == PointCats.Minor)
            {
                relevantCelPointPositions.Add(fcpPos);
            }
            CuspFullPos mc = fullHousesPositions.Mc;

        //    relevantCelPointPositions.Add()
        }



        // convert to PositionedPoints
        List<PositionedPoint> posPoints = _pointsMapping.MapFullPointPos2PositionedPoint(relevantCelPointPositions, CoordinateSystems.Ecliptical, true);
        // calculate distances
        List<DistanceBetween2Points> distances = _distanceCalculator.FindShortestDistances(posPoints);
        // define supported aspects
        List<AspectConfigSpecs> allAspects = request.Config.Aspects;
        List<AspectConfigSpecs> relevantAspects = new();
        foreach (AspectConfigSpecs acSpec in allAspects)
        {
            if (acSpec.IsUsed) relevantAspects.Add(acSpec);
        }


        // voor alle distances
        //     voor alle aspecten
        //         check for aspect



        return _aspectChecker.FindAspectsCelPoints(request.CalcChart, request.Config);
    }
}
