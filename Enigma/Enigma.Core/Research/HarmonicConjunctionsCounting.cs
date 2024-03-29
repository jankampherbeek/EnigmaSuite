﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Core.Research;
public interface IHarmonicConjunctionsCounting
{
    /// <summary>Perform a count for harmonic conunctions.</summary>
    /// <param name="charts">The calculated charts to check.</param>
    /// <param name="request">The original request.</param>
    /// <returns>The calculated counts.</returns>
    public CountHarmonicConjunctionsResponse CountHarmonicConjunctions(List<CalculatedResearchChart> charts, CountHarmonicConjunctionsRequest request);
}



/// <inheritdoc/>
public sealed class HarmonicConjunctionsCounting : IHarmonicConjunctionsCounting
{
    private readonly IHarmonicsHandler _harmonicsHandler;
    private readonly IPointsMapping _pointsMapping;
    private readonly IResearchMethodUtils _researchMethodUtils;

    public HarmonicConjunctionsCounting(IHarmonicsHandler harmonicsHandler, IPointsMapping pointsMapping, IResearchMethodUtils researchMethodUtils)
    {
        _harmonicsHandler = harmonicsHandler;
        _pointsMapping = pointsMapping;
        _researchMethodUtils = researchMethodUtils;
    }


    /// <inheritdoc/>
    public CountHarmonicConjunctionsResponse CountHarmonicConjunctions(List<CalculatedResearchChart> charts, CountHarmonicConjunctionsRequest request)
    {
        return PerformCount(charts, request);
    }

    private CountHarmonicConjunctionsResponse PerformCount(List<CalculatedResearchChart> charts, CountHarmonicConjunctionsRequest request)
    {
        List<ChartPoints> selectedPoints = request.PointSelection.SelectedPoints;
        Dictionary<TwoPointStructure, int> allCounts = InitializeAllCounts(selectedPoints);
        double orb = request.Orb;
        double harmonicNr = request.HarmonicNumber;

        foreach (CalculatedResearchChart calcResearchChart in charts)
        {
            Dictionary<ChartPoints, FullPointPos> relevantChartPointPositions = _researchMethodUtils.DefineSelectedPointPositions(calcResearchChart, request.PointSelection);
            List<PositionedPoint> posPoints = _pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions, CoordinateSystems.Ecliptical, true);

            Dictionary<ChartPoints, double> harmonicPositions = _harmonicsHandler.RetrieveHarmonicPositions(posPoints, harmonicNr);
            foreach (var posPoint in posPoints)
            {
                foreach (var harmonicPos in harmonicPositions)
                {
                    double first = Math.Min(posPoint.Position, harmonicPos.Value);
                    double second = Math.Max(posPoint.Position, harmonicPos.Value);
                    double difference = second - first;

                    if (difference > 180.0) difference = Math.Abs(difference - 360.0);
                    if (!(difference < orb)) continue;
                    TwoPointStructure structure = new(harmonicPos.Key, posPoint.Point);
                    allCounts[structure]++;
                }
            }
        }
        return new CountHarmonicConjunctionsResponse(request, allCounts);
    }

    private static Dictionary<TwoPointStructure, int> InitializeAllCounts(List<ChartPoints> selectedPoints)
    {
        const int countValue = 0;
        Dictionary<TwoPointStructure, int> allCounts = new();
        foreach (ChartPoints firstPoint in selectedPoints)
        {
            foreach (ChartPoints secondPoint in selectedPoints)
            {
                allCounts.Add(new TwoPointStructure(firstPoint, secondPoint), countValue);
            }
        }
        return allCounts;
    }


}