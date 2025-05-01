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
public interface IHarmonicConjunctionsCounting
{
    /// <summary>Perform a count for harmonic conunctions.</summary>
    /// <param name="charts">The calculated charts to check.</param>
    /// <param name="request">The original request.</param>
    /// <returns>The calculated counts.</returns>
    public CountHarmonicConjunctionsResponse CountHarmonicConjunctions(List<CalculatedResearchChart> charts, CountHarmonicConjunctionsRequest request);
}

/// <inheritdoc/>
public sealed class HarmonicConjunctionsCounting(
    IHarmonicsHandler harmonicsHandler,
    IPointsMapping pointsMapping,
    IResearchMethodUtils researchMethodUtils,
    IProjectDao projectDao)
    : IHarmonicConjunctionsCounting
{
    /// <inheritdoc/>
    public CountHarmonicConjunctionsResponse CountHarmonicConjunctions(List<CalculatedResearchChart> charts, CountHarmonicConjunctionsRequest request)
    {
        return PerformCount(charts, request);
    }

    private CountHarmonicConjunctionsResponse PerformCount(List<CalculatedResearchChart> charts, CountHarmonicConjunctionsRequest request)
    {
        var ctrlGroupFactor = projectDao.ReadProject(request.ProjectName)!.MultiFactor;
        var selectedPoints = request.PointSelection.SelectedPoints;
        var allCounts = InitializeAllCounts(selectedPoints);
        var orb = request.Orb;
        var harmonicNr = request.HarmonicNumber;

        foreach (var calcResearchChart in charts)
        {
            var relevantChartPointPositions = researchMethodUtils.DefineSelectedPointPositions(calcResearchChart, request.PointSelection);
            var posPoints = pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions, CoordinateSystems.Ecliptical, true);

            var harmonicPositions = harmonicsHandler.RetrieveHarmonicPositions(posPoints, harmonicNr);
            foreach (var posPoint in posPoints)
            {
                foreach (var harmonicPos in harmonicPositions)
                {
                    var first = Math.Min(posPoint.Position, harmonicPos.Value);
                    var second = Math.Max(posPoint.Position, harmonicPos.Value);
                    var difference = second - first;

                    if (difference > 180.0) difference = Math.Abs(difference - 360.0);
                    if (!(difference < orb)) continue;
                    TwoPointStructure structure = new(harmonicPos.Key, posPoint.Point);
                    allCounts[structure]++;
                }
            }
        }
        return new CountHarmonicConjunctionsResponse(request, ctrlGroupFactor, allCounts);
    }

    private static Dictionary<TwoPointStructure, int> InitializeAllCounts(List<ChartPoints> selectedPoints)
    {
        const int countValue = 0;
        Dictionary<TwoPointStructure, int> allCounts = new();
        foreach (var firstPoint in selectedPoints)
        {
            foreach (var secondPoint in selectedPoints)
            {
                allCounts.Add(new TwoPointStructure(firstPoint, secondPoint), countValue);
            }
        }
        return allCounts;
    }

}